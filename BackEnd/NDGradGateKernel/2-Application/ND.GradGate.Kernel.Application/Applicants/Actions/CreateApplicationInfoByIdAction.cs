using Microsoft.Extensions.Logging;
using ND.GradGate.Kernel.Application.Applicants.Interfaces.Actions;
using ND.GradGate.Kernel.DataAccess.Repositories.Interfaces;
using ND.GradGate.Kernel.Domain.Contracts.Applicant;
using ND.GradGate.Kernel.Domain.ApplicantData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.Application.Applicants.Actions
{
    public class CreateApplicantInfoAction : ICreateApplicantInfoAction
    {
        private readonly ILogger<CreateApplicantInfoAction> _logger;
        private readonly IApplicantRepository _applicantRepository;

        public CreateApplicantInfoAction(ILogger<CreateApplicantInfoAction> logger, IApplicantRepository applicantRepository)
        {
            _logger = logger;
            _applicantRepository = applicantRepository;
        }

        public async Task<bool> CreateApplicantInfoAsync(ApplicantDto applicantDto)
        {
            try
            {
                _logger.LogInformation($"Creating information for applicant with RefID {applicantDto.Ref}");

                var applicantToCreate = MapDtoToDomain(applicantDto);

                var result = await _applicantRepository.SaveAsync(applicantToCreate);

                return result != null; // Returns true if the operation was successful
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating applicant information");
                return false; // Returns false as the operation failed
            }
        }

        private Applicant MapDtoToDomain(ApplicantDto dto)
        {
            var applicant = new Applicant
            {
                Id = dto.Ref,
                LastName = dto.LastName,
                FirstName = dto.FirstName,
                Email = dto.Email,
                Sex = dto.Sex,
                Ethnicity = dto.Ethnicity,
                Country = dto.CitizenshipCountry,
                Field = dto.AreaOfStudy,
                Decision = dto.ApplicationStatus,

                AcademicHistories = dto.AcademicHistories?.Select(historyDto => new AcademicHistory
                {
                    ApplicantRef = dto.Ref, // Assuming the Ref is set before this method is called
                    Institution = historyDto.Institution,
                    Major = historyDto.Major,
                    Gpa = historyDto.GPA
                }).ToList() ?? new List<AcademicHistory>()
            };

            return applicant;
        }
    }
}
