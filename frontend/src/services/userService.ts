import { app } from "../api/app";
import type { UserType } from "../pages/initial/Create";

export const userService = {
  login: (data: UserType) => app.post("/Auth/Login", data),
  create: (data: UserType) => app.post("/User/Create", data),
  delete: () => app.delete("/User/DeleteMyAccount"),
};
