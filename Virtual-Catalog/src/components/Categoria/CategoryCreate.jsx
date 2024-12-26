import { useNavigate } from "react-router-dom";
import { createCategoria } from "../../service/categoryApi";
import CategoryForm from "./CategoryForm/CategoryForm";

const CategoryCreate = () => {
  const navigate = useNavigate();

  const handleCreate = async (category) => {
    await createCategoria(category);
    navigate("/categories");
  };

  return <CategoryForm onSubmit={handleCreate} />;
};

export default CategoryCreate;
