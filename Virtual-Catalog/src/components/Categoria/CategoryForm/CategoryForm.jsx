import { useState, useEffect } from "react";
import { ToastContainer, toast } from "react-toastify";
import ConfirmationModal from "../../common/ConfirmationModal";

const CategoryForm = ({ initialCategory, onSubmit, isEdit }) => {
  const [category, setCategory] = useState(
    initialCategory || { name: "", description: "" }
  );
  const [error, setError] = useState(null);

  // Modal state
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [modalData, setModalData] = useState({});

  useEffect(() => {
    if (initialCategory) {
      setCategory(initialCategory);
    }
  }, [initialCategory]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setCategory((prev) => ({ ...prev, [name]: value }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      toast.success(isEdit ? "Category updated successfully" : "Category created successfully");
      await onSubmit(category);
    } catch (err) {
      console.error("Error submitting category:", err);
      toast.error(isEdit ? "Failed to update category." : "Failed to create category.");
    }
  };

  const openSubmitModal = (e) => {
    e.preventDefault()
    setModalData({
      title: (isEdit ? "Confirm Updating" : "Confirm Creating"),
      message: (isEdit ? "Are you sure you want to update this category?" : "Are you sure you want to create the category?"),
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
        {isEdit ? "Edit Category" : "Create Category"}
      </h1>
      {error && <p className="text-red-500 mb-4">{error}</p>}
      <form
        onSubmit={e => openSubmitModal(e)}
        className="bg-white shadow-md rounded px-8 pt-6 pb-8 mb-4"
      >
        <div className="mb-4">
          <label
            className="block text-gray-700 text-sm font-bold mb-2"
            htmlFor="name"
          >
            Name
          </label>
          <input
            type="text"
            id="name"
            name="name"
            value={category.name}
            onChange={handleChange}
            required
            className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
          />
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

export default CategoryForm;
