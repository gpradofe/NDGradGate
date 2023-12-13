using ND.GradGate.Kernel.Domain.Contracts.Settings;
using ND.GradGate.Kernel.Domain.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.Application.Settings.Interfaces
{
    public interface ISettingApplication
    {
        Task<List<SettingDto>> GetAllSettingsAsync();
        Task<SettingDto> GetSettingByKeyAsync(string settingKey);
        Task AddOrUpdateSettingAsync(SettingDto setting);
    }
}
