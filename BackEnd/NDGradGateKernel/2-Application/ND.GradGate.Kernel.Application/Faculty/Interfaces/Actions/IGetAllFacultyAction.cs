using ND.GradGate.Kernel.Domain.Contracts.Faculty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.Application.Faculty.Interfaces.Actions
{
    public interface IGetAllFacultyAction
    {
        Task<List<FacultyDto>> GetAllFaculty();
    }
}
