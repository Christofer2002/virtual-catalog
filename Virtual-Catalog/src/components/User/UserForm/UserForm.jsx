import { useState } from 'react';
import { ToastContainer, toast } from "react-toastify";
import ConfirmationModal from "../../common/ConfirmationModal";

const UserForm = ({ initialData, onSubmit, error, availableRoles, isEdit }) => {
  const [user, setUser] = useState(
    initialData || {
      name: '',
      lastName: '',
      email: '',
      identification: '',
      password: '',
      roleName: '', // Add roleName
    }
  );

  // Modal state
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [modalData, setModalData] = useState({});

  const handleChange = (e) => {
    const { name, value } = e.target;
    setUser((prev) => ({ ...prev, [name]: value }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await onSubmit(user);
    }
    catch (err) {
      console.error("Error submitting user:", err);
      toast.error(isEdit ? "Failed to update user." : "Failed to create user.");
    }
  };

  const openSubmitModal = (e) => {
    e.preventDefault()
    setModalData({
      title: (isEdit ? "Confirm Updating" : "Confirm Creating"),
      message: (isEdit ? "Are you sure you want to update this category?" : "Are you sure you want to create the category?"),
      onConfirm: () => {
        handleSubmit(e);
        setIsModalOpen(false);
      },
    });
    setIsModalOpen(true);
  };

  return (
    <form
      onSubmit={e => openSubmitModal(e)}
      className="bg-white shadow-md rounded px-8 pt-6 pb-8 mb-4"
    >
      <ToastContainer />
      {error && <p className="text-red-500 mb-4">{error}</p>}
      <div className="mb-4">
        <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="identification">
          Identification
        </label>
        <input
          type="text"
          id="identification"
          name="identification"
          value={user.identification}
          onChange={handleChange}
          required
          disabled={isEdit}
          className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
        />
      </div>
      <div className="mb-4">
        <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="name">
          Name
        </label>
        <input
          type="text"
          id="name"
          name="name"
          value={user.name}
          onChange={handleChange}
          required
          className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
        />
      </div>
      <div className="mb-4">
        <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="lastName">
          Last Name
        </label>
        <input
          type="text"
          id="lastName"
          name="lastName"
          value={user.lastName}
          onChange={handleChange}
          required
          className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
        />
      </div>
      <div className="mb-4">
        <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="email">
          Email
        </label>
        <input
          type="email"
          id="email"
          name="email"
          value={user.email}
          onChange={handleChange}
          required
          className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
        />
      </div>
      {!isEdit && (
        <div className="mb-4">
          <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="password">
            Password
          </label>
          <input
            type="password"
            id="password"
            name="password"
            value={user.password}
            onChange={handleChange}
            required={!isEdit}
            className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
          />
        </div>
      )}
      <div className="mb-4">
        <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="roleName">
          Role
        </label>
        <select
          id="roleName"
          name="roleName"
          value={user.roleName}
          onChange={handleChange}
          required
          className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
        >
          <option value="" disabled>
            Select a role
          </option>
          {availableRoles.map((role) => (
            <option key={role.id} value={role.name}>
              {role.name}
            </option>
          ))}
        </select>
      </div>
      <button
        type="submit"
        className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline"
      >
        Submit
      </button>
      <ConfirmationModal
        isOpen={isModalOpen}
        title={modalData.title}
        message={modalData.message}
        onConfirm={modalData.onConfirm}
        onCancel={() => setIsModalOpen(false)}
      />
    </form>
  );
};

export default UserForm;
