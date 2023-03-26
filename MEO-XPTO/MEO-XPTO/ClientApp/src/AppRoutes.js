import { Home } from "./components/Home";
import { Formularios } from "./components/Formularios";
import CreateFormulario from "./components/CreateFormulario";
import Formulario from "./components/Formulario";
import Register from "./components/Register";
import Login from "./components/Login";

const AppRoutes = [
    {
        index: true,
        element: <Home />
    },
    {
        path: '/formularios-data',
        element: <Formularios />

    },
    {
        path: '/formularios-create',
        element: <CreateFormulario />
    },
    {
        path: '/formularios-data/:id',
        element: <Formulario />
    },
    {
        path: '/users-create',
        element: <Register />
    },
    {
        path: '/users-login',
        element: <Login />
    }
];

export default AppRoutes;
