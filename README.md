# Virtual Catalog

**Virtual Catalog** is a web-based platform designed to efficiently manage and showcase a catalog of products and categories. This project incorporates **role-based access control (RBAC)** and user management to streamline product and category operations. Built with **ASP.NET Core** in the backend and **React** in the frontend, Virtual Catalog emphasizes security, scalability, and seamless user experience.

---

## 📋 Features

- **Product and Category Management:** Create, update, view, and delete products and categories.
- **Role-Based Access Control:** Roles for Admin and User with specific permissions.
- **JWT Authentication:** Secure token-based authentication.
- **RESTful API:** Scalable backend with ASP.NET Core.
- **Modern Frontend:** Interactive UI built with React.
- **Database Migration Management:** Flyway is used to manage database migrations.

---

## 🛠️ Technologies Used

### Backend
- ASP.NET Core
- Microsoft SQL Server
- Flyway (for database migrations)
- Entity Framework Core

### Frontend
- React
- Vite.js

---

## 🚀 Installation and Setup

### Prerequisites
Make sure you have the following installed:

- [.NET SDK 8.0+](https://dotnet.microsoft.com/download)
- [Node.js 18+](https://nodejs.org/) and npm (bundled with Node.js)
- [Microsoft SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Flyway CLI](https://flywaydb.org/getstarted)

---

### Backend (VirtualCatalogAPI)

1. **Clone the repository**
   ```bash
   git clone https://github.com/your-username/virtual-catalog.git
   cd virtual-catalog/VirtualCatalogAPI
   ```

2. Configure the database connection
Open `appsettings.json` and update the connection string with your SQL Server credentials:
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=VirtualCatalogDB;User Id=YOUR_USER;Password=YOUR_PASSWORD;TrustServerCertificate=true;"
}

3. Configure Flyway for database migrations
Ensure `flyway.conf` exists in the project root directory and includes the following:
flyway.url=jdbc:sqlserver://YOUR_SERVER;databaseName=VirtualCatalogDB;encrypt=true;trustServerCertificate=true
flyway.user=YOUR_USER
flyway.password=YOUR_PASSWORD
flyway.locations=filesystem:./Migrations

4. Apply Flyway migrations
flyway migrate

5. Restore backend dependencies
dotnet restore

6. Run the backend server
dotnet run
The frontend will now be available at `http://localhost:7278`

---

### Frontend (Virtual-Catalog)

1. Navigate to the frontend directory
cd ../Virtual-Catalog

2. Install dependencies
npm install

3. Set up environment variables
# Create a `.env` file in the frontend root with the following content:
VITE_API_BASE_URL=https://localhost:5001/api

4. Run the development server
npm run dev
The frontend will now be available at `http://localhost:5173`

