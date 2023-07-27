import React from "react";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import Root from "./components/pages/RootPage";
import ProjectsPage from "./components/pages/ProjectsPage";

const routes = createBrowserRouter([
  {
    path: "",
    element: <Root />,
    children: [
      {
        index: true,
        element: <ProjectsPage />,
      },
    ],
  },
]);

const App = () => {
  return <RouterProvider router={routes}></RouterProvider>;
};

export default App;
