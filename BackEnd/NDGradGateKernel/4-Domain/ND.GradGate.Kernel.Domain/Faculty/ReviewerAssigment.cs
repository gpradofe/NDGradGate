using ND.GradGate.Kernel.Domain.ApplicantData;
using ND.GradGate.Kernel.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.Domain.Faculty
{
    public class ReviewerAssignment : AbstractEntity<ReviewerAssignment, int>
    {
        public int ReviewerId { get; set; }
        public int AdminId { get; set; }
        public int ApplicantRef { get; set; }
        public DateTime ReviewDate { get; set; }
        public string Comments { get; set; }
        public string Recommendation { get; set; }

        public virtual Reviewer Reviewer { get; set; } // New relationship
        public virtual FacultyAdministration FacultyAdministration { get; set; }
        public virtual Applicant Applicant { get; set; }
    }

}
