using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ND.GradGate.Kernel.Domain.Settings;
using Newtonsoft.Json;

namespace ND.GradGate.Kernel.Domain.Contracts.Settings
{
    public class SettingDto
    {
        public int Id { get; set; }
        public string SettingKey { get; set; }
        public List<string> Values { get; set; }

        public static SettingDto FromSetting(Setting setting)
        {
            return new SettingDto
            {
                Id = setting.Id,
                SettingKey = setting.SettingKey,
                Values = JsonConvert.DeserializeObject<List<string>>(setting.SettingValue)
            };
        }

        public Setting ToSetting()
        {
            return new Setting
            {
                Id = this.Id,
                SettingKey = this.SettingKey,
                SettingValue = JsonConvert.SerializeObject(this.Values)
            };
        }
    }
}
