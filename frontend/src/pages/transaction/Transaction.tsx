import { useState, useEffect } from "react";
import type { ColDef } from "ag-grid-community";
import { DataGrid } from "../../components/grid/DataGrid";
import { PrimaryButton } from "../../components/buttons/PrimaryButton";
import { transactionService } from "../../services/transactionService";
import { ConfirmModal } from "../../components/modals/ConfirmationModal";
import { TransactionForm } from "./TransactionForm";
import type { TransactionFormType } from "./TransactionForm";

type Transaction = {
  id: string;
  transactionDescription: string;
  transactionValue: number;
  transactionType: number;
  transactionTypeName: string;
  personId: string;
  personName: string;
  categoryId: string;
  categoryName: string;
};

export function Transaction() {
  const [gridData, setGridData] = useState<Transaction[]>([]);
  const [confirmOpen, setConfirmOpen] = useState(false);
  const [idDelete, setIdDelete] = useState<string>();
  const [formOpen, setFormOpen] = useState(false);
  const [idEdit, setIdEdit] = useState<string>();

  const columns: ColDef<Transaction>[] = [
    { field: "transactionDescription", headerName: "Descrição" },
    { field: "transactionTypeName", headerName: "Tipo" },
    { field: "personName", headerName: "Pessoa" },
    { field: "categoryName", headerName: "Categoria" },
    { field: "transactionValue", headerName: "Valor" },
  ];

  const totalsRow: Transaction = gridData.reduce(
    (acc, cur) => ({
      id: "TOTAL",
      transactionDescription: "TOTAL",
      transactionType: 0,
      transactionTypeName: "",
      personId: "",
      personName: "",
      categoryId: "",
      categoryName: "",
      transactionValue: acc.transactionValue + cur.transactionValue,
    }),
    {
      id: "TOTAL",
      transactionDescription: "TOTAL",
      transactionType: 0,
      transactionTypeName: "",
      personId: "",
      personName: "",
      categoryId: "",
      categoryName: "",
      transactionValue: 0,
    }
  );

  function handleCreate() {
    setFormOpen(true);
  }

  function handleEdit(id: string) {
    setIdEdit(id);
    setFormOpen(true);
  }

  function handleDelete(id: string) {
    setIdDelete(id);
    setConfirmOpen(true);
  }

  async function confirmDelete() {
    if (!idDelete) return;

    await transactionService.delete(idDelete);
  }

  function onCloseForm() {
    setFormOpen(false);
    setIdEdit("");
  }

  async function onSave(data: TransactionFormType) {
    if (data.id) {
      await transactionService.update(data);
    } else {
      await transactionService.create(data);
    }
    const newData = await transactionService.getAll();
    setGridData(newData);
    setFormOpen(false);
    setIdEdit("");
  }

  useEffect(() => {
    async function fetchData() {
      const data = await transactionService.getAll();
      setGridData(data);
    }

    fetchData();
  }, []);

  return (
    <>
      <div className="flex flex-col gap-4 p-6">
        <div className="flex justify-end">
          <PrimaryButton text="Novo" onClick={handleCreate} />
        </div>

        <DataGrid
          rowData={gridData}
          columnDefs={columns}
          onEdit={handleEdit}
          onDelete={handleDelete}
          pinnedBottomRowData={[totalsRow]}
        />
      </div>

      <ConfirmModal
        isOpen={confirmOpen}
        title="Excluir"
        message="Deseja excluir esta transação?"
        onClose={() => {
          setConfirmOpen(false);
          setIdDelete("");
        }}
        onConfirm={confirmDelete}
      />

      <TransactionForm
        isOpen={formOpen}
        onClose={onCloseForm}
        onSave={onSave}
        id={idEdit}
      />
    </>
  );
}
