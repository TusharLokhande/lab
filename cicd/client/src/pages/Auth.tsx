import microsoftLogo from "@/assets/microsoft.svg";
import googleLogo from "@/assets/google.svg";
import { MsalService } from "@/services/msalService";
import { userStore } from "@/store/userStore";
import { useNavigate } from "react-router-dom";
import api from "@/helpers/APICall";

export default function Auth() {
  const { setUser } = userStore();
  const navigate = useNavigate();

  const handleMicrosoftLogin = async () => {
    try {
      const loginResult = await MsalService.login();

      if (loginResult) {
        const obj = {
          name: loginResult.name,
          email: loginResult.email,
          phoneNumber: loginResult.phoneNumber,
          authProvider: {
            microsoftId: loginResult.id,
            AuthProviderType: "microsoft",
          },
        };

        const { status, data: responseData } = await api.post(
          "/auth/login",
          obj
        );

        if (status === 200) {
          const { data } = responseData;
          if (data) {
            setUser({
              name: String(data?.name),
              email: String(data?.email),
              roles: [],
              loginType: "microsoft",
            });
            navigate("/");
          }
        }
      }
    } catch (error) {
      console.error("Error while login in microsoft ", error);
    }
  };

  return (
    <div className="grid min-h-screen grid-cols-1 md:grid-cols-2">
      {/* Left Section (Brand / Illustration Background) */}
      <div className="hidden md:flex items-center justify-center bg-linear-to-br from-primary to-accent text-primary-foreground">
        <h1 className="text-4xl font-bold">Personal Finance</h1>
      </div>

      {/* Right Section (Login Panel) */}
      <div className="flex flex-col justify-center px-8 md:px-16 lg:px-24">
        <div className="max-w-sm w-full mx-auto">
          <h2 className="text-3xl font-semibold">Login</h2>
          <p className="text-muted-foreground mt-1 mb-8">
            Welcome to Personal Finance
          </p>

          <button
            onClick={handleMicrosoftLogin}
            className="w-full bg-primary text-primary-foreground rounded-lg py-3 font-medium flex items-center justify-center gap-3 hover:opacity-80 transition"
          >
            <img src={microsoftLogo} alt="" className="h-5" />
            Sign with Microsoft
          </button>

          <button className="w-full bg-muted text-foreground mt-4 rounded-lg py-3 font-medium flex items-center justify-center gap-3 hover:bg-muted-foreground/10 transition">
            <img src={googleLogo} alt="" className="h-5" />
            Sign with Google
          </button>
        </div>
      </div>
    </div>
  );
}
