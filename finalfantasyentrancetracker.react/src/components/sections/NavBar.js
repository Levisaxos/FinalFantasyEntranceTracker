import { User } from 'lucide-react';

const Navbar = () => {
    return (
      <nav className="h-16 bg-secondary border-b flex items-center justify-between px-4 z-0">
        <div className="text-lg font-semibold">My Application</div>
        <button className="p-2 hover:bg-gray-100 rounded-full">
          <User className="w-6 h-6 text-gray-600" />
        </button>
      </nav>
    );
  };

  export default Navbar