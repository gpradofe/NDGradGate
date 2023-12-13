using ND.GradGate.Kernel.Domain.Contracts.Faculty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.Application.Facultys.Interfaces
{
    public interface IFacultyApplication
    {
        Task<FacultyDto> GetFacultyByIdAsync(int refId);
        Task<List<FacultyDto>> GetFacultysByNameAsync(string name);
        Task<List<FacultyDto>> GetAllFaculty();
        Task<List<int>> GetAsssinedApplicantionsByReviewerIdAsync(int reviewerId);
        Task<bool> AssignPotentialAdvisorsAndAddCommentsAsync(List<AssignFacultyAndAddComment> assignFacultyAndAddComments);
        Task<List<FacultyDto>> SaveOrUpdateFacultyAsync(List<FacultyDto> facultyDtos);
    }
}