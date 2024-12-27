import axios from 'axios';
import { ROLE_URL } from '../utils/API_URL';

export const getRoles = async () => {
  try {
    const response = await axios.get(ROLE_URL);
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data || 'Failed to fetch roles.');
  }
};
