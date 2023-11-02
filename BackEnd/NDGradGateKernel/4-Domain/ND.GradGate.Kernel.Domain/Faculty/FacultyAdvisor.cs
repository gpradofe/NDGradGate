using ND.GradGate.Kernel.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.Domain.Faculty
{
    public class FacultyAdvisor : AbstractEntity<FacultyAdvisor, int>
    {
        public string Name { get; set; }

        public virtual ICollection<ApplicantAdvisorLink> ApplicantAdvisorLinks { get; set; }
    }

}
