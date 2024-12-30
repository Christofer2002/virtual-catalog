import { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { getUserById, updateUser } from '../../service/userApi';
import { getRoles } from '../../service/roleApi'; // Asegúrate de tener esta función para obtener roles
import UserForm from './UserForm/UserForm';

const UserEdit = () => {
  const { id } = useParams();
  const [user, setUser] = useState(null);
  const [availableRoles, setAvailableRoles] = useState([]);
  const [error, setError] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    fetchUser();
    fetchRoles();
  }, [id]);

  const fetchUser = async () => {
    try {
      const userData = await getUserById(id);
      console.log(userData)
      setUser(userData);
    } catch (err) {
      setError(err.message || 'Failed to fetch user.');
    }
  };

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
      await updateUser(id, userData);
      navigate('/virtual-catalog/users');
    } catch (err) {
      setError(err.message || 'Failed to update user.');
    }
  };

  if (!user) return <p>Loading...</p>;

  return (
    <div className="container mx-auto px-4 py-8">
      <h1 className="text-3xl font-bold mb-6 text-blue-800">Edit User</h1>
      <UserForm initialData={user} onSubmit={handleSubmit} error={error} availableRoles={availableRoles} isEdit={true} />
    </div>
  );
};

export default UserEdit;
