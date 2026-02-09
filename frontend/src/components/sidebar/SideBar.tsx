import { useState } from "react"
import { Search, FileText, ShoppingCart, Settings, Monitor } from 'lucide-react'
import SidebarItem from "./SideBarItem"
import SideBarDropdownItem from "./SideBarDropdownItem"


export default function SideBar() {
  const [isShort, setIsShort] = useState(false)

  return (
    <div>
      <div className="p-4 flex justify-between text-gray-300 w-64 h-max bg-gray-900" onClick={() => setIsShort(prev => !prev)}>
        <span>Навигация (?)</span>
        <span className={`transition-transform duration-200 ${isShort ? '-rotate-90' : 'rotate-90'}`}>▾</span>
      </div>

        <div className={`bg-gray-900 min-h-screen h-full pt-3 ${isShort ? 'w-16' : 'w-64'}`}>
            <div className="flex flex-col gap-7">
                <SidebarItem
                    icon={<Search size={22} />}
                    label="Главная"
                    isShort={isShort}
                    path='/'
                />
                <SidebarItem
                    icon={<Monitor size={22} />}
                    label="Мониторинг ТА"
                    isShort={isShort}
                    path='/monitoring'
                />
                <SideBarDropdownItem
                    icon={<FileText size={22} />}
                    label="Детальные отчёты"
                    isShort={isShort}
                    items={["Детальные отчёты"]}
                    path='/reports'
                />
                <SideBarDropdownItem
                    icon={<ShoppingCart size={22} />}
                    label="Учет ТМЦ"
                    isShort={isShort}
                    items={["Учет ТМЦ"]}
                    path='/tmc'
                />
                <SideBarDropdownItem
                    icon={<Settings size={22} />}
                    label="Администрирование"
                    isShort={isShort}
                    items={["Торговые автоматы", "Компании", "Пользователи", "Модемы", "Дополнительные"]}
                    path="/admin"
                />
        </div>
      </div>
    </div>
  )
}