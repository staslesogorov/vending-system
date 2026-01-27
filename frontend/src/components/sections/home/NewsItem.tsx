export default function NewsItem(props: {date: string, description: string}) {
    return (
        <div className="flex justify-between gap-16">
            <span>{props.date}</span>
            <a className="flex-1 min-w-0 break-words underline" href="#">{props.description}</a>
        </div>
    )
}