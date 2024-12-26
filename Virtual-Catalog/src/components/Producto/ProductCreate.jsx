import { useNavigate } from "react-router-dom";
import { createProducto } from "../../service/productApi";
import ProductForm from "./ProductForm/ProductForm";

const ProductCreate = () => {
  const navigate = useNavigate();

  const handleCreate = async (product) => {
    await createProducto(product);
    navigate("/products");
  };

  return <ProductForm onSubmit={handleCreate} />;
};

export default ProductCreate;
