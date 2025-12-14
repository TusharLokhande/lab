import React from "react"
import Select, { type Props as ReactSelectProps, type StylesConfig } from "react-select"
import { cn } from "@/lib/utils"

type Option = { label: string; value: string | number }

type Props = ReactSelectProps<Option> & {
  className?: string
  isMulti: boolean
}

export function Dropdown({ className ,isMulti: boolean= false, ...props }: Props) {
  const customStyles: StylesConfig<Option, false> = {
    control: (base, state) => ({
      ...base,
      backgroundColor: "transparent",
      borderColor: state.isFocused ? "hsl(var(--ring))" : "hsl(var(--input))",
      height: "36px",
      minHeight: "36px",
      borderRadius: "0.375rem",
      paddingLeft: "0.75rem",
      boxShadow: state.isFocused
        ? "0 0 0 3px hsl(var(--ring) / 0.5)"
        : "none",
      transition: "border-color 0.2s, box-shadow 0.2s",
      outline: "none",
      cursor: "pointer",
    }),
    menu: (base) => ({
      ...base,
      backgroundColor: "hsl(var(--background))",
      borderRadius: "0.375rem",
      marginTop: "4px",
      border: "1px solid hsl(var(--input))",
      zIndex: 50,
    }),
    option: (base, state) => ({
      ...base,
      backgroundColor: state.isSelected
        ? "hsl(var(--accent))"
        : state.isFocused
        ? "hsl(var(--accent) / 0.6)"
        : "transparent",
      color:
        state.isSelected || state.isFocused
          ? "hsl(var(--accent-foreground))"
          : "hsl(var(--foreground))",
      cursor: "pointer",
      padding: "8px 12px",
    }),
    singleValue: (base) => ({
      ...base,
      color: "hsl(var(--foreground))",
    }),
    placeholder: (base) => ({
      ...base,
      color: "hsl(var(--muted-foreground))",
    }),
    input: (base) => ({
      ...base,
      color: "hsl(var(--foreground))",
    }),
    indicatorSeparator: () => ({ display: "none" }),
    dropdownIndicator: (base) => ({
      ...base,
      color: "hsl(var(--muted-foreground))",
      paddingRight: "8px",
    }),
  }

  return (
    <Select
      className={cn("min-w-0", className)}
      classNamePrefix="rs"
      styles={customStyles}
      isMulti={false}
      {...props}
    />
  )
}
