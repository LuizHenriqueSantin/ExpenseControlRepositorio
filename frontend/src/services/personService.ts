import { app } from "../api/app";
import type { PersonFormType } from "../pages/person/PersonForm";

export const personService = {
  getAll: () => app.get("/Person/GetAll"),
  getById: (idEdit: string) => app.get(`/Person/GetById?id=${idEdit}`),
  create: (data: PersonFormType) => app.post("/Person/Create", data),
  update: (data: PersonFormType) => app.put("/Person/Update", data),
  delete: (idDelete: string) => app.delete(`/Person/Delete?id=${idDelete}`),
};
