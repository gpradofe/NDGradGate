using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.Domain.Contracts.Faculty
{
    public class CommentDto
    {
        public int FacultyId { get; set; }
        public int ApplicantId { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
    }
}
