import { useState, useEffect } from "react";

const CategoryForm = ({ initialCategory, onSubmit, isEdit }) => {
  const [category, setCategory] = useState(
    initialCategory || { name: "", description: "" }
  );
  const [error, setError] = useState(null);

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
      await onSubmit(category);
    } catch (err) {
      console.error("Error submitting category:", err);
      setError(isEdit ? "Failed to update category." : "Failed to create category.");
    }
  };

  return (
    <div className="container mx-auto px-4 py-8">
      <h1 className="text-3xl font-bold mb-6 text-blue-800">
        {isEdit ? "Edit Category" : "Create Category"}
      </h1>
      {error && <p className="text-red-500 mb-4">{error}</p>}
      <form
        onSubmit={handleSubmit}
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
    </div>
  );
};

export default CategoryForm;
