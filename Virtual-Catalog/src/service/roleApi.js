import axios from 'axios';

const API_URL = 'https://localhost:7278/api/role/';

export const getRoles = async () => {
  try {
    const response = await axios.get(API_URL);
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data || 'Failed to fetch roles.');
  }
};
