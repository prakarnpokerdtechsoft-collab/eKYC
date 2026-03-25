<script setup>
import { langState } from '../store/lang.js'

const emit = defineEmits(['back'])

const steps = [
  { id: 1, label: { th: 'Draft', en: 'Draft' }, completed: true },
  { id: 2, label: { th: 'In Process', en: 'In Process' }, completed: true },
  { id: 3, label: { th: 'Pre-Approved', en: 'Pre-Approved' }, completed: true },
  { id: 4, label: { th: 'Matching', en: 'Matching' }, completed: false },
  { id: 5, label: { th: 'Approved', en: 'Approved' }, completed: false }
]
</script>

<template>
  <div class="follow-status-container">
    <header class="status-header">
      <button class="back-btn" @click="$emit('back')">
        <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
          <path d="M15 18L9 12L15 6" stroke="white" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
        </svg>
      </button>
      <h1 class="header-title">{{ langState.t('ติดตามสถานะ', 'Follow Status') }}</h1>
      <div class="empty-space"></div>
    </header>

    <div class="content">
      <div class="status-card">
        <div class="card-header">
          <div class="status-icon-wrapper">
             <div class="status-circle"></div>
          </div>
          <div class="application-info">
            <h3 class="app-id">#250212000002</h3>
            <p class="app-status">On the way · 24 June</p>
          </div>
          <div class="chevron-wrapper">
            <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="#6b7280" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
              <polyline points="9 18 15 12 9 6"></polyline>
            </svg>
          </div>
        </div>

        <div class="progress-container">
          <div class="progress-line"></div>
          <div class="steps-wrapper">
            <div v-for="(step, index) in steps" :key="step.id" class="step-item">
              <div 
                class="step-circle" 
                :class="{ completed: step.completed }"
              >
                <svg v-if="step.completed" width="12" height="12" viewBox="0 0 24 24" fill="none" stroke="white" stroke-width="3" stroke-linecap="round" stroke-linejoin="round">
                  <polyline points="20 6 9 17 4 12"></polyline>
                </svg>
              </div>
              <span class="step-label" :class="{ active: index === 2 }">
                {{ langState.t(step.label.th, step.label.en) }}
              </span>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
.follow-status-container {
  display: flex;
  flex-direction: column;
  min-height: 100vh;
  background-color: #ffffff;
  width: 100%;
}

.status-header {
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

.content {
  padding: 20px;
}

.status-card {
  background: white;
  border-radius: 12px;
  padding: 20px;
  box-shadow: 0 4px 6px -1px rgba(0, 0, 0, 0.1);
}

.card-header {
  display: flex;
  align-items: center;
  gap: 16px;
  margin-bottom: 24px;
}

.status-icon-wrapper {
  width: 48px;
  height: 48px;
  border-radius: 50%;
  border: 1px solid #e5e7eb;
  display: flex;
  align-items: center;
  justify-content: center;
}

.status-circle {
  width: 24px;
  height: 24px;
  border-radius: 50%;
  border: 2px solid #374151;
}

.application-info {
  flex: 1;
}

.app-id {
  font-size: 18px;
  font-weight: 700;
  color: #111827;
  margin: 0;
}

.app-status {
  font-size: 14px;
  color: #6b7280;
  margin: 4px 0 0;
}

.progress-container {
  position: relative;
  padding-top: 10px;
}

.progress-line {
  position: absolute;
  top: 18px;
  left: 5%;
  right: 5%;
  height: 2px;
  background-color: #e5e7eb;
  z-index: 1;
}

.steps-wrapper {
  display: flex;
  justify-content: space-between;
  position: relative;
  z-index: 2;
}

.step-item {
  display: flex;
  flex-direction: column;
  align-items: center;
  width: 20%;
}

.step-circle {
  width: 18px;
  height: 18px;
  border-radius: 50%;
  background-color: #9ca3af;
  margin-bottom: 8px;
  display: flex;
  align-items: center;
  justify-content: center;
}

.step-circle.completed {
  background-color: #f97316;
}

.step-label {
  font-size: 10px;
  color: #6b7280;
  text-align: center;
  white-space: nowrap;
}

.step-label.active {
  color: #0d9488; /* Just an example color for current step label */
}
</style>
