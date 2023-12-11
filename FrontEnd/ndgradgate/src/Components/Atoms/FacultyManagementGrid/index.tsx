import { Column } from "primereact/column";
import { DataTable } from "primereact/datatable";
import React, { useEffect, useState } from "react";
import { StyledButton } from "../../Pages/AdminDashboard/styles";
import { Faculty } from "../../../types/Application/Faculty";
import { useApplicationContext } from "../../../context/ApplicationContext";
import { InputText } from "primereact/inputtext";
import { styled } from "styled-components";
import { Checkbox } from "primereact/checkbox";
import { TabPanel } from "../../../Helpers/TabPanelHelper";

interface FacultyManagementGridProps {
  valueTabIndex: number;
  index: number;
}
const FacultyManagementGrid: React.FC<FacultyManagementGridProps> = ({
  valueTabIndex,
  index,
}) => {
  const { faculty, fetchFaculty } = useApplicationContext();

  const [first, setFirst] = useState(0);
  const [totalRecords, setTotalRecords] = useState(faculty.length);
  const [rowsPerPage, setRowsPerPage] = useState(10);
  const [localFaculty, setLocalFaculty] = useState<Faculty[]>(faculty);
  const [addedFaculty, setAddedFaculty] = useState<Faculty[]>([]);
  const [updatedFaculty, setUpdatedFaculty] = useState<Faculty[]>([]);
  const [deletedFacultyIds, setDeletedFacultyIds] = useState<number[]>([]);
  const [isDataChanged, setIsDataChanged] = useState(false);

  useEffect(() => {
    setLocalFaculty(faculty);
    setTotalRecords(faculty.length);
  }, [faculty]);

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
  const onPageChange = (event: any) => {
    setFirst(event.first);
    setRowsPerPage(event.rows);
  };
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
    <TabPanel value={valueTabIndex} index={index}>
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
  );
};

export default FacultyManagementGrid;
