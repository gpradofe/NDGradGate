using Microsoft.Extensions.Logging;
using ND.GradGate.Kernel.Application.Settings.Interfaces.Actions;
using ND.GradGate.Kernel.DataAccess.Repositories.Interfaces;
using ND.GradGate.Kernel.Domain.Contracts.Settings;
using ND.GradGate.Kernel.Domain.Settings;

namespace ND.GradGate.Kernel.Application.Settings.Actions
{
    public class AddOrUpdateSettingAction : IAddOrUpdateSettingAction
    {
        private readonly ILogger<AddOrUpdateSettingAction> _logger;
        private readonly ISettingRepository _settingRepository;

        public AddOrUpdateSettingAction(ILogger<AddOrUpdateSettingAction> logger, ISettingRepository settingRepository)
        {
            _logger = logger;
            _settingRepository = settingRepository;
        }

        public async Task<SettingDto> AddOrUpdateSettingAsync(SettingDto settingDto)
        {
            try
            {
                _logger.LogInformation($"Adding or updating setting: {settingDto.SettingKey}.");

                var setting = settingDto.ToSetting();
                var savedSetting = await _settingRepository.SaveOrUpdateAsync(setting, setting.Id);

                _logger.LogInformation($"Successfully added or updated setting: {savedSetting.SettingKey}");

                return SettingDto.FromSetting(savedSetting);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in AddOrUpdateSettingAction: {ex.Message}");
                throw;
            }
        }
    }
}

