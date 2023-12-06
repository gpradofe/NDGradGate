using ND.GradGate.Kernel.Domain.Core.Entities;
using ND.GradGate.Kernel.Domain.EAV;
using ND.GradGate.Kernel.Domain.Faculty;
using System.Xml.Linq;

namespace ND.GradGate.Kernel.Domain.ApplicantData
{
    public class Applicant : AbstractEntity<Applicant, int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Sex { get; set; }
        public string Ethnicity { get; set; }
        public string Country { get; set; }
        public string Field { get; set; }
        public string Decision { get; set; }

        // Relationships
        public virtual ICollection<AcademicHistory> AcademicHistories { get; set; }
        public virtual ICollection<ApplicantAttribute> ApplicantAttributes { get; set; }
        public virtual ICollection<PotentialAdvisor> PotentialAdvisors { get; set; }
        public virtual ICollection<ReviewerAssignment> ReviewerAssignments { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }

}
