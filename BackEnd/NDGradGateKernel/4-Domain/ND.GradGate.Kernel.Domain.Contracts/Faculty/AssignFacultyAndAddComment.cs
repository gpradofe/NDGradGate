using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.Domain.Contracts.Faculty
{
    public class AssignFacultyAndAddComment
    {
        public int SenderId { get; set; }
        public int ApplicantID { get; set; }
        public List<int>? PotentialAdvisorId { get; set; }
        public string? Comment { get; set; }
    }
}
