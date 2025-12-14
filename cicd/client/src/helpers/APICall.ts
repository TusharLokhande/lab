import { MsalService } from "@/services/msalService";
import axios from "axios";

let isRefreshing = false;
let failedQueue: any[] = [];

const api = axios.create({
  baseURL: `${import.meta.env.VITE_BACKEND_URL}`,
});

// Attach access token on every request
api.interceptors.request.use(async (config) => {
  const token = await MsalService.getAccessToken();
  if (token) config.headers.Authorization = `Bearer ${token}`;
  return config;
});

// Handle 401 automatically → refresh token flow
api.interceptors.response.use(
  (response) => response,
  async (error) => {
    const originalRequest = error.config;

    // Already retried? bail out
    if (error.response?.status !== 401 || originalRequest._retry) {
      return Promise.reject(error);
    }

    if (isRefreshing) {
      return new Promise((resolve, reject) => {
        failedQueue.push({ resolve, reject });
      })
        .then((token) => {
          originalRequest.headers.Authorization = `Bearer ${token}`;
          return api(originalRequest);
        })
        .catch((err) => {
          return Promise.reject(err);
        });
    }

    originalRequest._retry = true;
    isRefreshing = true;

    try {
      const token = await MsalService.getAccessToken();
      if (!token) throw new Error("Token missing");

      originalRequest.headers.Authorization = `Bearer ${token}`;
      return api(originalRequest);
    } catch (err) {
      console.warn("Silent refresh failed. User must login again.");
      await MsalService.login();
      return Promise.reject(err);
    } finally {
      isRefreshing = false;
    }
  }
);

export default api;
