import React, { useEffect, useRef, useState } from "react";
import { Container, Row, Col } from "react-bootstrap";
import { DashboardContainer, Header, Section } from "./styles";
import { Toast } from "primereact/toast";
import { Faculty } from "../../../types/Application/Faculty";
import { Applicant } from "../../../types/Application/Applicant";
import ReviewerDataGrid from "../../Atoms/ReviewerDataGrid";
import { ChartContainer } from "../AnalyticsDashboard/styles";
import { Chart } from "primereact/chart";
import { Card } from "primereact/card";

const ReviewerOverviewPage: React.FC = () => {
  const [assignedApplications, setAssignedApplications] = useState<Applicant[]>(
    []
  );
  const [faculty, setFaculty] = useState<Faculty[]>([]);
  const toast = useRef<Toast>(null);

  const recommendFaculties = (applicationRef: number, facultyIds: number[]) => {
    console.log(
      `Recommend faculties ${facultyIds} for application ${applicationRef}`
    );
  };

  const recommendApplicationDecision = (
    applicationRef: number,
    decision: string
  ) => {
    console.log(
      `Recommend decision ${decision} for application ${applicationRef}`
    );
  };

  const facultyOptions = faculty.map((fac) => ({
    label: fac.name,
    value: fac.id,
  }));

   const statusBar = {
    labels: ["Todo", "In Progress", "Complete"],
    datasets: [
      {
        backgroundColor: [
          "#FF0000",
          "#FFCE56",
          "#00FF00",
        ],
        data: [0,1,0],
      },
    ],
  };

  return (
    <DashboardContainer>
      <Toast ref={toast} />
      <Container fluid>
        <Row>
          <Col md={12}>
            <Section>
              <Header>
                <header className="header-container">
                  <div className="dashboard">
                    <h1>Reviewer Dashboard</h1>
                  </div>
                  <div className="user-info">
                    <p>Current User: Tim Weninger</p>
                  </div>
                </header>
              </Header>
            </Section>
          </Col>
        </Row>
        <Row>
          <Col md={12}>
            <Section>
              {/* Your existing table */}
              <table style={{ width: "100%" }} >
                <thead>
                  <tr style={{ border: "1px solid black" }}>
                    <th>First Name</th>
                    <th>Applicant Status</th>
                    <th>Area of Study</th>
                    <th>Potential Faculty</th>
                    <th>Comments</th>
                  </tr>
                </thead>
                <tbody>
                  <tr style={{ border: "1px solid black" }}>
                    <td>Samuel</td>
                    <td>In Progress</td>
                    <td>Data Mining and Machine Learning</td>
                    <td>
                      <input type="checkbox" id="prof1" name="prof1" value="John Doe"></input>
                      <label> John Doe</label><br></br>
                      <input type="checkbox" id="prof2" name="prof2" value="Tim Weninger"></input>
                      <label> Tim Weninger</label>
                    </td>
                    <td><form action="/submit" method="post">
         <input type="text" id="comments_weninger" name="comments_weninger" placeholder="None"></input>
    </form></td>
                  </tr>
                </tbody>
              </table>
            </Section>
          </Col>
        </Row>

        <Row>
        <Col md={3}>
            <ChartContainer>
              <Card title="Status">
                <Chart type="doughnut" data={statusBar} />
              </Card>
            </ChartContainer>
          </Col>
        </Row>
      </Container>
    </DashboardContainer>
  );
};

export default ReviewerOverviewPage;
