import { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import { getAllUsers, deleteUser } from "../service/userApi";
import CustomFooter from "../components/common/CustomFooter";

export default function UserPage() {
  const [users, setUsers] = useState([]);
  const [error, setError] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetchUsers();
  }, []);

  const fetchUsers = async () => {
    try {
      setLoading(true);
      const data = await getAllUsers();
      console.log(data)
      setUsers(data);
      setError(null);
    } catch (error) {
      console.error("Error fetching users:", error);
      setError("Failed to fetch users. Please try again later.");
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async (userId) => {
    const confirmDelete = window.confirm("Are you sure you want to delete this user?");
    if (!confirmDelete) return;

    try {
      await deleteUser(userId);
      setUsers(users.filter((user) => user.id !== userId));
    } catch (error) {
      console.error("Error deleting user:", error);
      setError("Failed to delete user. Please try again.");
    }
  };

  if (loading) {
    return <p className="text-center text-gray-500">Loading...</p>;
  }

  if (error) {
    return <p className="text-center text-red-500">{error}</p>;
  }

  return (
    <div className="container mx-auto px-4 py-8">
      <h1 className="text-3xl font-bold mb-6 text-blue-800">Users</h1>
      <Link
        to="/users/create"
        className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded inline-block mb-4"
      >
        Create User
      </Link>
      {users.length === 0 ? (
        <p className="text-center text-gray-500">No users found.</p>
      ) : (
        <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-3 gap-4">
          {users.map((user) => (
            <div key={user.id} className="bg-white rounded-lg shadow-md p-6">
              <h2 className="text-lg font-semibold mb-2 text-gray-800">
                {user.name} {user.lastName}
              </h2>
              <p className="text-gray-600 mb-2">
                <strong>Email:</strong> {user.email}
              </p>
              <p className="text-gray-600 mb-2">
                <strong>Identification:</strong> {user.identification}
              </p>
              <div className="flex justify-between mt-4">
                <Link
                  to={`/users/${user.id}/edit`}
                  className="bg-green-500 hover:bg-green-700 text-white font-bold py-2 px-4 rounded"
                >
                  Edit
                </Link>
                <button
                  onClick={() => handleDelete(user.id)}
                  className="bg-red-500 hover:bg-red-700 text-white font-bold py-2 px-4 rounded"
                >
                  Delete
                </button>
              </div>
            </div>
          ))}
        </div>
      )}
      <div className="w-full mt-10">
        <CustomFooter />
      </div>
    </div>
  );
}
