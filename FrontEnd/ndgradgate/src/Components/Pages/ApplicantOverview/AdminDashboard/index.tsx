import React, { useRef, useState, useMemo, ChangeEvent } from "react";
import { Container, Row, Col } from "react-bootstrap";
import { DashboardContainer, Header, Section } from "./styles";
import { Toast } from "primereact/toast";
import DataGrid from "../../../Atoms/DataGrid";
import { useApplicationContext } from "../../../../context/ApplicationContext";
import Papa from "papaparse";
import ImportedDataGrid from "../../../Atoms/ImportedDataGrid";
import { Dialog } from "primereact/dialog";
type CSVData = Record<string, string | number>;

const ApplicantOverview: React.FC = () => {
  const [importedData, setImportedData] = useState<CSVData[]>([]);
  const [isImportDialogVisible, setIsImportDialogVisible] = useState(false);

  const handleFileSelect = (event: ChangeEvent<HTMLInputElement>) => {
    if (event.target.files && event.target.files[0]) {
      const file = event.target.files[0];
      Papa.parse(file, {
        header: true,
        skipEmptyLines: true,
        complete: (results) => {
          setImportedData(results.data as CSVData[]);
          setIsImportDialogVisible(true);
        },
      });
    }
  };

  const renderImportDialog = () => {
    return (
      <Dialog
        header="Imported Data"
        visible={isImportDialogVisible}
        style={{ width: "80%" }}
        onHide={() => setIsImportDialogVisible(false)}
      >
        <ImportedDataGrid data={importedData} />
      </Dialog>
    );
  };

  return (
    <DashboardContainer>
      <Container fluid>
        <Row>
          <Col md={12}>
            <Section>
              <Header>Application Overview</Header>
              <button
                onClick={() => document.getElementById("csvInput")?.click()}
              >
                Import CSV
              </button>
              <input
                id="csvInput"
                type="file"
                accept=".csv"
                style={{ display: "none" }}
                onChange={handleFileSelect}
              />
              <DataGrid />
              {renderImportDialog()}
            </Section>
          </Col>
        </Row>
      </Container>
    </DashboardContainer>
  );
};

export default ApplicantOverview;
