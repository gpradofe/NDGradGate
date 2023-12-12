import React, { useState } from "react";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import {
  Applicant,
  ApplicantAttribute,
} from "../../../types/Application/Applicant"; // Adjust the import path as necessary

interface CSVData {
  [key: string]: string | number;
}

interface ImportedDataGridProps {
  data: CSVData[];
}

const ImportedDataGrid: React.FC<ImportedDataGridProps> = ({ data }) => {
  const [expandedRows, setExpandedRows] = useState<any[]>([]);
  console.log(data);
  const transformData = (csvData: CSVData[]): Applicant[] => {
    return csvData.map((row, index) => {
      const applicant: Partial<Applicant> = {};
      const applicantAttributes: ApplicantAttribute[] = [];
      Object.entries(row).forEach(([key, value]) => {
        if (
          [
            "FirstName",
            "LastName",
            "Email",
            "Country",
            "Field",
            "Decision",
            "Status",
            "AcademicHistories",
          ].includes(key)
        ) {
          applicant[key as keyof Applicant] = value as any;
        } else {
          applicantAttributes.push({ Attribute: key, Value: String(value) });
        }
      });

      applicant["Ref"] = index;
      return {
        ...applicant,
        ApplicantAttributes: applicantAttributes,
      } as Applicant;
    });
  };

  const transformedData = transformData(data);

  const rowExpansionTemplate = (data: Applicant) => (
    <div>
      <h5>
        Attributes for {data.FirstName} {data.LastName}
      </h5>
      <DataTable value={data.Attributes}>
        <Column field="Attribute" header="Attribute" />
        <Column field="Value" header="Value" />
      </DataTable>
    </div>
  );

  const academicHistoryBodyTemplate = (rowData: Applicant) => {
    return (
      <div>
        {rowData.AcademicHistories?.map((history, index) => (
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

  return (
    <DataTable
      value={transformedData}
      expandedRows={expandedRows}
      onRowToggle={(e) => setExpandedRows(e.data as any)}
      dataKey="ref"
      rowExpansionTemplate={rowExpansionTemplate}
    >
      {/* Standard columns */}
      <Column field="FirstName" header="First Name" />
      <Column field="LastName" header="Last Name" />
      <Column field="Email" header="Email" />
      <Column field="Country" header="Country" />
      <Column field="Field" header="Field" />
      <Column field="Decision" header="Decision" />
      <Column field="Status" header="Status" />

      <Column
        field="AcademicHistories"
        header="Academic Histories"
        body={academicHistoryBodyTemplate}
      />

      <Column expander style={{ width: "3em" }} />
    </DataTable>
  );
};

export default ImportedDataGrid;
