import { Carousel, Button, Card } from 'flowbite-react';
import { FaUser, FaShoppingCart, FaClipboardList } from 'react-icons/fa';
import { Link } from 'react-router-dom'; // Import Link for navigation
import CustomFooter from '../components/common/CustomFooter';

export default function Home() {
  const authData = JSON.parse(localStorage.getItem("auth"));
  const userRole = authData?.role; // User rol

  return (
    <div className="bg-gray-100 min-h-screen p-4">
      <header className="bg-gray-800 text-white text-center py-5">
        <h1 className="text-4xl font-bold underline">Welcome Sir</h1>
        <p className="text-lg mt-2">The best experience to manage your products</p>
      </header>

      <main className="container mx-auto mt-10">
        {/* Carousel Section */}
        <div className="mb-10">
          <Carousel>
            <img src="https://via.placeholder.com/800x400" alt="Carousel image 1" />
            <img src="https://via.placeholder.com/800x400" alt="Carousel image 2" />
            <img src="https://via.placeholder.com/800x400" alt="Carousel image 3" />
          </Carousel>
        </div>

        {/* Grid Section */}
        <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
          {/* Card 1 - Categories */}
          <Card className="flex flex-col items-center text-center">
            <FaClipboardList className="text-yellow-500 text-6xl mb-4" />
            <h2 className="text-2xl font-bold">Categories</h2>
            <p className="mt-2 text-gray-600">Explore a wide variety of product categories.</p>
            <Link to="/categories" className="mt-4 relative inline-flex items-center justify-center p-0.5 mb-2 mr-2 overflow-hidden text-sm font-medium text-gray-900 rounded-lg group bg-gradient-to-br from-cyan-500 to-blue-500 group-hover:from-cyan-500 group-hover:to-blue-500 hover:text-white dark:text-white focus:ring-4 focus:outline-none focus:ring-cyan-200 dark:focus:ring-cyan-800">
              <span className="relative px-5 py-2.5 transition-all ease-in duration-75 bg-white dark:bg-gray-900 rounded-md group-hover:bg-opacity-0">
                See more
              </span>
            </Link>
          </Card>

          {/* Card 2 - Products */}
          <Card className="flex flex-col items-center text-center">
            <FaShoppingCart className="text-green-500 text-6xl mb-4" />
            <h2 className="text-2xl font-bold">Products</h2>
            <p className="mt-2 text-gray-600">Discover and manage all available products.</p>
            <Link to="/products" className="mt-4 relative inline-flex items-center justify-center p-0.5 mb-2 mr-2 overflow-hidden text-sm font-medium text-gray-900 rounded-lg group bg-gradient-to-br from-cyan-500 to-blue-500 group-hover:from-cyan-500 group-hover:to-blue-500 hover:text-white dark:text-white focus:ring-4 focus:outline-none focus:ring-cyan-200 dark:focus:ring-cyan-800">
              <span className="relative px-5 py-2.5 transition-all ease-in duration-75 bg-white dark:bg-gray-900 rounded-md group-hover:bg-opacity-0">
                See more
              </span>
            </Link>
          </Card>

          {/* Card 3 - Clients */}
          {userRole === "Admin" && (
          <Card className="flex flex-col items-center text-center">
            <FaUser className="text-blue-500 text-6xl mb-4" />
            <h2 className="text-2xl font-bold">Users</h2>
            <p className="mt-2 text-gray-600">Access and manage all users in the system as admin.</p>
            <Link to="/users" className="mt-4 relative inline-flex items-center justify-center p-0.5 mb-2 mr-2 overflow-hidden text-sm font-medium text-gray-900 rounded-lg group bg-gradient-to-br from-cyan-500 to-blue-500 group-hover:from-cyan-500 group-hover:to-blue-500 hover:text-white dark:text-white focus:ring-4 focus:outline-none focus:ring-cyan-200 dark:focus:ring-cyan-800">
              <span className="relative px-5 py-2.5 transition-all ease-in duration-75 bg-white dark:bg-gray-900 rounded-md group-hover:bg-opacity-0">
                See more
              </span>
            </Link>
          </Card>
          )}

          {/* Card 4 - Orders */}
{/*           {userRole === "Admin" && (
          <Card className="flex flex-col items-center text-center">
            <FaClipboardList className="text-red-500 text-6xl mb-4" />
            <h2 className="text-2xl font-bold">Orders</h2>
            <p className="mt-2 text-gray-600">Watch a detailed control with all of your orders and more.</p>
            <Link to="/orders" className="mt-4 relative inline-flex items-center justify-center p-0.5 mb-2 mr-2 overflow-hidden text-sm font-medium text-gray-900 rounded-lg group bg-gradient-to-br from-cyan-500 to-blue-500 group-hover:from-cyan-500 group-hover:to-blue-500 hover:text-white dark:text-white focus:ring-4 focus:outline-none focus:ring-cyan-200 dark:focus:ring-cyan-800">
              <span className="relative px-5 py-2.5 transition-all ease-in duration-75 bg-white dark:bg-gray-900 rounded-md group-hover:bg-opacity-0">
                See more
              </span>
            </Link>
          </Card>
          )} */}
        </div>
        <CustomFooter />
      </main>
    </div>
  );
}
