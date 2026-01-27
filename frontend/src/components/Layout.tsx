import Logo from "./header/Logo";
import UserDropdown from "./header/UserDropdown";
import Header from "./header/Header";
import { Outlet } from "react-router-dom";
import Main from "./Main";
import SideBar from "./sidebar/SideBar";

export default function Layout() {
    return (
        <>
            <Header>
                <Logo/>
                <UserDropdown/>
            </Header>
            <Main>
                <SideBar />
                <Outlet />
            </Main>
        </>
    )
}