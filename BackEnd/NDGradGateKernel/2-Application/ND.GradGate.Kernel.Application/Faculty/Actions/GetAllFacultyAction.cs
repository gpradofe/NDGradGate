using ND.GradGate.Kernel.Application.Faculty.Interfaces.Actions;
using ND.GradGate.Kernel.DataAccess.Repositories.Interfaces;
using ND.GradGate.Kernel.Domain.Contracts.Faculty;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.Application.Faculty.Actions
{
    public class GetAllFacultyAction : IGetAllFacultyAction
    {
        private readonly ILogger<GetAllFacultyAction> _logger;
        private readonly IFacultyRepository _facultyRepository;

        public GetAllFacultyAction(ILogger<GetAllFacultyAction> logger, IFacultyRepository facultyRepository)
        {
            _logger = logger;
            _facultyRepository = facultyRepository;
        }

        public async Task<List<FacultyDto>> GetAllFaculty()
        {
            try
            {
                _logger.LogInformation("Fetching all Faculty");

                var faculties = await _facultyRepository.GetAllFacultyAsync();
                var response = new List<FacultyDto>();

                foreach (var faculty in faculties)
                {
                    var facultyDto = new FacultyDto
                    {
                        Id = faculty.Id,
                        Name = faculty.Name,
                        Email = faculty.Email,
                        IsAdmin = faculty.IsAdmin.GetValueOrDefault(),
                        IsReviewer = faculty.IsReviewer.GetValueOrDefault(),
                        Field = faculty.Field,
                        PotentialAdvisors = faculty.PotentialAdvisors.Select(pa => new PotentialAdvisorDto
                        {
                            FacultyId = pa.FacultyId,
                            ApplicantId = pa.ApplicantId
                        }).ToList(),
                        ReviewerAssignments = faculty.ReviewerAssignments.Select(ra => new ReviewerAssignmentDto
                        {
                            FacultyId = ra.FacultyId,
                            ApplicantId = ra.ApplicantId,
                            CommentId = ra.CommentId,
                            Status = ra.Status
                        }).ToList(),
                        Comments = faculty.Comments.Select(c => new CommentDto
                        {
                            FacultyId = c.FacultyId,
                            ApplicantId = c.ApplicantId,
                            Content = c.Content,
                            Date = c.Date
                        }).ToList()
                    };

                    response.Add(facultyDto);
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
