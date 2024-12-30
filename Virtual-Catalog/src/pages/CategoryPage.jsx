import { useState, useEffect } from "react";
import { ToastContainer, toast } from "react-toastify";
import { FaTrash, FaEdit, FaPlus, FaBox } from "react-icons/fa"; // Importar íconos
import { Link } from "react-router-dom";
import { getAllCategorias, deleteCategoria } from "../service/categoryApi";
import CustomFooter from "../components/common/CustomFooter";
import ConfirmationModal from "../components/common/ConfirmationModal";

const CategoriaPage = () => {
  const authData = JSON.parse(localStorage.getItem("auth"));
  const userRole = authData?.role; // User role

  // Modal state
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [modalData, setModalData] = useState({});

  const [categories, setCategories] = useState([]);

  useEffect(() => {
    fetchData();
  }, []);

  const fetchData = async () => {
    try {
      const data = await getAllCategorias();
      setCategories(data);
    } catch (error) {
      console.error("Error fetching categories:", error);
    }
  };

  const handleDelete = async (categoryId) => {
    try {
      await deleteCategoria(categoryId);
      setCategories(
        categories.filter((category) => category.id !== categoryId)
      );
      toast.success("Category deleted successfully");
    } catch (error) {
      console.error("Error deleting category:", error);
      toast.error("Failed to delete category. Please try again.");
    }
  };

  const openDeleteModal = (categoryId) => {
    setModalData({
      title: "Confirm Deletion",
      message: "Are you sure you want to delete this category?",
      onConfirm: () => {
        handleDelete(categoryId);
        setIsModalOpen(false);
      },
    });
    setIsModalOpen(true);
  };

  return (
    <div className="container mx-auto px-4 py-8">
      <ToastContainer />
      <h1 className="text-3xl font-bold mb-6 text-blue-800">Categorías</h1>
      {userRole === "Admin" && (
        <Link
          to="/virtual-catalog/categories/create"
          className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded inline-flex items-center mb-6"
        >
          <FaPlus className="mr-2" />
          <span>Create Category</span>
        </Link>
      )}
      <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-6">
        {categories.map((category) => (
          <div
            key={category.id}
            className="bg-white rounded-lg shadow-md p-6 flex flex-col items-start w-full"
          >
            <h2 className="text-lg font-semibold mb-2 text-gray-800">
              {category.name}
            </h2>
            <p className="text-gray-600 mb-4">{category.description}</p>
            <div className="flex justify-between w-full">
              <Link
                to={`/virtual-catalog/categories/${category.id}/products`}
                className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded inline-flex items-center"
              >
                <FaBox className="mr-2" />
                <span>Products</span>
              </Link>
              {userRole === "Admin" && (
                <Link
                  to={`/virtual-catalog/categories/${category.id}/edit`}
                  className="bg-green-500 hover:bg-green-700 text-white font-bold py-2 px-4 rounded inline-flex items-center"
                >
                  <FaEdit className="mr-2" />
                  <span>Update</span>
                </Link>
              )}
              {userRole === "Admin" && (
                <button
                  onClick={() => openDeleteModal(category.id)}
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
};

export default CategoriaPage;
