<script setup>
const emit = defineEmits(['back', 'next'])

const handleBack = () => {
  emit('back')
}

const handleNext = () => {
  emit('next')
}
</script>

<template>
  <div class="instructions-container">
    <header class="orange-header">
      <button class="back-btn" @click="handleBack">
        <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
          <path d="M15 18L9 12L15 6" stroke="white" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
        </svg>
      </button>
      <h1 class="header-title">Prepare your ID card</h1>
      <div class="empty-space"></div>
    </header>

    <div class="content-body">
      <h2 class="section-title">Instructions</h2>

      <div class="instruction-list">
        <!-- Item 1: Frame -->
        <div class="instruction-item">
          <div class="icon-wrapper">
            <svg width="64" height="64" viewBox="0 0 64 64" fill="none" xmlns="http://www.w3.org/2000/svg">
              <rect x="18" y="24" width="28" height="18" rx="2" stroke="#4b5563" stroke-width="2"/>
              <rect x="22" y="34" width="6" height="4" rx="1" stroke="#4b5563" stroke-width="1.5"/>
              <circle cx="38" cy="33" r="3" stroke="#4b5563" stroke-width="1.5"/>
              <path d="M8 18V12H14M50 12H56V18M8 46V52H14M50 52H56V46" stroke="#4b5563" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
            </svg>
          </div>
          <p class="instruction-text">Place your card within the designated frame.</p>
        </div>

        <!-- Item 2: Lighting -->
        <div class="instruction-item">
          <div class="icon-wrapper">
            <svg width="64" height="64" viewBox="0 0 64 64" fill="none" xmlns="http://www.w3.org/2000/svg">
              <rect x="14" y="32" width="28" height="18" rx="2" stroke="#4b5563" stroke-width="2"/>
              <rect x="18" y="42" width="6" height="4" rx="1" stroke="#4b5563" stroke-width="1.5"/>
              <circle cx="34" cy="41" r="3" stroke="#4b5563" stroke-width="1.5"/>
              
              <path d="M44 28V26C44 21.5817 40.4183 18 36 18C31.5817 18 28 21.5817 28 26V28" stroke="#4b5563" stroke-width="2" stroke-linecap="round"/>
              <path d="M31 32H41M36 10V14M48 18L45 21M24 18L27 21M52 28H48" stroke="#4b5563" stroke-width="2" stroke-linecap="round"/>
            </svg>
          </div>
          <p class="instruction-text">Ensure there is sufficient lighting for visibility.</p>
        </div>

        <!-- Item 3: Glare -->
        <div class="instruction-item">
          <div class="icon-wrapper">
            <svg width="64" height="64" viewBox="0 0 64 64" fill="none" xmlns="http://www.w3.org/2000/svg">
              <rect x="14" y="36" width="28" height="18" rx="2" stroke="#4b5563" stroke-width="2"/>
              <rect x="18" y="46" width="6" height="4" rx="1" stroke="#4b5563" stroke-width="1.5"/>
              <path d="M22 28L28 22M25 25L32 20M36 16L32 22M16 30L20 26M42 42L52 52M52 42L42 52" stroke="#4b5563" stroke-width="2" stroke-linecap="round"/>
            </svg>
          </div>
          <p class="instruction-text">Beware of glare/reflections on the card surface.</p>
        </div>
      </div>

      <div class="actions">
        <button class="next-btn" @click="handleNext">Start taking ID card photo</button>
      </div>
    </div>
  </div>
</template>

<style scoped>
.instructions-container {
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
  background-color: var(--primary-orange, #f97316); /* Fallback to a clear orange if variable missing */
  color: white;
  padding: 16px 20px;
  height: 60px;
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
  width: 32px; /* Matches back-btn width roughly */
}

.content-body {
  flex: 1;
  display: flex;
  flex-direction: column;
  padding: 32px 24px;
  overflow-y: auto;
}

.section-title {
  text-align: center;
  color: var(--primary-orange, #f97316);
  font-size: 20px;
  font-weight: 700;
  margin-bottom: 40px;
  margin-top: 0;
}

.instruction-list {
  display: flex;
  flex-direction: column;
  gap: 40px;
  flex: 1;
}

.instruction-item {
  display: flex;
  flex-direction: column;
  align-items: center;
  text-align: center;
}

.icon-wrapper {
  margin-bottom: 16px;
  display: flex;
  justify-content: center;
  align-items: center;
}

.instruction-text {
  font-size: 14px;
  color: #ea580c; /* Matches typical orange text */
  margin: 0;
  max-width: 250px;
  line-height: 1.4;
}

.actions {
  margin-top: auto;
  padding-top: 32px;
  padding-bottom: 24px;
}

.next-btn {
  width: 100%;
  background-color: var(--primary-orange, #f97316);
  color: white;
  border: none;
  border-radius: 8px;
  padding: 16px;
  font-size: 16px;
  font-weight: 600;
  cursor: pointer;
  transition: all 0.2s ease;
}

.next-btn:active, .next-btn:hover {
  opacity: 0.9;
  transform: translateY(-1px);
}
</style>
