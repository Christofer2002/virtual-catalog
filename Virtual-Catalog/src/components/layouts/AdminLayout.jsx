import { Outlet } from 'react-router-dom';
import AdminSidebar from '../components/common/AdminSidebar';
import AdminHeader from '../components/common/AdminHeader';

const AdminLayout = () => {
  return (
    <div className="admin-layout">
      <AdminHeader />
      <div className="admin-layout__content">
        <AdminSidebar />
        <main>
          <Outlet />
        </main>
      </div>
    </div>
  );
};

export default AdminLayout;
