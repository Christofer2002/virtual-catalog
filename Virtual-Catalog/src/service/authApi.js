import axios from 'axios';

const API_URL = 'https://localhost:7278/api/auth/';

// Login function
export const login = async (credentials) => {
  try {
    const response = await axios.post(`${API_URL}login`, credentials);
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data || 'Error during login process');
  }
};

// Register
export const register = async (userData) => {
    try {
      const response = await axios.post(`${API_URL}register`, userData);
      return response.data;
    } catch (error) {
      throw new Error(error.response?.data || "Error during registration");
    }
  };

// Logout function
export const logout = async () => {
  try {
    const response = await axios.post(`${API_URL}logout`);
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data || 'Error during logout process');
  }
};

// Check session function
export const checkSession = async () => {
  try {
    const response = await axios.get(`${API_URL}session`);
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data || 'Error checking session');
  }
};
