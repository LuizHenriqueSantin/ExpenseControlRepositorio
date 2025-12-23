import { BaseModal } from "./BaseModal";
import { PrimaryButton } from "../buttons/PrimaryButton";

type Props = {
  isOpen: boolean;
  title: string;
  message: string;
  onClose: () => void;
  onConfirm: () => void;
  confirmText?: string;
};

export function ConfirmModal({
  isOpen,
  title,
  message,
  onClose,
  onConfirm,
  confirmText = "Sim",
}: Props) {
  return (
    <BaseModal isOpen={isOpen} title={title} onClose={onClose}>
      <div className="flex flex-col gap-6">
        <p className="text-white">{message}</p>

        <div className="flex justify-end">
          <PrimaryButton text={confirmText} onClick={onConfirm} />
        </div>
      </div>
    </BaseModal>
  );
}
