export default function SidebarItem(props: {icon: React.ReactNode, label: string, isShort: boolean}) {
  return (
    <div
      className="flex items-center h-6 gap-3 text-gray-400 hover:text-white cursor-pointer px-4"
      onClick={e => e.stopPropagation()}
    >
      {props.icon}
      {!props.isShort && <span>{props.label}</span>}
    </div>
  )
}