using Microsoft.Extensions.Logging;
using ND.GradGate.Kernel.Application.Applicants.Interfaces.Actions;
using ND.GradGate.Kernel.DataAccess.Repositories.Interfaces;
using ND.GradGate.Kernel.Domain.Contracts.Applicant;
using ND.GradGate.Kernel.Domain.ApplicantData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ND.GradGate.Kernel.Domain.EAV;

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

        public async Task<List<ApplicantDto>> CreateApplicantInfoAsync(List<ApplicantDto> applicantDtos)
        {
            try
            {
                var applicantsToCreate = applicantDtos.Select(MapDtoToDomain).ToList();

                var savedApplicants = await _applicantRepository.SaveMultipleAsync(applicantsToCreate);

                if (savedApplicants != null)
                {
                    return savedApplicants.Select(MapDomainToDto).ToList();
                }
                else
                {
                    return null; 
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating applicant information");
                return null;
            }
        }

        private ApplicantDto MapDomainToDto(Applicant applicant)
        {
            return new ApplicantDto
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
                Status = applicant.Status,
                AcademicHistories = applicant.AcademicHistories.Select(ah => new AcademicHistoryDto
                {
                    Institution = ah.Institution,
                    Major = ah.Major,
                    GPA = ah.Gpa
                }).ToList(),
                Attributes = applicant.ApplicantAttributes.Select(aa => new ApplicantAttributeDto
                {
                    Attribute = aa.Attribute,
                    Value = aa.Value
                }).ToList()
            };
        }

        private Applicant MapDtoToDomain(ApplicantDto dto)
        {
            var applicant = new Applicant
            {
                LastName = dto.LastName,
                FirstName = dto.FirstName,
                Email = dto.Email,
                Sex = dto.Sex,
                Ethnicity = dto.Ethnicity,
                Country = dto.CitizenshipCountry,
                Field = dto.AreaOfStudy,
                Decision = dto.Decision,

                AcademicHistories = dto.AcademicHistories?.Select(historyDto => new AcademicHistory
                {
                    Institution = historyDto.Institution,
                    Major = historyDto.Major,
                    Gpa = historyDto.GPA
                }).ToList() ?? new List<AcademicHistory>(),

                ApplicantAttributes = dto.Attributes?.Select(attrDto => new ApplicantAttribute
                {
                    Attribute = attrDto.Attribute,
                    Value = attrDto.Value
                }).ToList() ?? new List<ApplicantAttribute>(),
            };

            return applicant;
        }
    }
}
