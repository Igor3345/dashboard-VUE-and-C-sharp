<template>
  <div class="card shadow-sm h-100">
    <div class="card-header bg-white fw-bold task_list_header">
      <span>Мои задачи</span>

      <div
        class="btn-group btn-group-sm task_list_header__buttons"
        role="group"
      >
        <button
          type="button"
          class="btn btn-outline-primary rounded-pill px-3"
          :class="{ active: currentStatus === 'All' }"
          @click="setFilter('All')"
        >
          Все
        </button>
        <button
          type="button"
          class="btn btn-outline-primary rounded-pill px-3"
          :class="{ active: currentStatus === 'InProgress' }"
          @click="setFilter('InProgress')"
        >
          В работе
        </button>
        <button
          type="button"
          class="btn btn-outline-primary rounded-pill px-3"
          :class="{ active: currentStatus === 'Review' }"
          @click="setFilter('Review')"
        >
          На проверке
        </button>
        <button
          type="button"
          class="btn btn-outline-primary rounded-pill px-3"
          :class="{ active: currentStatus === 'Done' }"
          @click="setFilter('Done')"
        >
          Завершенные
        </button>
      </div>
    </div>

    <div class="card-body p-0" style="max-height: 400px; overflow-y: auto">
      <div v-if="loading" class="text-center py-4 text-muted">Загрузка...</div>

      <ul
        v-else-if="sortedTasks.length === 0"
        class="list-group list-group-flush text-center py-4 text-muted"
      >
        Нет задач
      </ul>

      <ul v-else class="list-group list-group-flush">
        <li
          v-for="task in sortedTasks"
          :key="task.id"
          class="list-group-item py-3 px-4 border-0 d-flex align-items-center justify-content-between"
        >
          <div class="d-flex align-items-center gap-3 flex-grow-1 min-w-0">
            <div class="form-check m-0">
              <input
                class="form-check-input"
                type="checkbox"
                :id="'task-' + task.id"
                :checked="task.status === 'Done'"
              />
            </div>
            <label
              class="form-check-label fw-semibold text-dark mb-0 text-truncate"
              :for="'task-' + task.id"
            >
              {{ task.title }}
            </label>
          </div>

          <div class="d-flex align-items-center gap-3 flex-shrink-0 ms-3">
            <span
              class="badge rounded-pill px-3 py-2 fw-medium"
              :class="getPriorityClass(task.priority)"
            >
              {{ task.priority }}
            </span>
            <span
              class="text-muted small fw-medium"
              style="min-width: 75px; text-align: right"
            >
              {{ formatDate(task.dueDate) }}
            </span>
          </div>
        </li>
      </ul>
    </div>
  </div>
</template>

<script setup>
import { ref, computed, onMounted } from "vue";

const props = defineProps({
  token: { type: String, required: true },
});

const tasks = ref([]);
const loading = ref(false);
const currentStatus = ref("All");

const priorityOrder = { High: 1, Medium: 2, Low: 3 };

const sortedTasks = computed(() => {
  return [...tasks.value].sort((a, b) => {
    return (
      (priorityOrder[a.priority] || 99) - (priorityOrder[b.priority] || 99)
    );
  });
});

const setFilter = (status) => {
  currentStatus.value = status;
  fetchData();
};

const fetchData = async () => {
  loading.value = true;
  try {
    let url = "/api/dashboard/tasks";
    if (currentStatus.value !== "All") {
      url += `?status=${currentStatus.value}`;
    }
    const res = await fetch(url, {
      headers: { Authorization: `Bearer ${props.token}` },
    });
    if (res.ok) tasks.value = await res.json();
  } catch (e) {
    console.error("Ошибка загрузки задач", e);
  } finally {
    loading.value = false;
  }
};

const getPriorityClass = (p) => {
  switch (p) {
    case "High":
      return "priority-high";
    case "Medium":
      return "priority-medium";
    case "Low":
      return "priority-low";
    default:
      return "bg-light text-dark";
  }
};

const formatDate = (d) => {
  if (!d) return "";
  return new Date(d).toLocaleDateString("ru-RU", {
    day: "numeric",
    month: "long",
  });
};

onMounted(fetchData);
</script>

<style scoped>
.priority-high {
  background-color: #fee2e2;
  color: #dc2626;
}
.priority-medium {
  background-color: #fef3c7;
  color: #d97706;
}
.priority-low {
  background-color: #d1fae5;
  color: #059669;
}

.min-w-0 {
  min-width: 0;
}
.text-truncate {
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}

.form-check-input {
  border-radius: 50%;
  width: 18px;
  height: 18px;
  cursor: pointer;
}
</style>
