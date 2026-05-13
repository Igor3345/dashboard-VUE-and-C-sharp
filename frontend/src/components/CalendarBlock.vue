<template>
  <div class="card shadow-sm h-100">
    <div
      class="card-header bg-white fw-bold d-flex justify-content-between align-items-center"
    >
      <div class="d-flex align-items-center gap-2">
        <button
          @click="changeMonth(-1)"
          class="btn btn-sm btn-link text-dark p-0"
        >
          &lt;
        </button>
        <span class="fw-semibold">{{ monthName }} {{ currentYear }}</span>
        <button
          @click="changeMonth(1)"
          class="btn btn-sm btn-link text-dark p-0"
        >
          &gt;
        </button>
      </div>
      <button
        @click="goToToday"
        class="btn btn-sm btn-outline-secondary"
        style="font-size: 12px"
      >
        Сегодня
      </button>
    </div>

    <div class="card-body p-3">
      <div class="calendar-grid mb-2">
        <div
          v-for="d in ['Пн', 'Вт', 'Ср', 'Чт', 'Пт', 'Сб', 'Вс']"
          :key="d"
          class="text-center text-muted small fw-bold"
        >
          {{ d }}
        </div>
      </div>

      <div class="calendar-grid">
        <div
          v-for="(day, index) in daysInMonth"
          :key="index"
          class="calendar-day text-center small py-2 rounded-circle"
          :class="{
            'text-muted': !day.isCurrentMonth,
            'bg-primary text-white fw-bold': isToday(day.date),
            'hover-bg': day.isCurrentMonth && !isToday(day.date),
          }"
          @click="day.isCurrentMonth && selectDate(day.date)"
        >
          {{ day.day }}
        </div>
      </div>

      <div class="mt-3 pt-3 border-top">
        <ul class="list-group list-group-flush">
          <li
            v-for="evt in events"
            :key="evt.id"
            class="list-group-item py-2 small border-0 px-0"
          >
            <div class="d-flex align-items-center">
              <span
                class="badge rounded-pill me-2"
                :style="{ backgroundColor: evt.color || '#0d6efd' }"
                >&nbsp;</span
              >
              <div class="flex-grow-1">
                <div class="fw-bold">{{ evt.title }}</div>
                <div class="text-muted" style="font-size: 12px">
                  {{ evt.startTime }} - {{ evt.endTime }}
                </div>
              </div>
            </div>
          </li>
          <li
            v-if="events.length === 0"
            class="list-group-item text-center text-muted py-3 border-0"
          >
            Нет событий
          </li>
        </ul>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed, onMounted } from "vue";

const props = defineProps({
  token: { type: String, required: true },
});

const currentDate = ref(new Date());
const events = ref([]);

const currentYear = computed(() => currentDate.value.getFullYear());
const currentMonth = computed(() => currentDate.value.getMonth());
const monthName = computed(() =>
  currentDate.value.toLocaleDateString("ru-RU", {
    month: "long",
    capitalize: true,
  }),
);

const daysInMonth = computed(() => {
  const year = currentYear.value;
  const month = currentMonth.value;

  const firstDay = new Date(year, month, 1);
  const lastDay = new Date(year, month + 1, 0);

  const days = [];

  let startDay = firstDay.getDay();
  if (startDay === 0) startDay = 7;

  for (let i = 1; i < startDay; i++) {
    const d = new Date(year, month, 1 - i);
    days.unshift({ day: d.getDate(), date: d, isCurrentMonth: false });
  }

  for (let i = 1; i <= lastDay.getDate(); i++) {
    const d = new Date(year, month, i);
    days.push({ day: i, date: d, isCurrentMonth: true });
  }

  while (days.length < 42) {
    const nextDate = new Date(
      year,
      month + 1,
      days.length - lastDay.getDate() + 1,
    );
    days.push({
      day: nextDate.getDate(),
      date: nextDate,
      isCurrentMonth: false,
    });
  }

  return days;
});

const isToday = (date) => {
  const today = new Date();
  return (
    date.getDate() === today.getDate() &&
    date.getMonth() === today.getMonth() &&
    date.getFullYear() === today.getFullYear()
  );
};

const changeMonth = (delta) => {
  currentDate.value = new Date(
    currentDate.value.getFullYear(),
    currentDate.value.getMonth() + delta,
    1,
  );
  fetchEvents();
};

const goToToday = () => {
  currentDate.value = new Date();
  fetchEvents();
};

const selectDate = (date) => {
  console.log("Выбрана дата:", date);
};

const fetchEvents = async () => {
  try {
    const y = currentDate.value.getFullYear();
    const m = currentDate.value.getMonth() + 1;
    const res = await fetch(`/api/dashboard/calendar?year=${y}&month=${m}`, {
      headers: { Authorization: `Bearer ${props.token}` },
    });
    if (res.ok) events.value = await res.json();
  } catch (e) {
    console.error("Ошибка календаря", e);
  }
};

onMounted(fetchEvents);
</script>

<style scoped>
.calendar-grid {
  display: grid;
  grid-template-columns: repeat(7, 1fr);
  gap: 4px;
}

.calendar-day {
  cursor: pointer;
  transition: all 0.2s;
  aspect-ratio: 1;
  display: flex;
  align-items: center;
  justify-content: center;
}

.calendar-day:hover:not(.bg-primary) {
  background-color: #f1f5f9;
}

.list-group {
  max-height: 150px;
  overflow-y: auto;
}
</style>
