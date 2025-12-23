import { useEffect, useState } from "react";
import { transactionService } from "../../services/transactionService";
import { selectService } from "../../services/selectService";
import { BaseModal } from "../../components/modals/BaseModal";
import { PrimaryButton } from "../../components/buttons/PrimaryButton";
import { TextInput } from "../../components/inputs/TextInput";
import { SelectInput } from "../../components/inputs/SelectInput";
import type { SelectOptions } from "../../components/inputs/SelectInput";
import { NumberInput } from "../../components/inputs/ValueInput";

export type TransactionFormType = {
  id?: string;
  transactionDescription: string;
  personId: string;
  transactionType: number;
  categoryId: string;
  transactionValue: number;
};

type Props = {
  isOpen: boolean;
  onClose: () => void;
  onSave: (data: TransactionFormType) => void;
  id?: string;
};

const initialForm = {
  transactionDescription: "",
  personId: "",
  transactionType: 0,
  categoryId: "",
  transactionValue: 0,
};

export function TransactionForm({ isOpen, onClose, onSave, id }: Props) {
  const [form, setForm] = useState<TransactionFormType>(initialForm);
  const [typesOptions, setTypesOptions] = useState<SelectOptions[]>();
  const [personOptions, setPersonOptions] = useState<SelectOptions[]>();
  const [categoryOptions, setCategoryOptions] = useState<SelectOptions[]>();
  const [inputDisable, setInputDisable] = useState(false);

  function handleSubmit() {
    onSave(form);
  }

  useEffect(() => {
    async function fetchData() {
      if (id && isOpen) {
        const data = await transactionService.getById(id);
        setForm(data);
      }

      const optionsType = await selectService.getTransactionTypes();
      const optionsPerson = await selectService.getPersons();
      setTypesOptions(optionsType);
      setPersonOptions(optionsPerson);
    }

    fetchData();
  }, [isOpen, id]);

  useEffect(() => {
    async function fetchOptions() {
      const options = await selectService.getCategories(form.transactionType);
      setCategoryOptions(options);
    }

    if (form.transactionType != 0) fetchOptions();
  }, [form.transactionType]);

  return (
    <BaseModal isOpen={isOpen} title="Categoria" onClose={onClose}>
      <div className="grid grid-cols-6 gap-4">
        <div className="col-span-3">
          <TextInput
            label="Descrição"
            value={form.transactionDescription}
            onChange={(value) =>
              setForm((f) => ({ ...f, transactionDescription: value }))
            }
          />
        </div>
        <div className="col-span-3">
          <SelectInput
            label="Pessoa"
            value={form.personId}
            options={personOptions}
            onChange={(selected) => {
              if (selected.infos?.age && selected.infos?.age < 18) {
                setForm((f) => ({
                  ...f,
                  personId: String(selected.value),
                  transactionType: 1,
                }));
                setInputDisable(true);
              } else {
                setForm((f) => ({
                  ...f,
                  personId: String(selected.value),
                }));
                setInputDisable(false);
              }
            }}
          />
        </div>
        <div className="col-span-2">
          <SelectInput
            disabled={inputDisable}
            label="Tipo"
            value={form.transactionType}
            options={typesOptions}
            onChange={(selected) =>
              setForm((f) => ({
                ...f,
                transactionType: Number(selected.value),
              }))
            }
          />
        </div>
        <div className="col-span-2">
          <SelectInput
            label="Categoria"
            value={form.categoryId}
            options={categoryOptions}
            onChange={(selected) =>
              setForm((f) => ({
                ...f,
                categoryId: String(selected.value),
              }))
            }
          />
        </div>
        <div className="col-span-2">
          <NumberInput
            label="Valor"
            value={form.transactionValue}
            onChange={(value) =>
              setForm((f) => ({ ...f, transactionValue: Number(value) }))
            }
            type="currency"
          />
        </div>
      </div>
      <div className="flex justify-end pt-4">
        <PrimaryButton text="Salvar" onClick={handleSubmit} />
      </div>
    </BaseModal>
  );
}
