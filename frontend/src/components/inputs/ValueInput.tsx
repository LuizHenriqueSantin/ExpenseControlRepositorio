type Props = {
  label: string;
  value: number | "";
  onChange: (value: number | "") => void;
  type?: string;
};

export function NumberInput({ label, value, onChange, type }: Props) {
  return (
    <div className="flex flex-col gap-1">
      <label className="text-sm text-gray-300">{label}</label>
      <input
        type="number"
        step={type == "currency" ? "0.01" : "1"}
        value={value}
        onChange={(e) =>
          onChange(e.target.value === "" ? "" : Number(e.target.value))
        }
        className="h-10 px-3 bg-gray-800 text-white border border-gray-700 rounded-md
                   focus:outline-none focus:ring-2 focus:ring-blue-500"
      />
    </div>
  );
}
