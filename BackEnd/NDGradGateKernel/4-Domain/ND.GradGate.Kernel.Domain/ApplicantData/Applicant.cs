using ND.GradGate.Kernel.Domain.Core.Entities;
using ND.GradGate.Kernel.Domain.EAV;
using ND.GradGate.Kernel.Domain.Faculty;

namespace ND.GradGate.Kernel.Domain.ApplicantData
{
    public class Applicant : AbstractEntity<Applicant, int>
    {
        public int Ref { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public char Sex { get; set; }
        public string Ethnicity { get; set; }
        public string CitizenshipCountry { get; set; }
        public string AreaOfStudy { get; set; }
        public string ApplicationStatus { get; set; }
        public string DepartmentRecommendation { get; set; }

        // Relationships
        public virtual ICollection<AcademicHistory> AcademicHistories { get; set; }
        public virtual ICollection<ApplicationDataValue> ApplicationDataValues { get; set; }
        public virtual ICollection<ApplicantAdvisorLink> ApplicantAdvisorLinks { get; set; }
        public virtual ICollection<ReviewerAssignment> ReviewerAssignments { get; set; }
    }

}
