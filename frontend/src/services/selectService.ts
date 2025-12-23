import { app } from "../api/app";

export const selectService = {
  getPurposes: () => app.get("/Select/GetPurposes"),
  getTransactionTypes: () => app.get(`/Select/GetTransactionTypes`),
  getPersons: () => app.get("/Select/GetPersons"),
  getCategories: (type: number) =>
    app.get(`/Select/GetCategories?type=${type}`),
};
