import { useNavigate } from "react-router-dom";

export default function SidebarItem(props: {icon: React.ReactNode, label: string, isShort: boolean, path: string}) {
  const navigate = useNavigate();
  return (
    <div
      className="flex items-center h-6 gap-3 text-gray-400 hover:text-white cursor-pointer px-4"
      onClick={(e) => {e.stopPropagation(); navigate(props.path)}}
    >
      {props.icon}
      {!props.isShort && <span>{props.label}</span>}
    </div>
  )
}