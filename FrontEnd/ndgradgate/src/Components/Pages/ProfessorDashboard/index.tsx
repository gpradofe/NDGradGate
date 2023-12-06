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
          <table style={{ width: "100%" }} >
                <thead>
                  <tr style={{ border: "1px solid black" }}>
                    <th>First Name</th>
                    <th>Applicant Status</th>
                    <th>Area of Study</th>
                    <th>Want Student</th>
                    <th>Comments</th>
                  </tr>
                </thead>
                <tbody>
                  <tr style={{ border: "1px solid black" }}>
                    <td>Samuel</td>
                    <td>In Progress</td>
                    <td>Data Mining and Machine Learning</td>
                    <td>
                      <input type="checkbox" id="yes" name="yes" value="Yes"></input>
                      <label> Yes</label><br></br>
                      <input type="checkbox" id="no" name="no" value="No"></input>
                      <label> No</label>
                    </td>
                    <td><form action="/submit" method="post">
         <input type="text" id="comments_weninger" name="comments_weninger" placeholder="None"></input>
    </form></td>
                  </tr>
                </tbody>
              </table>
        </Section>
      </Container>
      <Container>
        <Section>
          Number of Students admitted for Tim Weninger: 0  <br></br>
          Number of Students needed for Tim Weninger: <input type="text" id="comments_weninger" name="comments_weninger" placeholder="1"></input>
        </Section>
      </Container>
    </DashboardContainer>
  );
};

export default FacultyDashboard;
