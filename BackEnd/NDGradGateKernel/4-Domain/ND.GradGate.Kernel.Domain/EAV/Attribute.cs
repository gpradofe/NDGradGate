using ND.GradGate.Kernel.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.Domain.EAV
{
    public class Attribute : AbstractEntity<Attribute, int>
    {
        public int EntityId { get; set; }
        public string AttributeName { get; set; }
        public string AttributeType { get; set; }

        public virtual DynamicEntity DynamicEntity { get; set; }
        public virtual ICollection<ApplicationDataValue> ApplicationDataValues { get; set; }
    }
}
