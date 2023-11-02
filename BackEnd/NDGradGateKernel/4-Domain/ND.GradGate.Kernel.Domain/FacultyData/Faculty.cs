using ND.GradGate.Kernel.Domain.Core.Entities;
using ND.GradGate.Kernel.Domain.EAV;
using ND.GradGate.Kernel.Domain.Faculty;

namespace ND.GradGate.Kernel.Domain.FacultyData
{
    public class Faculty : AbstractEntity<Faculty, int>
    {
        public int Ref { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
    }

}