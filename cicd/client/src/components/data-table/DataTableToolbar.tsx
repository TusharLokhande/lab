import { Input } from "@/components/ui/input"
import { Button } from "@/components/ui/button"

export function DataTableToolbar({ table, searchKey, enableSelection }: any) {
  const selectedRows = table.getFilteredSelectedRowModel().rows

  return (
    <div className="flex justify-between items-center">
      {searchKey && (
        <Input
          placeholder={`Search ${searchKey}...`}
          value={table.getState().globalFilter ?? ""}
          onChange={(e) => table.setGlobalFilter(e.target.value)}
          className="w-60"
        />
      )}

      {/* {enableSelection && selectedRows.length > 0 && (
        <Button variant="destructive">
          Delete Selected ({selectedRows.length})
        </Button>
      )} */}
    </div>
  )
}
