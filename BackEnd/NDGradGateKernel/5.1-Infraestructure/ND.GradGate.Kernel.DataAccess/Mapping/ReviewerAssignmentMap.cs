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
            builder.Ignore(r => r.Id);
            // Key
            builder.HasKey(ra => new { ra.ReviewerId, ra.ApplicantRef }).HasName("pk_reviewer_assignment");

            // Fields
            builder.Property(ra => ra.ReviewerId).HasColumnName("reviewer_id").IsRequired();
            builder.Property(ra => ra.AdminId).HasColumnName("admin_id").IsRequired();
            builder.Property(ra => ra.ApplicantRef).HasColumnName("applicant_ref").IsRequired();
            builder.Property(ra => ra.ReviewDate).HasColumnName("review_date");
            builder.Property(ra => ra.Comments).HasColumnName("comments").HasColumnType("text");
            builder.Property(ra => ra.Recommendation).HasColumnName("recommendation");

            // Relationships
            builder.HasOne(ra => ra.Applicant)
                   .WithMany(a => a.ReviewerAssignments)
                   .HasForeignKey(ra => ra.ApplicantRef)
                   .HasConstraintName("fk_reviewer_assignment_applicants");

            builder.HasOne(ra => ra.Reviewer)
                   .WithMany(r => r.ReviewerAssignments)
                   .HasForeignKey(ra => ra.ReviewerId)
                   .HasConstraintName("fk_reviewer_assignment_reviewers");
        }
    }

}
