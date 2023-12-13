using Microsoft.AspNetCore.Mvc;
using ND.GradGate.Kernel.Application.Facultys.Interfaces;
using ND.GradGate.Kernel.Domain.Contracts.Faculty;
using ND.GradGate.Kernel.Service.Core.APIBase.StartupConfiguration.Base;
using Serilog.Context;

namespace ND.GradGate.Kernel.Controllers
{
    public class FacultyController : ApiControllerBase<FacultyController>
    {
        #region Attributes
        private readonly IFacultyApplication _facultyApplication;
        #endregion

        public FacultyController(ILogger<FacultyController> logger, IFacultyApplication FacultyApplication) : base(logger)
        {
            _facultyApplication = FacultyApplication;
        }

        #region Methods


        [HttpGet("GetAllFaculty")]
        public async Task<IActionResult> GetAllFaculty()
        {
            try
            {


                _logger.LogInformation($"Getting all Facutly.");

                List<FacultyDto> response = await _facultyApplication.GetAllFaculty();
                if (response != null && response.Any())
                {
                    return Ok(response);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on GetAllFacutly Controller Method");
                throw ex;
            }
        }

        [HttpGet("GetAssignedReviewer")]
        public async Task<IActionResult> GetAssignedApplicantsForReviewer(int reviewerId)
        {
            try
            {

                _logger.LogInformation($"Getting all Facutly.");

                List<int> response = await _facultyApplication.GetAsssinedApplicantionsByReviewerIdAsync(reviewerId);
                if (response != null && response.Any())
                {
                    return Ok(response);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on GetAllFacutly Controller Method");
                throw ex;
            }
        }
        [HttpPut("AssignPotentialAdvisorsAndAddComments")]
        public async Task<IActionResult> AssignPotentialAdvisorsAndAddComments([FromBody]  List<AssignFacultyAndAddComment> assignFacultyAndAddComments)
        {
            try
            {

                _logger.LogInformation($"Getting all Facutly.");

                bool response = await _facultyApplication.AssignPotentialAdvisorsAndAddCommentsAsync(assignFacultyAndAddComments);
                if ( response)
                {
                    return Ok(response);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on GetAllFacutly Controller Method");
                throw ex;
            }
        }
        [HttpPost("SaveOrUpdateFaculty")]
        public async Task<IActionResult> SaveOrUpdateFaculty([FromBody] List<FacultyDto> facultyDtos)
        {
            try
            {
                _logger.LogInformation("Processing save or update request for faculty.");

                List<FacultyDto> result = await _facultyApplication.SaveOrUpdateFacultyAsync(facultyDtos);
                if (result.Any())
                {
                    return Ok(result);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SaveOrUpdateFaculty endpoint");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        #endregion
    }
}
