using Microsoft.Extensions.Logging;
using ND.GradGate.Kernel.Application.Applicants.Interfaces.Actions;
using ND.GradGate.Kernel.DataAccess.Repositories.Interfaces;
using ND.GradGate.Kernel.Domain.Contracts.Applicant;
using ND.GradGate.Kernel.Domain.Contracts.Faculty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.Application.Applicants.Actions
{
    public class GetApplicantsInfoByNameAction : IGetApplicantsInfoByNameAction
    {
        #region Attributes
        private readonly ILogger<GetApplicantsInfoByNameAction> _logger;
        private readonly IApplicantRepository _applicantRepository;
        #endregion

        #region Constructor
        public GetApplicantsInfoByNameAction(ILogger<GetApplicantsInfoByNameAction> logger,
                                    IApplicantRepository applicantRepository)
        {
            _logger = logger;
            _applicantRepository = applicantRepository;
        }
        #endregion

        #region Methods
        public async Task<List<ApplicantDto>> GetApplicantInfoAsync(string firstName, string lastName)
        {
            try
            {
                _logger.LogInformation($"Fetching information for applicants with names matching: FirstName: {firstName}, LastName: {lastName}");

                var applicants = await _applicantRepository.GetByApplicantNameAsync(firstName, lastName);

                var response = applicants.Select(applicant => new ApplicantDto
                {
                    Id = applicant.Id,
                    LastName = applicant.LastName,
                    FirstName = applicant.FirstName,
                    Email = applicant.Email,
                    Sex = applicant.Sex,
                    Ethnicity = applicant.Ethnicity,
                    CitizenshipCountry = applicant.Country,
                    AreaOfStudy = applicant.Field,
                    Decision = applicant.Decision,
                    DepartmentRecommendation = applicant.Decision,
                    AcademicHistories = applicant.AcademicHistories.Select(ah => new AcademicHistoryDto
                    {
                        Institution = ah.Institution,
                        Major = ah.Major,
                        GPA = ah.Gpa
                    }).ToList(),
                    FacultyAdvisors = applicant.PotentialAdvisors.Select(aal => new FacultyAdvisorDto
                    {
                        Name = aal.Faculty.Name
                    }).ToList(),
                    Reviewers = applicant.ReviewerAssignments.Select(ra => new ReviewerDto
                    {
                        Recommendation = ra.Status
                    }).ToList()
                }).ToList();

                return response;
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
