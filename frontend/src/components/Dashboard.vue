<template>
  <div class="d-flex flex-column min-vh-100 bg-light">
    <nav class="navbar navbar-dark bg-dark shadow-sm">
      <div class="container-fluid">
        <span class="navbar-brand mb-0 h1"> ProAnalytics</span>
        <button
          @click="handleLogout"
          class="btn btn-outline-light btn-sm header_exit_button"
        >
          Выйти
        </button>
      </div>
    </nav>
    <div class="container py-4">
      <div class="mb-4">
        <h1 class="h3 mb-1">Добро пожаловать, {{ userName }}! 👋</h1>
        <p class="text-muted mb-0">
          Вот что происходит с вашими проектами сегодня.
        </p>
      </div>
      <StatsRow :token="token" />
      <div class="row g-4 mb-4">
        <div class="col-lg-7">
          <EfficiencyBlock :token="token" />
        </div>
        <div class="col-lg-5">
          <ActivityFeed :token="token" />
        </div>
      </div>
      <div class="row g-4">
        <div class="col-lg-7">
          <TaskList :token="token" />
        </div>
        <div class="col-lg-5">
          <CalendarBlock :token="token" />
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import StatsRow from "./StatsRow.vue";
import EfficiencyBlock from "./EfficiencyBlock.vue";
import ActivityFeed from "./ActivityFeed.vue";
import TaskList from "./TaskList.vue";
import CalendarBlock from "./CalendarBlock.vue";

defineProps({
  token: { type: String, required: true },
  userName: { type: String, default: "Пользователь" },
});
const emit = defineEmits(["logout"]);
const handleLogout = () => emit("logout");
</script>
