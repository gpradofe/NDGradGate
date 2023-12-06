import React, { useEffect, useRef, useState } from "react";
import { Container, Row, Col } from "react-bootstrap";
import { DashboardContainer, Header, Section } from "./styles";
import { Toast } from "primereact/toast";
import { Applicant } from "../../../../types/Application/Applicant";
import { Faculty } from "../../../../types/Application/Faculty";
import DataGrid from "../../../Atoms/DataGrid";
import apiServiceInstance from "../../../../services/ApiService";

const ApplicantOverview: React.FC = () => {
  const [applications, setApplications] = useState<Applicant[]>([]);
  const [filteredApplications, setFilteredApplications] = useState<Applicant[]>(
    []
  );
  const [globalFilter, setGlobalFilter] = useState("");
  const [faculty, setFaculty] = useState<Faculty[]>([]);
  const [visibleColumns, setVisibleColumns] = useState<{
    [key: string]: boolean;
  }>({
    FirstName: true,
    LastName: false,
    Email: false,
    ApplicationStatus: true,
    AreaOfStudy: true,
    CitizenshipCountry: true,
    DepartmentRecommendation: false,
    Ethnicity: false,
    Sex: false,
    AcademicHistories: false,
  });
  const toast = useRef<Toast>(null);

  const showSuccess = (message: string) => {
    toast.current?.show({
      severity: "success",
      summary: "Success",
      detail: message,
      life: 3000,
    });
  };

  useEffect(() => {
    apiServiceInstance
      .fetchApplications()
      .then((response) => {
        setApplications(response);
        setFilteredApplications(response);
      })
      .catch((error) => console.error("Error fetching applications:", error));
  }, []);
  const assignReviewer = (applicationRef: number, reviewerId: number) => {
    console.log(
      `Assign reviewer ${reviewerId} to application ${applicationRef}`
    );
  };

  const updateApplicationStatus = (applicationRef: number, status: string) => {
    console.log(`Update application ${applicationRef} status to ${status}`);
  };
  const facultyOptions = faculty.map((fac) => ({
    label: fac.name,
    value: fac.id,
  }));
  useEffect(() => {
    apiServiceInstance
      .fetchFaculty()
      .then((response) => {
        setFaculty(response); // Make sure the API returns the data in `response.data`
      })
      .catch((error) => console.error("Error fetching faculty:", error));
  }, []);
  return (
    <DashboardContainer>
      <Toast ref={toast} />
      <Container fluid>
        <Row>
          <Col md={12}>
            <Section>
              <Header>Application Overview</Header>
              <DataGrid
                data={filteredApplications}
                globalFilter={globalFilter}
                onGlobalFilterChange={(e) => setGlobalFilter(e.target.value)}
                onAssignReviewer={assignReviewer}
                onUpdateApplicationStatus={updateApplicationStatus}
                facultyOptions={facultyOptions}
                visibleColumns={visibleColumns}
                setVisibleColumns={setVisibleColumns}
                showSuccess={showSuccess}
              />
            </Section>
          </Col>
        </Row>
      </Container>
    </DashboardContainer>
  );
};

export default ApplicantOverview;
