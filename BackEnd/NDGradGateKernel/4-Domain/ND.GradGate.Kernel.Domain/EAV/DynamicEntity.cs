using ND.GradGate.Kernel.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.Domain.EAV
{
    public class DynamicEntity : AbstractEntity<DynamicEntity, int>
    {
        public string EntityName { get; set; }

        public virtual ICollection<Attribute> Attributes { get; set; }
    }
}
