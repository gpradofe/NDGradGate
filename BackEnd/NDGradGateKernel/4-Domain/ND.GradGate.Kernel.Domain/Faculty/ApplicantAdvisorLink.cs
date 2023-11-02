using ND.GradGate.Kernel.Domain.ApplicantData;
using ND.GradGate.Kernel.Domain.Core.Entities;
using ND.GradGate.Kernel.Domain.EAV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.Domain.Faculty
{
    public class ApplicantAdvisorLink : AbstractEntity<ApplicantAdvisorLink, int>
    {
        public int ApplicantRef { get; set; }
        public int AdvisorId { get; set; }

        public virtual Applicant Applicant { get; set; }
        public virtual FacultyAdvisor FacultyAdvisor { get; set; }
    }

}
