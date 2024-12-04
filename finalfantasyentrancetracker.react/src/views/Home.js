import { Navbar, Sidebar, Footer } from "../components/sections.js";
const HomePage = ({ children }) => {
    return (
      <div className="h-screen flex flex-col">
        <Navbar />
        <div className="flex flex-1 overflow-hidden">
          <Sidebar />
          <main className="flex-1 overflow-auto p-4">
            
          </main>
        </div>
        <Footer />
      </div>
    );
  };
  export default HomePage