import { BaseModal } from "./BaseModal";
import { PrimaryButton } from "../buttons/PrimaryButton";

type FormModalProps = {
  isOpen: boolean;
  title: string;
  onClose: () => void;
  onSave: () => void;
  children: React.ReactNode;
};

export function FormModal({
  isOpen,
  title,
  onClose,
  onSave,
  children,
}: FormModalProps) {
  return (
    <BaseModal isOpen={isOpen} title={title} onClose={onClose}>
      <div className="flex flex-col gap-4">
        {children}

        <div className="flex justify-end pt-4">
          <PrimaryButton text="Salvar" onClick={onSave} />
        </div>
      </div>
    </BaseModal>
  );
}
