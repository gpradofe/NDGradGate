export interface AcademicHistory {
  GPA: number;
  Institution: string;
  Major: string | null; // 'null' is a valid type in TypeScript to signify that the value can explicitly be null
}

export interface Applicant {
  AcademicHistories: AcademicHistory[];
  ApplicationStatus: string;
  AreaOfStudy: string;
  CitizenshipCountry: string;
  DepartmentRecommendation: string | null;
  Email: string;
  Ethnicity: string;
  FacultyAdvisors: any[]; // If you have a specific type for faculty advisors, replace 'any' with that type
  FirstName: string;
  LastName: string;
  Ref: number; // Assuming Ref is a unique identifier for the applicant and should be a number
  Reviewers: any[]; // Similar to FacultyAdvisors, replace 'any' with the specific type if available
  Sex: string; // 'M', 'F', or any other designation of sex or gender should be a string
}
