import { useEffect, useState } from "react";
import Module from "../Module";
import SectionHeader from "../SectionHeader";
import TitleBar from "../TitleBar";
import type IVendingMachines from "../../../interfaces/IVendingMachines";
import NewsItem from "./NewsItem";
import InfoItem from "./InfoItem";

export default function Home() {
    const [vendingMachines, setVendingMachines] = useState<IVendingMachines[]>([])
    const successMachines = vendingMachines.filter(m => m.status === "Работает")
    const brokenMachines = vendingMachines.filter(m => m.status === "Вышел из строя")
    const serviceMachines = vendingMachines.filter(m => m.status === "В ремонте/на обслуживании")

    useEffect(() => {
        const getMachines = async () => {
            const data = await fetch("http://localhost:5208/api/VendingMachines")
            const vendingMachines = await data.json()
            setVendingMachines(vendingMachines)
        }

        getMachines();
    }, [])

    return (
        <section className="flex-1 flex flex-col">
            <TitleBar title="Главная"/>
            <div className="mx-12">
                <SectionHeader title="Личный кабинет. Главная"/>

                <div className="grid grid-cols-3 gap-5">
                    <Module title="Эффективность сети" className="col-span-1">
                        <span> Все автоматы: {vendingMachines.length} </span>
                        <span> Работающие автоматы: {successMachines.length} </span>
                        <span> Процент: {Math.round((successMachines.length / vendingMachines.length) * 100)} % </span>
                    </Module>
                    <Module title="Состояние сети" className="col-span-1">
                        <span> Работающие автоматы: {successMachines.length} </span>
                        <span> Автоматы на ремонте/обслуживании: {serviceMachines.length} </span>
                        <span> Сломанные автоматы: {brokenMachines.length} </span>
                    </Module>
                    <Module title="Сводка..." className="col-span-1">
                        <InfoItem title="Денег в ТА" value="0 р." />
                        <InfoItem title="Сдача в ТА" value="0 р." />
                        <InfoItem title="Выручка, сегодня" value="0 р." />
                        <InfoItem title="Выручка, вчера" value="0 р." />
                        <InfoItem title="Инкасированно, сегодня" value="0 р." />
                        <InfoItem title="Инкасированно, вчера" value="0 р." />
                        <InfoItem title="Обслужено ТА, сег./вчера" value="0 / 0" />
                    </Module>
                    <Module title="Динамика продаж за последние 10 дней..." className="col-span-2"> 
                        <></>
                    </Module>
                    <Module title="Новости..." className="col-span-1">
                        <NewsItem date="01.01.25" description="ТекстТекстТекстТекстТекстТекстТекстТекст"/>
                        <NewsItem date="01.01.25" description="ТекстТекстТекстТекстТекстТекстТекстТекст"/>
                        <NewsItem date="01.01.25" description="ТекстТекстТекстТекстТекстТекстТекстТекст"/>
                        <NewsItem date="01.01.25" description="ТекстТекстТекстТекстТекстТекстТекстТекст"/>
                        <NewsItem date="01.01.25" description="ТекстТекстТекстТекстТекстТекстТекстТекст"/>
                        <NewsItem date="01.01.25" description="ТекстТекстТекстТекстТекстТекстТекстТекст"/>
                    </Module>
                </div>

            </div>
        </section>
    )
}