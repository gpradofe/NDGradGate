using ND.GradGate.Kernel.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ND.GradGate.Kernel.Domain.Faculty
{
    public class Faculty : AbstractEntity<Faculty, int>
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsReviewer { get; set; }
        public string Field { get; set; }

        // Relationships
        public virtual ICollection<PotentialAdvisor> PotentialAdvisors { get; set; }
        public virtual ICollection<ReviewerAssignment> ReviewerAssignments { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }

}
