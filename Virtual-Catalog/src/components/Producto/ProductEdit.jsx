import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { getProductoById, updateProducto } from "../../service/productApi";
import ProductForm from "./ProductForm/ProductForm";

const ProductEdit = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [initialProduct, setInitialProduct] = useState(null);

  useEffect(() => {
    const fetchProduct = async () => {
      try {
        const product = await getProductoById(id);
        setInitialProduct(product);
      } catch (err) {
        console.error("Error fetching product:", err);
      }
    };
    fetchProduct();
  }, [id]);

  const handleUpdate = async (product) => {
    await updateProducto(id, product);
    navigate("/virtual-catalog/products");
  };

  if (!initialProduct) return <p>Loading...</p>;

  return <ProductForm initialProduct={initialProduct} onSubmit={handleUpdate} isEdit />;
};

export default ProductEdit;
