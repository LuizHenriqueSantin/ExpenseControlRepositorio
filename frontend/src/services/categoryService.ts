import { app } from "../api/app";
import type { CategoryFormType } from "../pages/category/CategoryForm";

export const categoryService = {
  getAll: () => app.get("/Category/GetAll"),
  getById: (idEdit: string) => app.get(`/Category/GetById?id=${idEdit}`),
  create: (data: CategoryFormType) => app.post("/Category/Create", data),
  update: (data: CategoryFormType) => app.put("/Category/Update", data),
  delete: (idDelete: string) => app.delete(`/Category/Delete?=${idDelete}`),
};
