import { useState, useEffect } from "react";
import { getAllCategorias } from "../../../service/categoryApi";
import { ToastContainer, toast } from "react-toastify";
import ConfirmationModal from "../../common/ConfirmationModal";

const ProductForm = ({ initialProduct, onSubmit, isEdit }) => {
  const [product, setProduct] = useState(
    initialProduct || {
      name: "",
      description: "",
      price: 0,
      stock: 0,
      categoryId: 0,
    }
  );
  const [categories, setCategories] = useState([]);
  const [error, setError] = useState(null);

  // Modal state
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [modalData, setModalData] = useState({});

  useEffect(() => {
    fetchCategories();
  }, []);

  const fetchCategories = async () => {
    try {
      const data = await getAllCategorias();
      setCategories(data);
    } catch (error) {
      console.error("Error fetching categories:", error);
    }
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setProduct((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    const validationError = validateProduct(product);
    if (validationError) {
      setError(validationError);
      return;
    }

    const productData = {
      ...product,
      price: parseFloat(product.price),
      stock: parseInt(product.stock, 10),
      categoryId: parseInt(product.categoryId, 10),
    };

    try {
      await onSubmit(productData);
    } catch (err) {
      console.error("Error submitting product:", err);
      setError(err.message || "Failed to submit product.");
    }
  };

  const validateProduct = (product) => {
    if (!product.name || product.name.trim() === "") {
      return "Product name is required.";
    }
    if (!product.price || isNaN(product.price) || product.price <= 0) {
      return "Product price must be a positive number.";
    }
    if (!product.stock || isNaN(product.stock) || product.stock < 0) {
      return "Product stock must be a non-negative integer.";
    }
    if (!product.categoryId || isNaN(product.categoryId) || product.categoryId <= 0) {
      return "Valid category ID is required.";
    }
    return null;
  };

  const openSubmitModal = (e) => {
    e.preventDefault()
    setModalData({
      title: (isEdit ? "Confirm Updating" : "Confirm Creating"),
      message: (isEdit ? "Are you sure you want to update this product?" : "Are you sure you want to create the product?"),
      onConfirm: () => {
        handleSubmit(e);
        setIsModalOpen(false);
      },
    });
    setIsModalOpen(true);
  };

  return (
    <div className="container mx-auto px-4 py-8">
      <ToastContainer />
      <h1 className="text-3xl font-bold mb-6 text-blue-800">
        {isEdit ? "Edit Product" : "Create Product"}
      </h1>
      {error && <p className="text-red-500 mb-4">{error}</p>}
      <form onSubmit={e => openSubmitModal(e)} className="bg-white shadow-md rounded px-8 pt-6 pb-8 mb-4">
        <div className="mb-4">
          <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="name">
            Name
          </label>
          <input
            type="text"
            id="name"
            name="name"
            value={product.name}
            onChange={handleChange}
            required
            className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
          />
        </div>
        <div className="mb-4">
          <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="description">
            Description
          </label>
          <textarea
            id="description"
            name="description"
            value={product.description}
            onChange={handleChange}
            className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
          ></textarea>
        </div>
        <div className="mb-4">
          <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="price">
            Price
          </label>
          <input
            type="number"
            step="0.01"
            id="price"
            name="price"
            value={product.price}
            onChange={handleChange}
            required
            className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
          />
        </div>
        <div className="mb-4">
          <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="stock">
            Stock
          </label>
          <input
            type="number"
            id="stock"
            name="stock"
            value={product.stock}
            onChange={handleChange}
            required
            className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
          />
        </div>
        <div className="mb-4">
          <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="categoryId">
            Category
          </label>
          <select
            id="categoryId"
            name="categoryId"
            value={product.categoryId}
            onChange={handleChange}
            required
            className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
          >
            <option value="">Select a category</option>
            {categories.map((category) => (
              <option key={category.id} value={category.id}>
                {category.name}
              </option>
            ))}
          </select>
        </div>
        <div className="flex items-center justify-between">
          <button
            type="submit"
            className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline"
          >
            {isEdit ? "Update" : "Create"}
          </button>
        </div>
      </form>
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

export default ProductForm;
