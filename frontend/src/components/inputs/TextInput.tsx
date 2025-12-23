type Props = {
  label: string;
  value: string;
  onChange: (value: string) => void;
};

export function TextInput({ label, value, onChange }: Props) {
  return (
    <div className="flex flex-col gap-1">
      <label className="text-sm text-gray-300">{label}</label>
      <input
        type="text"
        value={value}
        onChange={(e) => onChange(e.target.value)}
        className="h-10 px-3 bg-gray-800 text-white border border-gray-700 rounded-md
                   focus:outline-none focus:ring-2 focus:ring-blue-500"
      />
    </div>
  );
}
