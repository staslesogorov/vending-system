export default function Header(props: {children: React.ReactNode}) {
    return (
        <header className='flex justify-between items-center px-3 border-b text-black border-gray-200 bg-white'>
            {props.children}
        </header>
    )
}