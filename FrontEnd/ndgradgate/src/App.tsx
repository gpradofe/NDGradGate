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
import "react-toastify/dist/ReactToastify.css";

import styled from "styled-components";
import SideBar from "./Components/Organisms/SideBar";
import LoginPage from "./Components/Pages/Login";
import ApplicantOverview from "./Components/Pages/ApplicantOverview/AdminDashboard";
import AdminDashboard from "./Components/Pages/AdminDashboard";
import ReviewerOverviewPage from "./Components/Pages/ReviewerDashboard.py";
import FacultyDashboard from "./Components/Pages/ProfessorDashboard";
import AnalyticsDashboard from "./Components/Pages/AnalyticsDashboard";
import {
  ApplicationProvider,
  useApplicationContext,
} from "./context/ApplicationContext";
import { ToastContainer } from "react-toastify";

function MainContent() {
  const { currentUser } = useApplicationContext();

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
          {currentUser && (
            <>
              {currentUser.IsAdmin && (
                <>
                  <Route
                    path="/ApplicantOverview"
                    element={<ApplicantOverview />}
                  />
                  <Route path="/adminDashboard" element={<AdminDashboard />} />
                  <Route
                    path="/reviewerDashboard"
                    element={<ReviewerOverviewPage />}
                  />
                  <Route
                    path="/facultyDashboard"
                    element={<FacultyDashboard />}
                  />
                  <Route
                    path="/analyticsDashboard"
                    element={<AnalyticsDashboard />}
                  />
                </>
              )}
              {currentUser.IsReviewer && !currentUser.IsAdmin && (
                <>
                  <Route
                    path="/reviewerDashboard"
                    element={<ReviewerOverviewPage />}
                  />
                  <Route
                    path="/facultyDashboard"
                    element={<FacultyDashboard />}
                  />
                </>
              )}
              {!currentUser.IsReviewer && !currentUser.IsAdmin && (
                <Route
                  path="/facultyDashboard"
                  element={<FacultyDashboard />}
                />
              )}
            </>
          )}
        </Routes>
      </ContentContainer>
    </div>
  );
}

function App() {
  return (
    <ApplicationProvider>
      <Router>
        <MainContent />
      </Router>
      <ToastContainer />
    </ApplicationProvider>
  );
}

export default App;
