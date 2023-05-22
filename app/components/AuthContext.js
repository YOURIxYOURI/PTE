import React, { createContext, useState } from "react";

export const AuthContext = createContext();

const AuthContextProvider = ({ children }) => {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [userData, setUserData] = useState(null);

  const login = (userData) => {
    setIsAuthenticated(true);
    setUserData(userData);
  };

  const logout = () => {
    setIsAuthenticated(false);
    setUserData(null);
  };

  return (
    <AuthContext.Provider value={{ isAuthenticated, userData, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};

export default AuthContextProvider;
