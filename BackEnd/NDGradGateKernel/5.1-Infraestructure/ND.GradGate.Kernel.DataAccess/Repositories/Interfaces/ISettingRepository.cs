using ND.GradGate.Kernel.DataAccess.Persistent.Repositories.Interfaces;
using ND.GradGate.Kernel.Domain.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.DataAccess.Repositories.Interfaces
{
    public interface ISettingRepository : IRepository<Setting, int>
    {
        Task<List<Setting>> GetAllSettingsAsync();
        Task<Setting> GetSettingByKeyAsync(string settingKey);
    }
}
