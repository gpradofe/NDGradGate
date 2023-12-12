using ND.GradGate.Kernel.Domain.Contracts.Applicant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.Application.Applicants.Interfaces.Actions
{
    public interface ICreateApplicantInfoAction
    {
        Task<List<ApplicantDto>> CreateApplicantInfoAsync(List<ApplicantDto> applicantDto);
    }
}