import React from "react";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import { Applicant } from "../../../types/Application/Applicant";

interface DataGridProps {
  data: Applicant[];
  onAdvisorAcceptance: (applicationRef: number, facultyId: number) => void;
  onAdvisorRejection: (applicationRef: number, facultyId: number) => void;
}

const FacultyDataGrid: React.FC<DataGridProps> = ({
  data,
  onAdvisorAcceptance,
  onAdvisorRejection,
}) => {
  const renderAdvisorDecision = (rowData: Applicant) => {
    return rowData.FacultyAdvisors.map((advisor, index) => (
      <div key={index}>
        {advisor.name}
        <button onClick={() => onAdvisorAcceptance(rowData.Ref, advisor.id)}>
          Accept
        </button>
        <button onClick={() => onAdvisorRejection(rowData.Ref, advisor.id)}>
          Reject
        </button>
      </div>
    ));
  };

  const renderAcademicHistory = (rowData: Applicant) => {
    return rowData.AcademicHistories.map((history, index) => (
      <div key={index}>
        <b>Institution:</b> {history.Institution}
        <br />
        <b>Major:</b> {history.Major}
        <br />
        <b>GPA:</b> {history.GPA.toFixed(2)}
      </div>
    ));
  };

  return (
    <DataTable value={data} paginator rows={10} dataKey="Ref">
      <Column field="FirstName" header="First Name" />
      <Column field="LastName" header="Last Name" />
      <Column field="Email" header="Email" />
      <Column field="ApplicationStatus" header="Application Status" />
      <Column field="AreaOfStudy" header="Area of Study" />
      <Column field="CitizenshipCountry" header="Citizenship Country" />
      <Column field="Ethnicity" header="Ethnicity" />
      <Column field="Sex" header="Sex" />
      <Column
        field="AcademicHistories"
        header="Academic Histories"
        body={renderAcademicHistory}
      />
      <Column header="Advisor Decision" body={renderAdvisorDecision} />
    </DataTable>
  );
};

export default FacultyDataGrid;
