import { useState, type FormEvent } from "react";
import { useNavigate } from "react-router-dom"
import { toast } from "react-toastify";

export default function LoginForm() {
    const [login, setLogin] = useState("");
    const [password, setPassword] = useState("");
    const navigate = useNavigate();

    const handlerForm = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        const data = await fetch("http://localhost:5208/api/Auth", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                login: `${login}`,
                password: `${password}`
            })
            })

        if (data.status != 200) {
            toast("Неверные логин или пароль")
            return
        }

        const user = await data.json();
        localStorage.setItem("user", JSON.stringify(user))
        navigate("/")
    }

  return (
    <section className="m-auto min-h-150 flex flex-col justify-center items-center text-black">
        <div className="flex flex-col items-center bg-white px-8 py-12 border border-gray-400 ">
            <h1 className="mb-5 text-3xl"> Авторизация</h1>
            <form
                className="p-3 flex flex-col gap-3 justify-center items-center "
                onSubmit={(e) => handlerForm(e)}
            >
                <input
                className="border border-gray-400"
                type="text"
                placeholder="Логин"
                value={login}
                onChange={(e) => setLogin(e.target.value)}
                />
                <input
                className="border border-gray-400"
                type="password"
                placeholder="Пароль"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                />
                <button className="mt-5 p-2 w-full bg-blue-400 transition duration-200 hover:bg-blue-300" type="submit">
                Войти
                </button>
            </form>
        </div>
    </section>
  );
}