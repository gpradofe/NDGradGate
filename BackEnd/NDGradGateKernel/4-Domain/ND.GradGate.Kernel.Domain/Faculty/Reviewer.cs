using ND.GradGate.Kernel.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.Domain.Faculty
{
    public class Reviewer : AbstractEntity<Reviewer, int>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Specialization { get; set; }

        public virtual ICollection<ReviewerAssignment> ReviewerAssignments { get; set; } = new List<ReviewerAssignment>();
    }
}
