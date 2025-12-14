import { create } from "zustand";
import { persist, createJSONStorage } from "zustand/middleware";

type User = {
  id?: string;
  name: string;
  email: string;
  roles: string[];
  loginType: "microsoft" | "google"
};

type UserState = {
  user: User | null;
  setUser: (user: User) => void;
  clearUser: () => void;
};

export const userStore = create<UserState>()(
  persist(
    (set) => ({
      user: null,
      setUser: (user) =>
        set(() => ({
          user,
        })),
      clearUser: () =>
        set(() => ({
          user: null,
        })),
    }),
    {
      name: "user-storage",
      storage: createJSONStorage(() => sessionStorage),
      partialize: (state) => ({
        user: state.user,
      }),
    }
  )
);
