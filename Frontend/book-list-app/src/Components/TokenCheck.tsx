import React from "react";

interface TokenCheckProps {
  children: React.ReactNode;
}

const TokenCheck: React.FC<TokenCheckProps> = ({ children }) => {
  const token = localStorage.getItem("token");
  const expiresAtString = localStorage.getItem("jwtExpiresAt");

  if (!token || !expiresAtString) {
    localStorage.removeItem("token");
    localStorage.removeItem("jwtExpiresAt");
    window.location.href = "/login";
    return null;
  }

  // Convert ISO timestamp to Date object
  const expiresAtTime = new Date(expiresAtString).getTime(); // Converts to milliseconds
  const currentTime = Date.now(); // Also in milliseconds

  if (currentTime >= expiresAtTime) {
    localStorage.removeItem("token");
    localStorage.removeItem("jwtExpiresAt");
    window.location.href = "/login";
    return null;
  }

  return <>{children}</>;
};

export default TokenCheck;
