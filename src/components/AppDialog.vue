<template>
    <Teleport to="body">
        <Transition name="modal-fade">
            <div v-if="isVisible" class="modal-overlay" @click.self="handleOverlayClick">
                <div class="modal-content">
                    <div class="icon-container" :class="type">
                        <svg v-if="type === 'success'" xmlns="http://www.w3.org/2000/svg" fill="none"
                            viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" class="svg-icon">
                            <path stroke-linecap="round" stroke-linejoin="round" d="m4.5 12.75 6 6 9-13.5" />
                        </svg>

                        <svg v-if="type === 'warning' || type === 'danger'" xmlns="http://www.w3.org/2000/svg"
                            fill="none" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" class="svg-icon">
                            <path stroke-linecap="round" stroke-linejoin="round"
                                d="M12 9v3.75m-9.303 3.376c-.866 1.5.217 3.374 1.948 3.374h14.71c1.73 0 2.813-1.874 1.948-3.374L13.949 3.378c-.866-1.5-3.032-1.5-3.898 0L2.697 16.126ZM12 15.75h.007v.008H12v-.008Z" />
                        </svg>
                    </div>

                    <h3>{{ title }}</h3>
                    <p>{{ message }}</p>

                    <div class="modal-actions" :class="{ 'is-alert': isAlert }">
                        <button v-if="!isAlert" @click="cancel" class="btn-cancel">
                            {{ cancelText }}
                        </button>

                        <button @click="confirm" :class="['btn-confirm', `btn-${type}`]">
                            {{ confirmText }}
                        </button>
                    </div>
                </div>
            </div>
        </Transition>
    </Teleport>
</template>

<script setup>
import { ref, onMounted, onUnmounted } from 'vue'

const isVisible = ref(false)
const title = ref('')
const message = ref('')
const type = ref('success')
const confirmText = ref('ตกลon')
const cancelText = ref('ยกเลิก')
const isAlert = ref(false) // ตัวแปรควบคุม Mode
let resolvePromise = null

const show = (options = {}) => {
    title.value = options.title || (options.isAlert ? 'แจ้งเตือน' : 'ยืนยัน')
    message.value = options.message || ''
    type.value = options.type || 'success'
    confirmText.value = options.confirmText || 'ตกลง'
    cancelText.value = options.cancelText || 'ยกเลิก'
    isAlert.value = options.isAlert || false // ถ้าส่ง true มาจะเป็น Alert

    isVisible.value = true
    return new Promise((resolve) => {
        resolvePromise = resolve
    })
}

const confirm = () => { isVisible.value = false; resolvePromise(true); }
const cancel = () => { isVisible.value = false; resolvePromise(false); }
const handleOverlayClick = () => { if (!isAlert.value) cancel(); }

// รองรับการกด Enter เพื่อยืนยัน
const handleKeyDown = (e) => {
    if (isVisible.value && e.key === 'Enter') confirm();
    if (isVisible.value && e.key === 'Escape' && !isAlert.value) cancel();
}

onMounted(() => window.addEventListener('keydown', handleKeyDown))
onUnmounted(() => window.removeEventListener('keydown', handleKeyDown))

defineExpose({ show })
</script>

<style scoped>
/* พื้นฐานเหมือนเดิม แต่เพิ่มความยืดหยุ่นของปุ่ม */
.modal-overlay {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background: rgba(15, 23, 42, 0.6);
    backdrop-filter: blur(8px);
    display: flex;
    justify-content: center;
    align-items: center;
    z-index: 9999;
}

.modal-content {
    background: white;
    padding: 2rem;
    border-radius: 24px;
    width: 90%;
    max-width: 380px;
    text-align: center;
    box-shadow: 0 25px 50px -12px rgba(0, 0, 0, 0.25);
}

.icon-container {
    width: 56px;
    height: 56px;
    border-radius: 18px;
    display: flex;
    align-items: center;
    justify-content: center;
    margin: 0 auto 1.25rem;
}

.svg-icon {
    width: 28px;
    height: 28px;
}

/* Themes */
.success {
    background: #ecfdf5;
    color: #10b981;
}

.warning {
    background: #fffbeb;
    color: #f59e0b;
}

.danger {
    background: #fef2f2;
    color: #ef4444;
}

.modal-actions {
    display: flex;
    gap: 12px;
    margin-top: 1.5rem;
}

.modal-actions.is-alert .btn-confirm {
    flex: 1;
    width: 100%;
}

/* Alert จะเต็มความกว้าง */

.btn-cancel,
.btn-confirm {
    flex: 1;
    padding: 12px 24px;
    border-radius: 14px;
    font-weight: 600;
    cursor: pointer;
    border: none;
    transition: all 0.2s cubic-bezier(0.4, 0, 0.2, 1);
}

.btn-cancel {
    background: #f1f5f9;
    color: #64748b;
}

.btn-success {
    background: #10b981;
    color: white;
}

.btn-warning {
    background: #f59e0b;
    color: white;
}

.btn-danger {
    background: #ef4444;
    color: white;
}

.btn-confirm:hover {
    transform: translateY(-1px);
    filter: brightness(1.05);
}

/* Animation */
.modal-fade-enter-active {
    transition: all 0.3s ease-out;
}

.modal-fade-leave-active {
    transition: all 0.2s ease-in;
}

.modal-fade-enter-from {
    opacity: 0;
    transform: scale(0.95) translateY(10px);
}

.modal-fade-leave-to {
    opacity: 0;
    transform: scale(0.95);
}
</style>