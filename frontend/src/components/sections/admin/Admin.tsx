import { useEffect, useMemo, useState } from "react";
import Module2 from "../Module2";
import TitleBar from "../TitleBar";
import type IVendingMachines from "../../../interfaces/IVendingMachines";
import { createColumnHelper, flexRender, getCoreRowModel, getFilteredRowModel, getPaginationRowModel, getSortedRowModel, useReactTable, type SortingState } from "@tanstack/react-table";
import {  ArrowUpDown, ChevronLeft, ChevronRight, Pen, Plus, Trash, Unlock } from "lucide-react";
import ExportButton from "./ExportButton";

const columnHelper = createColumnHelper<IVendingMachines>()



export default function Admin() {
    const [vendingMachines, setVendingMachines] = useState<IVendingMachines[]>([])
    const [isRows, setIsRows] = useState(true)
    const [sorting, setSorting] = useState<SortingState>([]);
    const [globalFilter, setGlobalFilter] = useState("");

    const handleEdit = (item: IVendingMachines) => {
    console.log("edit", item);
    }

    const handleDelete = (item: IVendingMachines) => {
        const confirmed = window.confirm(
            `Удалить автомат "${item.model}"?`
        );

        if (!confirmed) return;
        const deleteMachine = async (id: string) => {
            
            await fetch(`http://localhost:5208/api/VendingMachines/${id}`,
                {
                    method: "DELETE",
                }
            );
        }
        deleteMachine(item.id);

        setVendingMachines(prev =>
            prev.filter(machine => machine.id !== item.id)
        );
    }

    const handleUnlock = (item: IVendingMachines) => {
    console.log("unlock", item);
    }


    const data = useMemo(() => vendingMachines, [vendingMachines]);
    
    const columns = useMemo(
    () => [
        columnHelper.accessor("id",{
            cell: (info) => info.getValue(),
            header: () => (
                <span className="flex justify-center text-black font-bold">ID</span>
            )
        }),
        columnHelper.accessor("model",{
            cell: (info) => (
                <span className="text-blue-400">{info.getValue()}</span>
            ),
            header: () => (
                <span className="flex justify-center text-black font-bold">Название автомата</span>
            )
        }),
        columnHelper.accessor("serialNumber",{
            cell: (info) => info.getValue(),
            header: () => (
                <span className="flex justify-center text-black font-bold">Модель</span>
            )
        }),
        columnHelper.accessor("manufacturer",{
            cell: (info) => (
                <span className="text-blue-400">{info.getValue()}</span>
            ),
            header: () => (
                <span className="flex justify-center text-black font-bold">Компания</span>
            )
        }),
        columnHelper.accessor("inventoryNumber",{
            cell: (info) => info.getValue(),
            header: () => (
                <span className="flex justify-center text-black font-bold">Модем</span>
            )
        }),
        columnHelper.accessor("location",{
            cell: (info) => info.getValue(),
            header: () => (
                <span className="flex justify-center text-black font-bold">Адрес/Место</span>
            )
        }),
        columnHelper.accessor("manufactureDate",{
            cell: (info) => info.getValue(),
            header: () => (
                <span className="flex justify-center text-black font-bold">В работе с</span>
            )
        }),
        columnHelper.display({
            id: "actions",
            header: () => (
                <span className="flex justify-center text-black font-bold">
                    Действия
                </span>
            ),
            cell: ({ row }) => (
                <div className="flex justify-center items-center">
                    <button
                        className="p-1 cursor-pointer"
                        onClick={() => handleEdit(row.original)}
                    >
                        <Pen size={14} color="blue"/>
                    </button>
                    <button
                        className="p-1 cursor-pointer"
                        onClick={() => handleDelete(row.original)}
                    >
                        <Trash size={14} color="blue"/>
                    </button>
                    <button
                        className="p-1 cursor-pointer"
                        onClick={() =>handleUnlock(row.original)}
                    >
                        <Unlock size={14} color="blue"/>
                    </button>
                </div>
                ),
                enableSorting: true,
                enableColumnFilter: true,
        }),
    ], [handleDelete]);

    const table = useReactTable<IVendingMachines>({
        data,
        columns,
        state: {
            sorting,
            globalFilter,
        },
        initialState: {
            pagination: {
                pageSize: 5,
            },
        },
        getCoreRowModel: getCoreRowModel(),
        onSortingChange: setSorting,
        getSortedRowModel: getSortedRowModel(),
        onGlobalFilterChange: setGlobalFilter,
        getFilteredRowModel: getFilteredRowModel(),
        getPaginationRowModel: getPaginationRowModel(),
    });

    const pagination = table.getState().pagination;
    const pageIndex = pagination.pageIndex;
    const pageSize = pagination.pageSize;
    const totalRows = table.getFilteredRowModel().rows.length;
    const currentRows = table.getRowModel().rows.length;
    const from = totalRows === 0 ? 0 : pageIndex * pageSize + 1;
    const to = pageIndex * pageSize + currentRows;

    useEffect(() => {
        const getMachines = async () => {
            const data = await fetch("http://localhost:5208/api/VendingMachines")
            const vendingMachines = await data.json()
            console.log(vendingMachines)
            setVendingMachines(vendingMachines)
        }
        getMachines();
    }, [])
    
    
    return(
        <section className="flex-1 flex flex-col">
            <TitleBar title="Администрирование / Торговые автоматы"/>
            <div className="">
                <div className="mx-60">
                    <Module2
                        title={
                            <div className="flex flex-col bg-gray-50 p-4">
                                <span className="text-blue-400 text-2xl">Торговые автоматы</span>
                                <span className="text-black text-sm">Всего найдено {vendingMachines.length}</span>
                            </div>
                        }
                        isRows={isRows}
                        setIsRows={() => setIsRows(!isRows)}
                    >
                        <div className="text-black p-2">
                            <div className="flex flex-row gap-20 justify-between items-center">
                                <div className="flex items-center justify-center">
                                    <span className="mr-2">Показать</span>
                                    <select
                                        className="border border-gray-300 w-16 h-8"
                                        value={table.getState().pagination.pageSize}
                                        onChange={(e) => {
                                            table.setPageSize(Number(e.target.value))
                                        }}
                                    
                                    >
                                        {[5,10,20,30,50].map((pageSize) => (
                                            <option value={pageSize} key={pageSize}>
                                                {pageSize}
                                            </option>
                                        ))}
                                    </select>
                                    <span className="ml-2">записей</span>
                                </div>
                                <div className="w-100">
                                    <input 
                                        value={globalFilter ?? ""}
                                        onChange={(e) => setGlobalFilter(e.target.value)}
                                        placeholder="Фильтр"
                                        className="w-full px-4 py-2 border border-gray-300 placeholder-grey-50"
                                    />
                                </div>
                                <div className="flex flex-row items-center justify-center gap-5">
                                    <button className="flex flex-row gap-1 border border-black p-2">
                                        <Plus />
                                        Добавить
                                    </button>
                                    <ExportButton table={table}/>
                                </div>
                            </div>
                            <div className="my-8">
                                <table className="w-full">
                                    <thead className="bg-gray-50">
                                        {table.getHeaderGroups().map(HeaderGroup => (
                                            <tr key={HeaderGroup.id}>
                                                {HeaderGroup.headers.map((header) => (
                                                    <th key={header.id} className="px-6 py-3 text-left text-xs font-medium text-gray-500 tracking-wider">
                                                        <div
                                                            {...{
                                                                className: "cursor-pointer select-none flex items-center justify-center",
                                                                onClick: header.column.getToggleSortingHandler(),
                                                            }}
                                                        >
                                                            {flexRender(
                                                                header.column.columnDef.header,
                                                                header.getContext()
                                                            )}
                                                            <ArrowUpDown className="ml-2" size={14}/>
                                                        </div>
                                                    </th>
                                                ))}
                                            </tr>)
                                        )}
                                    </thead>
                                    <tbody className="bg-white divide-y divide-gray-200 border-y-2 border-black">
                                        {
                                            table.getRowModel().rows.map((row) => (
                                                <tr key={row.id} className="odd:bg-gray-50">
                                                    {row.getVisibleCells().map((cell) => (
                                                        <td key={cell.id} className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                                            {flexRender(cell.column.columnDef.cell, cell.getContext())}
                                                        </td>
                                                    ))}
                                                </tr>
                                            ))
                                        }
                                    </tbody>
                                </table>
                            </div>
                            <div className="flex justify-between">
                                <div className="flex items-end flex-row gap-1 text-xs font-semibold">
                                    <span>Записи с </span>
                                    <span>{from}</span>
                                    <span> до </span>
                                    <span>{to}</span>
                                    <span> из </span>
                                    <span>{totalRows}</span>
                                    <span> записей</span>
                                </div>
                                <div className="flex flex-row">
                                    <button
                                        className="p-2 border-2 border-gray-200 hover:bg-gray-200 disabled:opacity-50"
                                        onClick={() => table.previousPage()}
                                        disabled={!table.getCanPreviousPage()}
                                    >
                                        <ChevronLeft size={20} />
                                    </button>
                                    <span className="flex items-center bg-blue-400 px-3 text-white">
                                        <span> {table.getState().pagination.pageIndex + 1}</span>
                                    </span>
                                    <button
                                        className="p-2 border-2 border-gray-200 hover:bg-gray-200 disabled:opacity-50"
                                        onClick={() => table.nextPage()}
                                        disabled={!table.getCanNextPage()}
                                    >
                                        <ChevronRight size={20} />
                                    </button>
                                </div>
                            </div>
                        </div>
                    </Module2>
                </div>
            </div>
        </section>
    )
}