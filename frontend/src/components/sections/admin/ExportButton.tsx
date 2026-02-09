import { Forward } from "lucide-react";
import { useEffect, useRef, useState } from "react";
import DropdownButton from "../../DropdownButton";
import jsPDF from "jspdf";
import autoTable from "jspdf-autotable";

function exportToPDF(table: unknown) {
  const doc = new jsPDF();

  const headers = table
    .getAllColumns()
    .filter(col => col.getIsVisible())
    .map(col => col.columnDef.header as string);

  const rows = table.getFilteredRowModel().rows.map(row =>
    table
      .getAllColumns()
      .filter(col => col.getIsVisible())
      .map(col => row.getValue(col.id))
  );

  autoTable(doc, {
    head: [headers],
    body: rows,
  });

  doc.save("table.pdf");
}


function exportToCSV(table: unknown, filename = "table.csv") {
  const rows = table.getFilteredRowModel().rows;

  const headers = table
    .getAllColumns()
    .filter(col => col.getIsVisible())
    .map(col => col.id);

  const csv = [
    headers.join(","),

    ...rows.map(row =>
      headers
        .map(header => {
          const value = row.getValue(header);
          return `"${String(value ?? "").replace(/"/g, '""')}"`;
        })
        .join(",")
    ),
  ].join("\n");

  const blob = new Blob([csv], { type: "text/csv;charset=utf-8;" });
  const url = URL.createObjectURL(blob);

  const link = document.createElement("a");
  link.href = url;
  link.download = filename;
  link.click();

  URL.revokeObjectURL(url);
}

function exportToHTML(table: unknown) {
  const headers = table
    .getAllColumns()
    .filter(col => col.getIsVisible());

  const rows = table.getFilteredRowModel().rows;

  const html = `
    <!DOCTYPE html>
    <html lang="ru">
        <head>
            <meta charset="UTF-8" />
            <title>Экспорт таблицы</title>
            <style>
                table { border-collapse: collapse; width: 100%; }
                th, td { border: 1px solid #000; padding: 6px; }
                th { background: #f3f4f6; }
            </style>
        </head>
        <body>
            <table>
                <thead>
                    <tr>
                    ${headers.map(h => `<th>${h.id}</th>`).join("")}
                    </tr>
                </thead>
                <tbody>
                    ${rows.map(row =>
                        `<tr>
                            ${headers.map(col => `<td>${row.getValue(col.id) ?? ""}</td>`).join("")}
                        </tr>`
                        ).join("")}
                </tbody>
            </table>
        </body>
    </html>
    `;

  const blob = new Blob([html], { type: "text/html" });
  const url = URL.createObjectURL(blob);

  const a = document.createElement("a");
  a.href = url;
  a.download = "table.html";
  a.click();

  URL.revokeObjectURL(url);
}

export default function ExportButton(props: {table: unknown}) {
    const [open, setOpen] = useState(false);
    const ref = useRef<HTMLDivElement>(null);

    useEffect(() => {
        const handleClickOutside = (e: MouseEvent) => {
            if (ref.current && !ref.current.contains(e.target as Node)) {
                setOpen(false);
            }
        };
        document.addEventListener("mousedown", handleClickOutside);
        return () => document.removeEventListener("mousedown", handleClickOutside);
    }, []);
    
    return (
        <div className="relative"  ref={ref}>
            <button className="flex flex-row gap-1 border border-black p-2 cursor-pointer hover:bg-gray-50" onClick={() => {setOpen(!open)}}>
                <Forward />
                Экспорт
            </button>
            {open && (
                <div className="absolute flex flex-col top-full right-0 w-full bg-white rounded-b-md border-t border-gray-200 shadow-lg mt-0.1 overflow-hidden z-100">
                    <DropdownButton onClick={() => exportToCSV(props.table)}>
                        <span>CSV</span>
                    </DropdownButton>
                    <DropdownButton onClick={() => exportToPDF(props.table)}>
                        <span>PDF</span>
                    </DropdownButton>
                    <DropdownButton onClick={() => exportToHTML(props.table)}>
                        <span>HTML</span>
                    </DropdownButton>
                </div>
            )}
        </div>
    )
}