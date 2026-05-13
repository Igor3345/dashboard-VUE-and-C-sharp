<template>
  <div class="card shadow-sm h-100">
    <div
      class="card-header bg-white fw-bold d-flex justify-content-between align-items-center"
    >
      <span class="block-header">Эффективность</span>
      <select class="form-select form-select-sm w-auto">
        <option>За неделю</option>
        <option>За месяц</option>
      </select>
    </div>
    <div class="card-body">
      <div class="d-flex align-items-center">
        <div class="me-4 text-center">
          <CircularProgress
            :percent="stats?.overallProgress || 0"
            size="220"
            color="#0d6efd"
          >
            <span class="fs-3">{{ stats?.overallProgress }}%</span>
            <small class="text-muted d-block">Общий индекс</small>
          </CircularProgress>
        </div>

        <div class="flex-grow-1">
          <div class="mb-3">
            <div class="d-flex flex-column">
              <span class="fw-bold efficiencyNums">{{
                stats?.efficiencyIndex
              }}</span>
              <span class="text-muted small">Индекс эффективности</span>
            </div>
          </div>
          <div class="mb-3">
            <div class="d-flex flex-column">
              <span class="fw-bold efficiencyNums">{{ stats?.tasksDone }}</span>
              <span class="text-muted small">Задач выполнено</span>
            </div>
          </div>
          <div>
            <div class="d-flex flex-column">
              <span class="fw-bold efficiencyNums">{{
                formatTime(stats?.timeSavedMinutes)
              }}</span>
              <span class="text-muted small">Сэкономлено времени</span>
            </div>
          </div>
        </div>
      </div>

      <button class="btn btn-primary w-100 mt-4">Подробнее</button>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from "vue";
import CircularProgress from "./CircularProgress.vue";

const props = defineProps({
  token: { type: String, required: true },
});

const stats = ref(null);

const fetchData = async () => {
  try {
    const res = await fetch("/api/dashboard/stats", {
      headers: { Authorization: `Bearer ${props.token}` },
    });
    if (res.ok) stats.value = await res.json();
  } catch (e) {
    console.error("Ошибка загрузки эффективности", e);
  }
};

const formatTime = (minutes) => {
  if (!minutes) return "0ч 0м";
  const h = Math.floor(minutes / 60);
  const m = minutes % 60;
  return `${h}ч ${m}м`;
};

onMounted(fetchData);
</script>
