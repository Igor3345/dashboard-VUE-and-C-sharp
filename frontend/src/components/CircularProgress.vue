<template>
  <div
    class="circular-progress"
    :style="{ width: size + 'px', height: size + 'px' }"
  >
    <svg viewBox="0 0 100 100" class="progress-ring">
      <circle
        class="progress-ring__circle-bg"
        stroke="currentColor"
        stroke-width="8"
        fill="transparent"
        r="42"
        cx="50"
        cy="50"
      />
      <circle
        class="progress-ring__circle"
        :stroke="color"
        stroke-width="8"
        fill="transparent"
        r="42"
        cx="50"
        cy="50"
        :stroke-dasharray="circumference"
        :stroke-dashoffset="offset"
        stroke-linecap="round"
        transform="rotate(-90 50 50)"
      />
    </svg>
    <div class="progress-text">
      <slot
        ><div :style="{ color: props.textColor }">
          {{ Math.round(percent) }}%
        </div></slot
      >
    </div>
  </div>
</template>

<script setup>
import { computed } from "vue";

const props = defineProps({
  percent: { type: Number, default: 0 },
  size: { type: Number, default: 120 },
  color: { type: String, default: "#0d6efd" },
  textColor: { type: String, default: "#000" },
});

const radius = 42;
const circumference = 2 * Math.PI * radius;

const offset = computed(() => {
  return circumference - (props.percent / 100) * circumference;
});
</script>

<style scoped>
.circular-progress {
  position: relative;
  display: flex;
  align-items: center;
  justify-content: center;
}

.progress-ring {
  width: 100%;
  height: 100%;
}
.progress-ring__circle-bg {
  color: #e9ecef;
}
.progress-text {
  position: absolute;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  font-weight: bold;
  color: #333;
}
</style>
