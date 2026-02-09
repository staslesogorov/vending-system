import { Grid3x3, Rows3 } from "lucide-react";
export default function Module2(props: { children?: React.ReactNode, title?: React.ReactNode, isRows: boolean, setIsRows: () => void}) {
    return (
        <div className="bg-white mx-auto w-full m-5">
            <div className="relative border-t-2 border-blue-400">
                {props.title}
                <div className="absolute right-2 top-2">
                    <button onClick={props.setIsRows}><Grid3x3 color={`${props.isRows ? "black" : "blue"}`} /></button>
                    <button onClick={props.setIsRows}><Rows3 color={`${props.isRows ? "blue" : "black"}`} /></button>
                </div>
            </div>
            <div className="p-2">
                {props.children}
            </div>
        </div>
    )
}