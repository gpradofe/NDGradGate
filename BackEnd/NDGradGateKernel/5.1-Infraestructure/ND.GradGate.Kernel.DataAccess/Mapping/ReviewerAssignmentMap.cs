using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ND.GradGate.Kernel.Domain.Faculty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.DataAccess.Mapping
{
    public class ReviewerAssignmentMap : IEntityTypeConfiguration<ReviewerAssignment>
    {
        public void Configure(EntityTypeBuilder<ReviewerAssignment> builder)
        {
            builder.ToTable("reviewer_assignment");

            // Key
            builder.HasKey(ra => new { ra.FacultyId, ra.ApplicantId }).HasName("pk_reviewer_assignment");
            // Fields
            builder.Property(ra => ra.FacultyId).HasColumnName("faculty_id").IsRequired();
            builder.Property(ra => ra.ApplicantId).HasColumnName("applicant_id").IsRequired();
            builder.Property(ra => ra.CommentId).HasColumnName("comment_id"); // Optional
            builder.Property(ra => ra.Status).HasColumnName("status");

            builder.HasOne(ra => ra.Faculty)
                   .WithMany(f => f.ReviewerAssignments)
                   .HasForeignKey(ra => ra.FacultyId);

            builder.HasOne(ra => ra.Applicant)
                   .WithMany(a => a.ReviewerAssignments)
                   .HasForeignKey(ra => ra.ApplicantId);
        }
    }


}
