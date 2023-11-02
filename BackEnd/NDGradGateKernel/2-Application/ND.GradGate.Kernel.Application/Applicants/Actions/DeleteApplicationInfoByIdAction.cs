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
    public class DeleteApplicantInfoByIdAction : IDeleteApplicantInfoByIdAction
    {
        private readonly ILogger<DeleteApplicantInfoByIdAction> _logger;
        private readonly IApplicantRepository _applicantRepository;

        public DeleteApplicantInfoByIdAction(ILogger<DeleteApplicantInfoByIdAction> logger,
                                             IApplicantRepository applicantRepository)
        {
            _logger = logger;
            _applicantRepository = applicantRepository;
        }

        public async Task<bool> DeleteApplicantInfoAsync(int refId)
        {
            try
            {
                _logger.LogInformation($"Attempting to delete information for applicant with RefID {refId}");

                var applicantToDelete = await _applicantRepository.GetByApplicantIdAsync(refId);
                if (applicantToDelete == null)
                {
                    _logger.LogWarning($"No applicant found with RefID {refId}. Cannot delete.");
                    return false;
                }

                return await _applicantRepository.DeleteAsync(applicantToDelete);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting applicant with RefID {refId}: {ex.Message}");
                return false;
            }
        }
    }

}