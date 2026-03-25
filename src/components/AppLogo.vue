<script setup>
import logoImage from '../assets/images/messageImage_1773804078930.jpg'
</script>

<template>
  <div class="logo-wrapper">
    <img :src="logoImage" alt="Company Logo" class="logo-image" />
  </div>
</template>

<style scoped>
.logo-wrapper {
  display: flex;
  flex-direction: column;
  align-items: center;
}

.logo-image {
  width: 220px;
  max-height: 220px;
  object-fit: contain;
  margin-bottom: 16px;
}
</style>
