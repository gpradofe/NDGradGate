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
    public class DeleteApplicantInfoByIdAction: IDeleteApplicantInfoByIdAction
    {
        #region Attributes
        private readonly ILogger<DeleteApplicantInfoByIdAction> _logger;
        private readonly IApplicantRepository _applicantRepository;
        #endregion

        #region Constructor
        public DeleteApplicantInfoByIdAction(ILogger<DeleteApplicantInfoByIdAction> logger,
                                    IApplicantRepository applicantRepository)
        {
            _logger = logger;
            _applicantRepository = applicantRepository;
        }
        #endregion

        #region Methods
        public async Task DeleteApplicantInfoAsync(int refId)
        {
            try
            {
                _logger.LogInformation($"Deleting information for applicant with RefID {refId}");

                var applicantToDelete = await _applicantRepository.GetByApplicantIdAsync(refId);

                await _applicantRepository.DeleteAsync(applicantToDelete);
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