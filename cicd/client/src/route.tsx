import { Route, Routes, useNavigate } from "react-router-dom";
import UsersPage from "./pages/UserPage";
import Auth from "./pages/Auth";
import Layout from "./layout/Layout";
import { useEffect } from "react";
import { userStore } from "./store/userStore";

const AppRouter = () => {
  const { user } = userStore();
  const navigate = useNavigate()

  useEffect(() => {
    if(!user) {
      navigate("/auth")
    }
  }, []);

  return (
    <div>
      <Routes>
        <Route element={<Layout />}>
          <Route index element={<>Home</>} />
          <Route path="/user" element={<UsersPage />} />
        </Route>

        <Route path="/auth" element={<Auth />} />
      </Routes>
    </div>
  );
};

export default AppRouter;
