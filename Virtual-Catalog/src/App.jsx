import { BrowserRouter, Route, Routes } from "react-router-dom";
import CategoryPage from "./pages/CategoryPage";
import CategoryCreate from "./components/Categoria/CategoryCreate";
import CategoryEdit from "./components/Categoria/CategoryEdit";
import UserPage from "./pages/UserPage";
import UserCreate from "./components/User/UserCreate";
import UserEdit from "./components/User/UserEdit";
import OrderPage from "./pages/OrderPage";
import ProductPage from "./pages/ProductPage";
import ProductCreate from "./components/Producto/ProductCreate";
import ProductEdit from "./components/Producto/ProductEdit";
import Home from "./pages/Home";

import Header from "./components/common/Header";
import AuthLayout from "./components/layouts/Login/AuthLayout";
import ResetPassword from "./components/layouts/Login/ResetPassword";

const App = () => {
  return (
    <>
        <BrowserRouter>
          {/* <MainHeader /> */}
          <Header />
          <Routes>
            <Route index path="/virtual-catalog/" element={<Home />} />
            <Route path="/virtual-catalog/categories" element={<CategoryPage />} />
            <Route path="/virtual-catalog/categories/create" element={<CategoryCreate />} />
            <Route path="/virtual-catalog/categories/:id/edit" element={<CategoryEdit />} />
            <Route path="/virtual-catalog/users" element={<UserPage />} />
            <Route path="/virtual-catalog/users/create" element={<UserCreate />} />
            <Route path="/virtual-catalog/users/:id/edit" element={<UserEdit />} />
            <Route path="/virtual-catalog/orders" element={<OrderPage />} />
            <Route path="/virtual-catalog/products" element={<ProductPage />} />
            <Route path="/virtual-catalog/categories/:categoryId/products" element={<ProductPage />} />
            <Route path="/virtual-catalog/products/create" element={<ProductCreate />} />
            <Route path="/virtual-catalog/products/:id/edit" element={<ProductEdit />} />
            <Route path="/virtual-catalog/authlayout" element={<AuthLayout />} />
            <Route path="/virtual-catalog/reset-password" element={<ResetPassword />} />

            {/* <Route
            path="/productoDetalles/:idproducto"
            element={<Carritos />}
          />
          <Route path="/carrito" element={<Carrito />} /> */}
          </Routes>

          {/* <MainFooter /> */}
        </BrowserRouter>

    </>
  );
};

export default App;
