import { useState } from "react";
import type { UserType } from "./Create";
import { TextInput } from "../../components/inputs/TextInput";
import { PrimaryButton } from "../../components/buttons/PrimaryButton";
import { Link } from "react-router-dom";
import { userService } from "../../services/userService";

const initialForm = {
  email: "",
  password: "",
};

export function Login() {
  const [form, setForm] = useState<UserType>(initialForm);

  async function handleSubmit() {
    const token = await userService.login(form);
    localStorage.setItem("token", token.token);
    window.location.href = "/pessoas";
  }

  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-100">
      <div className="border-2 border-black bg-blue-500 p-8 rounded-lg w-full max-w-2xl">
        <div className="grid grid-cols-6 gap-4 text-white">
          <div className="col-span-6 text-center">
            <h1 className="text-2xl font-bold">Login</h1>
          </div>

          <div className="col-span-6">
            <TextInput
              label="Email"
              value={form.email}
              onChange={(value) => setForm((f) => ({ ...f, email: value }))}
            />
          </div>

          <div className="col-span-6">
            <TextInput
              label="Senha"
              value={form.password}
              onChange={(value) => setForm((f) => ({ ...f, password: value }))}
            />
          </div>

          <div className="col-span-3 flex justify-center">
            <Link
              to="/new"
              className="h-10 px-6 flex items-center justify-center
                         bg-black text-white rounded-md
                         hover:bg-gray-800 transition"
            >
              Ir para cadastro
            </Link>
          </div>

          <div className="col-span-3 flex justify-center">
            <PrimaryButton text="Entrar" onClick={handleSubmit} />
          </div>
        </div>
      </div>
    </div>
  );
}
