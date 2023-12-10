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
    public class GetAllSettingsAction : IGetAllSettingsAction
    {
        private readonly ILogger<GetAllSettingsAction> _logger;
        private readonly ISettingRepository _settingRepository;

        public GetAllSettingsAction(ILogger<GetAllSettingsAction> logger, ISettingRepository settingRepository)
        {
            _logger = logger;
            _settingRepository = settingRepository;
        }

        public async Task<List<SettingDto>> GetAllSettingsAsync()
        {
            _logger.LogInformation("Fetching all settings.");
            var settings = await _settingRepository.GetAll();
            var settingDtos = new List<SettingDto>();

            foreach (var setting in settings)
            {
                settingDtos.Add(SettingDto.FromSetting(setting));
            }

            return settingDtos;
        }
    }
}
