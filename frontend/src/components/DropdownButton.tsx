export default function DropdownButton(props: {children: React.ReactNode, onClick: () => void, className?: string}) {
    return (
        <button className={`px-3.5 py-2.5 text-sm cursor-pointer hover:bg-gray-50 ${props.className}`} onClick={props.onClick}>
            <div className='flex items-center justify-center'>
                {props.children}
            </div>
        </button>
    )

}