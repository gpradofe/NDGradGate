import React, { useRef, useState } from "react";
import { Container, Tab, Tabs } from "@mui/material";
import { DashboardContainer, Header } from "./styles";
import FacultyManagementGrid from "../../Atoms/FacultyManagementGrid";
import SettingsPanel from "../../Atoms/SettingsPanel";

const AdminDashboard: React.FC = () => {
  const tabIndexRef = useRef(0);
  const [tabIndex, setTabIndex] = useState(0);

  const handleTabChange = (event: React.SyntheticEvent, newValue: number) => {
    console.log("handleTabChange - New Tab Index:", newValue);
    tabIndexRef.current = newValue;
    setTabIndex(newValue);
  };

  return (
    <DashboardContainer>
      <Container>
        <Header>Admin Dashboard</Header>
        <Tabs
          value={tabIndex}
          onChange={handleTabChange}
          aria-label="Admin dashboard tabs"
        >
          <Tab label="Faculty Management" />
          <Tab label="Settings" />
        </Tabs>

        <FacultyManagementGrid valueTabIndex={tabIndex} index={0} />
        <SettingsPanel valueTabIndex={tabIndex} index={1} />
      </Container>
    </DashboardContainer>
  );
};

export default AdminDashboard;
