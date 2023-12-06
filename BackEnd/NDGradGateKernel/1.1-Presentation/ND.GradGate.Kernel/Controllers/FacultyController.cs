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
        private readonly IFacultyApplication _FacultyApplication;
        #endregion

        public FacultyController(ILogger<FacultyController> logger, IFacultyApplication FacultyApplication) : base(logger)
        {
            _FacultyApplication = FacultyApplication;
        }

        #region Methods
        [HttpGet("GetFacultyInfoById/{refId}")]

        public async Task<IActionResult> GetFacultyByIdAsync([FromRoute] int refId)
        {
            try
            {
                using (LogContext.PushProperty("RefID", refId))
                {
                    _logger.LogInformation($"Get Faculty info by RefID: {refId}.");

                    FacultyDto Faculty = await _FacultyApplication.GetFacultyByIdAsync(refId);
                    if (Faculty != null)
                    {
                        return Ok(Faculty);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on GetFacultyById");
                throw ex;
            }
        }

        [HttpGet("GetFacultysByName")]
        public async Task<IActionResult> GetFacultysByNameAsync([FromQuery] string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    return BadRequest("Name must be provided.");
                }

                using (LogContext.PushProperty("Name", name))
                {
                    _logger.LogInformation($"Search for Facultys with names matching:  {name}.");

                    List<FacultyDto> Facultys = await _FacultyApplication.GetFacultysByNameAsync( name);
                    if (Facultys != null && Facultys.Any())
                    {
                        return Ok(Facultys);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on GetFacultysByName");
                throw ex;
            }
        }


        #endregion
    }
}
