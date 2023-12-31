﻿using ND.GradGate.Kernel.Domain.ApplicantData;
using ND.GradGate.Kernel.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ND.GradGate.Kernel.Domain.Faculty
{
    public class ReviewerAssignment
    {
        public int FacultyId { get; set; }
        public int ApplicantId { get; set; }
        public int? CommentId { get; set; } // Optional reference to a comment
        public string Status { get; set; }

        // Relationships
        public virtual Faculty Faculty { get; set; }
        public virtual Applicant Applicant { get; set; }
    }

}
