using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.Domain.Contracts.Applicant
{
    public class UpdateApplicantStatusAndReviewerDto
    {
        public int Ref { get; set; }
        public List<int>? FacultyId { get; set; }
        public string? Status { get; set; }
    }
}
