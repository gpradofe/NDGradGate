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
        public int Ref { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
    }
}