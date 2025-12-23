import { api } from "./axios";
import "./interceptors";

export const app = {
  get: (url: string, params?: Record<string, unknown>) =>
    api.get(url, { params }).then((r) => r.data),

  post: (url: string, data?: unknown) =>
    api.post(url, data).then((r) => r.data),

  put: (url: string, data?: unknown) => api.put(url, data).then((r) => r.data),

  delete: (url: string) => api.delete(url).then((r) => r.data),
};
