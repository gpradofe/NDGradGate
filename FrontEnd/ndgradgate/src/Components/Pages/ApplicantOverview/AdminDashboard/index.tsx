import React, { useRef, useState, useMemo } from "react";
import { Container, Row, Col } from "react-bootstrap";
import { DashboardContainer, Header, Section } from "./styles";
import { Toast } from "primereact/toast";
import DataGrid from "../../../Atoms/DataGrid";
import { useApplicationContext } from "../../../../context/ApplicationContext";

const ApplicantOverview: React.FC = () => {
  const { applications, faculty } = useApplicationContext();
  const [globalFilter, setGlobalFilter] = useState("");
  const [visibleColumns, setVisibleColumns] = useState<{
    [key: string]: boolean;
  }>({
    FirstName: true,
    LastName: true,
    Email: true,
    ApplicationStatus: false,
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

  const assignReviewer = (applicationRef: number, reviewerId: number) => {
    console.log(
      `Assign reviewer ${reviewerId} to application ${applicationRef}`
    );
  };

  const updateApplicationStatus = (applicationRef: number, status: string) => {
    console.log(`Update application ${applicationRef} status to ${status}`);
  };

  const filteredApplications = useMemo(() => {
    return applications.filter((application) =>
      Object.values(application).some((value) =>
        value.toString().toLowerCase().includes(globalFilter.toLowerCase())
      )
    );
  }, [applications, globalFilter]);

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
