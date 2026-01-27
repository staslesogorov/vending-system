import { useNavigate } from "react-router-dom"

export default function NotFound() {
    const navigate = useNavigate()

    return(
        <div className="flex items-center justify-center h-screen">
            <button className="text-black text-5xl font-bold cursor-pointer hover:text-gray-500" onClick={() => navigate('/')}>Not Found</button>
        </div>
    )
}