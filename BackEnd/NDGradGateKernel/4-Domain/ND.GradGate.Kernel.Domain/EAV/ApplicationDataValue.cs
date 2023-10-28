using ND.GradGate.Kernel.Domain.ApplicantData;
using ND.GradGate.Kernel.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.Domain.EAV
{
    public class ApplicationDataValue : AbstractEntity<ApplicationDataValue, int>
    {
        public int ApplicantRef { get; set; }
        public int AttributeId { get; set; }
        public string Value { get; set; }

        public virtual Applicant Applicant { get; set; }
        public virtual Attribute Attribute { get; set; }
    }
}
