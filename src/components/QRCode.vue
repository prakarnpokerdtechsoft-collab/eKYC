<script setup>
import { defineProps } from 'vue'
import QrcodeVue from 'qrcode.vue'

// กำหนด Props สำหรับรับค่าจาก Parent
const props = defineProps({
    value: {
        type: String,
        required: true,
        default: 'https://google.com'
    },
    size: {
        type: Number,
        default: 200
    },
    level: {
        type: String,
        default: 'H' // L, M, Q, H
    },
    showDownload: {
        type: Boolean,
        default: true
    },
    filename: {
        type: String,
        default: 'qrcode'
    }
})

// ฟังก์ชันดาวน์โหลด (Logic อยู่ภายใน Component เลย)
const downloadQR = () => {
    const canvas = document.querySelector('.qr-container canvas')
    if (canvas) {
        const url = canvas.toDataURL('image/png')
        const link = document.createElement('a')
        link.download = `${props.filename}-${Date.now()}.png`
        link.href = url
        link.click()
    }
}
</script>

<template>
    <div class="qr-wrapper">
        <div class="qr-container">
            <qrcode-vue :value="props.value" :size="props.size" :level="props.level" render-as="canvas" />
        </div>

        <div v-if="props.showDownload" class="actions">
            <button @click="downloadQR" class="btn-dl">
                Download PNG
            </button>
        </div>
    </div>
</template>

<style scoped>
.qr-wrapper {
    display: flex;
    flex-direction: column;
    align-items: center;
    gap: 1rem;
    padding: 1rem;
}

.btn-dl {
    background-color: #42b883;
    color: white;
    border: none;
    padding: 8px 16px;
    border-radius: 4px;
    cursor: pointer;
    font-weight: bold;
}

.btn-dl:hover {
    background-color: #35495e;
}
</style>