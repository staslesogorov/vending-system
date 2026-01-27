import {useState, useEffect, useRef} from 'react';
import { useNavigate } from 'react-router-dom';
import type IUser from '../interfaces/IUser';

export default function UserDropdown() {
    const [open, setOpen] = useState(false);
    const ref = useRef<HTMLDivElement>(null);
    const navigate = useNavigate();
    const [currentUser, setCurrentUser] = useState<IUser|null>(() => {
        const user = localStorage.getItem('user')
        return user ? JSON.parse(user) : null
    });

    useEffect(() => {
        const handleClickOutside = (e: MouseEvent) => {
            if (ref.current && !ref.current.contains(e.target as Node)) {
                setOpen(false);
            }
        };
        document.addEventListener("mousedown", handleClickOutside);
        return () => document.removeEventListener("mousedown", handleClickOutside);
    }, []);

    useEffect(() => {
        if (!currentUser) navigate('/login')
    }, [])

    const leaveHandler = () => {
        localStorage.removeItem('user')
        setCurrentUser(null)
        navigate('/login')
        return
    }

    return (
        <div className="relative" ref={ref}>
            <div className="flex items-center gap-2 cursor-pointer p-2.5 hover:bg-gray-50" onClick={() => {setOpen(!open)}}>
                <img
                src={currentUser ? currentUser.image : "https://flagcdn.com/w20/ru.png"}
                alt="ru"
                width={20}
                height={20}
                />
                <div>
                    <div className="text-sm">{currentUser ? `${currentUser.fullName.split(" ")[0]} ${currentUser.fullName.split(" ")[1][0]}. ${currentUser.fullName.split(" ")[1][0]}.` : ""}</div>
                    <div className="text-gray-500 text-xs">{currentUser ? currentUser.role : ""}</div>
                </div>
                <span className={`transition-transform duration-200 ${open ? 'rotate-180' : ''}`}>▾</span>
            </div>

            {open && (
                <div className="absolute flex flex-col top-full right-0 w-full bg-white rounded-b-md border-t border-gray-400 shadow-lg mt-0.1 overflow-hidden z-100">
                    <button className="px-3.5 py-2.5 text-sm cursor-pointer hover:bg-gray-50">Мой профиль</button>
                    <button className="px-3.5 py-2.5 text-sm cursor-pointer hover:bg-gray-50">Мои сессии</button>
                    <button className="px-3.5 py-2.5 text-sm cursor-pointer hover:bg-gray-50 text-red-500" onClick={leaveHandler}>Выход</button>
                </div>
            )}
        </div>

    )
}