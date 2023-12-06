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
        data: [300, 450, 320, 500, 600],
      },
    ],
  };

  const ethnicityDataCurrent = {
    labels: ["Asian", "Black", "Hispanic", "White", "Other"],
    datasets: [
      {
        backgroundColor: [
          "#FF6384",
          "#36A2EB",
          "#FFCE56",
          "#4BC0C0",
          "#9966FF",
        ],
        data: [300, 100, 150, 400, 50],
      },
    ],
  };

  const acceptanceRateData = {
    labels: ["2018", "2019", "2020", "2021", "2022"],
    datasets: [
      {
        label: "Acceptance Rate (%)",
        backgroundColor: "#FFC107",
        data: [60, 55, 70, 65, 75],
      },
    ],
  };

  const areaOfStudyData = {
    labels: ["Engineering", "Science", "Arts", "Business", "IT"],
    datasets: [
      {
        backgroundColor: [
          "#FF6384",
          "#36A2EB",
          "#FFCE56",
          "#4BC0C0",
          "#9966FF",
        ],
        data: [200, 150, 100, 250, 200],
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
        <Header>Graduate Studies Application Analytics</Header>
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
              <Card title="Ethnicity Distribution">
                <Chart type="pie" data={ethnicityDataCurrent} />
              </Card>
            </ChartContainer>
          </Col>
        </Row>
        <Row>
          <Col md={6}>
            <ChartContainer>
              <Card title="Acceptance Rate Over Years">
                <Chart type="line" data={acceptanceRateData} />
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
              <Card title="Average GPA of Applicants">
                <Chart type="radar" data={averageGPAData} />
              </Card>
            </ChartContainer>
          </Col>
        </Row>
      </Container>
    </DashboardContainer>
  );
};

export default AnalyticsDashboard;
