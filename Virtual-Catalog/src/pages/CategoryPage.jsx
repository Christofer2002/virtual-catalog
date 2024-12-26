import { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import { getAllCategorias, deleteCategoria } from "../service/categoryApi";
import CustomFooter from "../components/common/CustomFooter";

const CategoriaPage = () => {
  const authData = JSON.parse(localStorage.getItem("auth"));
  const userRole = authData?.role; // User rol

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
    } catch (error) {
      console.error("Error deleting category:", error);
    }
  };

  return (
    <div className="container mx-auto px-4 py-8">
      <h1 className="text-3xl font-bold mb-6 text-blue-800">Categorías</h1>
      {userRole === "Admin" && (
      <Link
        to="/categories/create"
        className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded inline-block mb-6"
      >
        Create Category
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
                to={`/categories/${category.id}/products`}
                className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
              >
                Products
              </Link>
              {userRole === "Admin" && (
              <Link
                to={`/categories/${category.id}/edit`}
                className="bg-green-500 hover:bg-green-700 text-white font-bold py-2 px-4 rounded"
              >
                Update
              </Link>
              )}
              {userRole === "Admin" && (
              <button
                onClick={() => handleDelete(category.id)}
                className="bg-red-500 hover:bg-red-700 text-white font-bold py-2 px-4 rounded"
              >
                Delete
              </button>
              )}
            </div>
          </div>
        ))}
      </div>
      <div className="w-full mt-10">
        <CustomFooter />
      </div>
    </div>
  );
};

export default CategoriaPage;
