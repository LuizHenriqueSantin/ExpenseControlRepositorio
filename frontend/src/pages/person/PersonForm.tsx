import { useEffect, useState } from "react";
import { personService } from "../../services/personService";
import { BaseModal } from "../../components/modals/BaseModal";
import { PrimaryButton } from "../../components/buttons/PrimaryButton";
import { TextInput } from "../../components/inputs/TextInput";
import { NumberInput } from "../../components/inputs/ValueInput";

export type PersonFormType = {
  id?: string;
  name: string;
  age: number;
};

type Props = {
  isOpen: boolean;
  onClose: () => void;
  onSave: (data: PersonFormType) => void;
  id?: string;
};

const initialForm = {
  name: "",
  age: 0,
};

export function PersonForm({ isOpen, onClose, onSave, id }: Props) {
  const [form, setForm] = useState<PersonFormType>(initialForm);

  function handleSubmit() {
    onSave(form);
  }

  useEffect(() => {
    async function fetchData() {
      if (id && isOpen) {
        const data = await personService.getById(id);
        setForm(data);
      }
    }

    fetchData();
  }, [isOpen, id]);

  return (
    <BaseModal isOpen={isOpen} title="Pessoa" onClose={onClose}>
      <div className="flex flex-col gap-4">
        <TextInput
          label="Nome"
          value={form.name}
          onChange={(value) => setForm((f) => ({ ...f, name: value }))}
        />
        <NumberInput
          label="Idade"
          value={form.age}
          onChange={(value) => setForm((f) => ({ ...f, age: Number(value) }))}
        />
      </div>
      <div className="flex justify-end pt-4">
        <PrimaryButton text="Salvar" onClick={handleSubmit} />
      </div>
    </BaseModal>
  );
}
