import { useEffect, useState } from "react";
import { categoryService } from "../../services/categoryService";
import { selectService } from "../../services/selectService";
import { BaseModal } from "../../components/modals/BaseModal";
import { PrimaryButton } from "../../components/buttons/PrimaryButton";
import { TextInput } from "../../components/inputs/TextInput";
import { SelectInput } from "../../components/inputs/SelectInput";
import type { SelectOptions } from "../../components/inputs/SelectInput";

export type CategoryFormType = {
  id?: string;
  categoryDescription: string;
  purpose: number;
};

type Props = {
  isOpen: boolean;
  onClose: () => void;
  onSave: (data: CategoryFormType) => void;
  id?: string;
};

const initialForm = {
  categoryDescription: "",
  purpose: 0,
};

export function CategoryForm({ isOpen, onClose, onSave, id }: Props) {
  const [form, setForm] = useState<CategoryFormType>(initialForm);
  const [purposeOptions, setPurposeOptions] = useState<SelectOptions[]>();

  function handleSubmit() {
    onSave(form);
  }

  useEffect(() => {
    async function fetchData() {
      if (id && isOpen) {
        const data = await categoryService.getById(id);
        setForm(data);
      } else if (isOpen) {
        setForm(initialForm);
      }

      const options = await selectService.getPurposes();
      setPurposeOptions(options);
    }

    fetchData();
  }, [isOpen, id]);

  return (
    <BaseModal isOpen={isOpen} title="Categoria" onClose={onClose}>
      <div className="flex flex-col gap-4">
        <TextInput
          label="Descrição"
          value={form.categoryDescription}
          onChange={(value) =>
            setForm((f) => ({ ...f, categoryDescription: value }))
          }
        />
        <SelectInput
          label="Finalidade"
          value={form.purpose}
          options={purposeOptions}
          onChange={(selected) =>
            setForm((f) => ({
              ...f,
              purpose: Number(selected.value),
            }))
          }
        />
      </div>
      <div className="flex justify-end pt-4">
        <PrimaryButton text="Salvar" onClick={handleSubmit} />
      </div>
    </BaseModal>
  );
}
