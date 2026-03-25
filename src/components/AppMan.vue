<script setup>
import { ref, onMounted, onUnmounted } from 'vue'
import { checkStatus } from '../services/api'
import QRCode from './QRCode.vue'

const props = defineProps({
    id: String
})
const isQR = ref(true);
const status = ref('waiting');

// let pollTimer = null;

const check = async () => {
    try {
        const response = await checkStatus(props.id);
        console.log("🚀 ~ check ~ response:", response.data.status)
        // const response = {
        //     data: {
        //         isSuccess: true
        //     }
        // }
        if (response.data.status === 'verified') {
            status.value = 'success';
            nextStep();
            // stopPolling(); // 🛑 หยุดถามเมื่อสำเร็จแล้ว
        }
    } catch (error) {
        console.error("เช็คสถานะไม่สำเร็จ", error);
    }
};

// const startPolling = () => {
//     status.value = 'loading';
//     pollTimer = setInterval(check, 30000);
// };

// const stopPolling = () => {
//     if (pollTimer) {
//         clearInterval(pollTimer);
//         pollTimer = null;
//     }
// };

// onMounted(() => {
//     startPolling();
// });

// onUnmounted(() => {
//     stopPolling();
// });

const checkIfFinished = (event) => {
    // ทุกครั้งที่ iframe เปลี่ยนหน้า หรือโหลดใหม่ ฟังก์ชันนี้จะทำงาน
    console.log("iframe มีการโหลดหน้าใหม่ หรือขยับสถานะแล้ว");
    // คุณอาจจะใช้ Logic บางอย่างเช็คที่นี่ (ถ้า Domain เดียวกันจะเช็ค URL ได้)

    // 🚩 ตรวจสอบ Origin ของ Third-party เพื่อความปลอดภัย
    // if (event.origin !== 'https://third-party-app.com') return

    console.log("ได้รับข้อมูล:", event.data)

    // สมมติว่า Third-party ส่ง { type: 'VALIDATED' } มาเมื่อทำรายการเสร็จ
    if (event.data.type === 'VALIDATED' || event.data.status === 'success') {
        status.value = 'success'
    }
};

const nextStep = () => {
    if (status.value === 'success') {
        alert("กำลังไปหน้าถัดไป...")
        // router.push('/next-step') // ใช้ Vue Router เพื่อเปลี่ยนหน้า
        emit('next')
    }
}
</script>

<template>
    <div class="appman-container">
        <header class="appman-header">
            <button class="back-btn" @click="$emit('back')">
                <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <path d="M15 18L9 12L15 6" stroke="white" stroke-width="2" stroke-linecap="round"
                        stroke-linejoin="round" />
                </svg>
            </button>
            <h1 class="header-title">Apply for Auto Loan</h1>
            <div class="empty-space"></div>
        </header>

        <div v-if="isQR" class="qr-container">
            <QRCode :value="`https://techsoft-ekyc-demo.mac.appmanteam.com/apps/identity-verification/${id}?lang=th`"
                :size="250" filename="vue-website-qr" />
            <span>ยืนยันตัวตนผ่าน QR Code ด้วยโทรศัพท์มือถือ<br> หรือยืนยันตัวตนผ่านระบบนี้ทันที <a
                    @click="isQR = false">คลิกที่นี่</a>
                <br>แล้วกดปุ่มดำเนินการต่อ</span>
        </div>

        <div v-else class="iframe-wrapper">
            <iframe :src="`https://techsoft-ekyc-demo.mac.appmanteam.com/apps/identity-verification/${id}?lang=th`"
                class="full-screen-iframe" allow="camera; microphone; display-capture" frameborder="0"
                @load="checkIfFinished"></iframe>
        </div>

        <footer class="appman-footer">
            <button :class="['next-btn', status]" @click="check()">
                <span>ดำเนินการต่อ</span>
            </button>
            <!-- <button :class="['next-btn', status]" :disabled="status === 'loading'"
                @click="status === 'waiting' ? check() : nextStep()">
                <span v-if="status === 'waiting'">ตรวจสอบสถานะ</span>
                <span v-if="status === 'loading'" class="loader"></span>
                <span v-if="status === 'loading'">กรุณาดำเนินการในระบบ...</span>
                <span v-else-if="status === 'success'">ดำเนินการต่อ</span>
            </button> -->
        </footer>
    </div>
</template>

<style scoped>
.appman-container {
    display: flex;
    flex-direction: column;
    max-width: 480px;
    margin: 0 auto;
    min-height: 100vh;
    background-color: #fafafa;
    width: 100vw;
}

.appman-header {
    display: flex;
    align-items: center;
    justify-content: space-between;
    background-color: #f97316;
    /* Primary orange */
    padding: 16px;
    color: white;
}

.appman-footer {
    display: flex;
    align-items: center;
    justify-content: center;
    background-color: #f97316;
    padding: 14px;
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
    /* match the back button width to keep title centered */
}

.next-btn {
    display: flex;
    align-items: center;
    gap: 10px;
    padding: 12px 40px;
    border-radius: .5rem;
    border: none;
    font-size: 16px;
    font-weight: bold;
    cursor: pointer;
    transition: all 0.3s ease;
}

.next-btn.loading {
    background-color: #e0e0e0;
    color: #888;
    cursor: not-allowed;
}

.next-btn.success {
    background-color: #4CAF50;
    color: white;
    box-shadow: 0 4px 15px rgba(76, 175, 80, 0.3);
}

a {
    color: #f97316;
    cursor: pointer;
    text-decoration: underline;
}

.loader {
    width: 18px;
    height: 18px;
    border: 3px solid #ccc;
    border-bottom-color: transparent;
    border-radius: 50%;
    display: inline-block;
    animation: rotation 1s linear infinite;
}

@keyframes rotation {
    0% {
        transform: rotate(0deg);
    }

    100% {
        transform: rotate(360deg);
    }
}

.qr-container {
    display: flex;
    justify-content: center;
    align-items: center;
    flex-direction: column;
    margin: 0 auto;
    flex: 1;
}

.qr-container span {
    margin: 1.5rem;
    font-size: 16px;
    font-weight: 500;
    color: #666;
    text-align: center;
}

.iframe-wrapper {
    flex: 1;
    overflow: hidden;
}

.full-screen-iframe {
    width: 100%;
    height: 100%;
    border: none;
}
</style>