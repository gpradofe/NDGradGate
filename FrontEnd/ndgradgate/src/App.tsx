import React, { useState } from "react";
import {
  BrowserRouter as Router,
  Route,
  Routes,
  useLocation,
} from "react-router-dom";
import "bootstrap/dist/css/bootstrap.min.css";
import "primereact/resources/themes/lara-light-indigo/theme.css";
import "primeicons/primeicons.css";
import "primereact/resources/primereact.css";
import "primeflex/primeflex.css";
import "primereact/resources/primereact.min.css";
import "@fontsource/roboto/300.css";
import "@fontsource/roboto/400.css";
import "@fontsource/roboto/500.css";
import "@fontsource/roboto/700.css";

import styled from "styled-components";
import SideBar from "./Components/Organisms/SideBar";
import LoginPage from "./Components/Pages/Login";
import ApplicantOverview from "./Components/Pages/ApplicantOverview/AdminDashboard";
import AdminDashboard from "./Components/Pages/AdminDashboard";
import ReviewerOverviewPage from "./Components/Pages/ReviewerDashboard.py";
import FacultyDashboard from "./Components/Pages/ProfessorDashboard";
import AnalyticsDashboard from "./Components/Pages/AnalyticsDashboard";

function MainContent() {
  const [isSidebarOpen, setIsSidebarOpen] = useState(true);
  const location = useLocation();

  const toggleSidebar = () => {
    console.log("Toggling sidebar");
    setIsSidebarOpen(!isSidebarOpen);
  };

  const showSidebar = location.pathname !== "/";
  const ContentContainer = styled.div`
    margin-left: ${showSidebar && isSidebarOpen ? "250px" : "0"};
    transition: margin-left 0.3s;
  `;

  return (
    <div className="App">
      {showSidebar && (
        <SideBar isOpen={isSidebarOpen} toggleSidebar={toggleSidebar} />
      )}
      <ContentContainer>
        <Routes>
          <Route path="/" element={<LoginPage />} />
          <Route path="/ApplicantOverview" element={<ApplicantOverview />} />
          <Route path="/facultyDashboard" element={<FacultyDashboard />} />
          <Route path="/reviewerDashboard" element={<ReviewerOverviewPage />} />
          <Route path="/adminDashboard" element={<AdminDashboard />} />
          <Route path="/analyticsDashboard" element={<AnalyticsDashboard />} />
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
