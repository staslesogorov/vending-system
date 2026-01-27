import SectionHeader from "./SectionHeader";
import TitleBar from "./TitleBar";

export default function Home() {
    return (
        <section className="flex-1 flex flex-col">
            <TitleBar title="Главная"/>
            <div className="mx-12">
                <SectionHeader title="Личный кабинет. Главная"/>



            </div>
        </section>
    )
}