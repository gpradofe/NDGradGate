using Microsoft.Extensions.Logging;
using ND.GradGate.Kernel.Application.Applicants.Actions;
using ND.GradGate.Kernel.Application.Applicants.Interfaces;
using ND.GradGate.Kernel.Application.Applicants.Interfaces.Actions;
using ND.GradGate.Kernel.Domain.Contracts.Applicant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.Application.Applicants
{
    public class ApplicantApplication : IApplicantApplication
    {
        #region Attributes
        private readonly ILogger<ApplicantApplication> _logger;
        private readonly IGetApplicantInfoByIdAction _getApplicantInfoByIdAction;
        private readonly IGetApplicantsInfoByNameAction _getApplicantsInfoByNameAction;
        #endregion

        #region Constructors
        public ApplicantApplication(ILogger<ApplicantApplication> logger,
                                    IGetApplicantInfoByIdAction getApplicantInfoByIdAction,
                                    IGetApplicantsInfoByNameAction getApplicantsInfoByNameAction)
        {
            _logger = logger;
            _getApplicantInfoByIdAction = getApplicantInfoByIdAction;
            _getApplicantsInfoByNameAction = getApplicantsInfoByNameAction;
        }
        #endregion

        #region Methods
        public async Task<ApplicantDto> GetApplicantByIdAsync(int refId)
        {
            try
            {
                _logger.LogInformation($"Search for applicant data with RefID @{refId}.");

                ApplicantDto applicant = await _getApplicantInfoByIdAction.GetApplicantInfoAsync(refId);

                return applicant;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<List<ApplicantDto>> GetApplicantsByNameAsync(string firstName, string lastName)
        {
            try
            {
                _logger.LogInformation($"Search for applicants with names matching: FirstName: {firstName}, LastName: {lastName}.");

                List<ApplicantDto> applicants = await _getApplicantsInfoByNameAction.GetApplicantInfoAsync(firstName, lastName);

                return applicants;
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
