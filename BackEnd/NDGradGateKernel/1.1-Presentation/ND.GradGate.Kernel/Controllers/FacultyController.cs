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


        #endregion
    }
}
