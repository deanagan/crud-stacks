import { Routes, Route } from "react-router-dom";
import { Login, Contact, Home } from "../pages";
import { RoutePaths } from "./paths";

export const RoutesWrapper = () => (
  <Routes>
    <Route path={RoutePaths.home} element={<Home />} />
    <Route path={RoutePaths.contact} element={<Contact />} />
    <Route path={RoutePaths.login} element={<Login />} />
  </Routes>
);
