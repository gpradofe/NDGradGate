using ND.GradGate.Kernel.Domain.Contracts.Settings;
using ND.GradGate.Kernel.Domain.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.Application.Settings.Interfaces.Actions
{
    public interface IAddOrUpdateSettingAction
    {
        Task<SettingDto> AddOrUpdateSettingAsync(SettingDto settingDto);
    }
}
