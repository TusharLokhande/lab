import {
  type ColumnDef,
  flexRender,
  getCoreRowModel,
  getPaginationRowModel,
  getSortedRowModel,
  getFilteredRowModel,
  useReactTable,
} from "@tanstack/react-table";
import { useEffect, useState } from "react";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { Skeleton } from "@/components/ui/skeleton";
import { Checkbox } from "@/components/ui/checkbox";
import { DataTableToolbar } from "./DataTableToolbar";
import { DataTablePagination } from "./DataTablePagination";

type FetchParams = {
  page: number;
  sort?: string;
  order?: "asc" | "desc";
  search?: string;
};

type Props = {
  columns: ColumnDef<any, any>[];
  data: any[];
  totalCount?: number;
  fetchData?: (params: FetchParams) => void;
  serverSide?: boolean;
  pageSize?: number;
  searchKey?: string;
  enableSelection?: boolean;
  isLoading?: boolean;
};

export function DataTable({
  columns,
  data,
  totalCount = 0,
  fetchData,
  serverSide = false,
  pageSize = 10,
  searchKey,
  enableSelection = false,
  isLoading = false,
}: Props) {
  const [pageIndex, setPageIndex] = useState(0);
  const [sorting, setSorting] = useState<any[]>([]);
  const [globalFilter, setGlobalFilter] = useState("");
  const [rowSelection, setRowSelection] = useState({});

  // Inject checkbox column if selection enabled
  const finalColumns = enableSelection
    ? [
        {
          id: "select",
          header: ({ table }: any) => (
            <Checkbox
              checked={table.getIsAllRowsSelected()}
              onCheckedChange={(v) => table.toggleAllRowsSelected(!!v)}
            />
          ),
          cell: ({ row }: any) => (
            <Checkbox
              checked={row.getIsSelected()}
              onCheckedChange={(v) => row.toggleSelected(!!v)}
            />
          ),
          enableSorting: false,
        },
        ...columns,
      ]
    : columns;

  const table = useReactTable({
    data,
    columns: finalColumns,
    state: {
      sorting,
      globalFilter,
      rowSelection,
      pagination: { pageIndex, pageSize },
    },

    onSortingChange: setSorting,
    onGlobalFilterChange: setGlobalFilter,
    onRowSelectionChange: setRowSelection,

    manualPagination: serverSide,
    manualSorting: serverSide,
    manualFiltering: serverSide,

    pageCount: serverSide ? Math.ceil(totalCount / pageSize) : undefined,

    getCoreRowModel: getCoreRowModel(),
    getPaginationRowModel: !serverSide ? getPaginationRowModel() : undefined,
    getSortedRowModel: !serverSide ? getSortedRowModel() : undefined,
    getFilteredRowModel: !serverSide ? getFilteredRowModel() : undefined,
  });

  // Auto fetch from server when state changes
  useEffect(() => {
    if (!serverSide || !fetchData) return;

    const sort = sorting[0];
    fetchData({
      page: pageIndex,
      sort: sort?.id,
      order: sort?.desc ? "desc" : "asc",
      search: globalFilter,
    });
  }, [pageIndex, sorting, globalFilter]);

  const rows = table.getRowModel().rows;

  return (
    <div className="space-y-4">
      <DataTableToolbar
        table={table}
        //searchKey={searchKey}
        enableSelection={enableSelection}
      />

      <div className="rounded-md border">
        <Table>
          <TableHeader>
            {table.getHeaderGroups().map((group) => (
              <TableRow key={group.id}>
                {group.headers.map((header) => (
                  <TableHead key={header.id}>
                    {flexRender(
                      header.column.columnDef.header,
                      header.getContext()
                    )}
                  </TableHead>
                ))}
              </TableRow>
            ))}
          </TableHeader>

          <TableBody>
            {isLoading ? (
              [...Array(pageSize)].map((_, r) => (
                <TableRow key={r}>
                  {finalColumns.map((col, c) => (
                    <TableCell key={c}>
                      <Skeleton className="h-6 w-full" />
                    </TableCell>
                  ))}
                </TableRow>
              ))
            ) : rows.length ? (
              rows.map((row) => (
                <TableRow key={row.id}>
                  {row.getVisibleCells().map((cell) => (
                    <TableCell key={cell.id}>
                      {flexRender(
                        cell.column.columnDef.cell,
                        cell.getContext()
                      )}
                    </TableCell>
                  ))}
                </TableRow>
              ))
            ) : (
              <TableRow>
                <TableCell
                  colSpan={finalColumns.length}
                  className="py-6 text-center"
                >
                  No data found
                </TableCell>
              </TableRow>
            )}
          </TableBody>
        </Table>
      </div>

      <DataTablePagination table={table} setPageIndex={setPageIndex} />
    </div>
  );
}
