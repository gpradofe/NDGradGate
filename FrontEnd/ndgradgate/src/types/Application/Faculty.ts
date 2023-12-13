export interface PotentialAdvisorDto {
  Id: number;
}

export interface ReviewerAssignmentDto {
  AssignmentId: number;
  ReviewerName: string;
  ReviewDate: Date;
}

export interface CommentDto {
  FacultyId: number;
  ApplicantId: number;
  Content: string;
}

export interface Faculty {
  Id: number;
  Name: string;
  Email: string;
  IsAdmin: boolean;
  IsReviewer: boolean;
  Field: string;
  PotentialAdvisors: PotentialAdvisorDto[];
  ReviewerAssignments: ReviewerAssignmentDto[];
  Comments: CommentDto[];
}
