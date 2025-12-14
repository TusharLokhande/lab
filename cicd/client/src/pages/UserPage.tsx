// pages/UsersPage.tsx

import { useState } from "react"
import { DataTable } from "@/components/data-table/DataTable"
import { type ColumnDef } from "@tanstack/react-table"
import { Button } from "@/components/ui/button"
import { MoreVertical } from "lucide-react"
import {
  DropdownMenu,
  DropdownMenuTrigger,
  DropdownMenuContent,
  DropdownMenuItem,
} from "@/components/ui/dropdown-menu"

export type User = {
  id: number
  name: string
  email: string
}

// Sample local dataset (Client-side mode)
const initialUsers: User[] = [
  { id: 1, name: "Tush", email: "tush@example.com" },
  { id: 2, name: "Raj", email: "raj@example.com" },
  { id: 3, name: "Sam", email: "sam@example.com" },
  { id: 4, name: "Aditi", email: "aditi@example.com" },
  { id: 5, name: "Aman", email: "aman@example.com" },
  { id: 6, name: "Kiran", email: "kiran@example.com" },
  { id: 7, name: "Riya", email: "riya@example.com" },
  { id: 8, name: "Nisha", email: "nisha@example.com" },
]

// Column definitions including Actions menu
const userColumns: ColumnDef<User>[] = [
  {
    accessorKey: "name",
    header: "Name",
  },
  {
    accessorKey: "email",
    header: "Email",
  },
  {
    id: "actions",
    header: "Actions",
    cell: ({ row }) => {
      const user = row.original
      return (
        <DropdownMenu>
          <DropdownMenuTrigger asChild>
            <Button variant="ghost" size="sm">
              <MoreVertical />
            </Button>
          </DropdownMenuTrigger>
          <DropdownMenuContent>
            <DropdownMenuItem
              onClick={() => alert(`Editing ${user.name}`)}
            >
              Edit
            </DropdownMenuItem>
            <DropdownMenuItem
              className="text-red-600"
              onClick={() => alert(`Deleting ${user.name}`)}
            >
              Delete
            </DropdownMenuItem>
          </DropdownMenuContent>
        </DropdownMenu>
      )
    },
  },
]

export default function UsersPage() {
  const [users] = useState<User[]>(initialUsers)

  return (
    <div className="p-6 space-y-4">
      <h1 className="text-2xl font-bold">Users</h1>

      <DataTable
        columns={userColumns}
        data={users}
        searchKey="name"
        //enableSelection // optional
        // NO serverSide → this activates manual mode
      />
    </div>
  )
}
