<template>
    <Teleport to="body">
        <Transition name="fade">
            <div v-if="isVisible" class="loading-overlay">
                <div class="loading-content">
                    <div class="spinner-wrapper">
                        <svg class="spinner" viewBox="0 0 50 50">
                            <circle class="path" cx="25" cy="25" r="20" fill="none" stroke-width="5"></circle>
                        </svg>
                    </div>

                    <p v-if="message" class="loading-text">{{ message }}</p>
                </div>
            </div>
        </Transition>
    </Teleport>
</template>

<script setup>
import { ref } from 'vue'

const isVisible = ref(false)
const message = ref('กำลังโหลด...')

const show = (msg = 'กำลังโหลด...') => {
    message.value = msg
    isVisible.value = true
}

const hide = () => {
    isVisible.value = false
}

// Expose ให้เรียกใช้ผ่าน ref
defineExpose({ show, hide })
</script>

<style scoped>
.loading-overlay {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background: rgba(255, 255, 255, 0.7);
    /* พื้นหลังขาวใส */
    backdrop-filter: blur(4px);
    display: flex;
    justify-content: center;
    align-items: center;
    z-index: 10000;
    /* ต้องสูงกว่า AppDialog */
}

.loading-content {
    text-align: center;
    display: flex;
    flex-direction: column;
    align-items: center;
    gap: 1rem;
}

/* Spinner Animation */
.spinner {
    animation: rotate 2s linear infinite;
    width: 50px;
    height: 50px;
}

.spinner .path {
    stroke: #4f46e5;
    /* สี Indigo */
    stroke-linecap: round;
    animation: dash 1.5s ease-in-out infinite;
}

.loading-text {
    color: #4b5563;
    font-weight: 500;
    font-size: 1rem;
    margin: 0;
}

@keyframes rotate {
    100% {
        transform: rotate(360deg);
    }
}

@keyframes dash {
    0% {
        stroke-dasharray: 1, 150;
        stroke-dashoffset: 0;
    }

    50% {
        stroke-dasharray: 90, 150;
        stroke-dashoffset: -35;
    }

    100% {
        stroke-dasharray: 90, 150;
        stroke-dashoffset: -124;
    }
}

/* Fade Transition */
.fade-enter-active,
.fade-leave-active {
    transition: opacity 0.3s ease;
}

.fade-enter-from,
.fade-leave-to {
    opacity: 0;
}
</style>