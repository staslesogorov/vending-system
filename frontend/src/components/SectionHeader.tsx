import { useLocation } from 'react-router-dom'

const routeTitles: Record<string, string> = {
  '/': 'Главная',
  '/monitoring': 'Мониторинг ТА',
  '/reports': 'Детальные отчёты',
  '/tmc': 'Учёт ТМЦ',
  '/admin': 'Администрирование',
}

export default function SectionHeader() {
    const location = useLocation()
    const title = routeTitles[location.pathname]

    return (
        <div className="flex-1 bg-gray-950 h-max p-3 flex flex-row items-center justify-between" >
            <span className="text-gray-300 text-2xl">OOO Торговые Автоматы</span>
            <span className="text-gray-500">{title}</span>
        </div>
    )
}