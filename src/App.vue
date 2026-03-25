<script setup>
import { ref, onMounted, onUnmounted } from 'vue'
import SignIn from './components/SignIn.vue'
import ConfirmOTP from './components/ConfirmOTP.vue'
import Home from './components/Home.vue'
import IdCardInstructions from './components/IdCardInstructions.vue'
import IdCardCamera from './components/IdCardCamera.vue'
import Regis3ApplicationForm from './components/Regis3ApplicationForm.vue'
import News from './components/News.vue'
import Chat from './components/Chat.vue'
import FindDreamCar from './components/FindDreamCar.vue'
import FollowStatus from './components/FollowStatus.vue'
import LoanCalculator from './components/LoanCalculator.vue'
import AppMan from './components/AppMan.vue'
import AppDialog from './components/AppDialog.vue'
import AppLoading from './components/AppLoading.vue'
import { langState, setLang } from './store/lang.js'
import { createCase } from './services/api'

const confirm = ref(null)
const loading = ref(null)

const currentView = ref('SignIn')
const phoneNumber = ref('')
const errorMessage = ref('')
const verificationId = ref('')

const handleIncomingMessage = (event) => {
  // 🚩 สำคัญ: ตรวจสอบ Origin เสมอเพื่อความปลอดภัย
  // if (event.origin !== 'https://techsoft-ekyc-demo.mac.appmanteam.com') return 

  console.log('ได้รับข่าวจาก Third-party:', event.data)

  // ตัวอย่างการเช็คเงื่อนไข
  if (event.data.status === 'success') {
    alert('บันทึกข้อมูลสำเร็จ!')
  }
}

onMounted(() => {
  window.addEventListener('message', handleIncomingMessage)
})

onUnmounted(() => {
  window.removeEventListener('message', handleIncomingMessage)
})

const handleSignIn = (phone) => {
  phoneNumber.value = phone
  currentView.value = 'ConfirmOTP'
}

const handleBack = () => {
  currentView.value = 'SignIn'
}

const handleOTPSubmit = (otp) => {
  console.log('Submitted OTP for', phoneNumber.value, ':', otp)
  // Allow any OTP to pass for testing purposes
  currentView.value = 'Home'
}

const handlePhotoCaptured = () => {
  // Mock OCR/Upload delay or just transition immediately
  currentView.value = 'Regis3'
}

const handleApplicationConfirm = (formData) => {
  alert('Application confirmed! Returning to Home.')
  console.log('Confirmed Data:', formData)
  currentView.value = 'Home'
}

const applyLoan = async () => {
  const result = await confirm.value.show({
    title: 'ยืนยันการสมัครสินเชื่อ',
    message: `คุณต้องการสมัครสินเชื่อด้วยเบอร์โทรศัพท์ ${phoneNumber.value} ใช่หรือไม่?`,
    type: 'warning',
    confirmText: 'ยืนยัน'
  })

  if (!result) return

  loading.value.show('Loading...');

  try {
    const response = await createCase(phoneNumber.value);
    verificationId.value = response.data.verifications[0].proprietorVerifications[0].verificationId;
    // verificationId.value = "89c5b9d3-d22a-4dd5-8d36-9a9030125ba2";
    if (verificationId.value) {
      currentView.value = 'AppMan'
    } else {
      errorMessage.value = 'เกิดข้อผิดพลาดในการดึงข้อมูล';
      await confirm.value.show({
        title: 'เกิดข้อผิดพลาด',
        message: errorMessage.value,
        type: 'danger',
        isAlert: true,
        confirmText: 'รับทราบ'
      })
    }
  } catch (error) {
    errorMessage.value = 'เกิดข้อผิดพลาดในการดึงข้อมูล';
    await confirm.value.show({
      title: 'เกิดข้อผิดพลาด',
      message: errorMessage.value,
      type: 'danger',
      isAlert: true,
      confirmText: 'รับทราบ'
    })
  } finally {
    loading.value.hide();
  }
  // currentView.value = 'IdCardInstructions'
}
</script>

<template>
  <div class="app-wrapper" :class="{ 'with-padding': ['SignIn', 'ConfirmOTP'].includes(currentView) }">
    <!-- Global Language Toggle -->
    <div class="lang-toggle">
      <span :class="{ active: langState.current === 'TH', inactive: langState.current !== 'TH' }"
        @click="setLang('TH')">TH</span>
      <span class="separator">|</span>
      <span :class="{ active: langState.current === 'EN', inactive: langState.current !== 'EN' }"
        @click="setLang('EN')">EN</span>
    </div>

    <header class="top-bar" v-if="['SignIn', 'ConfirmOTP'].includes(currentView)">
      <div class="app-header">
        <button v-if="currentView === 'ConfirmOTP'" class="back-btn" @click="handleBack">
          <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
            <path d="M15 18L9 12L15 6" stroke="black" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" />
          </svg>
        </button>
        <div v-else class="empty-space"></div>
      </div>
    </header>

    <main class="main-content">
      <SignIn v-if="currentView === 'SignIn'" @submit="handleSignIn" />
      <ConfirmOTP v-else-if="currentView === 'ConfirmOTP'" :phone-number="phoneNumber" @submit="handleOTPSubmit" />
      <Home v-else-if="currentView === 'Home'" @apply-loan="applyLoan" @view-news="currentView = 'News'"
        @contact-us="currentView = 'Chat'" @find-dream-car="currentView = 'FindDreamCar'"
        @track-status="currentView = 'FollowStatus'" @calculate-loan="currentView = 'LoanCalculator'" />
      <IdCardInstructions v-else-if="currentView === 'IdCardInstructions'" @back="currentView = 'SignIn'"
        @next="currentView = 'IdCardCamera'" />
      <IdCardCamera v-else-if="currentView === 'IdCardCamera'" @back="currentView = 'IdCardInstructions'"
        @capture="handlePhotoCaptured" />
      <Regis3ApplicationForm v-else-if="currentView === 'Regis3'" @back="currentView = 'IdCardCamera'"
        @confirm="handleApplicationConfirm" />
      <News v-else-if="currentView === 'News'" @back="currentView = 'Home'" />
      <Chat v-else-if="currentView === 'Chat'" @back="currentView = 'Home'" />
      <FindDreamCar v-else-if="currentView === 'FindDreamCar'" @back="currentView = 'Home'" />
      <FollowStatus v-else-if="currentView === 'FollowStatus'" @back="currentView = 'Home'" />
      <LoanCalculator v-else-if="currentView === 'LoanCalculator'" @back="currentView = 'Home'" />
      <AppMan :id="verificationId" v-else-if="currentView === 'AppMan'" @back="currentView = 'Home'" />
    </main>

    <AppDialog ref="confirm" />
    <AppLoading ref="loading" />
  </div>
</template>

<style scoped>
.app-wrapper {
  display: flex;
  flex-direction: column;
  min-height: 100dvh;
  width: 100%;
  max-width: 480px;
  margin: 0 auto;
  background-color: #ffffff;
}

.with-padding {
  padding: 20px;
}

.top-bar {
  margin-bottom: 20px;
}

.app-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  height: 24px;
}

.back-btn {
  background: none;
  border: none;
  cursor: pointer;
  padding: 0;
  display: flex;
  align-items: center;
  transition: opacity 0.2s;
}

.back-btn:hover {
  opacity: 0.7;
}

.empty-space {
  width: 24px;
}

.lang-toggle {
  position: absolute;
  top: 24px;
  right: 24px;
  font-size: 13px;
  font-weight: 700;
  cursor: pointer;
  z-index: 1000;
}

.lang-toggle .active {
  color: #111;
}

.lang-toggle .separator {
  margin: 0 4px;
  color: #111;
}

.lang-toggle .inactive {
  color: #888;
  transition: opacity 0.2s;
}

.lang-toggle .inactive:hover {
  opacity: 0.8;
}

.main-content {
  flex: 1;
  display: flex;
  flex-direction: column;
}
</style>
