using ND.GradGate.Kernel.Domain.Contracts.Applicant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.Application.Applicants.Interfaces
{
    public interface IApplicantApplication
    {
        Task<ApplicantDto> GetApplicantByIdAsync(int refId);
        Task<List<ApplicantDto>> GetApplicantsByNameAsync(string firstName, string lastName);


    }
}
