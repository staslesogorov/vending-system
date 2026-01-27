import { ToastContainer } from "react-toastify";
import Layout from "./components/Layout";
import Login from "./components/Login";
import NotFound from "./components/NotFound";
import { BrowserRouter, Route, Routes} from 'react-router-dom'
import Home from "./components/Home"

export default function App() {
  return (
    <>
      <BrowserRouter>
        <Routes>
            <Route path="/login" element={<Login />} />

            <Route path="/" element={<Layout />}>
              <Route index element={<Home />} />
              {/* <Route path="monitoring" element={<Monitoring />} /> */}
            </Route>

            <Route path="*" element={<NotFound />} />
        </Routes>
      </BrowserRouter>
      
      <ToastContainer
        position="top-right"
        autoClose={3000}
        hideProgressBar={false}
        newestOnTop={false}
        closeOnClick
        rtl={false}
        pauseOnFocusLoss
        draggable
        pauseOnHover
      />
    </>
  )
}