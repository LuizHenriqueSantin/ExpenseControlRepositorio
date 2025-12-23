import { app } from "../api/app";
import type { TransactionFormType } from "../pages/transaction/TransactionForm";

export const transactionService = {
  getAll: () => app.get("/Transaction/GetAll"),
  getById: (idEdit: string) => app.get(`/Transaction/GetById?id=${idEdit}`),
  create: (data: TransactionFormType) => app.post("/Transaction/Create", data),
  update: (data: TransactionFormType) => app.put("/Transaction/Update", data),
  delete: (idDelete: string) =>
    app.delete(`/Transaction/Delete?id=${idDelete}`),
};
