import React, { ChangeEvent, useState } from "react";
import { Container, Tab, Tabs } from "@mui/material";
import { DashboardContainer, Header, StyledButton } from "./styles";
import styled from "styled-components";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import { Dialog } from "primereact/dialog";
import { InputText } from "primereact/inputtext";
import { Button } from "primereact/button";

// Extend existing styled components
const TabPanelContainer = styled.div`
  background: white;
  padding: 20px;
  border-radius: 10px;
  margin-bottom: 20px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.05);
`;
interface Faculty {
  id: number;
  name: string;
  department: string;
}

interface Reviewer {
  id: number;
  name: string;
  subject: string;
}

// Mock Data
const initialFacultyData: Faculty[] = [
  { id: 1, name: "John Doe", department: "Computer Science" },
  { id: 2, name: " Tim Weninger", department: "Computer Science" },
];

const initialReviewerData: Reviewer[] = [
  { id: 1, name: "Jane Smith", subject: "Computer Science" },
  { id: 2, name: " Tim Weninger", subject: "Computer Science" },
];

// Helper component for TabPanel
function TabPanel(props: {
  children?: React.ReactNode;
  index: number;
  value: number;
}) {
  const { children, value, index, ...other } = props;

  return (
    <div
      role="tabpanel"
      hidden={value !== index}
      id={`simple-tabpanel-${index}`}
      aria-labelledby={`simple-tab-${index}`}
      {...other}
    >
      {value === index && <TabPanelContainer>{children}</TabPanelContainer>}
    </div>
  );
}

// AdminDashboard Component
const AdminDashboard: React.FC = () => {
  const [tabIndex, setTabIndex] = useState(0);

  const [isDialogVisible, setIsDialogVisible] = useState(false);
  const [newEntry, setNewEntry] = useState<Partial<Faculty & Reviewer>>({});
  const [facultyData, setFacultyData] = useState<Faculty[]>(initialFacultyData);
  const [reviewerData, setReviewerData] =
    useState<Reviewer[]>(initialReviewerData);
  const [tempEntries, setTempEntries] = useState<(Faculty | Reviewer)[]>([]);
  const [isChanged, setIsChanged] = useState(false);
  const [originalFacultyData, setOriginalFacultyData] = useState<Faculty[]>([
    ...initialFacultyData,
  ]);
  const [originalReviewerData, setOriginalReviewerData] = useState<Reviewer[]>([
    ...initialReviewerData,
  ]);

  // Check if data has been changed
  const checkForChanges = () => {
    const facultyChanged =
      JSON.stringify(facultyData) !== JSON.stringify(originalFacultyData);
    const reviewerChanged =
      JSON.stringify(reviewerData) !== JSON.stringify(originalReviewerData);
    setIsChanged(facultyChanged || reviewerChanged);
  };

  const saveChanges = () => {
    setOriginalFacultyData([...facultyData]);
    setOriginalReviewerData([...reviewerData]);
    setIsChanged(false);
  };

  const revertChanges = () => {
    setFacultyData([...originalFacultyData]);
    setReviewerData([...originalReviewerData]);
    setIsChanged(false);
  };

  const handleTabChange = (event: React.SyntheticEvent, newValue: number) => {
    setTabIndex(newValue);
  };

  const openNewDialog = () => {
    setIsDialogVisible(true);
  };

  const saveData = () => {
    if (tabIndex === 0) {
      setFacultyData([...facultyData, ...(tempEntries as Faculty[])]);
    } else {
      setReviewerData([...reviewerData, ...(tempEntries as Reviewer[])]);
    }
    setTempEntries([]); // Clear temporary entries after saving
    closeDialog(); // Optionally close the dialog after saving
  };

  const closeDialog = () => {
    setIsDialogVisible(false);
    setNewEntry({ name: "", department: "" }); // Reset the form
    setTempEntries([]);
  };

  const handleNewEntryChange = (e: ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setNewEntry({ ...newEntry, [name]: value });
  };

  const addNewEntry = () => {
    if (tabIndex === 0) {
      // Ensure all faculty fields are defined
      const newFaculty: Faculty = {
        id: facultyData.length + tempEntries.length + 1,
        name: newEntry.name || "", // Default to empty string if undefined
        department: newEntry.department || "", // Default to empty string if undefined
      };
      setTempEntries([...tempEntries, newFaculty]);
    } else {
      // Ensure all reviewer fields are defined
      const newReviewer: Reviewer = {
        id: reviewerData.length + tempEntries.length + 1,
        name: newEntry.name || "", // Default to empty string if undefined
        subject: newEntry.department || "", // Using 'department' field for 'subject'
      };
      setTempEntries([...tempEntries, newReviewer]);
    }
    setNewEntry({ name: "", department: "" }); // Reset form after adding
  };
  const onTempEditComplete = (e: any, index: number) => {
    let { rowData, newValue, field } = e;
    const updatedTempEntries = [...tempEntries];
    updatedTempEntries[index] = { ...rowData, [field]: newValue };
    setTempEntries(updatedTempEntries);
  };
  const deleteRow = (rowData: Faculty | Reviewer) => {
    if (tabIndex === 0) {
      setFacultyData(facultyData.filter((f) => f.id !== rowData.id));
    } else {
      setReviewerData(reviewerData.filter((r) => r.id !== rowData.id));
    }
    checkForChanges();
  };

  const actionBodyTemplate = (rowData: any) => {
    return (
      <StyledButton variant="danger" onClick={() => deleteRow(rowData)}>
        Delete
      </StyledButton>
    );
  };
  const deleteTempRow = (rowData: Faculty | Reviewer) => {
    setTempEntries(tempEntries.filter((entry) => entry.id !== rowData.id));
  };

  const tempActionBodyTemplate = (rowData: Faculty | Reviewer) => {
    return (
      <StyledButton variant="danger" onClick={() => deleteTempRow(rowData)}>
        Delete
      </StyledButton>
    );
  };

  const textEditor = (options: any) => (
    <InputText
      type="text"
      value={options.value}
      onChange={(e) => options.editorCallback(e.target.value)}
    />
  );
  const onFacultyRowEditComplete = (e: any) => {
    let { newData, index } = e;
    let updatedFaculties = [...facultyData];
    updatedFaculties[index] = newData;
    setFacultyData(updatedFaculties);
    checkForChanges();
  };
  const onReviewerRowEditComplete = (e: any) => {
    let { newData, index } = e;
    let updatedReviewers = [...reviewerData];
    updatedReviewers[index] = newData;
    setReviewerData(updatedReviewers);
    checkForChanges();
  };
  return (
    <DashboardContainer>
      <Container>
        <header className="header-container">
          <div className="dashboard">
            <h1>Admin Dashboard</h1>
          </div>
          <div className="user-info">
            <p>Current User: Tim Weninger</p>
          </div>
         </header>
        <Tabs
          value={tabIndex}
          onChange={handleTabChange}
          aria-label="Admin dashboard tabs"
        >
          <Tab label="Faculty Management" />
          <Tab label="Reviewer Management" />
        </Tabs>

        <TabPanel value={tabIndex} index={0}>
          <h3>Faculty Management</h3>
          <DataTable
            value={facultyData}
            editMode="row"
            onRowEditComplete={onFacultyRowEditComplete}
            dataKey="id"
          >
            <Column field="id" header="ID" />
            <Column field="name" header="Name" editor={textEditor} />
            <Column
              field="department"
              header="Department"
              editor={textEditor}
            />
            <Column body={actionBodyTemplate} />
            <Column rowEditor />
          </DataTable>
          <StyledButton variant="primary" onClick={openNewDialog}>
            Add New Faculty
          </StyledButton>
        </TabPanel>

        <TabPanel value={tabIndex} index={1}>
          <h3>Reviewer Management</h3>
          <DataTable
            value={reviewerData}
            editMode="row"
            onRowEditComplete={onReviewerRowEditComplete}
            dataKey="id"
          >
            <Column field="id" header="ID" />
            <Column field="name" header="Name" editor={textEditor} />
            <Column field="subject" header="Subject" editor={textEditor} />
            <Column rowEditor />
            <Column body={actionBodyTemplate} />
          </DataTable>
          <StyledButton variant="primary" onClick={openNewDialog}>
            Add New Reviewer
          </StyledButton>
        </TabPanel>
        {isChanged && (
          <div
            style={{
              marginTop: "1em",
              display: "flex",
              justifyContent: "space-between",
            }}
          >
            <Button
              label="Save Changes"
              onClick={saveChanges}
              className="p-button-success"
            />
            <Button
              label="Revert Changes"
              onClick={revertChanges}
              className="p-button-secondary"
            />
          </div>
        )}
        <Dialog
          header="New Entry"
          visible={isDialogVisible}
          onHide={closeDialog}
        >
          <InputText
            name="name"
            value={newEntry.name}
            onChange={handleNewEntryChange}
            placeholder="Name"
          />
          <InputText
            name="department"
            value={newEntry.department}
            onChange={handleNewEntryChange}
            placeholder={tabIndex === 0 ? "Department" : "Subject"}
          />
          <Button label="Add" onClick={addNewEntry} />

          {/* Temporary entries DataTable */}
          <DataTable value={tempEntries} editMode="cell">
            <Column field="id" header="ID" />
            <Column
              field="name"
              header="Name"
              editor={textEditor}
              onCellEditComplete={(e) =>
                onTempEditComplete(
                  e,
                  tempEntries.findIndex((te) => te.id === e.rowData.id)
                )
              }
            />
            <Column
              field={tabIndex === 0 ? "department" : "subject"}
              header={tabIndex === 0 ? "Department" : "Subject"}
              editor={textEditor}
              onCellEditComplete={(e) =>
                onTempEditComplete(
                  e,
                  tempEntries.findIndex((te) => te.id === e.rowData.id)
                )
              }
            />
            <Column body={tempActionBodyTemplate} />
          </DataTable>

          <div
            style={{
              marginTop: "1em",
              display: "flex",
              justifyContent: "space-between",
            }}
          >
            <Button
              label="Save"
              onClick={saveData}
              className="p-button-success"
            />
            <Button
              label="Close"
              onClick={closeDialog}
              className="p-button-secondary"
            />
          </div>
        </Dialog>
      </Container>
    </DashboardContainer>
  );
};

export default AdminDashboard;
