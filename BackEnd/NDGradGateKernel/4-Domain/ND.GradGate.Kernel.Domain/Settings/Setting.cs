using ND.GradGate.Kernel.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.Domain.Settings
{
    public class Setting : AbstractEntity<Setting, int>
    {
        public string SettingKey { get; set; }
        public string SettingValue { get; set; }
    }
}
