using Microsoft.AspNetCore.Mvc;
using ND.GradGate.Kernel.Application.Settings.Interfaces;
using ND.GradGate.Kernel.Domain.Contracts.Settings;
using ND.GradGate.Kernel.Domain.Settings;
using ND.GradGate.Kernel.Service.Core.APIBase.StartupConfiguration.Base;

namespace ND.GradGate.Kernel.Controllers
{
    public class SettingController : ApiControllerBase<SettingController>
    {
        #region Attributes
        private readonly ISettingApplication _settingApplication;
        #endregion

        public SettingController(ILogger<SettingController> logger,
                                 ISettingApplication settingApplication)
                                 : base(logger)
        {
            _settingApplication = settingApplication;
        }

        #region Methods

        [HttpGet("GetAllSettings")]
        public async Task<IActionResult> GetAllSettingsAsync()
        {
            try
            {
                var settings = await _settingApplication.GetAllSettingsAsync();
                if (settings != null)
                {
                    return Ok(settings);
                }
                else
                {
                    return NoContent();
                }
            } 
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on GetAllSettings");
                throw;
            }
        }

        [HttpGet("GetSettingByKey/{settingKey}")]
        public async Task<IActionResult> GetSettingByKeyAsync(string settingKey)
        {
            try
            {
                var setting = await _settingApplication.GetSettingByKeyAsync(settingKey);
                if (setting != null)
                {
                    return Ok(setting);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on GetSettingByKey");
                throw;
            }
        }


        [HttpPost("AddOrUpdateSetting")]
        public async Task<IActionResult> AddOrUpdateSettingAsync([FromBody] SettingDto setting)
        {
            try
            {
                await _settingApplication.AddOrUpdateSettingAsync(setting);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on AddOrUpdateSetting");
                throw;
            }
        }

        #endregion
    }
}
