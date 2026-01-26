import Logo from "./Logo";
import UserDropdown from "./UserDropdown";
import Header from "./Header";
import { Outlet } from "react-router-dom";
import Main from "./Main";
import SideBar from "./SideBar";
import SectionHeader from "./SectionHeader"

export default function Layout() {
    return (
        <>
            <Header>
                <Logo/>
                <UserDropdown/>
            </Header>
            <Main>
                <SideBar />
                <SectionHeader />
                <Outlet />
            </Main>
        </>
    )
}