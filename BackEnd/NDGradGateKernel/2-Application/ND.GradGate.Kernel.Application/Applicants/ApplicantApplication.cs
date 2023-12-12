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
        private readonly IGetAllApplicantsAction _getAllApplicantsAction;
        private readonly IUpdateApplicantInfoByIdAction _updateApplicantInfoByIdAction;
        private readonly IUpdateApplicantStatusAndReviewerAction _updateApplicantStatusAndReviewerAction;
        private readonly ICreateApplicantInfoAction _createApplicantInfoByIdAction;
        private readonly IDeleteApplicantInfoByIdAction _deleteApplicantInfoByIdAction;
        #endregion

        #region Constructors
        public ApplicantApplication(ILogger<ApplicantApplication> logger,
                                    IGetApplicantInfoByIdAction getApplicantInfoByIdAction,
                                    IGetApplicantsInfoByNameAction getApplicantsInfoByNameAction,
                                    IUpdateApplicantInfoByIdAction updateApplicantInfoByIdAction,
                                    IDeleteApplicantInfoByIdAction deleteApplicantInfoById,
                                    IGetAllApplicantsAction getAllApplicantsAction,
                                    ICreateApplicantInfoAction createApplicantInfoAction, 
                                    IUpdateApplicantStatusAndReviewerAction updateApplicantStatusAndReviewerAction)
        {
            _logger = logger;
            _getApplicantInfoByIdAction = getApplicantInfoByIdAction;
            _getApplicantsInfoByNameAction = getApplicantsInfoByNameAction;
            _createApplicantInfoByIdAction = createApplicantInfoAction;
            _deleteApplicantInfoByIdAction = deleteApplicantInfoById;
            _updateApplicantInfoByIdAction = updateApplicantInfoByIdAction;
            _getAllApplicantsAction = getAllApplicantsAction;
            _updateApplicantStatusAndReviewerAction = updateApplicantStatusAndReviewerAction;
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
        public async Task<List<ApplicantDto>> GetAllApplicantsAsync()
        {
            try
            {
                _logger.LogInformation($"Fetching all applicants");

                List<ApplicantDto> applicants = await _getAllApplicantsAction.GetAllApplicantsAsync();

                return applicants;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<ApplicantDto> UpdateApplicantInfoAsync(int refId, ApplicantDto applicantDto)
        {
            try
            {
                _logger.LogInformation($"Update applicant data with RefID @{refId}.");

                var updatedApplicant = await _updateApplicantInfoByIdAction.UpdateApplicantInfoAsync(refId, applicantDto);

                if (updatedApplicant == null)
                {
                    _logger.LogError($"Failed to update applicant with RefID {refId}.");
                    // Optionally handle the failure case, such as throwing a custom exception or returning null
                }

                return updatedApplicant;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }


        public async Task<bool> CreateApplicantInfoAsync(ApplicantDto applicantDto)
        {
            try
            {
                _logger.LogInformation($"Create applicant data.");

                bool ret = await _createApplicantInfoByIdAction.CreateApplicantInfoAsync(applicantDto);

                return ret;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<bool> DeleteApplicantInfoAsync(int refId)
        {
            try
            {
                _logger.LogInformation($"Attempting to delete applicant data with RefID: {refId}.");

                var result = await _deleteApplicantInfoByIdAction.DeleteApplicantInfoAsync(refId);

                if (!result)
                {
                    _logger.LogWarning($"Deletion of applicant with RefID: {refId} failed.");
                    return false;
                }

                _logger.LogInformation($"Successfully deleted applicant data with RefID: {refId}.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while attempting to delete applicant data with RefID: {refId}. Error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateApplicantStatusAndReviewerAsync(List<UpdateApplicantStatusAndReviewerDto> updateApplicantStatusAndReviewerDtos)
        {
            try
            {
                _logger.LogInformation($"Updating applicant data @{updateApplicantStatusAndReviewerDtos}.");

                var updatedApplicant = await _updateApplicantStatusAndReviewerAction.UpdateApplicantInfoAsync(updateApplicantStatusAndReviewerDtos);

                if (updatedApplicant == null)
                {
                    _logger.LogError($"Failed to update applicants  {updateApplicantStatusAndReviewerDtos}.");
                }

                return updatedApplicant;
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
