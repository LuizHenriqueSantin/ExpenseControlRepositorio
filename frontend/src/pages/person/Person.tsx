import { useState, useEffect } from "react";
import type { ColDef } from "ag-grid-community";
import { DataGrid } from "../../components/grid/DataGrid";
import { PrimaryButton } from "../../components/buttons/PrimaryButton";
import { personService } from "../../services/personService";
import { ConfirmModal } from "../../components/modals/ConfirmationModal";
import { PersonForm } from "./PersonForm";
import type { PersonFormType } from "./PersonForm";

type Person = {
  id: string;
  name: string;
  age?: number;
  totalValueExpense: number;
  totalValueIncome: number;
  totalValue: number;
};

export function Person() {
  const [gridData, setGridData] = useState<Person[]>([]);
  const [confirmOpen, setConfirmOpen] = useState(false);
  const [idDelete, setIdDelete] = useState<string>();
  const [formOpen, setFormOpen] = useState(false);
  const [idEdit, setIdEdit] = useState<string>();

  const columns: ColDef<Person>[] = [
    { field: "name", headerName: "Nome" },
    { field: "age", headerName: "Idade" },
    { field: "totalValueExpense", headerName: "Total de gastos" },
    { field: "totalValueIncome", headerName: "Total de receitas" },
    { field: "totalValue", headerName: "Saldo" },
  ];

  const totalsRow: Person = gridData.reduce(
    (acc, cur) => ({
      id: "TOTAL",
      name: "TOTAL",
      totalValueExpense: acc.totalValueExpense + cur.totalValueExpense,
      totalValueIncome: acc.totalValueIncome + cur.totalValueIncome,
      totalValue: acc.totalValue + cur.totalValue,
    }),
    {
      id: "TOTAL",
      name: "TOTAL",
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

    await personService.delete(idDelete);
  }

  function onCloseForm() {
    setFormOpen(false);
    setIdEdit("");
  }

  async function onSave(data: PersonFormType) {
    if (data.id) {
      await personService.update(data);
    } else {
      await personService.create(data);
    }
    const newData = await personService.getAll();
    setGridData(newData);
    setFormOpen(false);
    setIdEdit("");
  }

  useEffect(() => {
    async function fetchData() {
      const data = await personService.getAll();
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
          pinnedBottomRowData={gridData.length > 0 ? [totalsRow] : undefined}
        />
      </div>

      <ConfirmModal
        isOpen={confirmOpen}
        title="Excluir"
        message="Deseja excluir esta pessoa?"
        onClose={() => {
          setConfirmOpen(false);
          setIdDelete("");
        }}
        onConfirm={confirmDelete}
      />
      <PersonForm
        isOpen={formOpen}
        onClose={onCloseForm}
        onSave={onSave}
        id={idEdit}
      />
    </>
  );
}
