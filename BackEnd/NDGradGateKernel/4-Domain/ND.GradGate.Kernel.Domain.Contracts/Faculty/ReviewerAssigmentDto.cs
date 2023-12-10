using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.Domain.Contracts.Faculty
{
    public class ReviewerAssignmentDto
    {
        public int FacultyId { get; set; }
        public int ApplicantId { get; set; }
        public int? CommentId { get; set; } 
        public string Status { get; set; }
    }

}
