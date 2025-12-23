type BaseModalProps = {
  isOpen: boolean;
  title: string;
  children: React.ReactNode;
  onClose: () => void;
};

export function BaseModal({
  isOpen,
  title,
  children,
  onClose,
}: BaseModalProps) {
  if (!isOpen) return null;

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center">
      <div className="absolute inset-0 bg-black/60" onClick={onClose} />

      <div className="relative z-10 w-full max-w-lg rounded-lg bg-blue-700 shadow-lg">
        <div className="flex items-center justify-between border-b border-blue-600 px-4 py-3">
          <h2 className="text-lg font-semibold text-white">{title}</h2>

          <button
            onClick={onClose}
            className="text-white hover:text-blue-300 transition"
          >
            ✕
          </button>
        </div>

        <div className="p-4">{children}</div>
      </div>
    </div>
  );
}
