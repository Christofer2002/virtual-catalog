import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { createUser } from '../../service/userApi';
import { getRoles } from '../../service/roleApi';
import UserForm from './UserForm/UserForm';

const UserCreate = () => {
  const [error, setError] = useState(null);
  const [availableRoles, setAvailableRoles] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    fetchRoles();
  }, []);

  const fetchRoles = async () => {
    try {
      const roles = await getRoles();
      setAvailableRoles(roles);
    } catch (err) {
      setError(err.message || 'Failed to fetch roles.');
    }
  };

  const handleSubmit = async (userData) => {
    try {
      await createUser(userData);
      navigate('/virtual-catalog/users');
    } catch (err) {
      setError(err.message || 'Failed to create user.');
    }
  };

  return (
    <div className="container mx-auto px-4 py-8">
      <h1 className="text-3xl font-bold mb-6 text-blue-800">Create User</h1>
      <UserForm onSubmit={handleSubmit} error={error} availableRoles={availableRoles} isEdit={false} />
    </div>
  );
};

export default UserCreate;
