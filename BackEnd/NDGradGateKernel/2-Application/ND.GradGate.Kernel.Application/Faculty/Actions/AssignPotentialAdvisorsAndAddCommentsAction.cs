using Microsoft.Extensions.Logging;
using ND.GradGate.Kernel.Application.Faculty.Interfaces.Actions;
using ND.GradGate.Kernel.DataAccess.Repositories.Interfaces;
using ND.GradGate.Kernel.Domain.Contracts.Faculty;
using ND.GradGate.Kernel.Domain.Faculty;

namespace ND.GradGate.Kernel.Application.Faculty.Actions
{
    public class AssignPotentialAdvisorsAndAddCommentsAction : IAssignPotentialAdvisorsAndAddCommentsAction
    {
        private readonly ILogger<AssignPotentialAdvisorsAndAddCommentsAction> _logger;
        private readonly IFacultyRepository _facultyRepository;

        public AssignPotentialAdvisorsAndAddCommentsAction(ILogger<AssignPotentialAdvisorsAndAddCommentsAction> logger,
                                             IFacultyRepository facultyRepository)
        {
            _logger = logger;
            _facultyRepository = facultyRepository;
        }

        public async Task<bool> AssignPotentialAdvisorsAndAddCommentsAsync(List<AssignFacultyAndAddComment> assignFacultyAndAddComments)
        {
            try
            {
                _logger.LogInformation($"Updating information for faculty and comments {assignFacultyAndAddComments}");

                // Get all unique faculty IDs (both sender and assigned)
                var allFacultyIds = assignFacultyAndAddComments.Select(f => f.SenderId).ToList();
                allFacultyIds.AddRange(assignFacultyAndAddComments.SelectMany(f => f.PotentialAdvisorId));
                allFacultyIds = allFacultyIds.Distinct().ToList();

                List<Domain.Faculty.Faculty> facultiesToUpdate = await _facultyRepository.GetByFacultyIdAsync(allFacultyIds);
                if (!facultiesToUpdate.Any())
                {
                    throw new KeyNotFoundException($"Faculties not found");
                }

                // Process each assignment
                foreach (var assignment in assignFacultyAndAddComments)
                {
                    // Update comments for the sender
                    var senderFaculty = facultiesToUpdate.FirstOrDefault(f => f.Id == assignment.SenderId);
                    if (senderFaculty != null)
                    {
                        if (!string.IsNullOrEmpty(assignment.Comment))
                        {
                            var currComment = senderFaculty.Comments.Where(c => c.ApplicantId == assignment.ApplicantID && c.FacultyId == assignment.SenderId).FirstOrDefault();

                            if (currComment != null)
                            {
                                
                            }
                            else
                            {
                                currComment = new Comment
                                {
                                    ApplicantId = assignment.ApplicantID,
                                    FacultyId = assignment.SenderId,
                                    Content = assignment.Comment,
                                    Date = DateTime.UtcNow,
                                };
                                senderFaculty.Comments.Add(currComment);
                            }
                        }
                    }

                    // Assign potential advisors
                    if (assignment.PotentialAdvisorId != null)
                    {
                        foreach (int assignedFacultyId in assignment.PotentialAdvisorId)
                        {
                            var assignedFaculty = facultiesToUpdate.FirstOrDefault(f => f.Id == assignedFacultyId);
                            if (assignedFaculty != null && !assignedFaculty.PotentialAdvisors.Any(pa => pa.ApplicantId == assignment.ApplicantID && pa.FacultyId == assignedFacultyId))
                            {
                                var potentialAdvisor = new PotentialAdvisor
                                {
                                    ApplicantId = assignment.ApplicantID,
                                    FacultyId = assignedFacultyId,
                                };
                                assignedFaculty.PotentialAdvisors.Add(potentialAdvisor);
                            }
                        }
                    }
                }

                await _facultyRepository.UpdateFacultyAndRelatedEntitiesAsync(facultiesToUpdate);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating faculty information");
                throw;
            }
        }
    }

}
