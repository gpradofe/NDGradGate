import React from "react";
import { Container, Row, Col } from "react-bootstrap";
import { Chart } from "primereact/chart";
import { Card } from "primereact/card";
import { DashboardContainer, Header, ChartContainer } from "./styles";

const AnalyticsDashboard: React.FC = () => {
  // Example mock data for charts
  const applicationTrendsData = {
    labels: ["2018", "2019", "2020", "2021", "2022"],
    datasets: [
      {
        label: "Total Applications",
        backgroundColor: "#42A5F5",
        data: [2000,2300,2450,2600,2700],
      },
    ],
  };

  const acceptanceRateData = {
    labels: ["2018", "2019", "2020", "2021", "2022"],
    datasets: [
      {
        label: "Acceptance Rate (%)",
        backgroundColor: "#FFC107",
        data: [10, 9, 8.6, 8.2, 7],
      },
    ],
  };

  const areaOfStudyData = {
    labels: ["Computer Science", "Chemical Engineering", "Mechanical Engineering", "Environmental Engineering", "Electrical Engineering", "Other"],
    datasets: [
      {
        backgroundColor: [
          "#FF6384",
          "#36A2EB",
          "#FFCE56",
          "#4BC0C0",
          "#9966FF",
          "#990000",
        ],
        data: [1000, 100, 600, 300, 200,300],
      },
    ],
  };

  const averageGPAData = {
    labels: ["Engineering", "Science", "Arts", "Business", "IT"],
    datasets: [
      {
        label: "Average GPA",
        backgroundColor: "#FF9F40",
        data: [3.5, 3.6, 3.2, 3.8, 3.7],
      },
    ],
  };

  // Additional data sets can be defined similarly

  return (
    <DashboardContainer>
      <Container fluid>
        <Header>
            <header className="header-container">
              <div className="dashboard">
                <h1>Graduate Studies Application Analytics</h1>
              </div>
              <div className="user-info">
                <p>Current User: Tim Weninger</p>
              </div>
          </header>
          </Header>
        <Row>
          <Col md={6}>
            <ChartContainer>
              <Card title="Application Trends">
                <Chart type="bar" data={applicationTrendsData} />
              </Card>
            </ChartContainer>
          </Col>
          <Col md={6}>
            <ChartContainer>
              <Card title="Area of Study Distribution">
                <Chart type="doughnut" data={areaOfStudyData} />
              </Card>
            </ChartContainer>
          </Col>
        </Row>
        <Row>
          <Col md={12}>
            <ChartContainer>
              <Card title="Acceptance Rate Over Years">
                <Chart type="line" data={acceptanceRateData} />
              </Card>
            </ChartContainer>
          </Col>
        </Row>
        <Row>
          <Col md={12}>
            <ChartContainer>
              <Card title="Average GPA of Applicants">
                <Chart type="radar" data={averageGPAData} />
              </Card>
            </ChartContainer>
          </Col>
        </Row>

        {/* Additional Rows and Columns for more charts can be added here */}
      </Container>
    </DashboardContainer>
  );
};

export default AnalyticsDashboard;
