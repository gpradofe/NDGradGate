using Microsoft.Extensions.Logging;
using ND.GradGate.Kernel.Application.Applicants.Interfaces.Actions;
using ND.GradGate.Kernel.DataAccess.Repositories.Interfaces;
using ND.GradGate.Kernel.Domain.ApplicantData;
using ND.GradGate.Kernel.Domain.Faculty;
using ND.GradGate.Kernel.Domain.Contracts.Applicant;
using ND.GradGate.Kernel.Domain.Contracts.Faculty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.Application.Applicants.Actions
{
    public class UpdateApplicantInfoByIdAction: IUpdateApplicantInfoByIdAction
    {
        #region Attributes
        private readonly ILogger<UpdateApplicantInfoByIdAction> _logger;
        private readonly IApplicantRepository _applicantRepository;
        #endregion

        #region Constructor
        public UpdateApplicantInfoByIdAction(ILogger<UpdateApplicantInfoByIdAction> logger,
                                    IApplicantRepository applicantRepository)
        {
            _logger = logger;
            _applicantRepository = applicantRepository;
        }
        #endregion

        #region Methods
        public async Task<ApplicantDto> UpdateApplicantInfoAsync(int refId, ApplicantDto applicant)
        {
            try
            {
                _logger.LogInformation($"Updating information for applicant with RefID {refId}");

                var applicantToUpdate = await _applicantRepository.GetByApplicantIdAsync(refId);

                applicantToUpdate.LastName = applicant.LastName;
                applicantToUpdate.FirstName = applicant.FirstName;
                applicantToUpdate.Email = applicant.Email;
                applicantToUpdate.Sex = applicant.Sex;
                applicantToUpdate.Ethnicity = applicant.Ethnicity;
                applicantToUpdate.CitizenshipCountry = applicant.CitizenshipCountry;
                applicantToUpdate.AreaOfStudy = applicant.AreaOfStudy;
                applicantToUpdate.ApplicationStatus = applicant.ApplicationStatus;
                applicantToUpdate.DepartmentRecommendation = applicant.DepartmentRecommendation;

                applicantToUpdate.AcademicHistories = applicant.AcademicHistories.Select(ah => new AcademicHistory
                {
                    Institution = ah.Institution,
                    Major = ah.Major,
                    Gpa = ah.GPA
                }).ToList();

                applicantToUpdate.ApplicantAdvisorLinks = applicant.FacultyAdvisors.Select(fa => new ApplicantAdvisorLink
                {
                    FacultyAdvisor = new FacultyAdvisor
                    {
                        Name = fa.Name
                    }
                }).ToList();

                applicantToUpdate.ReviewerAssignments = applicant.Reviewers.Select(r => new ReviewerAssignment
                {
                    Reviewer = new Reviewer
                    {
                        Name = r.Name
                    }
                }).ToList();

                await _applicantRepository.UpdateAsync(applicantToUpdate);

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