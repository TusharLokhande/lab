import { userStore } from "@/store/userStore";
import { MsalService } from "./msalService";

export class AuthService {
  static async logout() {
    const { user } = userStore.getState(); // <-- this is the fix

    if (user?.loginType === "microsoft") {
      await MsalService.logout();
    }
  }
}
