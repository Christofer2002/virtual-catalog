import { useState, useEffect } from "react";
import { ToastContainer, toast } from "react-toastify";
import { FaTrash, FaEdit, FaPlus } from "react-icons/fa"; // Importar íconos
import { Link } from "react-router-dom";
import { getAllUsers, deleteUser } from "../service/userApi";
import CustomFooter from "../components/common/CustomFooter";
import ConfirmationModal from "../components/common/ConfirmationModal";

export default function UserPage() {
  const authData = JSON.parse(localStorage.getItem("auth"));

  const [users, setUsers] = useState([]);
  const [error, setError] = useState(null);
  const [loading, setLoading] = useState(true);

  // Modal state
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [modalData, setModalData] = useState({});

  useEffect(() => {
    fetchUsers();
  }, []);

  const fetchUsers = async () => {
    try {
      setLoading(true);
      const data = await getAllUsers();
      const filteredData = data.filter((user) => user.id !== authData.userId);
      setUsers(filteredData);
      setError(null);
    } catch (error) {
      console.error("Error fetching users:", error);
      setError("Failed to fetch users. Please try again later.");
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async (userId) => {
    try {
      await deleteUser(userId);
      setUsers(users.filter((user) => user.id !== userId));
      toast.success("User deleted successfully");
    } catch (error) {
      console.error("Error deleting user:", error);
      toast.error("Failed to delete user. Please try again.");
    }
  };

  const openDeleteModal = (userId) => {
    setModalData({
      title: "Confirm Deletion",
      message: "Are you sure you want to delete this user?",
      onConfirm: () => {
        handleDelete(userId);
        setIsModalOpen(false);
      },
    });
    setIsModalOpen(true);
  };

  if (loading) {
    return <p className="text-center text-gray-500">Loading...</p>;
  }

  if (error) {
    return <p className="text-center text-red-500">{error}</p>;
  }

  return (
    <div className="container mx-auto px-4 py-8">
      <ToastContainer />
      <h1 className="text-3xl font-bold mb-6 text-blue-800">Users</h1>
      <Link
        to="/users/create"
        className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded inline-flex items-center mb-4"
      >
        <FaPlus className="mr-2" />
        <span>Create User</span>
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
                  className="bg-green-500 hover:bg-green-700 text-white font-bold py-2 px-4 rounded inline-flex items-center"
                >
                  <FaEdit className="mr-2" />
                  <span>Edit</span>
                </Link>
                <button
                  onClick={() => openDeleteModal(user.id)}
                  className="bg-red-500 hover:bg-red-700 text-white font-bold py-2 px-4 rounded inline-flex items-center"
                >
                  <FaTrash className="mr-2" />
                  <span>Delete</span>
                </button>
              </div>
            </div>
          ))}
        </div>
      )}
      <div className="w-full mt-10">
        <CustomFooter />
      </div>
      <ConfirmationModal
        isOpen={isModalOpen}
        title={modalData.title}
        message={modalData.message}
        onConfirm={modalData.onConfirm}
        onCancel={() => setIsModalOpen(false)}
      />
    </div>
  );
}
