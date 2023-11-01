using Microsoft.Extensions.Logging;
using ND.GradGate.Kernel.Application.Applicants.Interfaces.Actions;
using ND.GradGate.Kernel.DataAccess.Repositories.Interfaces;
using ND.GradGate.Kernel.Domain.Contracts.Applicant;
using ND.GradGate.Kernel.Domain.ApplicantData;
using ND.GradGate.Kernel.Domain.FacultyData;
using ND.GradGate.Kernel.Domain.Faculty;
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
                    Sex = applicant.Sex,
                    Ethnicity = applicant.Ethnicity,
                    CitizenshipCountry = applicant.CitizenshipCountry,
                    AreaOfStudy = applicant.AreaOfStudy,
                    ApplicationStatus = applicant.ApplicationStatus,
                    DepartmentRecommendation = applicant.DepartmentRecommendation,

                    AcademicHistories = applicant.AcademicHistories.Select(ah => new AcademicHistory
                    {
                        Institution = ah.Institution,
                        Major = ah.Major,
                        Gpa = ah.GPA
                    }).ToList(),

                    ApplicantAdvisorLinks = applicant.FacultyAdvisors.Select(fa => new ApplicantAdvisorLink
                    {
                        FacultyAdvisor = new FacultyAdvisor
                        {
                            Name = fa.Name
                        }
                    }).ToList(),

                    ReviewerAssignments = applicant.Reviewers.Select(r => new ReviewerAssignment
                    {
                        Reviewer = new Reviewer
                        {
                            Name = r.Name
                        },
                        Recommendation = r.Recommendation
                    }).ToList()
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