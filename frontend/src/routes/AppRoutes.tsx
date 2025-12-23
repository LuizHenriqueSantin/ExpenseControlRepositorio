import { BrowserRouter, Routes, Route } from "react-router-dom";
import { AppLayout } from "../layouts/AppLayout";
import { Login } from "../pages/initial/Login";
import { Register } from "../pages/initial/Create";
import { Person } from "../pages/person/Person";
import { Category } from "../pages/category/Category";
import { Transaction } from "../pages/transaction/Transaction";

export function AppRoutes() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Login />} />
        <Route path="/new" element={<Register />} />

        <Route element={<AppLayout />}>
          <Route path="/pessoas" element={<Person />} />
          <Route path="/categorias" element={<Category />} />
          <Route path="/transacoes" element={<Transaction />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}
