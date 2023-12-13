using Microsoft.Extensions.Logging;
using ND.GradGate.Kernel.Application.Faculty.Interfaces.Actions;
using ND.GradGate.Kernel.DataAccess.Repositories.Interfaces;
using ND.GradGate.Kernel.Domain.Contracts.Faculty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.Application.Faculty.Actions
{
    public class CreateOrUpdateFacultyAction : ICreateOrUpdateFacultyAction
    {
        private readonly ILogger<CreateOrUpdateFacultyAction> _logger;
        private readonly IFacultyRepository _facultyRepository;

        public CreateOrUpdateFacultyAction(ILogger<CreateOrUpdateFacultyAction> logger, IFacultyRepository facultyRepository)
        {
            _logger = logger;
            _facultyRepository = facultyRepository;
        }

        public async Task<List<FacultyDto>> CreateOrUpdateFacultyAsync(List<FacultyDto> facultyDtos)
        {
            try
            {
                var facultiesToSaveOrUpdate = new List<Domain.Faculty.Faculty>();

                foreach (var facultyDto in facultyDtos)
                {
                    var existingFaculty = await _facultyRepository.GetByFacultyIdAsync(facultyDto.Id);
                    var faculty = existingFaculty ?? new Domain.Faculty.Faculty();

                    faculty.Name = facultyDto.Name;
                    faculty.Email = facultyDto.Email;
                    faculty.IsReviewer = facultyDto.IsReviewer;
                    faculty.IsAdmin = facultyDto.IsAdmin;
                    faculty.Field = facultyDto.Field;

                    facultiesToSaveOrUpdate.Add(faculty);
                }

                var savedFaculties = await _facultyRepository.SaveOrUpdateMultipleAsync(facultiesToSaveOrUpdate);

                return savedFaculties.Select(MapDomainToDto).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in creating or updating faculty information");
                return null;
            }
        }

        private FacultyDto MapDomainToDto(Domain.Faculty.Faculty faculty)
        {
            return new FacultyDto
            {
                Id = faculty.Id,
                Name = faculty.Name,
                Email = faculty.Email,
                IsReviewer = faculty.IsReviewer.GetValueOrDefault(),
                Field = faculty.Field
            };
        }
    }


}
