export type SelectOptions = {
  label: string;
  value: string | number;
  infos?: PersonInfos;
};

type Props = {
  label: string;
  value: string | number;
  options?: SelectOptions[];
  onChange: (item: SelectOptions) => void;
  disabled?: boolean;
};

export function SelectInput({
  label,
  value,
  options,
  onChange,
  disabled = false,
}: Props) {
  return (
    <div className="flex flex-col gap-1">
      <label className="text-sm text-white">{label}</label>

      <select
        disabled={disabled}
        value={value}
        onChange={(e) => {
          const selected = options?.find(
            (o) => String(o.value) === e.target.value
          );
          if (selected) onChange(selected);
        }}
        className="
          h-10 px-3 rounded-md
          bg-blue-600 text-white
          border border-blue-500
          focus:outline-none focus:ring-2 focus:ring-blue-400
        "
      >
        <option value="">Selecione</option>

        {options?.map((opt) => (
          <option key={opt.value} value={opt.value}>
            {opt.label}
          </option>
        ))}
      </select>
    </div>
  );
}

export type PersonInfos = {
  age: number;
};
