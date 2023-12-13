import React, { useState } from "react";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import {
  AcademicHistory,
  Applicant,
  ApplicantAttribute,
} from "../../../types/Application/Applicant"; // Adjust the import path as necessary
import { useApplicationContext } from "../../../context/ApplicationContext";
import { StyledButton } from "../../Pages/AdminDashboard/styles";

interface CSVData {
  [key: string]: string | number;
}

interface ImportedDataGridProps {
  data: CSVData[];
}

const ImportedDataGrid: React.FC<ImportedDataGridProps> = ({ data }) => {
  const [expandedRows, setExpandedRows] = useState<any[]>([]);
  const { createApplicants, fetchApplications } = useApplicationContext();

  const transformData = (csvData: CSVData[]): Applicant[] => {
    return csvData.map((row, index) => {
      const applicant: Partial<Applicant> = {
        Id: index, // ID might be auto-generated by the server. If so, you can remove this line.
        FirstName: row.FirstName as string,
        LastName: row.LastName as string,
        Email: row.Email as string,
        CitizenshipCountry: row.Country as string,
        AreaOfStudy: row.Field as string,
        Decision: row.Decision as string,
        Status: row.Status as string,
        // Initialize these fields with default or empty values
        Sex: row.Sex as string, // Update as needed
        Ethnicity: row.Ethnicity as string, // Update as needed
        DepartmentRecommendation: null, // Update as needed
        FacultyAdvisors: [], // Update as needed
        Reviewers: [], // Update as needed
        AcademicHistories: [],
        Attributes: [],
      };

      // Function to extract academic history entries
      const extractAcademicHistory = (keyPrefix: string) => {
        const institution = row[`Institution${keyPrefix}`] as string;
        const major = row[`Major${keyPrefix}`] as string;
        const gpa = parseFloat(row[`GPA${keyPrefix}`] as string);

        if (institution || major || !isNaN(gpa)) {
          applicant.AcademicHistories!.push({
            Institution: institution,
            Major: major,
            GPA: gpa,
          });
        }
      };

      // Extract standard and custom academic history entries
      extractAcademicHistory("");
      extractAcademicHistory("_1"); // Adjust if needed for your naming convention

      Object.entries(row).forEach(([key, value]) => {
        if (
          key.startsWith("Institution") ||
          key.startsWith("Major") ||
          key.startsWith("GPA")
        ) {
        } else if (
          [
            "FirstName",
            "LastName",
            "Email",
            "Country",
            "Field",
            "Decision",
            "Status",
          ].includes(key)
        ) {
          applicant[key as keyof Applicant] = value as any;
        } else {
          if (value !== "") {
            applicant.Attributes!.push({
              Attribute: key,
              Value: String(value),
            });
          }
        }
      });

      return applicant as Applicant;
    });
  };

  const handleCreateApplicants = async () => {
    try {
      await createApplicants(transformedData); // Call the API method
      alert("Applicants created successfully");
      fetchApplications(); // Refetch applications after creation
    } catch (error) {
      alert("Error creating applicants");
      console.error(error);
    }
  };
  const transformedData = transformData(data);
  console.log(transformedData);

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
    <>
      <DataTable
        value={transformedData}
        expandedRows={expandedRows}
        onRowToggle={(e) => setExpandedRows(e.data as any)}
        dataKey="Id"
        rowExpansionTemplate={rowExpansionTemplate}
      >
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
      <StyledButton
        style={{ padding: "10px", marginTop: "10px", width: "15%" }}
        onClick={handleCreateApplicants}
      >
        Create Applicants{" "}
      </StyledButton>
    </>
  );
};

export default ImportedDataGrid;