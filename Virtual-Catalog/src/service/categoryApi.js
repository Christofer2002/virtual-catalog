import axios from "axios";
import { CATEGORY_URL } from "../utils/API_URL";

// Helper function to get the token from localStorage
const getToken = () => {
  const authData = JSON.parse(localStorage.getItem("auth"));
  return authData?.token || "";
};

export const getAllCategorias = async () => {
  try {
    const response = await axios.get(CATEGORY_URL, {
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
    const response = await axios.get(`${CATEGORY_URL}${id}`, {
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
    const response = await axios.post(CATEGORY_URL, categoriaData, {
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
    const response = await axios.put(`${CATEGORY_URL}${id}`, categoriaData, {
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
    const response = await axios.delete(`${CATEGORY_URL}${id}`, {
      headers: {
        Authorization: `Bearer ${getToken()}`
      }
    });
    return response.data;
  } catch (error) {
    throw new Error(error.response.data);
  }
};
