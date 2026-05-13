<template>
  <div class="container mt-5">
    <div class="row justify-content-center">
      <div class="col-md-4">
        <div class="card shadow">
          <div class="card-body p-5">
            <h2 class="text-center mb-4">Вход в систему</h2>

            <form @submit.prevent="handleLogin">
              <div class="mb-3">
                <label class="form-label">Логин</label>>
                <input
                  v-model="username"
                  type="text"
                  class="form-control"
                  required
                />
              </div>

              <div class="mb-3">
                <label class="form-label">Пароль</label>
                <input
                  v-model="password"
                  type="password"
                  class="form-control"
                  required
                />
              </div>

              <div class="d-grid">
                <button
                  type="submit"
                  class="btn btn-primary"
                  :disabled="loading"
                >
                  {{ loading ? "Вход..." : "Войти" }}
                </button>
              </div>

              <div v-if="error" class="alert alert-danger mt-3 mb-0">
                {{ error }}
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from "vue";

const emit = defineEmits(["login"]);

const username = ref("ivan");
const password = ref("password123");
const error = ref(null);
const loading = ref(false);

const handleLogin = async () => {
  error.value = null;
  loading.value = true;

  try {
    const response = await fetch("/api/auth/login", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        username: username.value,
        password: password.value,
      }),
    });

    if (response.ok) {
      const data = await response.json();
      emit("login", { token: data.token, fullName: data.fullName });
    } else {
      error.value = "Неверный логин или пароль";
    }
  } catch (err) {
    error.value = "Ошибка подключения к серверу";
    console.error(err);
  } finally {
    loading.value = false;
  }
};
</script>
