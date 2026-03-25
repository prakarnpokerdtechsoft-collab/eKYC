<script setup>
import { langState } from '../store/lang.js'

const emit = defineEmits(['apply-loan', 'view-news', 'contact-us', 'find-dream-car', 'track-status', 'calculate-loan'])

const handleViewNews = () => {
  emit('view-news')
}

const handleContactUs = () => {
  emit('contact-us')
}
</script>

<template>
  <div class="home-absolute-wrapper">
    <div class="home-content">
      <!-- Navbar -->
      <header class="navbar">
        <div class="user-profile">
          <div class="avatar">
            <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
              <path d="M12 2C6.48 2 2 6.48 2 12C2 17.52 6.48 22 12 22C17.52 22 22 17.52 22 12C22 6.48 17.52 2 12 2ZM12 6C13.93 6 15.5 7.57 15.5 9.5C15.5 11.43 13.93 13 12 13C10.07 13 8.5 11.43 8.5 9.5C8.5 7.57 10.07 6 12 6ZM12 20C9.97 20 7.57 19.18 5.86 17.12C7.55 15.8 9.68 15 12 15C14.32 15 16.45 15.8 18.14 17.12C16.43 19.18 14.03 20 12 20Z" fill="#f97316" />
            </svg>
          </div>
          <div class="greeting">
            <span class="greeting-sub">{{ langState.t('ยินดีต้อนรับ', 'Welcome') }}</span>
            <span class="greeting-main">{{ langState.t('ลูกค้าคนสำคัญ', 'Valued Customer') }}</span>
          </div>
        </div>
      </header>

      <!-- Hero Banner -->
      <section class="hero-banner">
        <div class="hero-content-inner">
          <h1 class="hero-title">{{ langState.t('สินเชื่อรถยนต์มือสอง', 'Used Car Hire-Purchase') }}</h1>
          <p class="hero-subtitle">{{ langState.t('อนุมัติไว ดอกเบี้ยถูก ผ่อนสบาย', 'Fast Approval, Low Interest, Easy Installments') }}</p>
          <button class="hero-btn" @click="$emit('apply-loan')">
            {{ langState.t('สมัครเลย', 'Apply Now') }}
          </button>
        </div>
      </section>

      <!-- Services Section -->
      <section class="services-section">
        <h2 class="section-title">{{ langState.t('บริการของเรา', 'Our Services') }}</h2>
        <div class="services-grid">
          
          <!-- 1: Apply for Auto Loan -->
          <div class="service-card" @click="$emit('apply-loan')">
            <div class="card-icon">
              <!-- Application icon -->
              <svg width="32" height="32" viewBox="0 0 24 24" fill="none" class="animated-icon">
                <path d="M14 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V8z" stroke="#f97316" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path>
                <polyline points="14 2 14 8 20 8" stroke="#f97316" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></polyline>
                <line x1="12" y1="18" x2="12" y2="12" stroke="#f97316" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></line>
                <line x1="9" y1="15" x2="15" y2="15" stroke="#f97316" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></line>
              </svg>
            </div>
            <div class="card-info">
              <h3 class="card-title" v-html="langState.t('สมัครสินเชื่อรถยนต์', 'Apply for Auto Loan')"></h3>
              <p class="card-desc">{{ langState.t('กรอกข้อมูลง่าย รู้ผลอนุมัติไว', 'Easy process, fast approval') }}</p>
            </div>
          </div>

          <!-- 2: Track Status -->
          <div class="service-card" @click="$emit('track-status')">
            <div class="card-icon">
              <svg width="32" height="32" viewBox="0 0 24 24" fill="none" class="animated-icon">
                <path d="M16 4h2a2 2 0 0 1 2 2v14a2 2 0 0 1-2 2H6a2 2 0 0 1-2-2V6a2 2 0 0 1 2-2h2" stroke="#f97316" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path>
                <rect x="8" y="2" width="8" height="4" rx="1" ry="1" stroke="#f97316" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></rect>
                <path d="M9 14h6" stroke="#f97316" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path>
                <path d="M9 10h6" stroke="#f97316" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path>
                <path d="M9 18h6" stroke="#f97316" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path>
              </svg>
            </div>
            <div class="card-info">
              <h3 class="card-title">{{ langState.t('ติดตามสถานะ', 'Track Status') }}</h3>
              <p class="card-desc">{{ langState.t('ตรวจสอบสถานะการสมัครของคุณ', 'Check your application status') }}</p>
            </div>
          </div>

          <!-- 3: Find Dream Car -->
          <div class="service-card" @click="$emit('find-dream-car')">
            <div class="card-icon">
              <svg width="32" height="32" viewBox="0 0 24 24" fill="none" class="animated-icon">
                <circle cx="11" cy="11" r="8" stroke="#f97316" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></circle>
                <line x1="21" y1="21" x2="16.65" y2="16.65" stroke="#f97316" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></line>
                <path d="M8 12h6M9 9l4 0" stroke="#f97316" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"></path>
              </svg>
            </div>
            <div class="card-info">
              <h3 class="card-title">{{ langState.t('ค้นหารถในฝัน', 'Find Dream Car') }}</h3>
              <p class="card-desc">{{ langState.t('เลือกรถที่ใช่ ในราคาที่ชอบ', 'Choose the right car for you') }}</p>
            </div>
          </div>

          <!-- 4: Calculate -->
          <div class="service-card" @click="$emit('calculate-loan')">
            <div class="card-icon">
              <svg width="32" height="32" viewBox="0 0 24 24" fill="none" class="animated-icon">
                <rect x="4" y="2" width="16" height="20" rx="2" ry="2" stroke="#f97316" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></rect>
                <line x1="8" y1="6" x2="16" y2="6" stroke="#f97316" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></line>
                <line x1="16" y1="14" x2="16" y2="14.01" stroke="#f97316" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></line>
                <line x1="16" y1="10" x2="16" y2="10.01" stroke="#f97316" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></line>
                <line x1="12" y1="14" x2="12" y2="14.01" stroke="#f97316" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></line>
                <line x1="12" y1="10" x2="12" y2="10.01" stroke="#f97316" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></line>
                <line x1="8" y1="14" x2="8" y2="14.01" stroke="#f97316" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></line>
                <line x1="8" y1="10" x2="8" y2="10.01" stroke="#f97316" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></line>
                <line x1="8" y1="18" x2="16" y2="18" stroke="#f97316" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></line>
              </svg>
            </div>
            <div class="card-info">
              <h3 class="card-title">{{ langState.t('คำนวณค่างวด', 'Calculate') }}</h3>
              <p class="card-desc">{{ langState.t('วางแผนการเงินได้ด้วยตัวเอง', 'Plan your finances easily') }}</p>
            </div>
          </div>

          <!-- 5: Contact us -->
          <div class="service-card" @click="handleContactUs">
            <div class="card-icon">
              <svg width="32" height="32" viewBox="0 0 24 24" fill="none" class="animated-icon">
                <path d="M21 11.5a8.38 8.38 0 0 1-.9 3.8 8.5 8.5 0 0 1-7.6 4.7 8.38 8.38 0 0 1-3.8-.9L3 21l1.9-5.7a8.38 8.38 0 0 1-.9-3.8 8.5 8.5 0 0 1 4.7-7.6 8.38 8.38 0 0 1 3.8-.9h.5a8.48 8.48 0 0 1 8 8v.5z" stroke="#f97316" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path>
                <path d="M9.09 9a3 3 0 0 1 5.83 1c0 2-3 3-3 3" stroke="#f97316" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path>
                <line x1="12" y1="17" x2="12.01" y2="17" stroke="#f97316" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></line>
              </svg>
            </div>
            <div class="card-info">
              <h3 class="card-title">{{ langState.t('ติดต่อเรา', 'Contact us') }}</h3>
              <p class="card-desc">{{ langState.t('พร้อมให้คำปรึกษาตลอดเวลา', 'We are here to help') }}</p>
            </div>
          </div>

          <!-- 6: Promotion And News -->
          <div class="service-card" @click="handleViewNews">
            <div class="card-icon">
              <svg width="32" height="32" viewBox="0 0 24 24" fill="none" class="animated-icon">
                <path d="M3 11l18-5v12L3 14v-3z" stroke="#f97316" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path>
                <path d="M11.6 16.8a3 3 0 1 1-5.8-1.6" stroke="#f97316" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path>
              </svg>
            </div>
            <div class="card-info">
              <h3 class="card-title" v-html="langState.t('โปรโมชั่นและข่าวสาร', 'Promotion & News')"></h3>
              <p class="card-desc">{{ langState.t('อัพเดทสิทธิพิเศษก่อนใคร', 'Update special offers') }}</p>
            </div>
          </div>

        </div>
      </section>
    </div>
  </div>
</template>

<style scoped>
@import url('https://fonts.googleapis.com/css2?family=Inter:wght@400;500;600;700;800&display=swap');

.home-absolute-wrapper {
  position: fixed; /* Breaks out of App.vue 480px parent by fixing to global viewport */
  top: 0;
  left: 0;
  width: 100vw;
  height: 100vh;
  background-color: #f4f7fc;
  z-index: 50; /* Places it over normal content, but beneath z-index 1000 settings */
  overflow-y: auto;
  overflow-x: hidden;
  font-family: 'Inter', -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, Helvetica, Arial, sans-serif;
}

.home-content {
  max-width: 1200px; /* Expansive layout for normal web */
  margin: 0 auto;
  padding: 0 32px 80px 32px;
}

/* Navbar */
.navbar {
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 32px 0;
}

.user-profile {
  display: flex;
  align-items: center;
  gap: 16px;
}

.avatar {
  width: 52px;
  height: 52px;
  background-color: #ffffff;
  border-radius: 50%;
  display: flex;
  align-items: center;
  justify-content: center;
  box-shadow: 0 4px 12px rgba(249, 115, 22, 0.15);
}

.avatar svg {
  fill: #f97316;
}

.greeting {
  display: flex;
  flex-direction: column;
}

.greeting-sub {
  font-size: 14px;
  color: #64748b;
  font-weight: 500;
}

.greeting-main {
  font-size: 18px;
  font-weight: 700;
  color: #0f172a;
}

/* Hero Banner */
.hero-banner {
  margin-top: 8px;
  width: 100%;
  min-height: 300px;
  border-radius: 28px;
  background: linear-gradient(135deg, rgba(15, 23, 42, 0.9) 0%, rgba(15, 23, 42, 0.5) 100%), 
              url('https://images.unsplash.com/photo-1533473359331-0135ef1b58bf?auto=format&fit=crop&w=1200&q=80') center/cover no-repeat;
  display: flex;
  align-items: center;
  padding: 56px;
  box-shadow: 0 20px 40px rgba(0,0,0,0.1);
  position: relative;
  overflow: hidden;
}

.hero-banner::after {
  content: '';
  position: absolute;
  top: 0;
  right: 0;
  width: 50%;
  height: 100%;
  background: linear-gradient(90deg, transparent, rgba(255, 255, 255, 0.05));
  pointer-events: none;
}

.hero-content-inner {
  max-width: 600px;
  z-index: 2;
}

.hero-title {
  font-size: 44px;
  font-weight: 800;
  color: #ffffff;
  margin: 0 0 16px 0;
  line-height: 1.2;
  text-shadow: 0 6px 16px rgba(0,0,0,0.4);
}

.hero-subtitle {
  font-size: 18px;
  color: #cbd5e1;
  margin: 0 0 36px 0;
  line-height: 1.5;
  font-weight: 400;
}

.hero-btn {
  background-color: #f97316;
  color: #fff;
  border: none;
  padding: 16px 40px;
  font-size: 16px;
  font-weight: 600;
  border-radius: 99px;
  cursor: pointer;
  transition: all 0.3s cubic-bezier(0.4, 0, 0.2, 1);
  box-shadow: 0 8px 16px rgba(249, 115, 22, 0.25);
}

.hero-btn:hover {
  background-color: #ea580c;
  transform: translateY(-2px) scale(1.02);
  box-shadow: 0 12px 24px rgba(249, 115, 22, 0.35);
}

/* Services Section */
.services-section {
  margin-top: 56px;
}

.section-title {
  font-size: 26px;
  font-weight: 800;
  color: #0f172a;
  margin: 0 0 28px 0;
}

.services-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(320px, 1fr));
  gap: 24px;
}

.service-card {
  background: #ffffff;
  border-radius: 20px;
  padding: 32px 28px;
  cursor: pointer;
  transition: all 0.3s cubic-bezier(0.4, 0, 0.2, 1);
  box-shadow: 0 4px 6px -1px rgba(0, 0, 0, 0.02), 0 2px 4px -1px rgba(0, 0, 0, 0.02);
  border: 1px solid rgba(226, 232, 240, 0.6);
  display: flex;
  flex-direction: column;
  align-items: flex-start;
}

.service-card:hover {
  transform: translateY(-6px);
  box-shadow: 0 20px 25px -5px rgba(0, 0, 0, 0.05), 0 10px 10px -5px rgba(0, 0, 0, 0.02);
  border-color: rgba(249, 115, 22, 0.3);
}

.card-icon {
  width: 64px;
  height: 64px;
  background: rgba(249, 115, 22, 0.08);
  border-radius: 18px;
  display: flex;
  align-items: center;
  justify-content: center;
  margin-bottom: 24px;
  transition: all 0.3s ease;
}

.service-card:hover .card-icon {
  background: #f97316;
  transform: scale(1.05);
}

/* Fill or stroke color switches to white on hover */
.animated-icon [stroke] {
  transition: all 0.3s ease;
}
.animated-icon [fill]:not([fill="none"]) {
  transition: all 0.3s ease;
}

.service-card:hover .animated-icon [stroke] {
  stroke: #ffffff !important;
}
.service-card:hover .animated-icon [fill]:not([fill="none"]) {
  fill: #ffffff !important;
}

.card-title {
  font-size: 19px;
  font-weight: 700;
  color: #1e293b;
  margin: 0 0 8px 0;
  line-height: 1.4;
}

.card-desc {
  font-size: 15px;
  color: #64748b;
  margin: 0;
  line-height: 1.5;
}

/* Responsive constraints for smaller screens */
@media (max-width: 768px) {
  .home-absolute-wrapper {
    position: absolute;
    width: 100vw;
  }
  
  .home-content {
    padding: 0 16px 32px 16px;
  }

  .navbar {
    padding: 16px 0;
  }

  .avatar {
    width: 42px;
    height: 42px;
  }

  .greeting-main {
    font-size: 16px;
  }
  
  .hero-banner {
    padding: 24px 20px;
    min-height: 200px;
    border-radius: 20px;
  }
  
  .hero-title {
    font-size: 26px;
  }
  
  .hero-subtitle {
    font-size: 14px;
    margin-bottom: 24px;
  }

  .hero-btn {
    padding: 12px 28px;
    font-size: 14px;
  }
  
  .services-grid {
    grid-template-columns: repeat(2, 1fr);
    gap: 12px;
  }

  .service-card {
    padding: 20px 16px;
    border-radius: 16px;
  }

  .card-icon {
    width: 48px;
    height: 48px;
    margin-bottom: 16px;
    border-radius: 14px;
  }

  .card-icon svg {
    width: 24px;
    height: 24px;
  }

  .card-title {
    font-size: 14px;
  }
  
  .card-desc {
    font-size: 12px;
  }
}
</style>
