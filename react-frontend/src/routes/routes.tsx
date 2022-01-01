import { Routes, Route, Navigate } from "react-router-dom";
import { Login, Register, Contact, Home, NotFound } from "../pages";
import { RoutePaths } from "./paths";

interface RoutesProp {
  isLoggedIn?: boolean;
}

export const RoutesWrapper: React.FC<RoutesProp> = ({ isLoggedIn = false }) => {

  return (
    <Routes>
      <Route
        path={RoutePaths.home}
        element={isLoggedIn ? <Home /> : <Navigate to="/login" state={{isFromLogOff: true}}/>}
      />
      <Route path={RoutePaths.contact} element={<Contact />} />
      <Route path={RoutePaths.register} element={<Register />} />
      <Route path={RoutePaths.login} element={<Login />} />
      <Route path="*" element={<NotFound />} />
    </Routes>
  );
};
