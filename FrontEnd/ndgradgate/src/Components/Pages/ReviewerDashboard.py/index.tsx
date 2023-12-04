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
    {/* Add more cells as needed */}
  </tr>
);
interface Application {
  id: number;
  name: string;
  status: string;
  assignedTo: string;
  //areaOfInterest: string;
}

interface Faculty {
  id: number;
  name: string;
  department: string;
  //numberOfStudents:int;
}

const mockApplications: Application[] = [
  {
    id: 1,
    name: "Alice Johnson",
    status: "Under Review",
    assignedTo: "Dr. Smith",
  },
  { id: 2, name: "Bob Brown", status: "Accepted", assignedTo: "Dr. Johnson" },
  // Add more applications
];

const mockFaculty: Faculty[] = [
  { id: 1, name: "Dr. Smith", department: "Computer Science" },
  { id: 2, name: "Dr. Johnson", department: "Engineering" },
  // Add more faculty members
];

const ReviewerDashboard: React.FC = () => {
  const [applications, setApplications] = useState<Application[]>([]);
  const [faculty, setFaculty] = useState<Faculty[]>([]);
  const [searchTerm, setSearchTerm] = useState<string>("");
  const [sortConfig, setSortConfig] = useState<SortConfig>({
    key: null,
    direction: "ascending",
  });

  const sortedApplications = useMemo(() => {
    if (sortConfig.key === null) {
      // If sort key is null, return the applications as is.
      return [...mockApplications];
    }

    // Now TypeScript knows sortConfig.key is not null here
    return [...mockApplications].sort((a, b) => {
      // Safely assume sortConfig.key is a valid key of Application.
      const aValue = a[sortConfig.key!];
      const bValue = b[sortConfig.key!];

      // You might want to handle non-string types here if your data can contain numbers or other types
      if (aValue < bValue) {
        return sortConfig.direction === "ascending" ? -1 : 1;
      }
      if (aValue > bValue) {
        return sortConfig.direction === "ascending" ? 1 : -1;
      }
      return 0; // equal values
    });
  }, [mockApplications, sortConfig]);

  const requestSort = (key: keyof Application) => {
    let direction: "ascending" | "descending" = "ascending";
    if (sortConfig.key === key && sortConfig.direction === "ascending") {
      direction = "descending";
    }
    setSortConfig({ key, direction });
  };

  useEffect(() => {
    setApplications([
      {
        id: 1,
        name: "Alice Johnson",
        status: "Under Review",
        assignedTo: "Dr. Smith",
      },
      {
        id: 2,
        name: "Bob Brown",
        status: "Accepted",
        assignedTo: "Dr. Johnson",
      },
      // ... more applications
    ]);

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
            <Header>Reviewer Dashboard: Managing Applications</Header>
            <div className="container-fluid">
              <table className="table table-bordered">
                <thead>
                  <tr>
                    <th>Application Name</th>
                    <th>Area of Interest</th>
                    <th>Additional Info</th>
                    <th>Potential Professor</th>
                    <th>Recomendation</th>
                  </tr>
                </thead>
                <tbody>
                  {mockApplications.map((app) => (
                    <tr key={app.id}>
                      <td>{app.name}</td>
                      <td>Data Mining</td>
                      <td>Add other needed info</td>
                      <td>
                        <select>
                          <option value="none">None</option>
                          <option value="prof1">Prof 1</option>
                          <option value="prof2">Prof 2</option>
                        </select>
                      </td>
                      <td>
                        <select>
                          <option value="interview">Interview</option>
                          <option value="probreject">Probable Reject</option>
                          <option value="moreoptions">More Options</option>
                        </select>
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          </Section>
        </Col>
      </Row>
      </Container>
    </DashboardContainer>
  );
};

export default ReviewerDashboard;
