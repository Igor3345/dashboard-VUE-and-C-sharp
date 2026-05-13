<template>
  <div class="card border-0 shadow-sm h-100 stat-card">
    <div
      class="card-body p-3 d-flex align-items-center justify-content-between"
    >
      <div class="flex-grow-1">
        <div class="d-flex align-items-center mb-2">
          <div class="icon-box me-2">
            <svg
              viewBox="0 0 24 24"
              fill="none"
              stroke="white"
              stroke-width="2.5"
              stroke-linecap="round"
              stroke-linejoin="round"
            >
              <polyline points="22 12 18 12 15 21 9 3 6 12 2 12"></polyline>
            </svg>
          </div>
          <span class="small fw-semibold">Общий прогресс</span>
        </div>

        <div
          class="d-flex align-items-baseline flex-wrap justify-content-center"
        >
          <h2 class="fw-bold mb-0 me-2 fs-3">{{ percent }}%</h2>
        </div>
      </div>

      <div class="ms-3 d-flex align-items-center flex-shrink-0">
        <CircularProgress
          :percent="percent"
          size="72"
          color="#2563eb"
          text-color="#5499FB"
        />
      </div>
    </div>
  </div>
</template>

<script setup>
import CircularProgress from "./CircularProgress.vue";

defineProps({
  percent: { type: Number, default: 0 },
});
</script>

<style scoped>
.stat-card {
  transition:
    transform 0.2s ease,
    box-shadow 0.2s ease;
}
.stat-card:hover {
  transform: translateY(-2px);
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.08) !important;
}
.icon-box {
  width: 44px;
  height: 44px;
  background-color: #2563eb;
  border-radius: 12px;
  display: flex;
  align-items: center;
  justify-content: center;
  flex-shrink: 0;
}
.icon-box svg {
  width: 22px;
  height: 22px;
}
</style>
