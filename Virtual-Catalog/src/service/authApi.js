import axios from 'axios';
import {AUTH_URL} from '../utils/API_URL';

// Login function
export const login = async (credentials) => {
  try {
    const response = await axios.post(`${AUTH_URL}login`, credentials);
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data || 'Error during login process');
  }
};

// Register
export const register = async (userData) => {
    try {
      const response = await axios.post(`${AUTH_URL}register`, userData);
      return response.data;
    } catch (error) {
      throw new Error(error.response?.data || "Error during registration");
    }
  };

// Logout function
export const logout = async () => {
  try {
    const response = await axios.post(`${AUTH_URL}logout`);
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data || 'Error during logout process');
  }
};

// Check session function
export const checkSession = async () => {
  try {
    const response = await axios.get(`${AUTH_URL}session`);
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data || 'Error checking session');
  }
};

export const requestResetPassword = async (email) => {
  try {
    const response = await axios.post(`${AUTH_URL}request-reset-password`, {email});
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data || 'Error requesting password reset');
  }
}

export const resetPassword = async (data) => {
  try {
    const response = await axios.post(`${AUTH_URL}reset-password`, data);
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data || 'Error resetting password');
  }
}
