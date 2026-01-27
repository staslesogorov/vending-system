import { useState } from "react";
import { toast } from "react-toastify"

export default function Module(props: {children: React.ReactNode, title: string, className: string}) {
    const [isHide, setIsHide] = useState(false)
    return (
        <>
        {!isHide && (
            <div className={`relative bg-white border border-gray-200 text-black ${props.className} min-h-50 pb-7`}>
                <div className="bg-gray-100 p-3 text-2xl">{props.title}</div>
                <div className="my-2 px-3 text-xs flex flex-col gap-1">{props.children}</div>
                <div className="absolute right-2 bottom-1">
                    <button className="hover:text-red-500 cursor-pointer" onClick={() => {toast("Панель скрыта"); setIsHide(!isHide)}}>x</button>
                </div>
            </div>
        )}
        </>
    )
}