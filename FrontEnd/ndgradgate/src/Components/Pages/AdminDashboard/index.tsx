import React, { useEffect, useMemo, useState } from "react";
import { Container, Row, Col, Form } from "react-bootstrap";
import {
  ApplicationRow,
  AssignmentForm,
  DashboardContainer,
  ExportButton,
  FacultyList,
  Header,
  SearchInput,
  Section,
  StyledButton,
  Table,
} from "./styles";
import { CSVLink } from "react-csv";

type SortConfig = {
  key: keyof Application | null;
  direction: "ascending" | "descending";
};
interface TableRowProps {
  app: Application;
}

const TableRow: React.FC<TableRowProps> = ({ app }) => (
  <tr>
    <td>{app.name}</td>
    <td>{app.status}</td>
  </tr>
);
interface Application {
  id: number;
  name: string;
  status: string;
  assignedTo: string;
}

interface Faculty {
  id: number;
  name: string;
  department: string;
}

const mockFaculty: Faculty[] = [
  { id: 1, name: "Dr. Smith", department: "Computer Science" },
  { id: 2, name: "Dr. Johnson", department: "Engineering" },
  // Add more faculty members
];

const AdminDashboard: React.FC = () => {
  const [applications, setApplications] = useState<Application[]>([]);
  const [faculty, setFaculty] = useState<Faculty[]>([]);
  const [searchTerm, setSearchTerm] = useState<string>("");
  const [sortConfig, setSortConfig] = useState<SortConfig>({
    key: null,
    direction: "ascending",
  });
  useEffect(() => {
    const fetchApplications = async () => {
      try {
        const response = await fetch(
          "https://localhost:5009/api/Applicant/GetAllApplicants",
          {
            headers: {
              accept: "application/json",
            },
            mode: "no-cors",
          }
        );
        console.log(response);
        if (!response.ok) {
          throw new Error(`HTTP error! status: ${response.status}`);
        }
        const data = await response.json();
        setApplications(data);
      } catch (error) {
        if (error instanceof Error) {
          console.error("Fetching applications failed: ", error.message);
        } else {
          console.error(
            "Fetching applications failed: An unknown error occurred"
          );
        }
      }
    };

    fetchApplications();
  }, []);

  const requestSort = (key: keyof Application) => {
    let direction: "ascending" | "descending" = "ascending";
    if (sortConfig.key === key && sortConfig.direction === "ascending") {
      direction = "descending";
    }
    setSortConfig({ key, direction });
  };

  useEffect(() => {
    setFaculty([
      { id: 1, name: "Dr. Smith", department: "Computer Science" },
      { id: 2, name: "Dr. Johnson", department: "Engineering" },
      // ... more faculty members
    ]);
  }, []);

  const handleAssignment = (appId: number, facultyName: string) => {
    // TODO: Implement assignment logic
  };

  const handleApplicationStatusChange = (appId: number, status: string) => {
    // TODO: Implement status change logic
  };

  const filteredFaculty = faculty.filter((f) =>
    f.name.toLowerCase().includes(searchTerm.toLowerCase())
  );

  return (
    <DashboardContainer>
      <Container fluid>
        <Row>
          <Col md={12}>
            <Section>
              <Header>Application Overview</Header>
              <Table>
                <thead>
                  <tr>
                    <th onClick={() => requestSort("name")}>Name</th>
                    <th>Status</th>
                  </tr>
                </thead>
                <tbody>
                  {applications.map((app) => (
                    <TableRow key={app.id} app={app} />
                  ))}
                </tbody>
              </Table>
            </Section>
          </Col>
        </Row>
        <Row>
          <Col md={6}>
            <Section>
              <Header>Assign Applications</Header>
              {applications.map((app) => (
                <AssignmentForm key={app.id}>
                  <span>{app.name}</span>
                  <Form.Select defaultValue={app.assignedTo}>
                    {mockFaculty.map((faculty) => (
                      <option key={faculty.id} value={faculty.name}>
                        {faculty.name}
                      </option>
                    ))}
                  </Form.Select>
                  <StyledButton variant="primary">Assign</StyledButton>
                </AssignmentForm>
              ))}
            </Section>
          </Col>

          <Col md={6}>
            <Section>
              <Header>Committee Composition</Header>
              <SearchInput
                type="text"
                placeholder="Search faculty..."
                value={searchTerm}
                onChange={(e) => setSearchTerm(e.target.value)}
              />
              <FacultyList>
                {filteredFaculty.map((member) => (
                  <li key={member.id}>
                    {member.name} ({member.department})
                  </li>
                ))}
              </FacultyList>
            </Section>
          </Col>
        </Row>

        <Col md={12}>
          <Section>
            <Header>Manage Applications</Header>
            {applications.map((app) => (
              <ApplicationRow key={app.id}>
                <span>{app.name}</span>
                <div>
                  <StyledButton variant="success">Accept</StyledButton>
                  <StyledButton variant="warning">Hold</StyledButton>
                  <StyledButton variant="danger">Reject</StyledButton>
                </div>
              </ApplicationRow>
            ))}
          </Section>
        </Col>

        <Col md={12}>
          <Section>
            <Header>Data Export</Header>
            <CSVLink
              data={applications}
              filename={"applications.csv"}
              className="btn btn-primary"
            >
              Export to CSV
            </CSVLink>
          </Section>
        </Col>
      </Container>
    </DashboardContainer>
  );
};

export default AdminDashboard;
