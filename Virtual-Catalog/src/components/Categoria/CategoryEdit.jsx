import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { getCategoriaById, updateCategoria } from "../../service/categoryApi";
import CategoryForm from "./CategoryForm/CategoryForm";

const CategoryEdit = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [initialCategory, setInitialCategory] = useState(null);

  useEffect(() => {
    const fetchCategory = async () => {
      try {
        const category = await getCategoriaById(id);
        setInitialCategory(category);
      } catch (err) {
        console.error("Error fetching category:", err);
      }
    };
    fetchCategory();
  }, [id]);

  const handleUpdate = async (category) => {
    await updateCategoria(id, category);
    navigate("/virtual-catalog/categories");
  };

  if (!initialCategory) return <p>Loading...</p>;

  return <CategoryForm initialCategory={initialCategory} onSubmit={handleUpdate} isEdit />;
};

export default CategoryEdit;
