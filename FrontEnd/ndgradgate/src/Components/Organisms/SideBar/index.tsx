import React from "react";
import {
  SideBarContainer,
  MenuTitle,
  CloseButton,
  ToggleSidebarButton,
  NavItem,
  NavList,
  StyledNavLink,
} from "./styles";
import { useApplicationContext } from "../../../context/ApplicationContext";
import { useNavigate } from "react-router-dom";
import { Button } from "react-bootstrap";

const SideBar: React.FC<{ isOpen: boolean; toggleSidebar: () => void }> = ({
  isOpen,
  toggleSidebar,
}) => {
  const { currentUser, setCurrentUser } = useApplicationContext();
  const navigate = useNavigate();

  const handleLogout = () => {
    setCurrentUser(null); // Clear the current user from context
    localStorage.removeItem("currentUser"); // Clear the user from localStorage
    navigate("/"); // Redirect to the login page
  };

  return (
    <SideBarContainer isOpen={isOpen}>
      <CloseButton onClick={toggleSidebar}>&times;</CloseButton>
      <MenuTitle>Menu</MenuTitle>
      <NavList>
        {currentUser?.IsAdmin && (
          <>
            <NavItem>
              <StyledNavLink to="/applicantoverview">
                Applicant Overview
              </StyledNavLink>
            </NavItem>
            <NavItem>
              <StyledNavLink to="/AdminDashboard">
                Admin Dashboard
              </StyledNavLink>
            </NavItem>
            <NavItem>
              <StyledNavLink to="/ReviewerDashboard">
                Reviewer Dashboard
              </StyledNavLink>
            </NavItem>
            <NavItem>
              <StyledNavLink to="/facultyDashboard">
                Professor Dashboard
              </StyledNavLink>
            </NavItem>
            <NavItem>
              <StyledNavLink to="/analyticsDashboard">Analytics</StyledNavLink>
            </NavItem>
          </>
        )}
        {currentUser?.IsReviewer && !currentUser?.IsAdmin && (
          <>
            <NavItem>
              <StyledNavLink to="/ReviewerDashboard">
                Reviewer Dashboard
              </StyledNavLink>
            </NavItem>
            <NavItem>
              <StyledNavLink to="/facultyDashboard">
                Professor Dashboard
              </StyledNavLink>
            </NavItem>
          </>
        )}
        {!currentUser?.IsReviewer && !currentUser?.IsAdmin && (
          <>
            {/* Links available for Faculty (not Reviewer or Admin) */}
            <NavItem>
              <StyledNavLink to="/facultyDashboard">
                Professor Dashboard
              </StyledNavLink>
            </NavItem>
          </>
        )}
      </NavList>
      {!isOpen && (
        <ToggleSidebarButton onClick={toggleSidebar}>
          &#9776;
        </ToggleSidebarButton>
      )}
      {currentUser && (
        <div
          style={{
            display: "flex",
            justifyContent: "space-between",
            alignItems: "center",
            padding: "10px",
          }}
        >
          <span>{currentUser.Name}</span>
          <Button variant="outline-secondary" size="sm" onClick={handleLogout}>
            Logout
          </Button>
        </div>
      )}
    </SideBarContainer>
  );
};

export default SideBar;
