import React, { useRef } from "react";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import { Dropdown } from "primereact/dropdown";
import { MultiSelect } from "primereact/multiselect";
import { StyledExportButton } from "./styles";
import { Section } from "../../Pages/ProfessorDashboard/styles";
import { Applicant } from "../../../types/Application/Applicant";
import ReviewerOverview from "../../Pages/ReviewerDashboard.py";

interface DataGridProps {
  data: Applicant[];
  onRecommendFaculties: (
    applicationRef: number,
    facultyNames: string[]
  ) => void;
  onRecommendApplicationDecision: (
    applicationRef: number,
    decision: string
  ) => void;
  facultyOptions: { label: string; value: number }[];
}

const ReviewerDataGrid: React.FC<DataGridProps> = ({
  data,
  onRecommendFaculties,
  onRecommendApplicationDecision,
  facultyOptions,
}) => {
  const dt = useRef<any>(null);

  const exportData = () => {
    dt.current?.exportCSV();
  };

  const facultyDropdownTemplate = (rowData: Applicant) => {
    const onFacultyChange = (e: { value: string[] }) => {
      onRecommendFaculties(rowData.Id, e.value);
    };

    return (
      <MultiSelect
        value={rowData.FacultyAdvisors?.map((fac) => fac.Name)}
        options={facultyOptions}
        onChange={onFacultyChange}
        placeholder="Select faculties"
        optionLabel="label"
        optionValue="value"
      />
    );
  };

  const decisionDropdownTemplate = (rowData: Applicant) => {
    const onDecisionChange = (e: { value: string }) => {
      onRecommendApplicationDecision(rowData.Id, e.value);
    };

    return (
      <Dropdown
        value={rowData.Status}
        options={[
          { label: "Accept", value: "Accept" },
          { label: "Reject", value: "Reject" },
          { label: "Inconclusive", value: "Inconclusive" },
        ]}
        onChange={onDecisionChange}
        placeholder="Select decision"
      />
    );
  };

  const header = (
    <div className="flex align-items-center justify-content-between">
      <StyledExportButton
        type="button"
        icon="pi pi-file"
        onClick={exportData}
        data-pr-tooltip="CSV"
        tooltip="Export to CSV"
        tooltipOptions={{ position: "bottom" }}
      />
    </div>
  );

  return (
    <Section>
      <DataTable
        ref={dt}
        value={data}
        header={header}
        paginator
        rows={10}
        dataKey="Id"
        emptyMessage="No applications found."
      >
        <Column field="FirstName" header="First Name" />
        <Column field="LastName" header="Last Name" />
        <Column field="Email" header="Email" />
        <Column field="AreaOfStudy" header="Area of Study" />
        <Column header="Recommend Faculties" body={facultyDropdownTemplate} />
        <Column
          header="Recommendation Decision"
          body={decisionDropdownTemplate}
        />
      </DataTable>
    </Section>
  );
};

export default ReviewerDataGrid;
