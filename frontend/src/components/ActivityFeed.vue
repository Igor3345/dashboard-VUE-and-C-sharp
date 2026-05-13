<template>
  <div class="card shadow-sm h-100">
    <div
      class="card-header bg-white fw-bold d-flex justify-content-between align-items-center"
    >
      <span class="block-header">Активность команды</span>
      <span class="badge bg-light text-primary rounded-pill px-3">
        {{ activity.length }}
      </span>
    </div>

    <ul
      class="list-group list-group-flush"
      style="max-height: 400px; overflow-y: auto"
    >
      <li
        v-for="log in activity"
        :key="log.id"
        class="list-group-item py-3 px-4 border-0"
      >
        <div class="d-flex justify-content-between align-items-start">
          <div class="flex-grow-1 me-3">
            <div class="d-flex align-items-baseline">
              <span class="fw-semibold text-dark">{{ log.memberName }}</span>
              <span class="text-muted small ms-1">{{ log.description }}</span>
            </div>
            <div class="text-muted small mt-1">
              {{ log.projectName }}
            </div>
          </div>

          <div class="d-flex flex-column align-items-end gap-1">
            <small class="text-muted">{{ formatDate(log.createdAt) }}</small>

            <div class="status-icon" :class="getIconClass(log.actionType)">
              <span v-html="getIconSvg(log.actionType)"></span>
            </div>
          </div>
        </div>
      </li>
    </ul>
  </div>
</template>

<script setup>
import { ref, onMounted } from "vue";

const props = defineProps({
  token: { type: String, required: true },
});

const activity = ref([]);

const fetchData = async () => {
  try {
    const res = await fetch("/api/dashboard/activity", {
      headers: { Authorization: `Bearer ${props.token}` },
    });
    if (res.ok) activity.value = await res.json();
  } catch (e) {
    console.error("Ошибка загрузки активности", e);
  }
};

const formatDate = (dateString) => {
  const date = new Date(dateString);
  const now = new Date();
  const diffMs = now - date;
  const diffHours = Math.floor(diffMs / (1000 * 60 * 60));

  if (diffHours < 24) {
    return `${diffHours} час${diffHours === 1 ? "" : diffHours < 5 ? "а" : "ов"} назад`;
  }
  return date.toLocaleDateString("ru-RU", { day: "numeric", month: "long" });
};

const getIconClass = (type) => {
  const map = {
    completed: "bg-success",
    updated: "bg-info",
    added: "bg-primary",
    created: "bg-warning",
  };
  return map[type] || "bg-secondary";
};

const getIconSvg = (type) => {
  const map = {
    completed: `<div class="d-flex"><svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-check-lg" viewBox="0 0 16 16">
  <path d="M12.736 3.97a.733.733 0 0 1 1.047 0c.286.289.29.756.01 1.05L7.88 12.01a.733.733 0 0 1-1.065.02L3.217 8.384a.757.757 0 0 1 0-1.06.733.733 0 0 1 1.047 0l3.052 3.093 5.4-6.425z"/>
</svg></div>`,
    updated: `<div class="d-flex"><svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-pencil" viewBox="0 0 16 16">
  <path d="M12.146.146a.5.5 0 0 1 .708 0l3 3a.5.5 0 0 1 0 .708l-10 10a.5.5 0 0 1-.168.11l-5 2a.5.5 0 0 1-.65-.65l2-5a.5.5 0 0 1 .11-.168zM11.207 2.5 13.5 4.793 14.793 3.5 12.5 1.207zm1.586 3L10.5 3.207 4 9.707V10h.5a.5.5 0 0 1 .5.5v.5h.5a.5.5 0 0 1 .5.5v.5h.293zm-9.761 5.175-.106.106-1.528 3.821 3.821-1.528.106-.106A.5.5 0 0 1 5 12.5V12h-.5a.5.5 0 0 1-.5-.5V11h-.5a.5.5 0 0 1-.468-.325"/>
</svg> </div>`,
    added: `<div class="d-flex"><svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-file-earmark" viewBox="0 0 16 16">
  <path d="M14 4.5V14a2 2 0 0 1-2 2H4a2 2 0 0 1-2-2V2a2 2 0 0 1 2-2h5.5zm-3 0A1.5 1.5 0 0 1 9.5 3V1H4a1 1 0 0 0-1 1v12a1 1 0 0 0 1 1h8a1 1 0 0 0 1-1V4.5z"/>
</svg></div>`,
    created: `<div class="d-flex"><svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-plus-circle" viewBox="0 0 16 16">
  <path d="M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14m0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16"/>
  <path d="M8 4a.5.5 0 0 1 .5.5v3h3a.5.5 0 0 1 0 1h-3v3a.5.5 0 0 1-1 0v-3h-3a.5.5 0 0 1 0-1h3v-3A.5.5 0 0 1 8 4"/>
</svg></div>`,
  };
  return map[type] || "";
};

onMounted(fetchData);
</script>

<style scoped>
.status-icon {
  width: 36px;
  height: 36px;
  border-radius: 50%;
  display: flex;
  align-items: center;
  justify-content: center;
  opacity: 0.8;
}
.status-icon svg {
  width: 14px;
  height: 14px;
}

.list-group-item {
  transition: background-color 0.2s;
}
.list-group-item:hover {
  background-color: #f8fafc;
}
</style>
