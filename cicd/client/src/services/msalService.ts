import { userStore } from "@/store/userStore";
import { PublicClientApplication } from "@azure/msal-browser";


const REDIRECT_URL = import.meta.env.VITE_REDIRECT_URI;

const msalConfig = {
  auth: {
    clientId: import.meta.env.VITE_AZURE_CLIENT_ID,
    authority: `https://login.microsoftonline.com/${
      import.meta.env.VITE_AZURE_TENANT_ID
    }`,
    redirectUri: REDIRECT_URL,
  },
  cache: {
    cacheLocation: "localStorage",
    storeAuthStateInCookie: false,
  },
};

const loginRequest = {
  scopes: [`api://${import.meta.env.VITE_AZURE_CLIENT_ID}/access_as_user`],
};

export const msalInstance = new PublicClientApplication(msalConfig);

msalInstance.addEventCallback((event) => {
  if (event.eventType === "msal:logoutSuccess") {
    const { clearUser } = userStore.getState();
    clearUser();
    window.location.href = REDIRECT_URL
  }
});

export class MsalService {
  static async login() {
    try {
      const result = await msalInstance.loginPopup(loginRequest);
      msalInstance.setActiveAccount(result.account);

      return {
        name: result?.account?.name,
        roles: result?.account?.idTokenClaims?.roles || [],
        email: result?.account?.idTokenClaims?.email,
        id: result?.account?.homeAccountId,
        phoneNumber: result?.account?.idTokenClaims?.phone_number,
      };
    } catch (err) {
      console.error("Error while login: ", err);
    }
    return null;
  }

  static async logout() {
    const account = msalInstance.getActiveAccount();
    await msalInstance.logoutRedirect({
      account,
      postLogoutRedirectUri: REDIRECT_URL,
    });
  }

  static async getActiveAccount() {
    const accounts = msalInstance.getAllAccounts();
    return accounts.length > 0 ? accounts[0] : null;
  }

  static async getAccessToken() {
    const account = await this.getActiveAccount();
    if (!account) return null;

    const request = { ...loginRequest, account };

    try {
      const result = await msalInstance.acquireTokenSilent(request);
      return result.accessToken;
    } catch (error) {
      console.warn("Silent token renewal failed -> redirecting login", error);
      await this.logout();
      return null;
    }
  }
}
