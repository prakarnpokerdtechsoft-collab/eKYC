<script setup>
import { ref } from 'vue'
import AppLogo from './AppLogo.vue'
import { langState } from '../store/lang.js'

const emit = defineEmits(['submit'])
const phone = ref('')

const handleSubmit = () => {
  if (phone.value) {
    emit('submit', phone.value)
  }
}
</script>

<template>
  <div class="signin-container">
    <div class="logo-section">
      <AppLogo />
    </div>

    <div class="role-text">{{ langState.t('ลูกค้า', 'Customer') }}</div>

    <div class="title-section">
      <h2>{{ langState.t('เข้าสู่ระบบ', 'Sign in') }}</h2>
      <p>{{ langState.t('กรุณากรอกเบอร์โทรศัพท์ของคุณ', 'Please enter your Phone No.') }}</p>
    </div>

    <form @submit.prevent="handleSubmit" class="form-section">
      <div class="input-group">
        <input 
          type="tel" 
          v-model="phone" 
          :placeholder="langState.t('เบอร์โทรศัพท์', 'Enter your phone number')" 
          class="phone-input"
          required
        />
      </div>
      <button type="submit" class="submit-btn" :disabled="!phone">{{ langState.t('เข้าสู่ระบบ', 'Sign In') }}</button>
    </form>
  </div>
</template>

<style scoped>
.signin-container {
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



.role-text {
  color: var(--primary-orange);
  font-weight: 600;
  font-size: 16px;
  margin-bottom: 16px;
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
  flex: 1;
}

.input-group {
  margin-bottom: auto;
}

.phone-input {
  width: 100%;
  padding: 16px;
  border: 1px solid #e5e7eb;
  border-radius: 8px;
  font-size: 16px;
  outline: none;
  transition: all 0.2s ease;
}

.phone-input:focus {
  border-color: var(--primary-orange);
  box-shadow: 0 0 0 3px rgba(245, 124, 31, 0.1);
}

.phone-input::placeholder {
  color: #9ca3af;
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
  margin-top: 24px;
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
