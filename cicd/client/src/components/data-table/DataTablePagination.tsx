import { Button } from "@/components/ui/button"

export function DataTablePagination({ table, setPageIndex }: any) {
  const pagination = table.getState().pagination
  const pageCount = table.getPageCount()

  return (
    <div className="flex justify-end items-center gap-3">
      <Button
        size="sm"
        variant="outline"
        disabled={pagination.pageIndex === 0}
        onClick={() => setPageIndex(pagination.pageIndex - 1)}
      >
        Prev
      </Button>

      <span>
        Page {pagination.pageIndex + 1} / {pageCount || 1}
      </span>

      <Button
        size="sm"
        variant="outline"
        disabled={pagination.pageIndex + 1 >= pageCount}
        onClick={() => setPageIndex(pagination.pageIndex + 1)}
      >
        Next
      </Button>
    </div>
  )
}
