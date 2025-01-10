import { useState, useContext } from "react";
import { useNavigate } from "react-router-dom";
import { FaUserCircle, FaLock, FaKey } from "react-icons/fa";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import CustomFooter from "../../common/CustomFooter";
import { login, register, requestResetPassword } from "../../../service/authApi";
import { AuthContext } from "../../../context/AuthContext";

const AuthLayout = () => {
  const navigate = useNavigate();
  const { login: setAuthData } = useContext(AuthContext);
  const [loading, setLoading] = useState(false);
  const [showRegisterModal, setShowRegisterModal] = useState(false);
  const [showResetModal, setShowResetModal] = useState(false);
  const [resetEmail, setResetEmail] = useState("");

  const handleLoginSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);

    const credentials = {
      email: e.target.email.value,
      password: e.target.password.value,
    };

    try {
      const response = await login(credentials);
      setAuthData(response);
      toast.success("Login successful");
      navigate("/virtual-catalog/");
    } catch (error) {
      toast.error(error.message || "Incorrect credentials");
    } finally {
      setLoading(false);
    }
  };

  const handleRegisterSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);

    const newUser = {
      identification: e.target.identification.value,
      name: e.target.name.value,
      lastName: e.target.lastName.value,
      email: e.target.email.value,
      password: e.target.password.value,
      roleId: 2,
    };

    try {
      const response = await register(newUser);
      toast.success("Registration successful");
      setShowRegisterModal(false);
    } catch (error) {
      toast.error(error.message || "Error during registration");
    } finally {
      setLoading(false);
    }
  };

  const handlePasswordReset = async (e) => {
    e.preventDefault();
    setLoading(true);
    try {
      await requestResetPassword( resetEmail );
      toast.success("Password reset link sent to your email");
      setShowResetModal(false);
      setResetEmail("");
    } catch (error) {
      toast.error("Error sending password reset link");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="min-h-screen bg-gradient-to-br from-blue-100 to-blue-300 flex flex-col items-center justify-center">
      <ToastContainer />
      <div className="bg-white shadow-lg rounded-lg p-6 max-w-sm w-full">
        <div className="text-center">
          <div className="flex justify-center items-center mb-4">
            <FaUserCircle className="text-6xl text-blue-500" />
          </div>
          <div className="flex justify-center items-center mb-4">
            <FaLock className="text-4xl text-gray-500" />
          </div>
          <h2 className="text-3xl font-bold mb-4 text-gray-700">Login</h2>
        </div>

        <form onSubmit={handleLoginSubmit}>
          <div className="mb-4">
            <label htmlFor="email" className="block text-gray-700 font-medium">
              Email
            </label>
            <input
              type="email"
              id="email"
              name="email"
              className="w-full px-3 py-2 rounded-lg border-2 border-gray-200 focus:outline-none focus:border-blue-500"
              placeholder="Enter your email"
              required
            />
          </div>
          <div className="mb-6">
            <label htmlFor="password" className="block text-gray-700 font-medium">
              Password
            </label>
            <input
              type="password"
              id="password"
              name="password"
              className="w-full px-3 py-2 rounded-lg border-2 border-gray-200 focus:outline-none focus:border-blue-500"
              placeholder="Enter your password"
              required
            />
          </div>
          <button
            type="submit"
            className={`w-full bg-blue-500 text-white py-2 px-4 rounded-lg hover:bg-blue-600 transition-colors ${loading ? "opacity-50 cursor-not-allowed" : ""}`}
            disabled={loading}
          >
            {loading ? "Loading..." : "Login"}
          </button>
        </form>
        <div className="text-center mt-4">
          <button
            onClick={() => setShowResetModal(true)}
            className="text-blue-500 underline hover:text-blue-700"
          >
            Forgot Password?
          </button>
        </div>
        <div className="text-center mt-2">
          <button
            onClick={() => setShowRegisterModal(true)}
            className="text-blue-500 underline hover:text-blue-700"
          >
            Register a new account
          </button>
        </div>
      </div>

      <div className="w-full">
        <CustomFooter />
      </div>

      {/* Register Modal */}
      {showRegisterModal && (
        <div className="fixed inset-0 bg-gray-800 bg-opacity-75 flex items-center justify-center">
          <div className="bg-white rounded-lg shadow-md p-6 w-96">
            <h2 className="text-2xl font-bold mb-4">Register</h2>
            <form onSubmit={handleRegisterSubmit}>
              <div className="mb-4">
                <label htmlFor="identification" className="block text-gray-700 font-medium">
                  Identification
                </label>
                <input
                  type="text"
                  id="identification"
                  name="identification"
                  className="w-full px-3 py-2 rounded-lg border-2 border-gray-200 focus:outline-none focus:border-blue-500"
                  placeholder="Enter your identification"
                  required
                />
              </div>
              <div className="mb-4">
                <label htmlFor="name" className="block text-gray-700 font-medium">
                  First Name
                </label>
                <input
                  type="text"
                  id="name"
                  name="name"
                  className="w-full px-3 py-2 rounded-lg border-2 border-gray-200 focus:outline-none focus:border-blue-500"
                  placeholder="Enter your first name"
                  required
                />
              </div>
              <div className="mb-4">
                <label htmlFor="lastName" className="block text-gray-700 font-medium">
                  Last Name
                </label>
                <input
                  type="text"
                  id="lastName"
                  name="lastName"
                  className="w-full px-3 py-2 rounded-lg border-2 border-gray-200 focus:outline-none focus:border-blue-500"
                  placeholder="Enter your last name"
                  required
                />
              </div>
              <div className="mb-4">
                <label htmlFor="email" className="block text-gray-700 font-medium">
                  Email
                </label>
                <input
                  type="email"
                  id="email"
                  name="email"
                  className="w-full px-3 py-2 rounded-lg border-2 border-gray-200 focus:outline-none focus:border-blue-500"
                  placeholder="Enter your email"
                  required
                />
              </div>
              <div className="mb-4">
                <label htmlFor="password" className="block text-gray-700 font-medium">
                  Password
                </label>
                <input
                  type="password"
                  id="password"
                  name="password"
                  className="w-full px-3 py-2 rounded-lg border-2 border-gray-200 focus:outline-none focus:border-blue-500"
                  placeholder="Enter your password"
                  required
                />
              </div>
              <div className="flex justify-between">
                <button
                  type="submit"
                  className="bg-blue-500 text-white py-2 px-4 rounded-lg hover:bg-blue-600 transition-colors"
                >
                  Register
                </button>
                <button
                  type="button"
                  onClick={() => setShowRegisterModal(false)}
                  className="bg-gray-300 text-gray-800 py-2 px-4 rounded-lg hover:bg-gray-400 transition-colors"
                >
                  Cancel
                </button>
              </div>
            </form>
          </div>
        </div>
      )}

      {/* Reset Password Modal */}
      {showResetModal && (
        <div className="fixed inset-0 bg-gray-800 bg-opacity-75 flex items-center justify-center">
          <div className="bg-white rounded-lg shadow-md p-6 w-96">
            <h2 className="text-2xl font-bold mb-4">Reset Password</h2>
            <form onSubmit={handlePasswordReset}>
              <div className="mb-4">
                <label htmlFor="resetEmail" className="block text-gray-700 font-medium">
                  Email
                </label>
                <input
                  type="email"
                  id="resetEmail"
                  name="resetEmail"
                  value={resetEmail}
                  onChange={(e) => setResetEmail(e.target.value)}
                  className="w-full px-3 py-2 rounded-lg border-2 border-gray-200 focus:outline-none focus:border-blue-500"
                  placeholder="Enter your email"
                  required
                />
              </div>
              <div className="flex justify-between">
                <button
                  type="submit"
                  className="bg-blue-500 text-white py-2 px-4 rounded-lg hover:bg-blue-600 transition-colors"
                >
                  Send Reset Link
                </button>
                <button
                  type="button"
                  onClick={() => setShowResetModal(false)}
                  className="bg-gray-300 text-gray-800 py-2 px-4 rounded-lg hover:bg-gray-400 transition-colors"
                >
                  Cancel
                </button>
              </div>
            </form>
          </div>
        </div>
      )}
    </div>
  );
};

export default AuthLayout;
