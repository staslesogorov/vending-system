export default function TitleBar(props: {title: string}) {
    return (
        <div className=" bg-gray-950 h-max p-3 flex flex-row items-center justify-between" >
            <span className="text-gray-300 text-2xl">OOO Торговые Автоматы</span>
            <span className="text-gray-500">{props.title}</span>
        </div>
    )
}