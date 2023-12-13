import { PotentialAdvisorDto } from "./Faculty";

export interface AcademicHistory {
  GPA: number;
  Institution: string;
  Major: string | null;
}

export interface ApplicantAttribute {
  Attribute: string;
  Value: string;
}

export interface Comment {
  FacultyId: number;
  ApplicantId: number;
  Content: string;
  Date: Date;
}

export interface Reviewer {
  FacultyId: number;
  Name: string;
  Recommendation: string;
}

export interface Applicant {
  Id: number;
  LastName: string;
  FirstName: string;
  Email: string;
  Sex: string;
  Ethnicity: string;
  CitizenshipCountry: string;
  AreaOfStudy: string;
  Decision: string;
  DepartmentRecommendation: string | null;
  AcademicHistories: AcademicHistory[];
  FacultyAdvisors: PotentialAdvisorDto[];
  Reviewers: Reviewer[];
  Attributes: ApplicantAttribute[];
  Comments: Comment[];
  Status: string;
}
