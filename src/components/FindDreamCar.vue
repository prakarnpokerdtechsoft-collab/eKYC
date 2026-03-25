<script setup>
import { ref } from 'vue'
import { langState } from '../store/lang.js'

const emit = defineEmits(['back'])

const selectedCar = ref('BMW M3 Touring LCI')
const selectedDealer = ref(null)

const dealers = [
  { id: 1, name: 'ธนโชค อัครมหาศาล', code: '#3-1014-80392-23-9', phone: '099-555-4444' },
  { id: 2, name: 'พีรวัส ทรัพย์ไพศาล', code: '#3-1012-38475-11-8', phone: '098-846-3333' },
  { id: 3, name: 'ธนไพศาล ดีลเลอร์', code: '#3-1019-23764-76-9', phone: '099-284-2255' },
  { id: 4, name: 'ชัยชนะ กิจเจริญรุ่ง', code: '#3-1019-23764-76-9', phone: '099-284-2255' }
]

const handleClear = () => {
  selectedDealer.value = null
  selectedCar.value = 'BMW M3 Touring LCI'
}

const handleSubmit = () => {
  if (!selectedDealer.value) {
    alert('Please select a dealer')
    return
  }
  alert(`Submitted for dealer ID: ${selectedDealer.value}`)
}
</script>

<template>
  <div class="find-car-page">
    <header class="orange-header">
      <button class="back-button" @click="$emit('back')">
        <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
          <path d="M15 18L9 12L15 6" stroke="white" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
        </svg>
      </button>
      <h1 class="header-title">{{ langState.t('ค้นหารถในฝัน', 'Find Dream Car') }}</h1>
    </header>

    <div class="content">
      <div class="form-group">
        <label class="label">{{ langState.t('เลือกรถยนต์', 'Select Car') }}</label>
        <div class="select-wrapper">
          <select v-model="selectedCar" class="custom-select">
            <option value="BMW M3 Touring LCI">BMW M3 Touring LCI</option>
            <option value="Honda Civic RS">Honda Civic RS</option>
          </select>
          <div class="select-arrows">
            <svg width="10" height="14" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
              <path d="M7 10l5-5 5 5h-10zm0 4l5 5 5-5h-10z" fill="#666"/>
            </svg>
          </div>
        </div>
      </div>

      <div class="car-preview">
        <!-- Placeholder image or realistic URL. Let's use an unsplash or generic car URL if possible, or just a gray box with "Car Image" -->
        <div class="car-image-box">
          <img src="https://images.unsplash.com/photo-1555215695-3004980ad54e?ixlib=rb-4.0.3&auto=format&fit=crop&w=500&q=60" alt="Car" class="car-image" />
        </div>
        <h2 class="car-name">2015 BMW 528i 2.0 f10 (ปี10-16) Luxury Sedan</h2>
        <p class="car-tags">#BMW #528i luxury (Facelift)</p>
      </div>

      <div class="dealer-section">
        <label class="label">{{ langState.t('รายชื่อดีลเลอร์', 'Dealer list') }}</label>
        <div class="dealer-list-card">
          <div 
            v-for="dealer in dealers" 
            :key="dealer.id"
            class="dealer-item"
            @click="selectedDealer = dealer.id"
          >
            <div class="radio-outer" :class="{ 'selected': selectedDealer === dealer.id }">
              <div class="radio-inner" v-if="selectedDealer === dealer.id"></div>
            </div>
            <div class="dealer-info">
              <div class="dealer-name">{{ dealer.name }}</div>
              <div class="dealer-code">{{ dealer.code }} / {{ dealer.phone }}</div>
            </div>
          </div>
        </div>
      </div>
    </div>

    <div class="action-buttons">
      <button class="btn-submit" @click="handleSubmit">{{ langState.t('ยืนยัน', 'Submit') }}</button>
      <button class="btn-clear" @click="handleClear">{{ langState.t('ล้างข้อมูล', 'Clear') }}</button>
    </div>
  </div>
</template>

<style scoped>
.find-car-page {
  display: flex;
  flex-direction: column;
  min-height: 100vh;
  background-color: #ffffff;
  max-width: 480px;
  margin: 0 auto;
}

.orange-header {
  background-color: #f97316;
  color: white;
  display: flex;
  align-items: center;
  padding: 16px;
  position: sticky;
  top: 0;
  z-index: 10;
}

.back-button {
  background: none;
  border: none;
  cursor: pointer;
  padding: 0;
  display: flex;
  align-items: center;
  margin-right: 16px;
}

.header-title {
  font-size: 18px;
  font-weight: 700;
  margin: 0;
}

.content {
  padding: 24px;
  flex: 1;
}

.form-group {
  margin-bottom: 24px;
}

.label {
  display: block;
  font-size: 12px;
  color: #888;
  margin-bottom: 8px;
  font-weight: 600;
}

.select-wrapper {
  position: relative;
}

.custom-select {
  width: 100%;
  padding: 12px 36px 12px 16px;
  border: 1px solid #ddd;
  border-radius: 20px;
  appearance: none;
  background-color: white;
  font-size: 14px;
  font-weight: 600;
  color: #333;
}

.select-arrows {
  position: absolute;
  right: 16px;
  top: 50%;
  transform: translateY(-50%);
  pointer-events: none;
}

.car-preview {
  margin-bottom: 32px;
}

.car-image-box {
  width: 100%;
  height: 180px;
  border-radius: 8px;
  overflow: hidden;
  margin-bottom: 12px;
  background-color: #e0e0e0;
}

.car-image {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.car-name {
  font-size: 14px;
  font-weight: 700;
  color: #222;
  margin: 0 0 4px 0;
  line-height: 1.4;
}

.car-tags {
  font-size: 11px;
  font-weight: 600;
  color: #666;
  margin: 0;
}

.dealer-section {
  margin-bottom: 32px;
}

.dealer-list-card {
  background-color: white;
  border-radius: 16px;
  padding: 8px 16px;
  box-shadow: 0 4px 12px rgba(0,0,0,0.03);
  border: 1px solid #eee;
}

.dealer-item {
  display: flex;
  align-items: center;
  padding: 16px 0;
  border-bottom: 1px solid #f0f0f0;
  cursor: pointer;
}

.dealer-item:last-child {
  border-bottom: none;
}

.radio-outer {
  width: 24px;
  height: 24px;
  border-radius: 50%;
  border: 2px solid #ccc;
  display: flex;
  align-items: center;
  justify-content: center;
  margin-right: 16px;
  flex-shrink: 0;
  transition: border-color 0.2s;
}

.radio-outer.selected {
  border-color: #22c55e;
  border-width: 2px;
}

.radio-inner {
  width: 14px;
  height: 14px;
  border-radius: 50%;
  background-color: #22c55e;
}

.dealer-info {
  display: flex;
  flex-direction: column;
}

.dealer-name {
  font-size: 14px;
  font-weight: 600;
  color: #333;
  margin-bottom: 4px;
}

.dealer-code {
  font-size: 11px;
  color: #888;
}

.action-buttons {
  display: flex;
  gap: 16px;
  padding: 16px 24px 32px;
  background-color: #ffffff;
}

.btn-submit, .btn-clear {
  flex: 1;
  padding: 14px;
  border: none;
  border-radius: 24px;
  font-size: 16px;
  font-weight: 600;
  cursor: pointer;
  color: white;
  transition: opacity 0.2s;
}

.btn-submit {
  background-color: #f97316;
}

.btn-clear {
  background-color: #6b7280;
}

.btn-submit:hover, .btn-clear:hover {
  opacity: 0.9;
}
</style>
