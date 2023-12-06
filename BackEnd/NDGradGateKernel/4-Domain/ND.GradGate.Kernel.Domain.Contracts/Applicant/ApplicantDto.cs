using ND.GradGate.Kernel.Domain.Contracts.Faculty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.Domain.Contracts.Applicant
{
    public class ApplicantDto
    {
        public int Ref { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Sex { get; set; }
        public string Ethnicity { get; set; }
        public string CitizenshipCountry { get; set; }
        public string AreaOfStudy { get; set; }
        public string ApplicationStatus { get; set; }
        public string? DepartmentRecommendation { get; set; }
        public List<AcademicHistoryDto>? AcademicHistories { get; set; }
        public List<FacultyAdvisorDto>? FacultyAdvisors { get; set; }
        public List<ReviewerDto>? Reviewers { get; set; }
        public List<ApplicantAttributeDto>? Attributes { get; set; }
        public List<CommentDto>? Comments { get; set; }
    }
}
