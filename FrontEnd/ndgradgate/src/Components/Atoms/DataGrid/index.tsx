import React, { useEffect, useMemo, useRef, useState } from "react";
import { DataTable } from "primereact/datatable";
import { Button } from "primereact/button";
import { Tooltip } from "primereact/tooltip";
import { Column } from "primereact/column";
import { Dropdown } from "primereact/dropdown";
import { Section } from "../../Pages/AdminDashboard/styles";
import { Applicant } from "../../../types/Application/Applicant";
import { ActionButton, StyledExportButton } from "./styles";
import { useApplicationContext } from "../../../context/ApplicationContext";
import { Toast } from "primereact/toast";
import { MultiSelect } from "primereact/multiselect";
type ColumnOption = {
  label: string;
  value: keyof Applicant; // This ensures that value is a key of Applicant
};
type ExpandedRows = {
  [key: number]: boolean;
};
interface VisibleColumns {
  [key: string]: boolean;
}
const DataGrid: React.FC = ({}) => {
  const dt = useRef<any>(null);

  const [localData, setLocalData] = useState<Applicant[]>([]);
  const [isChanged, setIsChanged] = useState(false);
  const hasApplicantChanged = (applicant: Applicant) => {
    const originalApplicant = applications.find((a) => a.Id === applicant.Id);
    if (!originalApplicant) return false;

    const isStatusChanged = applicant.Status !== originalApplicant.Status;
    const isReviewersChanged =
      JSON.stringify(applicant.Reviewers.map((r) => r.FacultyId).sort()) !==
      JSON.stringify(
        originalApplicant.Reviewers.map((r) => r.FacultyId).sort()
      );

    return isStatusChanged || isReviewersChanged;
  };
  // Prepare the options for the column visibility dropdown
  const columnOptions: ColumnOption[] = [
    { label: "First Name", value: "FirstName" },
    { label: "Last Name", value: "LastName" },
    { label: "Email", value: "Email" },
    { label: "Application Status", value: "Status" },
    { label: "Area of Study", value: "AreaOfStudy" },
    { label: "Citizenship Country", value: "CitizenshipCountry" },
    { label: "Department Recommendation", value: "DepartmentRecommendation" },
    { label: "Ethnicity", value: "Ethnicity" },
    { label: "Sex", value: "Sex" },
    { label: "Academic History", value: "AcademicHistories" },
  ];
  const {
    applications = [],
    faculty,
    settings,
    updateApplicantStatusAndReviewer,
    fetchApplications,
  } = useApplicationContext();
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
  const [expandedRows, setExpandedRows] = useState<ExpandedRows>({});
  useEffect(() => {
    setLocalData(applications); // Initialize local data with context data
  }, [applications]);

  const revertChanges = () => {
    setLocalData(applications); // Revert to original data from context
    setIsChanged(false);
  };
  const applyChanges = () => {
    const updateData = localData.filter(hasApplicantChanged).map((app) => ({
      Ref: app.Id,
      FacultyId: app.Reviewers.map((r) => r.FacultyId),
      Status: app.Status,
    }));

    if (updateData.length > 0) {
      updateApplicantStatusAndReviewer(updateData)
        .then(() => {
          setIsChanged(false);
          showSuccess("Changes applied successfully");
          // Fetch updated applications data
          fetchApplications();
        })
        .catch((error) => {
          console.error("Error applying changes:", error);
        });
    } else {
      showSuccess("No changes to apply");
    }
  };

  const statusSettings = settings.find((s) => s.SettingKey === "statuses");
  const statusOptions = statusSettings
    ? statusSettings.Values.map((status) => ({ label: status, value: status }))
    : [];

  const toast = useRef<Toast>(null);

  const showSuccess = (message: string) => {
    toast.current?.show({
      severity: "success",
      summary: "Success",
      detail: message,
      life: 3000,
    });
  };
  const renderReviewerMultiSelect = (rowData: Applicant) => {
    const selectedFacultyIds =
      localData
        .find((app) => app.Id === rowData.Id)
        ?.Reviewers.map((r) => r.FacultyId) || [];

    return (
      <MultiSelect
        value={selectedFacultyIds}
        options={facultyOptions}
        onChange={(e) => assignReviewer(rowData.Id, e.value)}
        placeholder="Select Reviewers"
      />
    );
  };
  const assignReviewer = (applicationRef: number, reviewerIds: number[]) => {
    const updatedLocalData = localData.map((app) => {
      if (app.Id === applicationRef) {
        // Map selected reviewer IDs to Reviewer objects
        const newReviewers = reviewerIds.map((facultyId) => {
          const facultyMember = faculty.find((f) => f.Id === facultyId);
          return {
            Name: facultyMember?.Name || "Unknown",
            FacultyId: facultyId,
            Recommendation: "Default Recommendation",
          };
        });

        return {
          ...app,
          Reviewers: newReviewers,
        };
      }
      return app;
    });

    setLocalData(updatedLocalData);
    setIsChanged(true);
  };
  const updateApplicationStatus = (applicationRef: number, status: string) => {
    const updatedLocalData = localData.map((app) => {
      if (app.Id === applicationRef) {
        return { ...app, ApplicationStatus: status };
      }
      return app;
    });
    setLocalData(updatedLocalData);
    setIsChanged(true);
  };

  const filteredApplications = useMemo(() => {
    return applications.filter((application) =>
      Object.values(application).some(
        (value) =>
          value &&
          value.toString().toLowerCase().includes(globalFilter.toLowerCase())
      )
    );
  }, [applications, globalFilter]);

  const facultyOptions = faculty.map((fac) => ({
    label: fac.Name,
    value: fac.Id,
  }));
  const onColumnToggle = (e: { value: string }) => {
    // Copy the current visibleColumns state
    const updatedVisibleColumns: VisibleColumns = { ...visibleColumns };

    if (updatedVisibleColumns[e.value] !== undefined) {
      updatedVisibleColumns[e.value] = !updatedVisibleColumns[e.value];
    }

    // Update the state with the new visibleColumns
    setVisibleColumns(updatedVisibleColumns);
  };
  const exportData = () => {
    dt.current.exportCSV();
    showSuccess("Data exported to CSV successfully!");
  };

  const renderStatusDropdown = (rowData: Applicant) => {
    const currentStatus =
      localData.find((app) => app.Id === rowData.Id)?.Status || "";
    return (
      <Dropdown
        value={currentStatus}
        options={statusOptions}
        onChange={(e) => updateApplicationStatus(rowData.Id, e.value)}
        placeholder="Select Status"
      />
    );
  };

  const expandAll = () => {
    const allExpandedRows: ExpandedRows = {};
    applications.forEach((app) => {
      if (app.Attributes && app.Attributes.length > 0) {
        allExpandedRows[app.Id] = true;
      }
    });
    setExpandedRows(allExpandedRows);
  };

  const collapseAll = () => {
    setExpandedRows({});
  };

  const header = (
    <div className="flex align-items-center justify-content-between">
      <Button
        icon="pi pi-plus"
        label="Expand All"
        onClick={expandAll}
        className="mr-2"
      />
      <Button icon="pi pi-minus" label="Collapse All" onClick={collapseAll} />
      <StyledExportButton
        type="button"
        icon="pi pi-file"
        onClick={() => exportData()}
        data-pr-tooltip="CSV"
        tooltip="Export to CSV" // Added tooltip for clarity
        tooltipOptions={{ position: "bottom" }} // Tooltip position
      />
      <span className="p-input-icon-left">
        <i className="pi pi-search" />
        <input
          type="search"
          value={globalFilter}
          onChange={(e) => setGlobalFilter(e.target.value)}
          className="p-inputtext p-component"
          placeholder="Global Search"
        />
        <Dropdown
          value={Object.keys(visibleColumns).filter(
            (key) => visibleColumns[key]
          )}
          options={columnOptions}
          onChange={onColumnToggle}
          optionLabel="label"
          placeholder="Show/Hide Columns"
          multiple
        />
      </span>
    </div>
  );
  const academicHistoryBodyTemplate = (rowData: Applicant) => {
    return (
      <div>
        {rowData.AcademicHistories &&
          rowData.AcademicHistories.map((history, index) => (
            <div key={index}>
              <div>
                <b>Institution:</b> {history.Institution}
              </div>
              <div>
                <b>Major:</b> {history.Major}
              </div>
              <div>
                <b>GPA:</b> {history.GPA.toFixed(2)}
              </div>
            </div>
          ))}
      </div>
    );
  };

  const allowExpansion = (rowData: Applicant) => {
    return rowData.Attributes && rowData.Attributes.length > 0;
  };
  const rowExpansionTemplate = (data: Applicant) => {
    // Aggregate attributes into a single object
    type AttributesData = { [key: string]: string };

    // Aggregate attributes into a single object with a specified type
    const attributesData = data.Attributes.reduce<AttributesData>(
      (acc, attr) => {
        acc[attr.Attribute] = attr.Value;
        return acc;
      },
      {}
    );

    // Dynamically create columns based on the attributes
    const attributeColumns = data.Attributes.map((attr) => (
      <Column
        key={attr.Attribute}
        field={attr.Attribute}
        header={attr.Attribute}
      />
    ));

    return (
      <div>
        <h5>
          Attributes for {data.FirstName} {data.LastName}
        </h5>
        <DataTable value={[attributesData]} showGridlines>
          {attributeColumns}
        </DataTable>
      </div>
    );
  };
  const footer = isChanged ? (
    <div className="p-d-flex p-jc-end p-mt-2">
      <Button
        label="Revert"
        onClick={revertChanges}
        className="p-button-outlined p-mr-2"
      />
      <Button
        label="Apply"
        onClick={applyChanges}
        className="p-button-success"
      />
    </div>
  ) : null;
  return (
    <Section>
      <Toast ref={toast} />
      <Tooltip target=".export-buttons>button" position="bottom" />
      <DataTable
        ref={dt}
        value={filteredApplications}
        paginator
        header={header}
        rows={10}
        globalFilter={globalFilter}
        paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink RowsPerPageDropdown"
        rowsPerPageOptions={[5, 10, 20, 50]}
        dataKey="Id"
        emptyMessage="No applications found."
        expandedRows={expandedRows}
        onRowToggle={(e) => setExpandedRows(e.data)}
        rowExpansionTemplate={rowExpansionTemplate}
        showGridlines={true}
        footer={footer}
      >
        <Column style={{ width: "3em" }} expander={allowExpansion} />

        {columnOptions.map((col) => {
          if (col.value === "AcademicHistories") {
            return (
              visibleColumns[col.value] && (
                <Column
                  key={col.value}
                  field={col.value as string}
                  header={col.label}
                  body={academicHistoryBodyTemplate}
                  sortable
                  filter
                />
              )
            );
          } else {
            return (
              visibleColumns[col.value] && (
                <Column
                  key={col.value}
                  field={col.value as string}
                  header={col.label}
                  sortable
                  filter
                />
              )
            );
          }
        })}
        <Column header="Assign Reviewers" body={renderReviewerMultiSelect} />

        <Column header="Status" body={renderStatusDropdown} />
      </DataTable>
    </Section>
  );
};

export default DataGrid;
