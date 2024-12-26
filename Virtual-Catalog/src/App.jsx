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
import AuthLayout from "./components/layouts/AuthLayout";

const App = () => {
  return (
    <>
        <BrowserRouter>
          {/* <MainHeader /> */}
          <Header />
          <Routes>
            <Route index path="/" element={<Home />} />
            <Route path="/categories" element={<CategoryPage />} />
            <Route path="/categories/create" element={<CategoryCreate />} />
            <Route path="/categories/:id/edit" element={<CategoryEdit />} />
            <Route path="/users" element={<UserPage />} />
            <Route path="/users/create" element={<UserCreate />} />
            <Route path="/users/:id/edit" element={<UserEdit />} />
            <Route path="/orders" element={<OrderPage />} />
            <Route path="/products" element={<ProductPage />} />
            <Route path="/categories/:categoryId/products" element={<ProductPage />} />
            <Route path="/products/create" element={<ProductCreate />} />
            <Route path="/products/:id/edit" element={<ProductEdit />} />
            <Route path="/AuthLayout" element={<AuthLayout />} />

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
