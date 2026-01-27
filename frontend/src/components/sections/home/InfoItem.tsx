export default function NewsItem(props: {title: string, value: string}) {
    return (
        <div className="flex justify-between">
            <span>{props.title}</span>
            <span>{props.value}</span>
        </div>
    )
}