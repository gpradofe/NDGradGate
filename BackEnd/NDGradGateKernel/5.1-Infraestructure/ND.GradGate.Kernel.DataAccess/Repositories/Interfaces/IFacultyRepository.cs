using ND.GradGate.Kernel.DataAccess.Persistent.Repositories.Interfaces;
using ND.GradGate.Kernel.Domain.Faculty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.DataAccess.Repositories.Interfaces
{
    public interface IFacultyRepository : IRepository<Faculty, int>
    {
        Task<Faculty> GetByFacultyIdAsync(int Id);
        Task<List<Faculty>> GetByFacultyIdAsync(List<int> Id);

        Task<List<Faculty>> GetByFacultyNameAsync(string name);
        Task<List<Faculty>> GetAllFacultyAsync();
        Task UpdateFacultyAndRelatedEntitiesAsync(List<Faculty> facultiesToUpdate);
        Task<List<Faculty>> SaveOrUpdateMultipleAsync(List<Faculty> faculties);
    }
}