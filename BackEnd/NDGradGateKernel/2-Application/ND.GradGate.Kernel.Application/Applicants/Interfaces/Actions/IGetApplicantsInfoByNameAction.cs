using ND.GradGate.Kernel.Domain.Contracts.Applicant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.Application.Applicants.Interfaces.Actions
{
    public interface IGetApplicantsInfoByNameAction
    {
        Task<List<ApplicantDto>> GetApplicantInfoAsync(string firstName, string lastName);

    }
}
