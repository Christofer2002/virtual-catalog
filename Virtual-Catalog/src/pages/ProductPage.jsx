import { useState, useEffect } from "react";
import { ToastContainer, toast } from "react-toastify";
import { FaTrash, FaEdit, FaPlus } from "react-icons/fa";
import { useParams, Link } from "react-router-dom";
import { getProductsByCategory, deleteProducto, getAllProductos } from "../service/productApi";
import CustomFooter from "../components/common/CustomFooter";
import { getCategoriaById } from "../service/categoryApi";
import ConfirmationModal from "../components/common/ConfirmationModal";

export default function ProductPage() {
  const authData = JSON.parse(localStorage.getItem("auth"));
  const userRole = authData?.role; // User rol

  // Modal state
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [modalData, setModalData] = useState({});

  const { categoryId } = useParams();
  const [products, setProducts] = useState([]);
  const [category, setCategory] = useState({});
  const [error, setError] = useState(null);

  useEffect(() => {
    fetchProducts();
  }, [categoryId]);

  const fetchProducts = async () => {
    try {
      if (categoryId === undefined) {
        const data = await getAllProductos();
        setProducts(data);
        return;
      }
      const data = await getProductsByCategory(categoryId);
      fetchCategory();
      setProducts(data);
    } catch (error) {
      console.error("Error fetching products:", error.message);
      setError("Failed to fetch products. Please try again.");
    }
  };

  const fetchCategory = async () => {
    try {
      const data = await getCategoriaById(categoryId);
      setCategory(data);
      return;
    } catch (error) {
      console.error("Error fetching products:", error.message);
      setError("Failed to fetch products. Please try again.");
    }
  };

  const handleDelete = async (productId) => {
    try {
      await deleteProducto(productId);
      setProducts(products.filter((product) => product.id !== productId));
      toast.success("Product deleted successfully");
    } catch (error) {
      console.error("Error deleting product:", error.message);
      toast.error("Failed to delete product. Please try again.");
    }
  };

  const openDeleteModal = (productId) => {
    setModalData({
      title: "Confirm Deletion",
      message: "Are you sure you want to delete this product?",
      onConfirm: () => {
        handleDelete(productId);
        setIsModalOpen(false);
      },
    });
    setIsModalOpen(true);
  };

  return (
    <div className="container mx-auto px-4 py-8">
      <ToastContainer />
      {
        categoryId !== undefined ? (
          <h1 className="text-3xl font-bold mb-6 text-blue-800">
            Products of {category?.name || "this category"}
          </h1>
        ) : (
          <h1 className="text-3xl font-bold mb-6 text-blue-800">All Products</h1>
        )
      }
      {error && <p className="text-red-500 mb-4">{error}</p>}
      {userRole === "Admin" && (
        <Link
          to="/virtual-catalog/products/create"
          className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded inline-flex items-center mb-6"
        >
          <FaPlus className="mr-2" />
          <span>Create Product</span>
        </Link>
      )}
      <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-6">
        {products.map((product) => (
          <div
            key={product.id}
            className="bg-white rounded-lg shadow-md p-6 flex flex-col items-start w-full"
          >
            <h2 className="text-lg font-semibold mb-2 text-gray-800">
              {product.name}
            </h2>
            <p className="text-gray-600 mb-4">{product.description}</p>
            <p className="text-gray-800 font-semibold">
              Price: ${product.price}
            </p>
            <p className="text-gray-800 font-semibold">
              Stock: {product.stock}
            </p>
            <div className="flex justify-between w-full mt-4">
              {userRole === "Admin" && (
                <Link
                  to={`/virtual-catalog/products/${product.id}/edit`}
                  className="bg-green-500 hover:bg-green-700 text-white font-bold py-2 px-4 rounded inline-flex items-center"
                >
                  <FaEdit className="mr-2" />
                  <span>Update</span>
                </Link>
              )}
              {userRole === "Admin" && (
                <button
                  onClick={() => openDeleteModal(product.id)}
                  className="bg-red-500 hover:bg-red-700 text-white font-bold py-2 px-4 rounded inline-flex items-center"
                >
                  <FaTrash className="mr-2" /> 
                  <span>Delete</span>
                </button>
              )}
            </div>
          </div>
        ))}
      </div>
      <div className="w-full mt-10">
        <CustomFooter />
      </div>
      <ConfirmationModal
        isOpen={isModalOpen}
        title={modalData.title}
        message={modalData.message}
        onConfirm={modalData.onConfirm}
        onCancel={() => setIsModalOpen(false)}
      />
    </div>
  );
}
