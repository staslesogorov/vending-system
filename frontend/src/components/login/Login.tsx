import Header from "../header/Header";
import LoginForm from "./LoginForm";
import Logo from "../header/Logo";

export default function Login() {
    return (
        <>
            <Header>
                <Logo/>
            </Header>
            <LoginForm/>
        </>
    )
}