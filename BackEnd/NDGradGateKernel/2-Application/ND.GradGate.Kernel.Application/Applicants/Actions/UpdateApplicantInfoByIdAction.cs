using Microsoft.Extensions.Logging;
using ND.GradGate.Kernel.Application.Applicants.Interfaces.Actions;
using ND.GradGate.Kernel.DataAccess.Repositories.Interfaces;
using ND.GradGate.Kernel.Domain.ApplicantData;
using ND.GradGate.Kernel.Domain.Contracts.Applicant;
using System.Reflection;
using System.Runtime.Intrinsics.Arm;

namespace ND.GradGate.Kernel.Application.Applicants.Actions
{
    public class UpdateApplicantInfoByIdAction : IUpdateApplicantInfoByIdAction
    {
        private readonly ILogger<UpdateApplicantInfoByIdAction> _logger;
        private readonly IApplicantRepository _applicantRepository;

        public UpdateApplicantInfoByIdAction(ILogger<UpdateApplicantInfoByIdAction> logger,
                                             IApplicantRepository applicantRepository)
        {
            _logger = logger;
            _applicantRepository = applicantRepository;
        }

        public async Task<ApplicantDto> UpdateApplicantInfoAsync(int refId, ApplicantDto applicantDto)
        {
            try
            {
                _logger.LogInformation($"Updating information for applicant with RefID {refId}");

                var applicantToUpdate = await _applicantRepository.GetByApplicantIdAsync(refId);
                if (applicantToUpdate == null)
                {
                    throw new KeyNotFoundException($"Applicant with RefID {refId} not found.");
                }

                //ApplicantMappingHelper.MapDtoToDomain(applicantDto, applicantToUpdate);
                applicantToUpdate.Email = applicantDto.Email;
                applicantToUpdate.FirstName = applicantDto.FirstName;

                var updatedApplicant = await _applicantRepository.UpdateAsync(applicantToUpdate);


                return applicantDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating applicant information");
                throw;
            }
        }
    }
    public class ApplicantMappingHelper
    {
        public static void MapDtoToDomain(ApplicantDto dto, Applicant domain)
        {
            // Reflective mapping for primitive and string types
            foreach (PropertyInfo dtoProp in dto.GetType().GetProperties())
            {
                if (dtoProp.PropertyType.IsPrimitive || dtoProp.PropertyType == typeof(string))
                {
                    PropertyInfo domainProp = domain.GetType().GetProperty(dtoProp.Name);
                    if (domainProp != null && domainProp.CanWrite)
                    {
                        var value = dtoProp.GetValue(dto);
                        if (value != null) 
                        {
                            domainProp.SetValue(domain, value);
                        }
                    }
                }
            }

            MapAcademicHistories(dto, domain);
            MapFacultyAdvisors(dto, domain);
            MapReviewers(dto, domain);
        }

        private static void MapAcademicHistories(ApplicantDto dto, Applicant domain)
        {
            // Example implementation. Adapt as necessary
            foreach (var dtoHistory in dto.AcademicHistories)
            {
                var domainHistory = domain.AcademicHistories.FirstOrDefault(h => h.ApplicantRef == domain.Ref);


                if (domainHistory != null)
                {
                    domainHistory.Gpa = dtoHistory.GPA;
                }
                else
                {
                    continue;
                }
            }
        }

        private static void MapFacultyAdvisors(ApplicantDto dto, Applicant domain)
        {
            if (dto.FacultyAdvisors != null)
            {
            }
        }

        private static void MapReviewers(ApplicantDto dto, Applicant domain)
        {
            if (dto.Reviewers != null)
            {
            }
        }


    }
}

