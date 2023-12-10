import React, { useEffect, useState } from "react";
import {
  Container,
  Tab,
  Tabs,
  Accordion,
  AccordionSummary,
  AccordionDetails,
  Typography,
  List,
  ListItem,
} from "@mui/material";
import { DashboardContainer, Header, StyledButton } from "./styles";
import styled from "styled-components";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import { InputText } from "primereact/inputtext";
import { useApplicationContext } from "../../../context/ApplicationContext";
import { Faculty } from "../../../types/Application/Faculty";
import { Checkbox } from "primereact/checkbox";
import { Setting } from "../../../types/Settings/Setting";
import ExpandMoreIcon from "@mui/icons-material/ExpandMore";

// Extend existing styled components
const TabPanelContainer = styled.div`
  background: white;
  padding: 20px;
  border-radius: 10px;
  margin-bottom: 20px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.05);
`;

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
  const [rowsPerPage, setRowsPerPage] = useState(10);
  const { faculty, fetchFaculty, settings, fetchSettings, addOrUpdateSetting } =
    useApplicationContext();
  const [first, setFirst] = useState(0);
  const [totalRecords, setTotalRecords] = useState(faculty.length);

  const [localFaculty, setLocalFaculty] = useState<Faculty[]>(faculty);
  const [addedFaculty, setAddedFaculty] = useState<Faculty[]>([]);
  const [updatedFaculty, setUpdatedFaculty] = useState<Faculty[]>([]);
  const [deletedFacultyIds, setDeletedFacultyIds] = useState<number[]>([]);
  const [isDataChanged, setIsDataChanged] = useState(false);

  const [localSettings, setLocalSettings] = useState(settings);
  const [isSettingsChanged, setIsSettingsChanged] = useState(false);
  const [expandedPanel, setExpandedPanel] = useState<string | false>(false);
  const [editedSettings, setEditedSettings] = useState<{
    [key: number]: string[];
  }>({});

  const handlePanelChange =
    (panel: string) => (event: React.SyntheticEvent, isExpanded: boolean) => {
      setExpandedPanel(isExpanded ? panel : false);
    };
  useEffect(() => {
    setLocalFaculty(faculty);
    setTotalRecords(faculty.length);
  }, [faculty]);
  useEffect(() => {
    setLocalSettings(settings);
  }, [settings]);

  const handleTabChange = (event: React.SyntheticEvent, newValue: number) => {
    setTabIndex(newValue);
  };
  const onPageChange = (event: any) => {
    setFirst(event.first);
    setRowsPerPage(event.rows);
  };

  const onRowEditComplete = (e: any) => {
    const updatedFaculty = e.newData;
    setUpdatedFaculty((prev) => [
      ...prev.filter((f) => f.Id !== updatedFaculty.Id),
      updatedFaculty,
    ]);

    // Update the localFaculty state
    setLocalFaculty((prev) =>
      prev.map((f) => (f.Id === updatedFaculty.Id ? updatedFaculty : f))
    );
    setIsDataChanged(true);
  };
  const addNewFaculty = () => {
    const newFaculty: Faculty = {
      Id: Math.max(0, ...localFaculty.map((f) => f.Id)) + 1,
      Name: "",
      Email: "",
      IsAdmin: false,
      IsReviewer: false,
      Field: "",
      PotentialAdvisors: [],
      ReviewerAssignments: [],
      Comments: [],
    };

    setAddedFaculty((prev) => [...prev, newFaculty]);
    setLocalFaculty((prev) => [...prev, newFaculty]);
    setIsDataChanged(true);
  };

  const deleteFaculty = (id: number) => {
    setDeletedFacultyIds((prev) => [...prev, id]);
    setLocalFaculty((prev) => prev.filter((f) => f.Id !== id));
    setIsDataChanged(true);
  };

  const revertChanges = async () => {
    await fetchFaculty();
    setAddedFaculty([]);
    setUpdatedFaculty([]);
    setDeletedFacultyIds([]);
    setIsDataChanged(false);
  };
  const reviewerBodyTemplate = (reviewer: Faculty) => {
    return <Checkbox checked={reviewer.IsReviewer} />;
  };
  const handleAddSetting = (newSetting: Setting) => {
    setLocalSettings([...localSettings, newSetting]);
    setIsSettingsChanged(true);
  };

  const handleUpdateSetting = (updatedSetting: Setting) => {
    setLocalSettings(
      localSettings.map((s) =>
        s.Id === updatedSetting.Id ? updatedSetting : s
      )
    );
    setIsSettingsChanged(true);
  };

  const handleDeleteSetting = (settingId: number) => {
    setLocalSettings(localSettings.filter((s) => s.Id !== settingId));
    setIsSettingsChanged(true);
  };
  const handleSettingValueChange = (
    settingId: number,
    value: string[],
    index: number
  ) => {
    const updatedSettings = { ...editedSettings, [settingId]: value };
    setEditedSettings(updatedSettings);
  };
  const applySettingChanges = async (settingId: number) => {
    await addOrUpdateSetting(localSettings.find((s) => s.Id === settingId)!);

    await fetchSettings();
    setIsSettingsChanged(false);
  };

  const revertSettingChanges = (settingId: number) => {
    setLocalSettings(settings);
    setIsSettingsChanged(false);
  };
  const actionBodyTemplate = (rowData: Faculty) => (
    <StyledButton variant="danger" onClick={() => deleteFaculty(rowData.Id)}>
      Delete
    </StyledButton>
  );

  const textEditor = (options: any) => (
    <InputText
      type="text"
      value={options.value}
      onChange={(e) => options.editorCallback(e.target.value)}
    />
  );
  const reviewerEditor = (options: any) => {
    return (
      <Checkbox
        checked={options.rowData.IsReviewer}
        onChange={(e) => options.editorCallback(e.checked)}
      />
    );
  };
  const applyChanges = async () => {
    try {
      await fetchFaculty();

      setAddedFaculty([]);
      setUpdatedFaculty([]);
      setDeletedFacultyIds([]);
      setIsDataChanged(false);
    } catch (error) {
      console.error("Error applying changes:", error);
    }
  };

  return (
    <DashboardContainer>
      <Container>
        <Header>Admin Dashboard</Header>
        <Tabs
          value={tabIndex}
          onChange={handleTabChange}
          aria-label="Admin dashboard tabs"
        >
          <Tab label="Faculty Management" />
          <Tab label="Settings" />
        </Tabs>

        <TabPanel value={tabIndex} index={0}>
          <h3>Faculty Management</h3>

          <DataTable
            value={localFaculty}
            paginator
            paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
            currentPageReportTemplate="Showing {first} to {last} of {totalRecords} entries"
            first={first}
            rows={rowsPerPage}
            totalRecords={totalRecords}
            onPage={onPageChange}
            rowsPerPageOptions={[5, 10, 20, 50]}
            editMode="row"
            onRowEditComplete={onRowEditComplete}
            dataKey="Id"
          >
            <Column field="Id" header="ID" />
            <Column field="Name" header="Name" editor={textEditor} />
            <Column
              field="IsReviewer"
              header="Is Reviewer"
              editor={reviewerEditor}
              body={reviewerBodyTemplate}
            />

            <Column body={actionBodyTemplate} />
            <Column rowEditor />
          </DataTable>
          <StyledButton variant="primary" onClick={addNewFaculty}>
            Add New Faculty
          </StyledButton>
          {isDataChanged && (
            <>
              <StyledButton variant="secondary" onClick={revertChanges}>
                Revert Changes
              </StyledButton>
              <StyledButton variant="success" onClick={applyChanges}>
                Apply Changes
              </StyledButton>
            </>
          )}
        </TabPanel>
        <TabPanel value={tabIndex} index={1}>
          <h3>Settings</h3>
          {localSettings &&
            localSettings.map((setting, index) => {
              console.log("setting (Accordion):", setting);
              return (
                <Accordion
                  key={setting.Id}
                  expanded={expandedPanel === `panel${index}`}
                  onChange={handlePanelChange(`panel${index}`)}
                >
                  <AccordionSummary
                    expandIcon={<ExpandMoreIcon />}
                    aria-controls={`panel${index}bh-content`}
                    id={`panel${index}bh-header`}
                  >
                    <Typography>{setting.SettingKey}</Typography>
                  </AccordionSummary>
                  <AccordionDetails>
                    <List>
                      {setting.Values &&
                        setting.Values.map((value, valueIndex) => {
                          console.log("setting (ListItem):", setting); // Add this line for logging inside the ListItem
                          console.log("value:", value); // Add this line for logging

                          return (
                            <ListItem key={valueIndex}>
                              <InputText
                                value={
                                  editedSettings[setting.Id]?.[valueIndex] ||
                                  value
                                }
                                onChange={(e) =>
                                  handleSettingValueChange(
                                    setting.Id,
                                    [
                                      ...setting.Values.slice(0, valueIndex),
                                      e.target.value,
                                      ...setting.Values.slice(valueIndex + 1),
                                    ],
                                    valueIndex
                                  )
                                }
                              />
                            </ListItem>
                          );
                        })}
                    </List>
                    <StyledButton variant="secondary" onClick={revertChanges}>
                      Revert Changes
                    </StyledButton>
                    <StyledButton variant="success" onClick={applyChanges}>
                      Apply Changes
                    </StyledButton>
                  </AccordionDetails>
                </Accordion>
              );
            })}
        </TabPanel>
      </Container>
    </DashboardContainer>
  );
};

export default AdminDashboard;
