import { Routes, Route } from "react-router-dom";
import { Login, Register, Contact, Home, NotFound } from "../pages";
import { RoutePaths } from "./paths";

export const RoutesWrapper = () => (
  <Routes>
    <Route path={RoutePaths.home} element={<Home />} />
    <Route path={RoutePaths.contact} element={<Contact />} />
    <Route path={RoutePaths.login} element={<Login />} />
    <Route path={RoutePaths.register} element={<Register />} />
    <Route path="*" element={<NotFound />} />
  </Routes>
);
