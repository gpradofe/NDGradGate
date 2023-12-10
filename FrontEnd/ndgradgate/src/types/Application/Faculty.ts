export interface PotentialAdvisorDto {
  Id: number;
  Name: string;
  ExpertiseArea: string;
}

export interface ReviewerAssignmentDto {
  AssignmentId: number;
  ReviewerName: string;
  ReviewDate: Date;
}

export interface CommentDto {
  CommentId: number;
  Text: string;
  AuthorName: string;
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
