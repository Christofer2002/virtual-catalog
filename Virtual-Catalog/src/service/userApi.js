import axios from 'axios';
import { USER_URL } from '../utils/API_URL';

// Helper function to get the token from localStorage
const getToken = () => {
  const authData = JSON.parse(localStorage.getItem('auth'));
  return authData?.token || '';
};

// Get all users
export const getAllUsers = async () => {
  try {
    const response = await axios.get(USER_URL, {
      headers: {
        Authorization: `Bearer ${getToken()}`
      }
    });
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data || 'Error fetching users');
  }
};

// Get a user by ID
export const getUserById = async (id) => {
  try {
    const response = await axios.get(`${USER_URL}${id}`, {
      headers: {
        Authorization: `Bearer ${getToken()}`
      }
    });
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data || 'Error fetching user by ID');
  }
};

// Create a new user
export const createUser = async (userData) => {
  try {
    const response = await axios.post(USER_URL, userData, {
      headers: {
        Authorization: `Bearer ${getToken()}`
      }
    });
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data || 'Error creating user');
  }
};

// Update a user
export const updateUser = async (id, userData) => {
  try {
    const response = await axios.put(`${USER_URL}${id}`, userData, {
      headers: {
        Authorization: `Bearer ${getToken()}`
      }
    });
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data || 'Error updating user');
  }
};

// Delete a user
export const deleteUser = async (id) => {
  try {
    const response = await axios.delete(`${USER_URL}${id}`, {
      headers: {
        Authorization: `Bearer ${getToken()}`
      }
    });
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data || 'Error deleting user');
  }
};
