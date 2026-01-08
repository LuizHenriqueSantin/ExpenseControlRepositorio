import { useState, useEffect } from "react";
import type { ColDef } from "ag-grid-community";
import { DataGrid } from "../../components/grid/DataGrid";
import { PrimaryButton } from "../../components/buttons/PrimaryButton";
import { categoryService } from "../../services/categoryService";
import { ConfirmModal } from "../../components/modals/ConfirmationModal";
import { CategoryForm } from "./CategoryForm";
import type { CategoryFormType } from "./CategoryForm";

type Category = {
  id: string;
  categoryDescription: string;
  purpose: number;
  purposeName: string;
  totalValueExpense: number;
  totalValueIncome: number;
  totalValue: number;
};

export function Category() {
  const [gridData, setGridData] = useState<Category[]>([]);
  const [confirmOpen, setConfirmOpen] = useState(false);
  const [idDelete, setIdDelete] = useState<string>();
  const [formOpen, setFormOpen] = useState(false);
  const [idEdit, setIdEdit] = useState<string>();

  const columns: ColDef<Category>[] = [
    { field: "categoryDescription", headerName: "Descrição" },
    { field: "purposeName", headerName: "Finalidade" },
    { field: "totalValueExpense", headerName: "Total de gastos" },
    { field: "totalValueIncome", headerName: "Total de receitas" },
    { field: "totalValue", headerName: "Saldo" },
  ];

  const totalsRow: Category = gridData.reduce(
    (acc, cur) => ({
      id: "TOTAL",
      categoryDescription: "TOTAL",
      purpose: 0,
      purposeName: "",
      totalValueExpense: acc.totalValueExpense + cur.totalValueExpense,
      totalValueIncome: acc.totalValueIncome + cur.totalValueIncome,
      totalValue: acc.totalValue + cur.totalValue,
    }),
    {
      id: "TOTAL",
      categoryDescription: "TOTAL",
      purpose: 0,
      purposeName: "",
      totalValueExpense: 0,
      totalValueIncome: 0,
      totalValue: 0,
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

    await categoryService.delete(idDelete);
  }

  function onCloseForm() {
    setFormOpen(false);
    setIdEdit("");
  }

  async function onSave(data: CategoryFormType) {
    if (data.id) {
      await categoryService.update(data);
    } else {
      await categoryService.create(data);
    }
    const newData = await categoryService.getAll();
    setGridData(newData);
    setFormOpen(false);
    setIdEdit("");
  }

  useEffect(() => {
    async function fetchData() {
      const data = await categoryService.getAll();
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
        message="Deseja excluir esta categoria?"
        onClose={() => {
          setConfirmOpen(false);
          setIdDelete("");
        }}
        onConfirm={confirmDelete}
      />

      <CategoryForm
        isOpen={formOpen}
        onClose={onCloseForm}
        onSave={onSave}
        id={idEdit}
      />
    </>
  );
}
