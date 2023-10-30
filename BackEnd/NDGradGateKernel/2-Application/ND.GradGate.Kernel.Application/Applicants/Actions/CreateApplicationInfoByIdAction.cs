using Microsoft.Extensions.Logging;
using ND.GradGate.Kernel.Application.Applicants.Interfaces.Actions;
using ND.GradGate.Kernel.DataAccess.Repositories.Interfaces;
using ND.GradGate.Kernel.Domain.Contracts.Applicant;
using ND.GradGate.Kernel.Domain.ApplicantData;
using ND.GradGate.Kernel.Domain.Contracts.Faculty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.Application.Applicants.Actions
{
    public class CreateApplicantInfoByIdAction: ICreateApplicantInfoByIdAction
    {
        #region Attributes
        private readonly ILogger<CreateApplicantInfoByIdAction> _logger;
        private readonly IApplicantRepository _applicantRepository;
        #endregion

        #region Constructor
        public CreateApplicantInfoByIdAction(ILogger<CreateApplicantInfoByIdAction> logger,
                                    IApplicantRepository applicantRepository)
        {
            _logger = logger;
            _applicantRepository = applicantRepository;
        }
        #endregion

        #region Methods
        public async Task<ApplicantDto> CreateApplicantInfoAsync(ApplicantDto applicant)
        {
            try
            {
                _logger.LogInformation($"Creating information for applicant with RefID {applicant.Ref}");

                var applicantToCreate = new Applicant
                {
                    Ref = applicant.Ref,
                    LastName = applicant.LastName,
                    FirstName = applicant.FirstName,
                    Email = applicant.Email,
                };

                await _applicantRepository.Add(applicantToCreate);

                return applicant;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        #endregion
    }
}