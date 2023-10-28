using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.Domain.Contracts.Applicant
{
    public class AcademicHistoryDto
    {
        public string Institution { get; set; }
        public string Major { get; set; }
        public decimal GPA { get; set; }
    }

}
