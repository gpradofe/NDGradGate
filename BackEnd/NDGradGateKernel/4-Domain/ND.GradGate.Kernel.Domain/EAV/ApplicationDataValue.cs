using ND.GradGate.Kernel.Domain.ApplicantData;
using ND.GradGate.Kernel.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.Domain.EAV
{
    public class ApplicantAttribute : AbstractEntity<ApplicantAttribute, int>
    {
        public int ApplicantId { get; set; }
        public string Attribute { get; set; }
        public string Value { get; set; }

        // Relationship
        public virtual Applicant Applicant { get; set; }
    }
}
