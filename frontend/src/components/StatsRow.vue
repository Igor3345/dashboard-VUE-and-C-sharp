<template>
  <div class="row g-3 mb-4">
    <div class="col-6 col-lg-3">
      <ProgressCard :percent="stats?.overallProgress || 0" />
    </div>

    <div
      class="col-6 col-lg-3"
      v-for="(card, index) in otherCards"
      :key="index"
    >
      <StatsCard
        :title="card.title"
        :value="card.value"
        :unit="card.unit"
        :icon="card.icon"
      />
    </div>
  </div>
</template>

<script setup>
import { ref, computed, onMounted } from "vue";
import CircularProgress from "./CircularProgress.vue";
import StatsCard from "./StatsCard.vue";
import ProgressCard from "./ProgressCard.vue";

const props = defineProps({ token: { type: String, required: true } });
const stats = ref(null);

const fetchData = async () => {
  try {
    const res = await fetch("/api/dashboard/stats", {
      headers: { Authorization: `Bearer ${props.token}` },
    });
    if (res.ok) stats.value = await res.json();
  } catch (e) {
    console.error(e);
  }
};

const otherCards = computed(() => [
  {
    title: "Задач выполнено",
    value: stats.value?.tasksDone || "...",
    icon: "✅",
  },
  {
    title: "В работе",
    value: stats.value?.tasksInProgress || "...",
    icon: "⏳",
  },
  {
    title: "Команда",
    value: stats.value?.teamActiveCount || "...",
    icon: "👥",
  },
]);

onMounted(fetchData);
</script>
