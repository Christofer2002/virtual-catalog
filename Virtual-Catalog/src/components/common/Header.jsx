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
          {/* Logo */}
          <div className="flex items-center">
            <div className="flex-shrink-0">
              <Link className="nav-link text-white" to="/virtual-catalog">
                <GiAbstract065 className="text-white text-3xl" />
              </Link>
            </div>
            {/* Desktop Menu */}
            <div className="hidden md:flex ml-10 space-x-6">
              <Link className="nav-link text-white font-extrabold" to="/virtual-catalog/virtual-catalog">
                Inicio
              </Link>
              <Link className="nav-link text-white hover:text-gray-300" to="/virtual-catalog/categories">
                Categories
              </Link>
              <Link className="nav-link text-white hover:text-gray-300" to="/virtual-catalog/products">
                Products
              </Link>
              {authData?.role === "Admin" && (
                <Link className="nav-link text-white hover:text-gray-300" to="/virtual-catalog/users">
                  Users
                </Link>
              )}
            </div>
          </div>
          {/* Logout or Login */}
          <div className="hidden md:block">
            <div className="ml-4 flex items-center">
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
                  to="/virtual-catalog/authlayout"
                  className="text-gray-300 hover:bg-gray-700 hover:text-white px-3 py-2 rounded-md text-sm font-medium flex items-center"
                >
                  <FaUserCircle className="mr-2" />
                  Login
                </Link>
              )}
            </div>
          </div>
          {/* Mobile Menu */}
          <div className="md:hidden flex items-center">
            <button
              onClick={toggleDropdown}
              className="text-gray-300 hover:bg-gray-700 hover:text-white px-3 py-2 rounded-md text-sm font-medium flex items-center"
            >
              <FaChevronDown
                className={`ml-2 transition-transform ${isDropdownOpen ? "transform rotate-180" : ""
                  }`}
              />
            </button>
          </div>
        </div>
        {/* Mobile Dropdown */}
        {isDropdownOpen && (
          <div className="md:hidden">
            <div className="px-2 pt-2 pb-3 space-y-1 sm:px-3">
              <Link
                to="/virtual-catalog"
                className="text-gray-300 hover:bg-gray-700 hover:text-white block px-3 py-2 rounded-md text-base font-medium"
              >
                Home
              </Link>
              <Link
                to="/virtual-catalog/categories"
                className="text-gray-300 hover:bg-gray-700 hover:text-white block px-3 py-2 rounded-md text-base font-medium"
              >
                Categories
              </Link>
              <Link
                to="/virtual-catalog/products"
                className="text-gray-300 hover:bg-gray-700 hover:text-white block px-3 py-2 rounded-md text-base font-medium"
              >
                Products
              </Link>
              {authData?.role === "Admin" && (
                <Link
                  to="/virtual-catalog/users"
                  className="text-gray-300 hover:bg-gray-700 hover:text-white block px-3 py-2 rounded-md text-base font-medium"
                >
                  Users
                </Link>
              )}
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
                  to="/virtual-catalog/authlayout"
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
