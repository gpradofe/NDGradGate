using ND.GradGate.Kernel.Domain.Contracts.Faculty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.Domain.Contracts.Faculty
{
    public class FacultyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsReviewer { get; set; }
        public string Field { get; set; }

        // DTOs for relationships
        public List<PotentialAdvisorDto> PotentialAdvisors { get; set; }
        public List<ReviewerAssignmentDto> ReviewerAssignments { get; set; }
        public List<CommentDto> Comments { get; set; }
    }
}
