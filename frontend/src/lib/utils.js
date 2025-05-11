import { clsx } from "clsx";
import { twMerge } from "tailwind-merge"
import { format } from "date-fns";

export function cn(...inputs) {
  return twMerge(clsx(inputs));
}

export const formatDate = (date, style = "iso") => {
  if (!date) return "";
  const newDate = new Date(date);

  switch (style) {
    case "iso":
      return format(newDate, "yyyy-MM-dd");
    case "display":
      return newDate.toLocaleDateString("en-GB");
    default:
      return newDate.toISOString();
  }
};

export const handleApiError = (error, fallbackMessage = "Something went wrong") => {
  return (
    error?.response?.data?.errors ||
    error?.response?.data?.message ||
    error?.message ||
    fallbackMessage
  );
};

export const getInitialJobData = () => ({
  companyName: "",
  position: "",
  status: "",
  dateApplied: "",
});
