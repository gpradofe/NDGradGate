import React, { useEffect, useState, useRef } from "react";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import { InputText } from "primereact/inputtext";
import { Dropdown } from "primereact/dropdown";
import { Toast } from "primereact/toast";
import { Button } from "primereact/button";
import { useApplicationContext } from "../../../context/ApplicationContext";
import { Faculty } from "../../../types/Application/Faculty";
import { Applicant } from "../../../types/Application/Applicant";
import { MultiSelect } from "primereact/multiselect";

const ReviewerDataGrid: React.FC = () => {
  const {
    faculty,
    applications,
    currentUser,
    getAssignedReviewer,
    assignPotentialAdvisorsAndAddComments,
    fetchApplications,
  } = useApplicationContext();
  const [originalAssignedApplicants, setOriginalAssignedApplicants] = useState<
    Applicant[]
  >([]);

  const [assignedApplicants, setAssignedApplicants] = useState<Applicant[]>([]);
  const [comments, setComments] = useState<{ [key: number]: string }>({});
  const [selectedFaculty, setSelectedFaculty] = useState<{
    [key: number]: number[];
  }>({});
  const [isChanged, setIsChanged] = useState(false);
  const toast = useRef<Toast>(null);

  useEffect(() => {
    if (currentUser) {
      getAssignedReviewer(currentUser.Id)
        .then((assignedIds) => {
          const updatedAssignedApplicants = applications
            .filter((app) => assignedIds.includes(app.Id))
            .map((applicant) => {
              const existingComments = faculty
                .filter((f) =>
                  f.Comments.some(
                    (c) =>
                      c.ApplicantId === applicant.Id &&
                      c.FacultyId === currentUser.Id
                  )
                )
                .map(
                  (f) =>
                    f.Comments.find(
                      (c) =>
                        c.ApplicantId === applicant.Id &&
                        c.FacultyId === currentUser.Id
                    )?.Content || ""
                );

              // Load existing potential advisor selections by matching faculty IDs
              const existingFacultyIds = applicant.FacultyAdvisors.map(
                (advisor) => advisor.Id
              ).filter((id) => faculty.some((f) => f.Id === id));

              setComments((prevComments) => ({
                ...prevComments,
                [applicant.Id]: existingComments.join(", "),
              }));
              setSelectedFaculty((prevSelected) => ({
                ...prevSelected,
                [applicant.Id]: existingFacultyIds,
              }));

              return applicant;
            });

          setAssignedApplicants(updatedAssignedApplicants);
          setOriginalAssignedApplicants(updatedAssignedApplicants);
        })
        .catch((error) =>
          console.error("Error fetching assigned reviewer data:", error)
        );
    }
  }, [currentUser, applications, getAssignedReviewer]);
  const onCommentChange = (
    e: React.ChangeEvent<HTMLInputElement>,
    applicantId: number
  ) => {
    setComments({ ...comments, [applicantId]: e.target.value });
    setIsChanged(true);
  };

  const onFacultyChange = (applicantId: number, facultyIds: number[]) => {
    setSelectedFaculty({ ...selectedFaculty, [applicantId]: facultyIds });
    setIsChanged(true);
  };

  const applyChanges = () => {
    const data = assignedApplicants.map((applicant) => {
      const facultyIds = selectedFaculty[applicant.Id] || [];
      return {
        SenderId: currentUser?.Id || 0,
        ApplicantID: applicant.Id,
        PotentialAdvisorId:
          facultyIds.length > 0
            ? facultyIds
            : (applicant.FacultyAdvisors || []).map((pa) => pa.Id),
        Comment: comments[applicant.Id] || "",
      };
    });

    assignPotentialAdvisorsAndAddComments(data)
      .then(() => {
        setIsChanged(false);
        showSuccess("Changes applied successfully");
        fetchApplications(); // Refetch applications data
      })
      .catch((error) => {
        console.error("Error applying changes:", error);
      });
  };

  const showSuccess = (message: string) => {
    toast.current?.show({
      severity: "success",
      summary: "Success",
      detail: message,
      life: 3000,
    });
  };

  const renderCommentInput = (rowData: Applicant) => {
    return (
      <InputText
        value={comments[rowData.Id] || ""}
        onChange={(e) => onCommentChange(e, rowData.Id)}
      />
    );
  };

  const renderFacultyDropdown = (rowData: Applicant) => {
    const options = faculty.map((f) => ({ label: f.Name, value: f.Id }));
    return (
      <MultiSelect
        value={selectedFaculty[rowData.Id] || []}
        options={options}
        onChange={(e) => onFacultyChange(rowData.Id, e.value)}
      />
    );
  };

  const footer = isChanged ? (
    <div className="p-d-flex p-jc-end p-mt-2">
      <Button
        label="Apply"
        onClick={applyChanges}
        className="p-button-success"
      />
    </div>
  ) : null;
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
  const revertChanges = () => {
    setAssignedApplicants(originalAssignedApplicants); // Revert to the original list of assigned applicants
    setIsChanged(false);
  };
  return (
    <div>
      <Toast ref={toast} />
      <DataTable value={assignedApplicants}>
        <Column field="FirstName" header="First Name" />
        <Column field="LastName" header="Last Name" />
        <Column field="Email" header="Email" />
        <Column field="AreaOfStudy" header="Area of Study" />
        <Column field="CitizenshipCountry" header="Citizenship Country" />
        <Column body={academicHistoryBodyTemplate} header="Academic History" />
        <Column body={renderCommentInput} header="Comment" />
        <Column
          body={renderFacultyDropdown}
          header="Assign Potential Advisors"
        />
        {/* Include other columns here as needed */}
      </DataTable>
      {isChanged && (
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
      )}
    </div>
  );
};

export default ReviewerDataGrid;
