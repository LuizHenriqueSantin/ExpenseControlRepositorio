type Props = {
  text: string;
  onClick: () => void;
  disabled?: boolean;
};

export function PrimaryButton({ text, onClick, disabled = false }: Props) {
  return (
    <button
      onClick={onClick}
      disabled={disabled}
      className="h-10 px-6 bg-blue-600 text-white rounded-md
                 hover:bg-blue-500 transition
                 disabled:opacity-50 disabled:cursor-not-allowed"
    >
      {text}
    </button>
  );
}
