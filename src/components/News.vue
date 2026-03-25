<script setup>
import { ref } from 'vue'
import { langState } from '../store/lang.js'

const emit = defineEmits(['back'])

const newsItems = ref([
  {
    id: 1,
    tag: 'บทความ',
    title: 'มารู้จักรถแลกเงิน',
    author: 'ltoalt',
    date: 'May 11, 2020',
    snippet: 'มารู้จักรถแลกเงิน ว่ารถแลกเงินคืออะไร รถแลกเงินคือ การนำรถไปเป็นหลักทรัพย์ค้ำประกันเพื่อแลกรับเงินสด...',
    image: 'https://images.unsplash.com/photo-1549317661-bd32c8ce0db2?auto=format&fit=crop&w=400&h=200&q=80'
  },
  {
    id: 2,
    tag: 'บทความ',
    title: 'เลือกซื้อรถยนต์มือสองอย่างไร ถึงจะไม่โดนย้อมแมว',
    author: 'ltoalt',
    date: 'May 9, 2020',
    snippet: 'อย่างที่เราได้เกริ่นไปในตอนแรกว่า การเลือกซื้อรถมือสองนั้นต้องทำการศึกษาข้อมูลกันพอสมควรเลยทีเดียว...',
    image: 'https://images.unsplash.com/photo-1553440569-bcc63803a83d?auto=format&fit=crop&w=400&h=200&q=80'
  },
  {
    id: 3,
    tag: 'บทความ',
    title: 'รีไฟแนนซ์รถอย่างไร ถึงจะคุ้มค่าที่สุด?',
    author: 'ltoalt',
    date: 'May 9, 2020',
    snippet: 'การรีไฟแนนซ์รถถือเป็นอีกช่องทางสำหรับคนที่ต้องการหาเงินก้อนมาใช้จ่ายในเรื่องจำเป็น แต่ประเด็นของหลาย ๆ...',
    image: 'https://images.unsplash.com/photo-1580273916550-e323be2ae537?auto=format&fit=crop&w=400&h=200&q=80'
  }
])
</script>

<template>
  <div class="news-container">
    <header class="news-header">
      <button class="back-btn" @click="$emit('back')">
        <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
          <path d="M15 18L9 12L15 6" stroke="white" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
        </svg>
      </button>
      <h1 class="header-title">{{ langState.t('ข่าวสารและโปรโมชั่น', 'News & Promotions') }}</h1>
      <div class="empty-space"></div>
    </header>

    <div class="news-list">
      <article v-for="item in newsItems" :key="item.id" class="news-card">
        <div class="card-image-wrapper">
          <span class="tag">{{ item.tag }}</span>
          <img :src="item.image" :alt="item.title" class="news-image" />
        </div>
        <div class="card-content">
          <h2 class="news-title">{{ item.title }}</h2>
          <div class="news-meta">
            <span class="author">{{ item.author }}</span>
            <span class="dot">•</span>
            <span class="date">{{ item.date }}</span>
          </div>
          <p class="news-snippet">{{ item.snippet }}</p>
        </div>
      </article>
    </div>
  </div>
</template>

<style scoped>
.news-container {
  display: flex;
  flex-direction: column;
  max-width: 480px;
  margin: 0 auto;
  min-height: 100vh;
  background-color: #ffffff;
}

.news-header {
  display: flex;
  align-items: center;
  justify-content: space-between;
  background-color: #f97316; /* Primary orange */
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
  width: 40px; /* match the back button width to keep title centered */
}

.news-list {
  padding: 16px;
  display: flex;
  flex-direction: column;
  gap: 24px;
}

.news-card {
  background: white;
  border-radius: 8px;
  overflow: hidden;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.05);
  display: flex;
  flex-direction: column;
}

.card-image-wrapper {
  position: relative;
  width: 100%;
  height: 180px;
}

.tag {
  position: absolute;
  top: 12px;
  left: 0;
  background-color: white;
  color: #f97316;
  font-size: 12px;
  font-weight: 700;
  padding: 4px 12px;
  border-top-right-radius: 4px;
  border-bottom-right-radius: 4px;
  box-shadow: 0 2px 4px rgba(0,0,0,0.1);
  z-index: 2;
}

.news-image {
  width: 100%;
  height: 100%;
  object-fit: cover;
  display: block;
}

.card-content {
  padding: 16px;
}

.news-title {
  margin: 0 0 8px 0;
  font-size: 16px;
  font-weight: 700;
  color: #1f2937;
  line-height: 1.4;
}

.news-meta {
  display: flex;
  align-items: center;
  font-size: 12px;
  color: #6b7280;
  margin-bottom: 12px;
}

.dot {
  margin: 0 6px;
}

.news-snippet {
  margin: 0;
  font-size: 13px;
  color: #4b5563;
  line-height: 1.5;
  display: -webkit-box;
  -webkit-line-clamp: 2;
  line-clamp: 2;
  -webkit-box-orient: vertical;
  overflow: hidden;
}
</style>
