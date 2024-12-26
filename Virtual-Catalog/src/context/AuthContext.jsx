import React, { createContext, useState, useEffect } from "react";

export const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [authData, setAuthData] = useState(null);

  useEffect(() => {
    const storedAuth = localStorage.getItem("auth");
    if (storedAuth) {
      setAuthData(JSON.parse(storedAuth));
    }
  }, []);

  const login = (data) => {
    localStorage.setItem("auth", JSON.stringify(data));
    setAuthData(data);
  };

  const logout = () => {
    localStorage.removeItem("auth");
    setAuthData(null);
  };

  return (
    <AuthContext.Provider value={{ authData, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};
