import { useState } from "react";
import { FiLogOut, FiX } from "react-icons/fi";
import { Link } from "react-router-dom";
import { userService } from "../services/userService";
import { ConfirmModal } from "./modals/ConfirmationModal";

export function Header() {
  const [open, setOpen] = useState(false);

  function logOut() {
    localStorage.removeItem("token");
    window.location.href = "/";
  }

  function handleOpen() {
    setOpen(true);
  }

  async function confirmDelete() {
    await userService.delete();
    localStorage.removeItem("token");
    window.location.href = "/";
  }

  return (
    <>
      <header className="w-full h-16 bg-blue-600 text-white flex justify-between px-6 shadow">
        <div className="flex h-full">
          <VerticalDivider />
          <HeaderButton label="Pessoas" to="/pessoas" />
          <VerticalDivider />
          <HeaderButton label="Categorias" to="/categorias" />
          <VerticalDivider />
          <HeaderButton label="Transações" to="/transacoes" />
          <VerticalDivider />
        </div>
        <div className="flex h-full gap-2">
          <IconButton title="Sair" onClick={logOut}>
            <FiLogOut size={30} />
          </IconButton>
          <IconButton title="Excluir conta" onClick={handleOpen}>
            <FiX size={36} />
          </IconButton>
        </div>
      </header>
      <ConfirmModal
        isOpen={open}
        title="Excluir"
        message="Deseja excluir sua conta?"
        onClose={() => {
          setOpen(false);
        }}
        onConfirm={confirmDelete}
      />
    </>
  );
}

function HeaderButton({ label, to }: { label: string; to: string }) {
  return (
    <Link
      to={to}
      className="h-full px-6 flex items-center justify-center hover:bg-blue-500 transition"
    >
      {label}
    </Link>
  );
}

function VerticalDivider() {
  return <div className="w-px h-full bg-white" />;
}

function IconButton({
  children,
  title,
  onClick,
}: {
  children: React.ReactNode;
  title: string;
  onClick: () => void;
}) {
  return (
    <button
      title={title}
      className=" w-16 h-16 flex items-center justify-center rounded-full hover:bg-blue-500 transition"
      onClick={onClick}
    >
      {children}
    </button>
  );
}
