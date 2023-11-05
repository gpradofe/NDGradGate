import React, { useState } from "react";
import {
  BrowserRouter as Router,
  Route,
  Routes,
  useLocation,
} from "react-router-dom";
import "bootstrap/dist/css/bootstrap.min.css";
import styled from "styled-components";
import SideBar from "./Components/Organisms/SideBar";
import LoginPage from "./Components/Pages/Login";
import AdminDashboard from "./Components/Pages/AdminDashboard";
import ProfessorDashboard from "./Components/Pages/ProfessorDashboard";
import ReviewerDashboard from "./Components/Pages/ReviewerDashboard.py";



function MainContent() {
  const [isSidebarOpen, setIsSidebarOpen] = useState(true);
  const location = useLocation(); // Now, this is inside the context of Router

  const toggleSidebar = () => {
    console.log("Toggling sidebar");
    setIsSidebarOpen(!isSidebarOpen);
  };

  const ContentContainer = styled.div`
    margin-left: ${isSidebarOpen ? "250px" : "0"};
    transition: margin-left 0.3s;
  `;

  return (
    <div className="App">
      {location.pathname !== "/" && ( // Check the current pathname
        <SideBar isOpen={isSidebarOpen} toggleSidebar={toggleSidebar} />
      )}
      <ContentContainer>
        <Routes>
          <Route path="/" element={<LoginPage />} />
          <Route path="/adminDashboard" element={<AdminDashboard />} />
          <Route path="/professorDashboard" element={<ProfessorDashboard />} />
          <Route path="/reviewerDashboard" element={<ReviewerDashboard />} />
        </Routes>
      </ContentContainer>
    </div>
  );
}

function App() {
  return (
    <Router>
      <MainContent />
    </Router>
  );
}

export default App;
