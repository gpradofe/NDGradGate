﻿using Microsoft.Extensions.Logging;
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
    public class GetAllApplicantsAction : IGetAllApplicantsAction
    {
        #region Attributes
        private readonly ILogger<GetAllApplicantsAction> _logger;
        private readonly IApplicantRepository _applicantRepository;
        #endregion

        #region Constructor
        public GetAllApplicantsAction(ILogger<GetAllApplicantsAction> logger,
                                    IApplicantRepository applicantRepository)
        {
            _logger = logger;
            _applicantRepository = applicantRepository;
        }
        #endregion

        #region Methods
        public async Task<List<ApplicantDto>> GetAllApplicantsAsync()
        {
            try
            {
                _logger.LogInformation($"Fetching all applicants from Database");

                var applicants = await _applicantRepository.GetAllApplicants();

                var response = applicants.Select(applicant => new ApplicantDto
                {
                    Ref = applicant.Id,
                    LastName = applicant.LastName,
                    FirstName = applicant.FirstName,
                    Email = applicant.Email,
                    Sex = applicant.Sex,
                    Ethnicity = applicant.Ethnicity,
                    CitizenshipCountry = applicant.Country,
                    AreaOfStudy = applicant.Field,
                    ApplicationStatus = applicant.Decision,
                    AcademicHistories = applicant.AcademicHistories.Select(ah => new AcademicHistoryDto
                    {
                        Institution = ah.Institution,
                        Major = ah.Major,
                        GPA = ah.Gpa
                    }).ToList(),
                    FacultyAdvisors = applicant.PotentialAdvisors.Select(aal => new FacultyAdvisorDto
                    {
                        Name = aal.Faculty.Name,
                    }).ToList(),
                    Reviewers = applicant.ReviewerAssignments.Select(ra => new ReviewerDto
                    {
                        Name = ra.Faculty.Name,
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
