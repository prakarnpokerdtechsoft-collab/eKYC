<script setup>
import { ref, watch, onMounted } from 'vue'
import AppLogo from './AppLogo.vue'
import { langState } from '../store/lang.js'

const props = defineProps({
  phoneNumber: {
    type: String,
    required: true
  }
})

const emit = defineEmits(['submit'])

const otpValues = ref(['', '', '', '', '', ''])
const inputRefs = ref([])

const timer = ref(30)
const resendDisabled = ref(true)

const startTimer = () => {
  timer.value = 30
  resendDisabled.value = true
  const interval = setInterval(() => {
    if (timer.value > 0) {
      timer.value--
    } else {
      resendDisabled.value = false
      clearInterval(interval)
    }
  }, 1000)
}

onMounted(() => {
  startTimer()
})

const formatTime = (seconds) => {
  const m = Math.floor(seconds / 60)
  const s = seconds % 60
  return `0${m}.${s < 10 ? '0' + s : s}`
}

const handleInput = (index, event) => {
  const value = event.target.value
  
  if (value && !/^\d+$/.test(value)) {
    otpValues.value[index] = ''
    return
  }

  if (value && index < 5) {
    if (inputRefs.value[index + 1]) {
      inputRefs.value[index + 1].focus()
    }
  }
}

const handleKeydown = (index, event) => {
  if (event.key === 'Backspace' && !otpValues.value[index] && index > 0) {
    if (inputRefs.value[index - 1]) {
      inputRefs.value[index - 1].focus()
    }
  }
}

const handlePaste = (event) => {
  event.preventDefault()
  const pastedData = event.clipboardData.getData('text/plain').trim()
  const numbers = pastedData.replace(/\D/g, '').slice(0, 6)
  
  for (let i = 0; i < numbers.length; i++) {
    otpValues.value[i] = numbers[i]
  }
  
  const focusIndex = Math.min(numbers.length, 5)
  if (inputRefs.value[focusIndex]) {
    setTimeout(() => {
      inputRefs.value[focusIndex].focus()
    }, 10)
  }
}

const handleSubmit = () => {
  const otp = otpValues.value.join('')
  if (otp.length === 6) {
    emit('submit', otp)
  }
}

const handleResend = () => {
  if (!resendDisabled.value) {
    otpValues.value = ['', '', '', '', '', '']
    startTimer()
  }
}
</script>

<template>
  <div class="otp-container">
    <div class="logo-section">
      <AppLogo />
    </div>

    <div class="title-section">
      <h2>{{ langState.t('ยืนยันรหัส OTP', 'OTP Verification') }}</h2>
      <p>{{ langState.t('กรุณากรอกรหัส OTP', 'Please enter OTP') }}</p>
    </div>

    <form @submit.prevent="handleSubmit" class="form-section">
      <div class="otp-inputs" @paste="handlePaste">
        <input
          v-for="(val, index) in otpValues"
          :key="index"
          type="text"
          inputmode="numeric"
          maxlength="1"
          v-model="otpValues[index]"
          class="otp-digit"
          :class="{'has-value': val}"
          @input="handleInput(index, $event)"
          @keydown="handleKeydown(index, $event)"
          :ref="el => { if (el) inputRefs[index] = el }"
        />
      </div>

      <div class="timer">
        {{ formatTime(timer) }}
      </div>

      <div class="resend-text">
        {{ langState.t('ยังไม่ได้รับรหัส OTP ?', 'Do not send OTP ?') }} 
        <button type="button" class="resend-btn" :disabled="resendDisabled" @click="handleResend">
          {{ langState.t('ส่งรหัสอีกครั้ง', 'Send OTP') }}
        </button>
      </div>

      <div class="filler"></div>

      <button type="submit" class="submit-btn" :disabled="otpValues.join('').length !== 6">
        {{ langState.t('ยืนยัน', 'Submit') }}
      </button>
    </form>
  </div>
</template>

<style scoped>
.otp-container {
  display: flex;
  flex-direction: column;
  align-items: center;
  padding-top: 0;
  flex: 1;
}

.logo-section {
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-bottom: 8px;
}



.title-section {
  text-align: center;
  margin-bottom: 24px;
  width: 100%;
}

.title-section h2 {
  font-size: 20px;
  font-weight: 700;
  margin-bottom: 8px;
  color: #111;
}

.title-section p {
  font-size: 14px;
  color: #555;
  margin: 0;
}

.form-section {
  width: 100%;
  display: flex;
  flex-direction: column;
  align-items: center;
  flex: 1;
}

.otp-inputs {
  display: flex;
  justify-content: space-between;
  width: 100%;
  max-width: 320px;
  margin-bottom: 24px;
  gap: 8px;
}

.otp-digit {
  width: 45px;
  height: 45px;
  border-radius: 50%;
  border: 1px solid var(--primary-orange);
  text-align: center;
  font-size: 20px;
  font-weight: 600;
  color: #111;
  outline: none;
  background-color: transparent;
  transition: all 0.2s ease;
  padding: 0;
}

.otp-digit::-webkit-outer-spin-button,
.otp-digit::-webkit-inner-spin-button {
  -webkit-appearance: none;
  margin: 0;
}

.otp-digit:focus {
  box-shadow: 0 0 0 3px rgba(245, 124, 31, 0.2);
}

.timer {
  font-size: 14px;
  font-weight: 500;
  color: #666;
  margin-bottom: 16px;
}

.resend-text {
  font-size: 13px;
  color: #999;
}

.resend-btn {
  background: none;
  border: none;
  color: var(--primary-orange);
  font-size: 13px;
  font-weight: 600;
  cursor: pointer;
  padding: 0;
  transition: opacity 0.2s;
}

.resend-btn:hover:not(:disabled) {
  opacity: 0.8;
}

.resend-btn:disabled {
  color: #ccc;
  cursor: not-allowed;
}

.filler {
  flex: 1;
}

.submit-btn {
  width: 100%;
  background-color: var(--primary-orange);
  color: white;
  border: none;
  border-radius: 8px;
  padding: 16px;
  font-size: 16px;
  font-weight: 600;
  cursor: pointer;
  transition: all 0.2s ease;
  margin-bottom: 16px;
  margin-top: 16px;
}

.submit-btn:hover {
  opacity: 0.9;
  transform: translateY(-1px);
  box-shadow: 0 4px 12px rgba(245, 124, 31, 0.2);
}

.submit-btn:disabled {
  opacity: 0.5;
  cursor: not-allowed;
  transform: none;
  box-shadow: none;
}
</style>
