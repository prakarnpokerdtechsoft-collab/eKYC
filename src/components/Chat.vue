<script setup>
import { ref } from 'vue'
import { langState } from '../store/lang.js'

const emit = defineEmits(['back'])

const message = ref('')
const chatHistory = ref([
  {
    id: 1,
    sender: 'bot',
    text: 'Hello, how can i help you?'
  }
])

const sendMessage = () => {
  if (message.value.trim()) {
    chatHistory.value.push({
      id: Date.now(),
      sender: 'user',
      text: message.value
    })
    message.value = ''
  }
}
</script>

<template>
  <div class="chat-container">
    <header class="chat-header">
      <button class="back-btn" @click="$emit('back')">
        <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
          <path d="M15 18L9 12L15 6" stroke="white" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
        </svg>
      </button>
      <h1 class="header-title">{{ langState.t('ติดต่อเรา', 'Contact Us') }}</h1>
      <div class="empty-space"></div>
    </header>

    <div class="chat-body">
      <div v-for="msg in chatHistory" :key="msg.id" class="message-row" :class="msg.sender">
        <div v-if="msg.sender === 'bot'" class="bot-icon">
          <svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="#f97316" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
            <rect x="3" y="11" width="18" height="10" rx="2" ry="2"></rect>
            <circle cx="12" cy="5" r="2"></circle>
            <path d="M12 7v4"></path>
            <line x1="8" y1="16" x2="8" y2="16"></line>
            <line x1="16" y1="16" x2="16" y2="16"></line>
          </svg>
        </div>
        <div class="message-bubble">
          {{ msg.text }}
        </div>
      </div>
    </div>

    <div class="chat-input-area">
      <div class="input-pill">
        <input 
          type="text" 
          v-model="message" 
          :placeholder="langState.t('พิมพ์ข้อความของคุณ...', 'Write your message')" 
          @keyup.enter="sendMessage"
        />
        <button class="mic-btn">
          <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="#9ca3af" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
            <path d="M12 1a3 3 0 0 0-3 3v8a3 3 0 0 0 6 0V4a3 3 0 0 0-3-3z"></path>
            <path d="M19 10v2a7 7 0 0 1-14 0v-2"></path>
            <line x1="12" y1="19" x2="12" y2="23"></line>
            <line x1="8" y1="23" x2="16" y2="23"></line>
          </svg>
        </button>
        <button class="send-btn" @click="sendMessage">
          <svg width="22" height="22" viewBox="0 0 24 24" fill="none" stroke="#f97316" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
            <line x1="22" y1="2" x2="11" y2="13"></line>
            <polygon points="22 2 15 22 11 13 2 9 22 2" fill="#f97316" stroke="none"></polygon>
          </svg>
        </button>
      </div>
    </div>
  </div>
</template>

<style scoped>
.chat-container {
  display: flex;
  flex-direction: column;
  max-width: 480px;
  margin: 0 auto;
  height: 100vh;
  background-color: #ffffff;
}

.chat-header {
  display: flex;
  align-items: center;
  justify-content: space-between;
  background-color: var(--primary-orange, #f97316);
  padding: 16px;
  color: white;
}

.back-btn {
  background: none;
  border: none;
  cursor: pointer;
  padding: 8px;
  display: flex;
  align-items: center;
  justify-content: center;
}

.header-title {
  margin: 0;
  font-size: 18px;
  font-weight: 700;
  flex: 1;
  text-align: center;
}

.empty-space {
  width: 40px;
}

.chat-body {
  flex: 1;
  padding: 24px 16px;
  overflow-y: auto;
  display: flex;
  flex-direction: column;
  gap: 16px;
}

.message-row {
  display: flex;
  align-items: flex-start;
  gap: 8px;
}

.message-row.user {
  flex-direction: row-reverse;
}

.bot-icon {
  width: 36px;
  height: 36px;
  border-radius: 50%;
  border: 1px solid #f97316;
  background: #fff;
  display: flex;
  align-items: center;
  justify-content: center;
  flex-shrink: 0;
}

.message-bubble {
  padding: 14px 20px;
  border-radius: 20px;
  font-size: 14px;
  line-height: 1.5;
  max-width: 75%;
}

.bot .message-bubble {
  background-color: #f3f4f6;
  color: #333;
  border-top-left-radius: 4px;
}

.user .message-bubble {
  background-color: var(--primary-orange, #f97316);
  color: white;
  border-top-right-radius: 4px;
}

.chat-input-area {
  padding: 16px;
  background-color: transparent;
  padding-bottom: 30px; /* Leave some space at bottom like standard modern chats */
}

.input-pill {
  display: flex;
  align-items: center;
  background-color: #fff;
  border-radius: 40px;
  padding: 6px 12px;
  box-shadow: 0 4px 16px rgba(0, 0, 0, 0.08);
  border: 1px solid #f3f4f6;
}

.input-pill input {
  flex: 1;
  border: none;
  outline: none;
  font-size: 14px;
  padding: 12px 12px;
}

.input-pill input::placeholder {
  color: #9ca3af;
}

.mic-btn {
  background: none;
  border: none;
  padding: 8px;
  cursor: pointer;
  display: flex;
  align-items: center;
  justify-content: center;
}

.send-btn {
  background: none;
  border: none;
  padding: 8px;
  cursor: pointer;
  display: flex;
  align-items: center;
  justify-content: center;
  margin-left: 4px;
}
</style>
