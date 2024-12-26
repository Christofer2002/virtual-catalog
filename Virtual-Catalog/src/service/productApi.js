import axios from 'axios';

const API_URL = 'https://localhost:7278/api/product/'; 

// Helper function to get the token from localStorage
const getToken = () => {
  const authData = JSON.parse(localStorage.getItem("auth"));
  return authData?.token || "";
};

export const getAllProductos = async () => {
  try {
    const response = await axios.get(API_URL);
    return response.data;
  } catch (error) {
    throw new Error(error.response.data);
  }
};

export const getProductoById = async (id) => {
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

export const createProducto = async (productoData) => {
  try {
    const response = await axios.post(API_URL, productoData, {
      headers: {
        Authorization: `Bearer ${getToken()}`
      }
    });
    return response.data;
  } catch (error) {
    throw new Error(error.response.data);
  }
};

export const updateProducto = async (id, productoData) => {
  try {
    const response = await axios.put(`${API_URL}${id}`, productoData, {
      headers: {
        Authorization: `Bearer ${getToken()}`
      }
    });
    return response.data;
  } catch (error) {
    throw new Error(error.response.data);
  }
};

export const deleteProducto = async (id) => {
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

export const getProductsByCategory = async (categoryId) => {
  try {
    const response = await axios.get(`${API_URL}category/${categoryId}`);
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data || "Error fetching products by category.");
  }
};
