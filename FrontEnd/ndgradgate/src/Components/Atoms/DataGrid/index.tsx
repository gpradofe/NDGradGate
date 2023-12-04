import React, { useRef, useState } from "react";
import { DataTable } from "primereact/datatable";
import { Button } from "primereact/button";
import { Tooltip } from "primereact/tooltip";
import { Column } from "primereact/column";
import { Dropdown } from "primereact/dropdown";
import { Section } from "../../Pages/AdminDashboard/styles";
import { Applicant } from "../../../types/Application/Applicant";
import { ActionButton, StyledExportButton } from "./styles";
type ColumnOption = {
  label: string;
  value: keyof Applicant; // This ensures that value is a key of Applicant
};
interface DataGridProps {
  data: Applicant[];
  globalFilter?: string;
  onGlobalFilterChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
  onAssignReviewer: (applicationRef: number, reviewerId: number) => void;
  onUpdateApplicationStatus: (applicationRef: number, status: string) => void;
  facultyOptions: { label: string; value: number }[];
  visibleColumns: { [key: string]: boolean };
  setVisibleColumns: (
    value: React.SetStateAction<{ [key: string]: boolean }>
  ) => void;
  showSuccess: (message: string) => void; // Add this line
}

interface VisibleColumns {
  [key: string]: boolean;
}
const DataGrid: React.FC<DataGridProps> = ({
  data,
  globalFilter,
  onGlobalFilterChange,
  onAssignReviewer,
  onUpdateApplicationStatus,
  facultyOptions,
  visibleColumns,
  setVisibleColumns,
  showSuccess,
}) => {
  const dt = useRef<any>(null);

  const [acceptedApps, setAcceptedApps] = useState<Record<number, boolean>>({});
  const [deniedApps, setDeniedApps] = useState<Record<number, boolean>>({});
  const [assignedReviewers, setAssignedReviewers] = useState<
    Record<number, number | null>
  >({});
  // Prepare the options for the column visibility dropdown
  const columnOptions: ColumnOption[] = [
    { label: "First Name", value: "FirstName" },
    { label: "Last Name", value: "LastName" },
    { label: "Email", value: "Email" },
    { label: "Application Status", value: "ApplicationStatus" },
    { label: "Area of Study", value: "AreaOfStudy" },
    { label: "Citizenship Country", value: "CitizenshipCountry" },
    { label: "Department Recommendation", value: "DepartmentRecommendation" },
    { label: "Ethnicity", value: "Ethnicity" },
    { label: "Sex", value: "Sex" },
    { label: "Academic History", value: "AcademicHistories" },
  ];

  const onColumnToggle = (e: { value: string }) => {
    // Copy the current visibleColumns state
    const updatedVisibleColumns: VisibleColumns = { ...visibleColumns };

    // Toggle the visibility of the selected column
    // If the column is currently visible, hide it, and vice-versa
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
  const handleAccept = (applicationRef: number) => {
    onUpdateApplicationStatus(applicationRef, "Accepted");
    setAcceptedApps({ ...acceptedApps, [applicationRef]: true });
    setDeniedApps({ ...deniedApps, [applicationRef]: false }); // Reset denied state if previously denied
    showSuccess("Application accepted.");
  };

  // Handler for denying an application
  const handleDeny = (applicationRef: number) => {
    onUpdateApplicationStatus(applicationRef, "Declined");
    setDeniedApps({ ...deniedApps, [applicationRef]: true });
    setAcceptedApps({ ...acceptedApps, [applicationRef]: false }); // Reset accepted state if previously accepted
    showSuccess("Application denied.");
  };

  // Handler for assigning a reviewer
  const handleAssignReviewer = (applicationRef: number, reviewerId: number) => {
    onAssignReviewer(applicationRef, reviewerId);
    setAssignedReviewers({
      ...assignedReviewers,
      [applicationRef]: reviewerId,
    });
    showSuccess("Reviewer assigned.");
  };

  const header = (
    <div className="flex align-items-center justify-content-between">
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
          onChange={onGlobalFilterChange}
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
  const renderReviewerDropdown = (rowData: Applicant) => {
    const reviewerName = assignedReviewers[rowData.Ref]
      ? facultyOptions.find(
          (option) => option.value === assignedReviewers[rowData.Ref]
        )?.label
      : null;

    const onReviewerChange = (e: { value: number }) => {
      handleAssignReviewer(rowData.Ref, e.value);
    };

    if (reviewerName) {
      return <span>{reviewerName}</span>; // Display the reviewer's name
    } else {
      return (
        <Dropdown
          value={rowData.Reviewers?.[0]?.id} // Make sure Reviewers is populated and has an id
          options={facultyOptions}
          onChange={onReviewerChange}
          placeholder="Select Reviewer"
          optionLabel="label"
          optionValue="value"
        />
      );
    }
  };

  return (
    <Section>
      <Tooltip target=".export-buttons>button" position="bottom" />
      <DataTable
        ref={dt}
        value={data}
        paginator
        header={header}
        rows={10}
        globalFilter={globalFilter}
        paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink RowsPerPageDropdown"
        rowsPerPageOptions={[5, 10, 20, 50]}
        dataKey="id"
        emptyMessage="No applications found."
      >
        {columnOptions.map((col) => {
          if (col.value === "AcademicHistories") {
            return (
              visibleColumns[col.value] && (
                <Column
                  key={col.value}
                  field={col.value as string}
                  header={col.label}
                  body={(rowData: Applicant) => {
                    return (
                      <div>
                        {rowData.AcademicHistories.map((history, index) => (
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
                  }}
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
        <Column
          field="Reviewer"
          header="Assign Reviewer"
          body={(rowData: Applicant) => renderReviewerDropdown(rowData)}
        />
        <Column
          header="Actions"
          body={(rowData: Applicant) => (
            <div style={{ textAlign: "center" }}>
              {acceptedApps[rowData.Ref] || deniedApps[rowData.Ref] ? (
                // If accepted or denied, show a badge-like indicator
                <ActionButton
                  icon={
                    acceptedApps[rowData.Ref]
                      ? "pi pi-check-circle"
                      : "pi pi-times-circle"
                  }
                  className={`${
                    acceptedApps[rowData.Ref]
                      ? "p-button-success"
                      : "p-button-danger"
                  } p-button-outlined`}
                  disabled
                />
              ) : (
                // If not processed yet, show action buttons
                <>
                  <ActionButton
                    icon="pi pi-check"
                    className="p-button-success"
                    onClick={() => handleAccept(rowData.Ref)}
                    tooltip="Accept"
                    tooltipOptions={{ position: "top" }}
                  />
                  <ActionButton
                    icon="pi pi-times"
                    className="p-button-danger"
                    onClick={() => handleDeny(rowData.Ref)}
                    tooltip="Decline"
                    tooltipOptions={{ position: "top" }}
                  />
                </>
              )}
            </div>
          )}
        ></Column>
      </DataTable>
    </Section>
  );
};

export default DataGrid;
