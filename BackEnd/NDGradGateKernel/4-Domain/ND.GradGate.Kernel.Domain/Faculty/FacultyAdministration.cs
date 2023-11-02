using ND.GradGate.Kernel.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.Domain.Faculty
{
    public class FacultyAdministration : AbstractEntity<FacultyAdministration, int>
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }

        public virtual ICollection<ReviewerAssignment> ReviewerAssignments { get; set; }
    }

}
