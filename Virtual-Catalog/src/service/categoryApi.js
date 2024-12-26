import axios from "axios";

const API_URL = "https://localhost:7278/api/category/";

// Helper function to get the token from localStorage
const getToken = () => {
  const authData = JSON.parse(localStorage.getItem("auth"));
  return authData?.token || "";
};

export const getAllCategorias = async () => {
  try {
    const response = await axios.get(API_URL, {
      headers: {
        Authorization: `Bearer ${getToken()}`
      }
    });
    return response.data;
  } catch (error) {
    throw new Error(error.response.data);
  }
};

export const getCategoriaById = async (id) => {
  try {
    const response = await axios.get(`${API_URL}${id}`, {
      headers: {
        Authorization: `Bearer ${getToken()}`
      }
    });
    return response.data;
  } catch (error) {
    throw new Error(error.response.data);
  }
};

export const createCategoria = async (categoriaData) => {
  try {
    const response = await axios.post(API_URL, categoriaData, {
      headers: {
        Authorization: `Bearer ${getToken()}`
      }
    });
    return response.data;
  } catch (error) {
    throw new Error(error.response.data);
  }
};

export const updateCategoria = async (id, categoriaData) => {
  try {
    const response = await axios.put(`${API_URL}${id}`, categoriaData, {
      headers: {
        Authorization: `Bearer ${getToken()}`
      }
    });
    return response.data;
  } catch (error) {
    throw new Error(error.response.data);
  }
};

export const deleteCategoria = async (id) => {
  try {
    const response = await axios.delete(`${API_URL}${id}`, {
      headers: {
        Authorization: `Bearer ${getToken()}`
      }
    });
    return response.data;
  } catch (error) {
    throw new Error(error.response.data);
  }
};
