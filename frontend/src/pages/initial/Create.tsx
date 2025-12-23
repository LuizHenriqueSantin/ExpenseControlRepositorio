import { useState } from "react";
import { TextInput } from "../../components/inputs/TextInput";
import { PrimaryButton } from "../../components/buttons/PrimaryButton";
import { Link } from "react-router-dom";
import { userService } from "../../services/userService";

export type UserType = {
  email: string;
  password: string;
};

const initialForm = {
  email: "",
  password: "",
};

export function Register() {
  const [form, setForm] = useState<UserType>(initialForm);

  async function handleSubmit() {
    await userService.create(form);
    window.location.href = "/";
  }

  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-100">
      <div className="border-2 border-black bg-blue-500 p-8 rounded-lg w-full max-w-2xl">
        <div className="grid grid-cols-6 gap-4 text-white">
          <div className="col-span-6 text-center">
            <h1 className="text-2xl font-bold">Cadastre-se</h1>
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
              to="/"
              className="h-10 px-6 flex items-center justify-center
                         bg-black text-white rounded-md
                         hover:bg-gray-800 transition"
            >
              Ir para login
            </Link>
          </div>

          <div className="col-span-3 flex justify-center">
            <PrimaryButton text="Cadastrar" onClick={handleSubmit} />
          </div>
        </div>
      </div>
    </div>
  );
}
