using ND.GradGate.Kernel.DataAccess.Persistent.Repositories.Interfaces;
using ND.GradGate.Kernel.Domain.ApplicantData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.DataAccess.Repositories.Interfaces
{
    public interface IApplicantRepository : IRepository<Applicant, int>
    {
        Task<Applicant> GetByApplicantIdAsync(int Id);
        Task<List<Applicant>> GetByApplicantIdAsync(List<int> ids);
        Task<List<Applicant>> GetByApplicantNameAsync(string firstName, string lastName);
        Task<List<Applicant>> GetAllApplicants();
        Task UpdateApplicantAndRelatedEntitiesAsync(List<Applicant> applicantsToUpdate);
        Task<List<Applicant>> SaveMultipleAsync(List<Applicant> applicants);
        Task<List<Applicant>> GetApplicantByReviewerId(int reviewerId);
    }
}
