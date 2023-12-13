using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.Domain.Contracts.Faculty
{
    public class ReviewerDto
    {
        
        public string Recommendation { get; set; }
        public int FacultyId { get; set; }
    }
}
