import { AgGridReact } from "ag-grid-react";
import type { ColDef, ICellRendererParams } from "ag-grid-community";
import { FaEdit, FaTrash } from "react-icons/fa";
import "ag-grid-community/styles/ag-grid.css";
import "ag-grid-community/styles/ag-theme-alpine.css";

type Props<T extends WithId> = {
  rowData: T[];
  columnDefs: ColDef<T>[];
  pinnedBottomRowData?: T[];
  onEdit?: (id: string) => void;
  onDelete?: (id: string) => void;
};

export function DataGrid<T extends WithId>({
  rowData,
  columnDefs,
  pinnedBottomRowData,
  onEdit,
  onDelete,
}: Props<T>) {
  const actionColumn: ColDef<T> = {
    headerName: "Ações",
    width: 80,
    cellRenderer: (params: ICellRendererParams<T>) => {
      if (params.node?.rowPinned) return null;

      const data = params.data;
      if (!data) return null;

      return (
        <div className="flex items-center gap-3 h-full">
          {onEdit && (
            <button
              title="Editar"
              onClick={() => onEdit(data.id)}
              className="text-blue-600 hover:text-blue-500 transition"
            >
              <FaEdit />
            </button>
          )}

          {onDelete && (
            <button
              title="Excluir"
              onClick={() => onDelete(data.id)}
              className="text-red-600 hover:text-red-400 transition"
            >
              <FaTrash />
            </button>
          )}
        </div>
      );
    },
  };

  return (
    <div className="ag-theme-alpine w-full h-[500px]">
      <AgGridReact
        rowData={rowData}
        columnDefs={[...columnDefs, actionColumn]}
        pinnedBottomRowData={pinnedBottomRowData}
        defaultColDef={{
          flex: 1,
          sortable: true,
          filter: true,
          resizable: true,
        }}
      />
    </div>
  );
}

type WithId = {
  id: string;
};
