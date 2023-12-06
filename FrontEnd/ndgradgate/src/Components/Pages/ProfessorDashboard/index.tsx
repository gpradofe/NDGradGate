import React, { useState, useEffect } from "react";
import { Container } from "react-bootstrap";
import { Applicant } from "../../../types/Application/Applicant";
import { Faculty } from "../../../types/Application/Faculty";
import FacultyDataGrid from "../../Atoms/FacultyDataGrid";
import { DashboardContainer, Section, Header } from "./styles";

const FacultyDashboard: React.FC = () => {
  const [applicants, setApplicants] = useState<Applicant[]>([]);
  const [faculty, setFaculty] = useState<Faculty>(/* Your faculty data here */);

  const handleAdvisorAcceptance = (applicantRef: number, facultyId: number) => {
    // Logic to accept the advisor role
    console.log(`Accept advisor role for applicant ${applicantRef}`);
  };

  const handleAdvisorRejection = (applicantRef: number, facultyId: number) => {
    // Logic to reject the advisor role
    console.log(`Reject advisor role for applicant ${applicantRef}`);
  };

  return (
    <DashboardContainer>
      <Container fluid>
        <Section>
          <Header>
            <header className="header-container">
              <div className="dashboard">
                <h1>Faculty Dashboard</h1>
              </div>
              <div className="user-info">
                <p>Current User: Tim Weninger</p>
              </div>
          </header>
          </Header>
          <FacultyDataGrid
            data={applicants}
            onAdvisorAcceptance={handleAdvisorAcceptance}
            onAdvisorRejection={handleAdvisorRejection}
          />
        </Section>
      </Container>
    </DashboardContainer>
  );
};

export default FacultyDashboard;
