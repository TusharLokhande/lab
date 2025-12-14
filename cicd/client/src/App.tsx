import React from "react";
import { Button } from "@/components/ui/button";
import Layout from "./layout/Layout";
import { ThemeProvider } from "next-themes";
import AppRouter from "./route";
import { Dropdown } from "./components/Dropdown";
import { Input } from "./components/ui/input";


const App = () => {
  return (
    <ThemeProvider attribute="class" storageKey="ui-theme">
      <AppRouter/>
    </ThemeProvider>
  );
};

export default App;
