<template>
  <div
    v-if="authStatus === 'checking'"
    class="d-flex justify-content-center align-items-center vh-100 bg-light"
  >
    <div class="text-center">
      <div
        class="spinner-border text-primary mb-3"
        style="width: 48px; height: 48px"
      ></div>
      <p class="text-muted">Проверка авторизации...</p>
    </div>
  </div>

  <Login v-else-if="authStatus === 'unauthenticated'" @login="handleLogin" />

  <Dashboard
    v-else
    :key="dashboardKey"
    :token="token"
    :user-name="userName"
    @logout="handleLogout"
  />
</template>

<script setup>
import { ref, onMounted } from "vue";
import Login from "./components/Login.vue";
import Dashboard from "./components/Dashboard.vue";

const authStatus = ref("checking");
const token = ref(localStorage.getItem("token"));
const userName = ref(localStorage.getItem("userName") || "Пользователь");
const dashboardKey = ref(0);

const clearAuth = () => {
  token.value = null;
  userName.value = "Пользователь";
  localStorage.removeItem("token");
  localStorage.removeItem("userName");
  dashboardKey.value++;
  authStatus.value = "unauthenticated";
};

const verifyAuth = async () => {
  if (!token.value) {
    authStatus.value = "unauthenticated";
    return;
  }

  try {
    const res = await fetch("/api/dashboard/stats", {
      headers: { Authorization: `Bearer ${token.value}` },
    });

    if (res.ok) {
      authStatus.value = "authenticated";
    } else {
      clearAuth();
    }
  } catch (error) {
    console.warn("API недоступен при проверке токена:", error);
    clearAuth();
  }
};

const handleLogin = (payload) => {
  const newToken = typeof payload === "object" ? payload.token : payload;
  const newName =
    typeof payload === "object" ? payload.fullName || "Иван" : "Иван";

  token.value = newToken;
  userName.value = newName;
  localStorage.setItem("token", newToken);
  localStorage.setItem("userName", newName);

  dashboardKey.value++;
  authStatus.value = "authenticated";
};

const handleLogout = () => {
  clearAuth();
};

onMounted(verifyAuth);
</script>

<style>
body {
  background-color: #f8f9fa;
}
</style>
