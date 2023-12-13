using Microsoft.Extensions.Logging;
using ND.GradGate.Kernel.Application.Faculty.Interfaces.Actions;
using ND.GradGate.Kernel.DataAccess.Repositories.Interfaces;
using ND.GradGate.Kernel.Domain.ApplicantData;
using ND.GradGate.Kernel.Domain.Contracts.Faculty;

namespace ND.GradGate.Kernel.Application.Faculty.Actions
{
    public class GetAssignedApplicationsForReviewerAction : IGetAssignedApplicationsForReviewerAction
    {
        private readonly ILogger<GetAssignedApplicationsForReviewerAction> _logger;
        private readonly IFacultyRepository _facultyRepository;
        private readonly IApplicantRepository _applicantRepository;


        public GetAssignedApplicationsForReviewerAction(ILogger<GetAssignedApplicationsForReviewerAction> logger, 
                                                        IFacultyRepository facultyRepository,
                                                        IApplicantRepository applicantRepository)
        {
            _logger = logger;
            _facultyRepository = facultyRepository;
            _applicantRepository = applicantRepository;
        }

        public async Task<List<int>> GetAsssinedApplicantionsByReviewerIdAsync(int reviewerId)
        {
            try
            {
                _logger.LogInformation("Fetching all Faculty");
                List<Applicant> result = await _applicantRepository.GetApplicantByReviewerId(reviewerId);

                List<int> assignedApplicantsId = result.Select(a => a.Id).ToList() ?? new List<int>();

                return assignedApplicantsId;
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
