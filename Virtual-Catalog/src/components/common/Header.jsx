import { Link, useNavigate } from "react-router-dom";
import { useContext, useState } from "react";
import { FaUserCircle, FaChevronDown } from "react-icons/fa";
import { GiAbstract065 } from "react-icons/gi";
import { AuthContext } from "../../context/AuthContext";
import { toast } from "react-toastify";

const Header = () => {
  const navigate = useNavigate();
  const { authData, logout } = useContext(AuthContext);
  const [isDropdownOpen, setIsDropdownOpen] = useState(false);

  const toggleDropdown = () => {
    setIsDropdownOpen(!isDropdownOpen);
  };

  const handleLogout = async () => {
    try {
      logout();
      toast.success("Logged out successfully");
      navigate("/AuthLayout");
    } catch (error) {
      toast.error("Error during logout");
    }
  };

  return (
    <nav className="bg-gray-800">
      <div className="max-w-7xl mx-auto px-2 sm:px-6 lg:px-8">
        <div className="flex items-center justify-between h-16">
          <div className="flex items-center">
            <div className="flex-shrink-0">
              <Link className="nav-link text-white" to="/">
                <GiAbstract065 className="text-white text-3xl" />
              </Link>
            </div>
            <div className="hidden md:block">
              <div className="ml-10 flex items-baseline space-x-4">
                <Link className="nav-link text-white font-extrabold" to="/">
                  Inicio
                </Link>
                <div className="relative">
                  <button
                    onClick={toggleDropdown}
                    className="text-gray-300 hover:bg-gray-700 hover:text-white 
                    px-3 py-2 rounded-md text-sm font-medium flex items-center"
                  >
                    Pages
                    <FaChevronDown
                      className={`ml-2 transition-transform ${
                        isDropdownOpen ? "transform rotate-180" : ""
                      }`}
                    />
                  </button>
                  {isDropdownOpen && (
                    <div className="absolute right-0 mt-2 w-48 bg-white rounded-md shadow-lg z-20">
                      <Link
                        to="/categories"
                        className="block px-4 py-2 text-gray-800 hover:bg-gray-200"
                      >
                        Categories
                      </Link>
                      <Link
                        to="/products"
                        className="block px-4 py-2 text-gray-800 hover:bg-gray-200"
                      >
                        Products
                      </Link>
                      {authData?.role === "Admin" && (
                        <Link
                          to="/users"
                          className="block px-4 py-2 text-gray-800 hover:bg-gray-200"
                        >
                          Users
                        </Link>
                      )}
                      {/* {authData?.role === "Admin" && (
                        <Link
                          to="/orders"
                          className="block px-4 py-2 text-gray-800 hover:bg-gray-200"
                        >
                          Orders
                        </Link>
                      )} */}
                    </div>
                  )}
                </div>
              </div>
            </div>
          </div>
          <div className="hidden md:block">
            <div className="ml-4 flex items-center md:ml-6">
              {authData ? (
                <button
                  onClick={handleLogout}
                  className="text-gray-300 hover:bg-gray-700 hover:text-white px-3 py-2 rounded-md text-sm font-medium flex items-center"
                >
                  <FaUserCircle className="mr-2" />
                  Logout
                </button>
              ) : (
                <Link
                  to="/AuthLayout"
                  className="text-gray-300 hover:bg-gray-700 hover:text-white px-3 py-2 rounded-md text-sm font-medium flex items-center"
                >
                  <FaUserCircle className="mr-2" />
                  Login
                </Link>
              )}
            </div>
          </div>
          <div className="md:hidden flex items-center">
            <button
              onClick={toggleDropdown}
              className="text-gray-300 hover:bg-gray-700 hover:text-white 
              px-3 py-2 rounded-md text-sm font-medium flex items-center"
            >
              <FaChevronDown
                className={`ml-2 transition-transform ${
                  isDropdownOpen ? "transform rotate-180" : ""
                }`}
              />
            </button>
          </div>
        </div>
        {isDropdownOpen && (
          <div className="md:hidden">
            <div className="px-2 pt-2 pb-3 space-y-1 sm:px-3">
              <Link
                to="/"
                className="text-gray-300 hover:bg-gray-700 hover:text-white block px-3 py-2 rounded-md text-base font-medium"
              >
                Home
              </Link>
              <Link
                to="/categories"
                className="text-gray-300 hover:bg-gray-700 hover:text-white block px-3 py-2 rounded-md text-base font-medium"
              >
                Categories
              </Link>
              <Link
                to="/products"
                className="text-gray-300 hover:bg-gray-700 hover:text-white block px-3 py-2 rounded-md text-base font-medium"
              >
                Products
              </Link>
              {authData?.role === "Admin" && (
                <Link
                  to="/users"
                  className="text-gray-300 hover:bg-gray-700 hover:text-white block px-3 py-2 rounded-md text-base font-medium"
                >
                  Users
                </Link>
              )}
   {/*            {authData?.role === "Admin" && (
                <Link
                  to="/orders"
                  className="text-gray-300 hover:bg-gray-700 hover:text-white block px-3 py-2 rounded-md text-base font-medium"
                >
                  Orders
                </Link>
              )} */}
              {authData ? (
                <button
                  onClick={handleLogout}
                  className="text-gray-300 hover:bg-gray-700 hover:text-white block px-3 py-2 rounded-md text-base font-medium"
                >
                  <FaUserCircle className="mr-2" />
                  Logout
                </button>
              ) : (
                <Link
                  to="/AuthLayout"
                  className="text-gray-300 hover:bg-gray-700 hover:text-white block px-3 py-2 rounded-md text-base font-medium"
                >
                  <FaUserCircle className="mr-2" />
                  Login
                </Link>
              )}
            </div>
          </div>
        )}
      </div>
    </nav>
  );
};

export default Header;
