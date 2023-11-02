using ND.GradGate.Kernel.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.Domain.ApplicantData
{
    public class AcademicHistory : AbstractEntity<AcademicHistory, int>
    {
        public int ApplicantRef { get; set; }
        public string Institution { get; set; }
        public string Major { get; set; }
        public decimal Gpa { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public virtual Applicant Applicant { get; set; }
    }

}
