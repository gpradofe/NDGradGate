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

const SideBar: React.FC<{ isOpen: boolean; toggleSidebar: () => void }> = ({
  isOpen,
  toggleSidebar,
}) => {
  // Note: This is a placeholder. You will need to fetch the user's name and image from MetaMask.

  return (
    <>
      <SideBarContainer isOpen={isOpen}>
        <CloseButton onClick={toggleSidebar}>
          &times; {/* This is an "X" close icon */}
        </CloseButton>
        <MenuTitle>Menu</MenuTitle>
        <NavList>
          <NavItem>
            <StyledNavLink
              to="/applicantoverview"
              className={({ isActive }) => (isActive ? "active" : "")}
            >
              Applicant Overview
            </StyledNavLink>
          </NavItem>
          <NavItem>
            <StyledNavLink
              to="/AdminDashboard"
              className={({ isActive }) => (isActive ? "active" : "")}
            >
              Admin Dashboard
            </StyledNavLink>
          </NavItem>
          <NavItem></NavItem>
        </NavList>
      </SideBarContainer>
      {!isOpen && (
        <ToggleSidebarButton onClick={toggleSidebar}>
          &#9776;
        </ToggleSidebarButton>
      )}
    </>
  );
};

export default SideBar;
