<template>
  <div class="card shadow-sm h-100 border-0">
    <div
      class="card-body text-center d-flex flex-column justify-content-center"
    >
      <div class="d-flex align-items-center justify-content-center gap-2">
        <div v-if="icon" class="fs-2 mb-2">{{ icon }}</div>

        <div>
          <h6 class="mb-2">{{ title }}</h6>
          <p class="stats_card_nums fw-bold mb-0">
            {{ value }}
            <span v-if="unit" class="fs-6 text-muted">{{ unit }}</span>
          </p>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
defineProps({
  title: String,
  value: [Number, String],
  unit: String,
  icon: String,
  value: { type: [Number, String], required: true },
  unit: { type: String, default: "" },
});
</script>
