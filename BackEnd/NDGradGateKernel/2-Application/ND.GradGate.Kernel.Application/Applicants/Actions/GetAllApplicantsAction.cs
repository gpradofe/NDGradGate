using Microsoft.Extensions.Logging;
using ND.GradGate.Kernel.Application.Applicants.Interfaces.Actions;
using ND.GradGate.Kernel.DataAccess.Repositories.Interfaces;
using ND.GradGate.Kernel.Domain.ApplicantData;
using ND.GradGate.Kernel.Domain.Contracts.Applicant;
using ND.GradGate.Kernel.Domain.Contracts.Faculty;
using ND.GradGate.Kernel.Domain.EAV;
using ND.GradGate.Kernel.Domain.Faculty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.Application.Applicants.Actions
{
    public class GetAllApplicantsAction : IGetAllApplicantsAction
    {
        private readonly ILogger<GetAllApplicantsAction> _logger;
        private readonly IApplicantRepository _applicantRepository;

        public GetAllApplicantsAction(ILogger<GetAllApplicantsAction> logger, IApplicantRepository applicantRepository)
        {
            _logger = logger;
            _applicantRepository = applicantRepository;
        }

        public async Task<List<ApplicantDto>> GetAllApplicantsAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all applicants from Database");

                var applicants = await _applicantRepository.GetAllApplicants();
                var response = applicants.Select(MapToApplicantDto).ToList();

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        private ApplicantDto MapToApplicantDto(Applicant applicant)
        {
            return new ApplicantDto
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
                AcademicHistories = MapToAcademicHistoryDtoList(applicant.AcademicHistories),
                FacultyAdvisors = MapToFacultyAdvisorDtoList(applicant.PotentialAdvisors),
                Reviewers = MapToReviewerDtoList(applicant.ReviewerAssignments),
                Attributes = MapToApplicantAttributeDtoList(applicant.ApplicantAttributes),
                Comments = MapToCommentDtoList(applicant.Comments)
            };
        }

        private List<AcademicHistoryDto> MapToAcademicHistoryDtoList(ICollection<AcademicHistory> academicHistories)
        {
            return academicHistories?.Select(ah => new AcademicHistoryDto
            {
                Institution = ah.Institution,
                Major = ah.Major,
                GPA = ah.Gpa
            }).ToList() ?? new List<AcademicHistoryDto>();
        }

        private List<FacultyAdvisorDto> MapToFacultyAdvisorDtoList(ICollection<PotentialAdvisor> potentialAdvisors)
        {
            return potentialAdvisors?.Select(pa => new FacultyAdvisorDto
            {
                Name = pa.Faculty?.Name
            }).Where(fa => fa.Name != null).ToList() ?? new List<FacultyAdvisorDto>();
        }

        private List<ReviewerDto> MapToReviewerDtoList(ICollection<ReviewerAssignment> reviewerAssignments)
        {
            return reviewerAssignments?.Select(ra => new ReviewerDto
            {
                Name = ra.Faculty?.Name,
                Recommendation = ra.Status
            }).Where(r => r.Name != null).ToList() ?? new List<ReviewerDto>();
        }

        private List<ApplicantAttributeDto> MapToApplicantAttributeDtoList(ICollection<ApplicantAttribute> applicantAttributes)
        {
            // Assuming ApplicantAttributeDto exists and has properties for Attribute and Value
            return applicantAttributes?.Select(aa => new ApplicantAttributeDto
            {
                Attribute = aa.Attribute,
                Value = aa.Value
            }).ToList() ?? new List<ApplicantAttributeDto>();
        }

        private List<CommentDto> MapToCommentDtoList(ICollection<Comment> comments)
        {
            // Assuming CommentDto exists and has properties for Content and Date
            return comments?.Select(c => new CommentDto
            {
                Content = c.Content,
                Date = c.Date
            }).ToList() ?? new List<CommentDto>();
        }
    }
}
