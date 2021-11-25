import { Routes, Route, Navigate } from "react-router-dom";
import { Login, Register, Contact, Home, NotFound } from "../pages";
import { RoutePaths } from "./paths";


interface RoutesProp {
  isLoggedIn?: boolean;
}

export const RoutesWrapper: React.FC<RoutesProp> = ({isLoggedIn = false}) => {
  //export const RoutesWrapper = () => (

  return (
    <Routes>
      <Route path={RoutePaths.home} element={isLoggedIn ? <Home /> : <Navigate to="/login" />} />
      <Route path={RoutePaths.contact} element={<Contact />} />
      <Route path={RoutePaths.login} element={<Login />} />
      <Route path={RoutePaths.register} element={<Register />} />
      <Route path="*" element={<NotFound />} />
    </Routes>
  );
};

// App.js
// import routes from './routes';
// import { useRoutes } from 'react-router-dom';

// function App() {
//   const { isLoggedIn } = useSelector((state) => state.auth);

//   const routing = useRoutes(routes(isLoggedIn));

//   return (
//     <>
//       {routing}
//     </>
//   );
// }

// routes.js

// import { Navigate,Outlet } from 'react-router-dom';

// const routes = (isLoggedIn) => [
//   {
//     path: '/app',
//     element: isLoggedIn ? <DashboardLayout /> : <Navigate to="/login" />,
//     children: [
//       { path: '/dashboard', element: <Dashboard /> },
//       { path: '/account', element: <Account /> },
//       { path: '/', element: <Navigate to="/app/dashboard" /> },
//       {
//         path: 'member',
//         element: <Outlet />,
//         children: [
//           { path: '/', element: <MemberGrid /> },
//           { path: '/add', element: <AddMember /> },
//         ],
//       },
//     ],
//   },
//   {
//     path: '/',
//     element: !isLoggedIn ? <MainLayout /> : <Navigate to="/app/dashboard" />,
//     children: [
//       { path: 'login', element: <Login /> },
//       { path: '/', element: <Navigate to="/login" /> },
//     ],
//   },
// ];

// export default routes;
