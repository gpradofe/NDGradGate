import React from "react";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import { Applicant } from "../../../types/Application/Applicant";

interface DataGridProps {
  data: Applicant[];
  onAdvisorAcceptance: (applicationRef: number, facultyName: string) => void;
  onAdvisorRejection: (applicationRef: number, facultyName: string) => void;
}

const FacultyDataGrid: React.FC<DataGridProps> = ({
  data,
  onAdvisorAcceptance,
  onAdvisorRejection,
}) => {
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
    </DataTable>
  );
};

export default FacultyDataGrid;
