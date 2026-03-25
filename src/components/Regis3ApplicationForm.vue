<script setup>
import { ref } from 'vue'

const emit = defineEmits(['back', 'confirm'])

// Pre-filled form data structure based on the mockup
const formData = ref({
  idCardNumber: '',
  prefix: '',
  firstNameTh: '',
  lastNameTh: '',
  firstNameEn: '',
  lastNameEn: '',
  dob: '',
  issueDate: '',
  expiryDate: '',
  addressNo: '',
  moo: '',
  room: '',
  floor: '',
  village: '',
  soi: '',
  road: '',
  province: '',
  district: '',
  subDistrict: '',
  postalCode: ''
})

const handleBack = () => {
  emit('back')
}

const handleConfirm = () => {
  // Pass the confirmed form data up to the parent
  emit('confirm', formData.value)
}
</script>

<template>
  <div class="regis3-container">
    <header class="orange-header">
      <button class="back-btn" @click="handleBack">
        <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
          <path d="M15 18L9 12L15 6" stroke="white" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
        </svg>
      </button>
      <h1 class="header-title">Application Form</h1>
      <div class="empty-space"></div>
    </header>

    <div class="form-body">
      <form @submit.prevent="handleConfirm" class="application-form">
        <p class="review-text">Please review your informations</p>

        <!-- ID Card Number -->
        <div class="form-group">
          <label>ID-Card Number</label>
          <input type="text" v-model="formData.idCardNumber" />
        </div>

        <!-- Prefix and First Name TH -->
        <div class="form-row">
          <div class="form-group" style="flex: 0.4;">
            <label>Prefix</label>
            <select v-model="formData.prefix">
              <option>นาย</option>
              <option>นาง</option>
              <option>นางสาว</option>
            </select>
          </div>
          <div class="form-group" style="flex: 0.6;">
            <label>First Name (Thai)</label>
            <input type="text" v-model="formData.firstNameTh" />
          </div>
        </div>

        <!-- Last Name TH -->
        <div class="form-group">
          <label>Last Name (Thai)</label>
          <input type="text" v-model="formData.lastNameTh" />
        </div>

        <!-- First Name EN -->
        <div class="form-group">
          <label>First Name (Eng.)</label>
          <input type="text" v-model="formData.firstNameEn" />
        </div>

        <!-- Last Name EN -->
        <div class="form-group">
          <label>Last Name (Eng.)</label>
          <input type="text" v-model="formData.lastNameEn" />
        </div>

        <!-- Date of Birth -->
        <div class="form-group input-with-icon">
          <label>Date of Birth</label>
          <div class="input-wrapper">
            <input type="date" v-model="formData.dob" class="date-input" />
            <svg class="calendar-icon" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="#9ca3af" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><rect x="3" y="4" width="18" height="18" rx="2" ry="2"></rect><line x1="16" y1="2" x2="16" y2="6"></line><line x1="8" y1="2" x2="8" y2="6"></line><line x1="3" y1="10" x2="21" y2="10"></line></svg>
          </div>
        </div>

        <!-- Date of Issue -->
        <div class="form-group input-with-icon">
          <label>Date of Issue (ID Card)</label>
          <div class="input-wrapper">
            <input type="date" v-model="formData.issueDate" class="date-input" />
            <svg class="calendar-icon" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="#9ca3af" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><rect x="3" y="4" width="18" height="18" rx="2" ry="2"></rect><line x1="16" y1="2" x2="16" y2="6"></line><line x1="8" y1="2" x2="8" y2="6"></line><line x1="3" y1="10" x2="21" y2="10"></line></svg>
          </div>
        </div>

        <!-- Date of Expiry -->
        <div class="form-group input-with-icon">
          <label>Date of Expiry (ID-Card)</label>
          <div class="input-wrapper">
            <input type="date" v-model="formData.expiryDate" class="date-input" />
            <svg class="calendar-icon" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="#9ca3af" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><rect x="3" y="4" width="18" height="18" rx="2" ry="2"></rect><line x1="16" y1="2" x2="16" y2="6"></line><line x1="8" y1="2" x2="8" y2="6"></line><line x1="3" y1="10" x2="21" y2="10"></line></svg>
          </div>
        </div>

        <!-- Address No and Moo -->
        <div class="form-row">
          <div class="form-group">
            <label>Address No</label>
            <input type="text" v-model="formData.addressNo" />
          </div>
          <div class="form-group">
            <label>Moo</label>
            <input type="text" v-model="formData.moo" />
          </div>
        </div>

        <!-- Room and Floor -->
        <div class="form-row">
          <div class="form-group">
            <label>Room</label>
            <input type="text" v-model="formData.room" />
          </div>
          <div class="form-group">
            <label>Floor</label>
            <input type="text" v-model="formData.floor" />
          </div>
        </div>

        <!-- Village / Building -->
        <div class="form-group">
          <label>Village / Building</label>
          <input type="text" v-model="formData.village" />
        </div>

        <!-- Soi and Road -->
        <div class="form-row">
          <div class="form-group">
            <label>Soi</label>
            <input type="text" v-model="formData.soi" />
          </div>
          <div class="form-group">
            <label>Road</label>
            <input type="text" v-model="formData.road" />
          </div>
        </div>

        <!-- Province -->
        <div class="form-group">
          <label>Province</label>
          <input type="text" v-model="formData.province" />
        </div>

        <!-- District -->
        <div class="form-group">
          <label>District</label>
          <select v-model="formData.district">
            <option>บางละมุง</option>
            <option>เมืองชลบุรี</option>
            <option>ศรีราชา</option>
          </select>
        </div>

        <!-- Sub-district -->
        <div class="form-group">
          <label>Sub-district</label>
          <select v-model="formData.subDistrict">
            <option>หนองปรือ</option>
            <option>แสนสุข</option>
            <option>บางสระเก้า</option>
          </select>
        </div>

        <!-- Postal Code -->
        <div class="form-group">
          <label>Postal Code</label>
          <input type="text" v-model="formData.postalCode" />
        </div>

        <div class="actions">
          <button type="submit" class="submit-btn">Confirm</button>
        </div>
      </form>
    </div>
  </div>
</template>

<style scoped>
.regis3-container {
  display: flex;
  flex-direction: column;
  height: 100%;
  width: 100%;
  position: absolute;
  top: 0;
  left: 0;
  background-color: #fff;
  z-index: 100;
}

.orange-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  background-color: var(--primary-orange, #f97316);
  color: white;
  padding: 16px 20px;
  height: 60px;
  flex-shrink: 0;
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
  font-size: 18px;
  font-weight: 600;
  margin: 0;
}

.empty-space {
  width: 32px;
}

.form-body {
  flex: 1;
  display: flex;
  flex-direction: column;
  padding: 24px;
  overflow-y: auto;
  background-color: #fcfcfc; /* very light background similar to the image */
}

.review-text {
  font-size: 13px;
  font-weight: 700;
  color: #111;
  text-align: left;
  margin-bottom: 20px;
  margin-top: 0;
}

.application-form {
  display: flex;
  flex-direction: column;
  gap: 16px;
  max-width: 480px;
  margin: 0 auto;
  width: 100%;
}

.form-row {
  display: flex;
  gap: 12px;
}

.form-group {
  display: flex;
  flex-direction: column;
  flex: 1;
}

.form-group label {
  font-size: 11px;
  color: #888;
  font-weight: 500;
  margin-bottom: 4px;
  margin-left: 2px;
}

.form-group input, .form-group select {
  border: 1px solid #e5e7eb;
  border-radius: 8px;
  padding: 12px 12px;
  font-size: 13px;
  font-weight: 700; /* Bolder font for inputs like in the mockup */
  color: #374151;
  outline: none;
  background-color: #fff;
  transition: border-color 0.2s;
  width: 100%;
}

.form-group input:focus, .form-group select:focus {
  border-color: var(--primary-orange, #f97316);
}

.input-wrapper {
  position: relative;
  display: flex;
  align-items: center;
}

.input-wrapper input {
  padding-right: 40px; /* space for the calendar icon */
}

.calendar-icon {
  position: absolute;
  right: 12px;
  pointer-events: none;
  stroke: #9ca3af;
  z-index: 1;
}

/* Hide the native date picker calendar icon but make the whole input area trigger the calendar */
.date-input::-webkit-calendar-picker-indicator {
  opacity: 0;
  width: 100%;
  height: 100%;
  position: absolute;
  top: 0;
  left: 0;
  cursor: pointer;
  z-index: 2;
}

.actions {
  margin-top: 24px;
  margin-bottom: 40px;
}

.submit-btn {
  width: 100%;
  background-color: var(--primary-orange, #f97316);
  color: white;
  border: none;
  border-radius: 8px;
  padding: 16px;
  font-size: 16px;
  font-weight: 700;
  cursor: pointer;
  transition: all 0.2s ease;
  box-shadow: 0 4px 12px rgba(245, 124, 31, 0.2);
}

.submit-btn:hover, .submit-btn:active {
  opacity: 0.9;
  transform: translateY(-1px);
}
</style>
