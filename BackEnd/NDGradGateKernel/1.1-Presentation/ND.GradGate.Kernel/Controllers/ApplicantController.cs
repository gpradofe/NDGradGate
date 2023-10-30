using Microsoft.AspNetCore.Mvc;
using ND.GradGate.Kernel.Application.Applicants.Interfaces;
using ND.GradGate.Kernel.Domain.Contracts.Applicant;
using ND.GradGate.Kernel.Service.Core.APIBase.StartupConfiguration.Base;
using Serilog.Context;

namespace ND.GradGate.Kernel.Controllers
{
    public class ApplicantController : ApiControllerBase<ApplicantController>
    {
        #region Attributes
        private readonly IApplicantApplication _applicantApplication;
        #endregion

        public ApplicantController(ILogger<ApplicantController> logger, IApplicantApplication applicantApplication) : base(logger)
        {
            _applicantApplication = applicantApplication;
        }

        #region Methods
        [HttpGet("GetApplicantInfoById/{refId}")]

        public async Task<IActionResult> GetApplicantByIdAsync([FromRoute] int refId)
        {
            try
            {
                using (LogContext.PushProperty("RefID", refId))
                {
                    _logger.LogInformation($"Get applicant info by RefID: {refId}.");

                    ApplicantDto applicant = await _applicantApplication.GetApplicantByIdAsync(refId);
                    if (applicant != null)
                    {
                        return Ok(applicant);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on GetApplicantById");
                throw ex;
            }
        }

        [HttpGet("GetApplicantsByName")]
        public async Task<IActionResult> GetApplicantsByNameAsync([FromQuery] string? firstName, [FromQuery] string? lastName)
        {
            try
            {
                if (string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName))
                {
                    return BadRequest("Either firstName or lastName must be provided.");
                }

                using (LogContext.PushProperty("FirstName", firstName))
                using (LogContext.PushProperty("LastName", lastName))
                {
                    _logger.LogInformation($"Search for applicants with names matching: FirstName: {firstName}, LastName: {lastName}.");

                    List<ApplicantDto> applicants = await _applicantApplication.GetApplicantsByNameAsync(firstName, lastName);
                    if (applicants != null && applicants.Any())
                    {
                        return Ok(applicants);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on GetApplicantsByName");
                throw ex;
            }
        }

        [HttpPut("UpdateApplicant/{id}")]
        public async Task<IActionResult> UpdateApplicant(int id, [FromBody] ApplicantDto updatedApplicant)
        {
            var applicant = await _applicantApplication.GetApplicantByIdAsync(id);
            if (applicant == null)
            {
                return NotFound();
            }

            applicant.LastName = updatedApplicant.LastName;
            applicant.FirstName = updatedApplicant.FirstName;
            applicant.Email = updatedApplicant.Email;
            // Update other properties as needed

            await _applicantApplication.UpdateApplicantInfoAsync(id, applicant);

            return NoContent();
        }

        [HttpPost("CreateApplicant")]
        public async Task<ActionResult<ApplicantDto>> CreateApplicant(ApplicantDto applicant)
        {
            await _applicantApplication.CreateApplicantInfoAsync(applicant);

            return CreatedAtAction(nameof(GetApplicantByIdAsync), new { id = applicant.Ref }, applicant);
        }

        [HttpDelete("DeleteApplicant/{id}")]
        public async Task<IActionResult> DeleteApplicant(int id)
        {
            var applicant = await _applicantApplication.GetApplicantByIdAsync(id);

            if (applicant == null)
            {
                return NotFound();
            }

            await _applicantApplication.DeleteApplicantInfoAsync(id);

            return NoContent();
        }


        #endregion
    }
}