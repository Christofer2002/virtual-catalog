import { Outlet } from 'react-router-dom';

const MinimalLayout = () => {
  return (
    <div className="minimal-layout">
      <Outlet />
    </div>
  );
};

export default MinimalLayout;
