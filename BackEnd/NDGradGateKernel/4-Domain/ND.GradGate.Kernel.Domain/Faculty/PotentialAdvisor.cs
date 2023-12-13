using ND.GradGate.Kernel.Domain.ApplicantData;
using ND.GradGate.Kernel.Domain.Core.Entities;
using ND.GradGate.Kernel.Domain.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.Domain.Faculty
{
    public class PotentialAdvisor : AbstractEntity<PotentialAdvisor, int> 
    {
        public int FacultyId { get; set; }
        public int ApplicantId { get; set; }

        public virtual Faculty Faculty { get; set; }
        public virtual Applicant Applicant { get; set; }
    }
}
