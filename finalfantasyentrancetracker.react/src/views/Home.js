import Navbar from '../components/NavBar'
import Sidebar from '../components/SideBar'
import Footer from '../components/Footer'

const HomePage = ({ children }) => {
    return (
      <div className="h-screen flex flex-col">
        <Navbar />
        <div className="flex flex-1 overflow-hidden">
          <Sidebar />
          <main className="flex-1 overflow-auto p-4">
            {children}
          </main>
        </div>
        <Footer />
      </div>
    );
  };
  export default HomePage