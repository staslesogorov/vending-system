import { useState, useRef, useEffect } from "react"

export default function Header() {
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
        <header className='flex justify-between items-center px-3 border-b text-black border-gray-400'>
            <img src="/logo.png" alt="Лого" width={50} height={50} className="ml-5"/>

            <div className="relative" ref={ref}>
                <div className="flex items-center gap-2 cursor-pointer p-2.5" onClick={() => {setOpen(!open)}}>
                    <img
                    src="https://flagcdn.com/w20/ru.png"
                    alt="ru"
                    width={20}
                    height={14}
                    />
                    <div>
                        <div className="text-sm">Автоматов А.А.</div>
                        <div className="text-gray-500 text-xs">Администратор</div>
                    </div>
                    <span className={`transition-transform duration-200 ${open ? 'rotate-180' : ''}`}>▾</span>
                </div>

                {open && (
                    <div className="absolute top-full right-0 w-full bg-white rounded-b-md border-t border-gray-400 shadow-lg mt-0.1 overflow-hidden z-100">
                        <div className="px-3.5 py-2.5 text-sm cursor-pointer hover:bg-gray-100">Мой профиль</div>
                        <div className="px-3.5 py-2.5 text-sm cursor-pointer hover:bg-gray-100">Мои сессии</div>
                        <div className="px-3.5 py-2.5 text-sm cursor-pointer hover:bg-gray-100 ">Выход</div>
                    </div>
                )}
            </div>
  
        </header>
    )
}