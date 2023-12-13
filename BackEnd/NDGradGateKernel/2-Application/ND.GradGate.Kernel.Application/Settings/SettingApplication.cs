using Microsoft.Extensions.Logging;
using ND.GradGate.Kernel.Application.Settings.Interfaces;
using ND.GradGate.Kernel.Application.Settings.Interfaces.Actions;
using ND.GradGate.Kernel.Domain.Contracts.Settings;
using ND.GradGate.Kernel.Domain.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.Application.Settings
{
    public class SettingApplication : ISettingApplication
    {
        #region Attributes
        private readonly ILogger<SettingApplication> _logger;
        private readonly IGetAllSettingsAction _getAllSettingsAction;
        private readonly IGetSettingByKeyAction _getSettingByKeyAction;
        private readonly IAddOrUpdateSettingAction _addOrUpdateSettingAction;
        #endregion

        #region Constructors
        public SettingApplication(ILogger<SettingApplication> logger,
                                  IGetAllSettingsAction getAllSettingsAction,
                                  IGetSettingByKeyAction getSettingByKeyAction,
                                  IAddOrUpdateSettingAction addOrUpdateSettingAction)
        {
            _logger = logger;
            _getAllSettingsAction = getAllSettingsAction;
            _getSettingByKeyAction = getSettingByKeyAction;
            _addOrUpdateSettingAction = addOrUpdateSettingAction;
        }
        #endregion

        #region Methods
        public async Task<List<SettingDto>> GetAllSettingsAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all settings.");
                return await _getAllSettingsAction.GetAllSettingsAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<SettingDto> GetSettingByKeyAsync(string settingKey)
        {
            try
            {
                _logger.LogInformation($"Fetching setting by key: {settingKey}.");
                return await _getSettingByKeyAction.GetSettingByKeyAsync(settingKey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task AddOrUpdateSettingAsync(SettingDto setting)
        {
            try
            {
                _logger.LogInformation($"Adding or updating setting: {setting.SettingKey}.");
                await _addOrUpdateSettingAction.AddOrUpdateSettingAsync(setting);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        #endregion
    }
}
