using Microsoft.Extensions.Logging;
using ND.GradGate.Kernel.Application.Settings.Interfaces.Actions;
using ND.GradGate.Kernel.DataAccess.Repositories.Interfaces;
using ND.GradGate.Kernel.Domain.Contracts.Settings;
using ND.GradGate.Kernel.Domain.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.Application.Settings.Actions
{
    public class GetSettingByKeyAction : IGetSettingByKeyAction
    {
        private readonly ILogger<GetSettingByKeyAction> _logger;
        private readonly ISettingRepository _settingRepository;

        public GetSettingByKeyAction(ILogger<GetSettingByKeyAction> logger, ISettingRepository settingRepository)
        {
            _logger = logger;
            _settingRepository = settingRepository;
        }

        public async Task<SettingDto> GetSettingByKeyAsync(string settingKey)
        {
            try
            {
                _logger.LogInformation($"Fetching setting by key: {settingKey}.");
                var setting = await _settingRepository.GetSettingByKeyAsync(settingKey);

                if (setting != null)
                {
                    return SettingDto.FromSetting(setting);
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching setting by key: {settingKey}");
                throw;
            }
        }
    }
}
