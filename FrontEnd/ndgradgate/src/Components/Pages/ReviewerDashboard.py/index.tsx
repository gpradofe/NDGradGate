import React, { useEffect, useRef, useState } from "react";
import { Container, Row, Col } from "react-bootstrap";
import { DashboardContainer, Header, Section } from "./styles";
import { Toast } from "primereact/toast";
import { Faculty } from "../../../types/Application/Faculty";
import { Applicant } from "../../../types/Application/Applicant";
import ReviewerDataGrid from "../../Atoms/ReviewerDataGrid";

const ReviewerOverviewPage: React.FC = () => {
  const [assignedApplications, setAssignedApplications] = useState<Applicant[]>(
    []
  );
  const [faculty, setFaculty] = useState<Faculty[]>([]);
  const toast = useRef<Toast>(null);

  const recommendFaculties = (
    applicationRef: number,
    facultyNames: string[]
  ) => {
    console.log(
      `Recommend faculties ${facultyNames} for application ${applicationRef}`
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
    label: fac.Name,
    value: fac.Id,
  }));

  return (
    <DashboardContainer>
      <Toast ref={toast} />
      <Container fluid>
        <Row>
          <Col md={12}>
            <Section>
              <Header>Reviewer Overview</Header>
              <ReviewerDataGrid />
            </Section>
          </Col>
        </Row>
      </Container>
    </DashboardContainer>
  );
};

export default ReviewerOverviewPage;
