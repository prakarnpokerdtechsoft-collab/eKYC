<script setup>
import { ref, onMounted, onUnmounted } from 'vue'

const emit = defineEmits(['back', 'capture'])

const videoElement = ref(null)
const stream = ref(null)
const errorMsg = ref('')

const startCamera = async () => {
  try {
    if (!navigator.mediaDevices || !navigator.mediaDevices.getUserMedia) {
      console.warn('Camera API not supported in this context (e.g., non-HTTPS). Entering Mock Mode.')
      errorMsg.value = 'MOCK MODE: Camera unavailable. You can still test the capture button.'
      return
    }
    
    stream.value = await navigator.mediaDevices.getUserMedia({ 
      video: { facingMode: 'environment' } 
    })
    if (videoElement.value) {
      videoElement.value.srcObject = stream.value
    }
  } catch (err) {
    console.error('Error accessing camera:', err)
    errorMsg.value = 'MOCK MODE: Permission denied or no camera found. You can still test the capture button.'
  }
}

const stopCamera = () => {
  if (stream.value) {
    stream.value.getTracks().forEach(track => track.stop())
    stream.value = null
  }
}

const handleBack = () => {
  stopCamera()
  emit('back')
}

const handleCapture = () => {
  stopCamera()
  emit('capture')
}

onMounted(() => {
  startCamera()
})

onUnmounted(() => {
  stopCamera()
})
</script>

<template>
  <div class="camera-container">
    <header class="orange-header">
      <button class="back-btn" @click="handleBack">
        <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
          <path d="M15 18L9 12L15 6" stroke="white" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
        </svg>
      </button>
      <h1 class="header-title">Take a photo of ID card</h1>
      <div class="empty-space"></div>
    </header>

    <div class="camera-body">
      <!-- Video Feed Background -->
      <video 
        ref="videoElement" 
        class="video-feed" 
        autoplay 
        playsinline
        muted
      ></video>
      
      <!-- Fallback Error Message -->
      <div v-if="errorMsg" class="error-msg">{{ errorMsg }}</div>

      <!-- UI Overlay -->
      <div class="overlay">
        <!-- Frame Guide -->
        <div class="frame-guide">
          <div class="corner top-left"></div>
          <div class="corner top-right"></div>
          <div class="corner bottom-left"></div>
          <div class="corner bottom-right"></div>
        </div>

        <!-- Small ID Icon beneath frame -->
        <div class="camera-id-icon">
          <svg width="60" height="40" viewBox="0 0 60 40" fill="none" xmlns="http://www.w3.org/2000/svg">
            <rect x="2" y="2" width="56" height="36" rx="6" fill="#60a5fa" stroke="#fbbf24" stroke-width="2" stroke-dasharray="8 4"/>
            <circle cx="20" cy="20" r="6" fill="#1e3a8a"/>
            <rect x="34" y="14" width="16" height="4" rx="2" fill="#1e3a8a"/>
            <rect x="34" y="22" width="10" height="4" rx="2" fill="#1e3a8a"/>
            <path d="M12 30C12 26 16 26 20 26C24 26 28 26 28 30V32H12V30Z" fill="#1e3a8a"/>
          </svg>
        </div>

        <!-- Capture Button -->
        <div class="capture-btn-wrapper">
          <button class="capture-btn" @click="handleCapture"></button>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
.camera-container {
  display: flex;
  flex-direction: column;
  height: 100%;
  width: 100%;
  position: absolute;
  top: 0;
  left: 0;
  background-color: #333;
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
  z-index: 10;
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

.camera-body {
  flex: 1;
  position: relative;
  overflow: hidden;
  background-color: #1f2937; /* Dark grayish fallback for camera */
}

.video-feed {
  width: 100%;
  height: 100%;
  object-fit: cover;
  position: absolute;
  top: 0;
  left: 0;
}

.error-msg {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  color: white;
  background: rgba(0,0,0,0.6);
  padding: 16px;
  border-radius: 8px;
  text-align: center;
  z-index: 2;
  width: 80%;
}

.overlay {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  z-index: 5;
  display: flex;
  flex-direction: column;
  align-items: center;
}

.frame-guide {
  width: 85%;
  height: 220px;
  margin-top: 60px;
  border-radius: 12px;
  background: rgba(255, 255, 255, 0.1);
  box-shadow: 0 0 0 9999px rgba(31, 41, 55, 0.7); /* Dark semi-transparent overlay outside */
  position: relative;
}

/* Orange corners for the frame guide */
.corner {
  position: absolute;
  width: 40px;
  height: 40px;
  border-color: #f59e0b; /* yellow-orange */
  border-style: solid;
}

.top-left {
  top: 0;
  left: 0;
  border-width: 4px 0 0 4px;
  border-top-left-radius: 12px;
}

.top-right {
  top: 0;
  right: 0;
  border-width: 4px 4px 0 0;
  border-top-right-radius: 12px;
}

.bottom-left {
  bottom: 0;
  left: 0;
  border-width: 0 0 4px 4px;
  border-bottom-left-radius: 12px;
}

.bottom-right {
  bottom: 0;
  right: 0;
  border-width: 0 4px 4px 0;
  border-bottom-right-radius: 12px;
}

.camera-id-icon {
  margin-top: 40px;
}

.capture-btn-wrapper {
  margin-top: auto;
  margin-bottom: 40px;
}

.capture-btn {
  width: 72px;
  height: 72px;
  border-radius: 50%;
  background-color: white;
  border: 4px solid #d1d5db; /* gray border */
  outline: 2px solid white;
  outline-offset: 2px;
  cursor: pointer;
  transition: transform 0.1s;
}

.capture-btn:active {
  transform: scale(0.95);
}
</style>
