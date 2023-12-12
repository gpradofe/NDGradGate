using Microsoft.Extensions.Logging;
using ND.GradGate.Kernel.Application.Applicants.Interfaces.Actions;
using ND.GradGate.Kernel.DataAccess.Repositories.Interfaces;
using ND.GradGate.Kernel.Domain.ApplicantData;
using ND.GradGate.Kernel.Domain.Contracts.Applicant;
using ND.GradGate.Kernel.Domain.Faculty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.Application.Applicants.Actions
{
    public class UpdateApplicantStatusAndReviewerAction : IUpdateApplicantStatusAndReviewerAction
    {
        private readonly ILogger<UpdateApplicantStatusAndReviewerAction> _logger;
        private readonly IApplicantRepository _applicantRepository;
        private readonly IFacultyRepository _facultyRepository;

        public UpdateApplicantStatusAndReviewerAction(ILogger<UpdateApplicantStatusAndReviewerAction> logger,
                                             IApplicantRepository applicantRepository,
                                             IFacultyRepository facultyRepository)
        {
            _logger = logger;
            _applicantRepository = applicantRepository;
            _facultyRepository = facultyRepository;
        }

        public async Task<bool> UpdateApplicantInfoAsync(List<UpdateApplicantStatusAndReviewerDto> updateApplicantStatusAndReviewerDtos)
        {
            try
            {
                _logger.LogInformation($"Updating information for applicants {updateApplicantStatusAndReviewerDtos}");
                List<int> applicantIds = updateApplicantStatusAndReviewerDtos.Select(dto => dto.Ref).ToList();

                // Get applicants with their related data
                List<Applicant> applicantsToUpdate = await _applicantRepository.GetByApplicantIdAsync(applicantIds);
                if (!applicantsToUpdate.Any())
                {
                    throw new KeyNotFoundException($"Applicants not found");
                }

                // Prepare the applicants with the updates
                foreach (var applicantDto in updateApplicantStatusAndReviewerDtos)
                {
                    var currApplicant = applicantsToUpdate.FirstOrDefault(a => a.Id == applicantDto.Ref);
                    if (currApplicant == null)
                    {
                        _logger.LogWarning($"Applicant with ID {applicantDto.Ref} not found");
                        continue;
                    }

                    if (!string.IsNullOrEmpty(applicantDto.Status))
                    {
                        currApplicant.Status = applicantDto.Status;
                    }

                    if (applicantDto.FacultyId != null)
                    {
                        foreach (int facultyId in applicantDto.FacultyId)
                        {
                            if (!currApplicant.ReviewerAssignments.Any(ra => ra.FacultyId == facultyId))
                            {
                                var reviewerAssignment = new ReviewerAssignment
                                {
                                    ApplicantId = applicantDto.Ref,
                                    FacultyId = facultyId,
                                    Status = "Assigned",
                                };
                                currApplicant.ReviewerAssignments.Add(reviewerAssignment);
                            }
                        }
                    }
                }

                await _applicantRepository.UpdateApplicantAndRelatedEntitiesAsync(applicantsToUpdate);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating applicant information");
                throw;
            }
        }

    }
}

