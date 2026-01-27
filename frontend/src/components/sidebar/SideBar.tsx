import { useState } from "react"
import { Search, FileText, ShoppingCart, Settings, Monitor } from 'lucide-react'
import SidebarItem from "./SideBarItem"
import { useNavigate } from "react-router-dom"


export default function SideBar() {
  const [isShort, setIsShort] = useState(false)
  const navigate = useNavigate()

  return (
    <div>
      <div className="p-4 flex justify-between text-gray-300 w-64 h-max bg-gray-900" onClick={() => setIsShort(prev => !prev)}>
        <span>Навигация</span>
        <span className={`transition-transform duration-200 ${isShort ? '-rotate-90' : 'rotate-90'}`}>▾</span>
      </div>

        <div className={`bg-gray-900 h-screen pt-3 ${isShort ? 'w-16' : 'w-64'}`} onClick={() => setIsShort(prev => !prev)}>
            <div className="flex flex-col gap-7">
                <SidebarItem
                    icon={<Search size={22} />}
                    label="Главная"
                    isShort={isShort}
                    onClick={() => navigate('/')}
                />
                <SidebarItem
                    icon={<Monitor size={22} />}
                    label="Мониторинг ТА"
                    isShort={isShort}
                    onClick={() => navigate('/monitoring')}
                />
                <SidebarItem
                    icon={<FileText size={22} />}
                    label="Детальные отчёты"
                    isShort={isShort}
                    onClick={() => navigate('/reports')}
                />
                <SidebarItem
                    icon={<ShoppingCart size={22} />}
                    label="Учет ТМЦ"
                    isShort={isShort}
                    onClick={() => navigate('/tmc')}
                />
                <SidebarItem
                    icon={<Settings size={22} />}
                    label="Администрирование"
                    isShort={isShort}
                    onClick={() => navigate('/admin')}
                />
        </div>
      </div>
    </div>
  )
}