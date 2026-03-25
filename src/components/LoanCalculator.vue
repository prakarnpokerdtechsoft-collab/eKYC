<script setup>
import { ref } from 'vue'
import { langState } from '../store/lang.js'

const emit = defineEmits(['back'])

const activeTab = ref('Standard')
const carPrice = ref('')
const downPayment = ref('')
const interestRate = ref('')
const loanTerm = ref('')
const serviceFee = ref('')
const otherServiceFee = ref('')

const results = ref(null)

const calculate = () => {
  const price = parseFloat(carPrice.value) || 0
  const down = parseFloat(downPayment.value) || 0
  const rate = parseFloat(interestRate.value) || 0
  const term = parseInt(loanTerm.value) || 0
  const sFee = parseFloat(serviceFee.value) || 0
  const oFee = parseFloat(otherServiceFee.value) || 0

  if (price <= 0 || term <= 0) {
    results.value = null
    return
  }

  // Formula:
  // Loan Amount = Price - (Price * Down / 100)
  // Total Interest = Loan Amount * (Rate / 100) * (Term / 12)
  // Total Payment = Loan Amount + Total Interest + Fees
  
  const downAmount = (price * down) / 100
  const loanAmount = price - downAmount
  const totalInterest = loanAmount * (rate / 100) * (term / 12)
  const totalPayment = loanAmount + totalInterest + sFee + oFee
  const monthlyInstallment = Math.round(totalPayment / term)

  if (activeTab.value === 'Standard') {
    results.value = {
      loanAmount: loanAmount,
      totalPayment: totalPayment,
      monthlyInstallment: monthlyInstallment
    }
  } else {
    results.value = {
      carAmount: price,
      downPayment: down,
      loanTerm: term,
      interestRate: rate,
      serviceFee: sFee,
      otherServiceFee: oFee,
      totalPayment: totalPayment
    }
  }
}

// Watch for any changes to trigger "Analysis" (Calculations) in AI mode
import { watch } from 'vue'
watch([carPrice, downPayment, interestRate, loanTerm, serviceFee, otherServiceFee, activeTab], () => {
  if (activeTab.value === 'AI') {
    calculate()
  } else if (activeTab.value === 'Standard' && !carPrice.value) {
    results.value = null
  }
})

const clear = () => {
  carPrice.value = ''
  downPayment.value = ''
  interestRate.value = ''
  loanTerm.value = ''
  serviceFee.value = 0
  otherServiceFee.value = 0
  results.value = null
}

const formatNumber = (num) => {
  return new Intl.NumberFormat('th-TH', { minimumFractionDigits: 2, maximumFractionDigits: 2 }).format(num)
}
</script>

<template>
  <div class="loan-calculator">
    <header class="orange-header">
      <button class="back-btn" @click="$emit('back')">
        <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
          <path d="M15 18L9 12L15 6" stroke="white" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" />
        </svg>
      </button>
      <h1 class="header-title">{{ langState.t('เครื่องคำนวณสินเชื่อ', 'Loan Calculator') }}</h1>
    </header>

    <div class="content">
      <div class="tabs">
        <button :class="['tab-btn', activeTab === 'Standard' ? 'active-standard' : 'inactive']" @click="activeTab = 'Standard'">
          {{ langState.t('Standard', 'Standard') }}
        </button>
        <button :class="['tab-btn', activeTab === 'AI' ? 'active-ai' : 'inactive']" @click="activeTab = 'AI'">
          {{ langState.t('AI Calculator', 'AI Calculator') }}
        </button>
      </div>

      <div class="form-group">
        <label>{{ langState.t('ราคารถ', 'Car Price') }}</label>
        <input type="number" v-model="carPrice" placeholder="" />
      </div>

      <div class="form-group">
        <label>{{ langState.t('เงินดาวน์ (%)', 'Down Payment (%)') }}</label>
        <input type="number" v-model="downPayment" placeholder="" />
      </div>

      <!-- Order in AI tab image: Price, Down, Term, Interest, Service Fee, Other Service Fee -->
      <div class="form-group">
        <label>{{ langState.t('ระยะเวลาผ่อน (เดือน)', 'Loan Term (Months)') }}</label>
        <select v-model="loanTerm">
          <option value="" disabled selected>{{ langState.t('เลือกระยะเวลาผ่อน', 'Select Term') }}</option>
          <option :value="12">12</option>
          <option :value="24">24</option>
          <option :value="36">36</option>
          <option :value="48">48</option>
          <option :value="60">60</option>
          <option :value="72">72</option>
          <option :value="84">84</option>
        </select>
      </div>

      <div class="form-group">
        <label>{{ langState.t('อัตราดอกเบี้ย (%)', 'Interest Rate (%)') }}</label>
        <input type="number" v-model="interestRate" step="0.1" placeholder="" />
      </div>

      <template v-if="activeTab === 'AI'">
        <div class="form-group">
          <label>{{ langState.t('ค่าบริการ', 'Service Fee') }}</label>
          <input type="number" v-model="serviceFee" placeholder="" />
        </div>
        <div class="form-group">
          <label>{{ langState.t('ค่าบริการอื่นๆ', 'Other Service Fee') }}</label>
          <input type="number" v-model="otherServiceFee" placeholder="" />
        </div>
      </template>

      <div class="button-group">
        <button class="clear-btn" @click="clear">{{ langState.t('ล้างข้อมูล', 'Clear') }}</button>
        <button class="calculate-btn" @click="calculate">{{ langState.t('คำนวณ', 'Calculate') }}</button>
      </div>

      <!-- Result Card for Standard -->
      <div v-if="results && activeTab === 'Standard'" class="result-card">
        <div class="result-item">
          <span class="result-label">{{ langState.t('ยอดจัดสินเชื่อ', 'Loan Amount') }}</span>
          <span class="result-value">{{ formatNumber(results.loanAmount) }}</span>
        </div>
        <div class="result-item">
          <span class="result-label">{{ langState.t('ยอดชำระรวม', 'Total Payment') }}</span>
          <span class="result-value">{{ formatNumber(results.totalPayment) }}</span>
        </div>
        <div class="result-item monthly">
          <span class="result-label">{{ langState.t('ค่างวดต่อเดือน (บาท)', 'Monthly Installment (THB)') }}</span>
          <span class="result-value highlighted">{{ formatNumber(results.monthlyInstallment) }}</span>
        </div>
      </div>

      <!-- Result Card for AI (Based on User Image) -->
      <div v-if="results && activeTab === 'AI'" class="result-card ai-results">
        <div class="result-item">
          <span class="result-label">Car Amount</span>
          <span class="result-value">{{ formatNumber(results.carAmount) }} Baht</span>
        </div>
        <div class="result-item">
          <span class="result-label">Loan Term</span>
          <span class="result-value">{{ results.loanTerm }} Month</span>
        </div>
        <div class="result-item">
          <span class="result-label">Interest rate</span>
          <span class="result-value">{{ results.interestRate }} %</span>
        </div>
        <div class="result-item">
          <span class="result-label">Service Fee</span>
          <span class="result-value">{{ results.serviceFee }} Baht</span>
        </div>
        <div class="result-item">
          <span class="result-label">Other Service Fee</span>
          <span class="result-value">{{ results.otherServiceFee }} Baht</span>
        </div>
        <div class="result-item total">
          <span class="result-label">Total Payment</span>
          <span class="result-value highlighted">{{ formatNumber(results.totalPayment) }} Baht</span>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
.loan-calculator {
  display: flex;
  flex-direction: column;
  height: 100%;
  background-color: #ffffff;
}

.orange-header {
  background-color: #f57c1f;
  padding: 16px;
  display: flex;
  align-items: center;
  gap: 12px;
  color: white;
}

.back-btn {
  background: none;
  border: none;
  cursor: pointer;
  padding: 4px;
  display: flex;
  align-items: center;
}

.header-title {
  font-size: 20px;
  font-weight: 700;
  margin: 0;
}

.content {
  padding: 20px;
  flex: 1;
  overflow-y: auto;
}

.tabs {
  display: flex;
  gap: 12px;
  margin-bottom: 24px;
}

.tab-btn {
  flex: 1;
  padding: 12px;
  border-radius: 12px;
  border: none;
  font-weight: 700;
  cursor: pointer;
  font-size: 16px;
}

.active-standard {
  background-color: #6b7280;
  color: white;
}

.active-ai {
  background-color: #f57c1f;
  color: white;
}

.inactive {
  background-color: #e5e7eb;
  color: #6b7280;
}

.form-group {
  margin-bottom: 16px;
}

.form-group label {
  display: block;
  font-size: 14px;
  color: #9ca3af;
  margin-bottom: 6px;
  font-weight: 500;
}

.form-group input,
.form-group select {
  width: 100%;
  padding: 12px;
  border: 1px solid #e5e7eb;
  border-radius: 12px;
  font-size: 16px;
  background-color: white;
  color: #111827;
}

.button-group {
  display: flex;
  gap: 12px;
  margin-top: 24px;
}

.clear-btn,
.calculate-btn {
  flex: 1;
  padding: 14px;
  border-radius: 16px;
  border: none;
  font-size: 20px;
  font-weight: 700;
  cursor: pointer;
}

.clear-btn {
  background-color: #6b7280;
  color: white;
}

.calculate-btn {
  background-color: #f57c1f;
  color: white;
}

.result-card {
  margin-top: 32px;
  background-color: white;
  border-radius: 20px;
  padding: 20px;
  box-shadow: 0 10px 15px -3px rgba(0, 0, 0, 0.1), 0 4px 6px -2px rgba(0, 0, 0, 0.05);
}

.result-item {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 10px 0;
  border-bottom: 1px solid #f3f4f6;
}

.result-item:last-child {
  border-bottom: none;
}

.result-label {
  font-weight: 700;
  color: #111827;
  font-size: 16px;
}

.result-value {
  color: #374151;
  font-size: 16px;
}

.result-item.monthly,
.result-item.total {
  margin-top: 8px;
  padding-top: 16px;
}

.result-value.highlighted {
  color: #111827;
  font-weight: 800;
  font-size: 18px;
}

.ai-results .result-label {
  font-weight: 800;
}
</style>
