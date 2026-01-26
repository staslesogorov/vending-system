export default function Main(props: {children: React.ReactNode}) {
    return(
        <div className="flex">{props.children}</div>
    )
}