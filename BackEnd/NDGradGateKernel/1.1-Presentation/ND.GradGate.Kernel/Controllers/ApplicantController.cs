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
        [HttpGet("GetAllApplicants")]

        public async Task<IActionResult> GetAllApplicants()
        {
            try
            {
                
                    _logger.LogInformation($"Getting all applicants");

                    List<ApplicantDto> applicants = await _applicantApplication.GetAllApplicantsAsync();
                    if (applicants != null)
                    {
                        return Ok(applicants);
                    }
                    else
                    {
                        return NoContent();
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

        [HttpPut("UpdateApplicant/{refId}")]
        public async Task<IActionResult> UpdateApplicant(int refId, [FromBody] ApplicantDto updatedApplicant)
        {
            try
            {
                var existingApplicant = await _applicantApplication.GetApplicantByIdAsync(refId);
                if (existingApplicant == null)
                {
                    return NotFound($"Applicant with Ref ID {refId} not found.");
                }

                var updateResult = await _applicantApplication.UpdateApplicantInfoAsync(refId, updatedApplicant);
                if (updateResult == null)
                {
                    // Log the error or handle the case where the update is not successful
                    _logger.LogError($"Failed to update applicant with Ref ID {refId}.");
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred while updating the applicant.");
                }

                // Optionally, fetch and return the updated applicant
                var updatedEntity = await _applicantApplication.GetApplicantByIdAsync(refId);
                return Ok(updatedEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating applicant with Ref ID {refId}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }


        /// <summary>
        /// Creates a new applicant.
        /// </summary>
        /// <param name="applicant">The applicant data.</param>
        /// <returns>Returns the created applicant with the corresponding route.</returns>
        /// <response code="201">Returns the newly created applicant.</response>
        /// <response code="400">If the applicant data is not valid.</response>
        /// <response code="500">If there was an internal server error.</response>
        [HttpPost("CreateApplicant")]
        [ProducesResponseType(typeof(ApplicantDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApplicantDto>> CreateApplicant([FromBody] ApplicantDto applicant)
        {
            if (applicant == null)
            {
                return BadRequest("Applicant data must not be null.");
            }

            var result = await _applicantApplication.CreateApplicantInfoAsync(applicant);

            if (result)
            {
                return CreatedAtAction(nameof(_applicantApplication.CreateApplicantInfoAsync), new { id = applicant.Ref }, applicant);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the applicant.");
            }
        }

        [HttpDelete("DeleteApplicant/{refId}")]
        public async Task<IActionResult> DeleteApplicant(int refId)
        {
            try
            {
                var applicant = await _applicantApplication.GetApplicantByIdAsync(refId);

                if (applicant == null)
                {
                    return NotFound($"Applicant with Ref ID {refId} not found.");
                }

                var deletionResult = await _applicantApplication.DeleteApplicantInfoAsync(refId);
                if (!deletionResult)
                {
                    // Log the error or handle the case where the deletion is not successful
                    _logger.LogError($"Failed to delete applicant with Ref ID {refId}.");
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred while deleting the applicant.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting applicant with Ref ID {refId}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }


        #endregion
    }
}
