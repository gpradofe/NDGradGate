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
    public class ReviewerMap : IEntityTypeConfiguration<Reviewer>
    {
        public void Configure(EntityTypeBuilder<Reviewer> builder)
        {
            builder.ToTable("reviewers");

            // Key
            builder.HasKey(r => r.Id).HasName("pk_reviewers");

            // Fields
            builder.Property(r => r.Id).HasColumnName("reviewer_id").IsRequired();
            builder.Property(r => r.Name).HasColumnName("name").IsRequired().HasMaxLength(255);
            builder.Property(r => r.Email).HasColumnName("email").IsRequired().HasMaxLength(255);

            // Set unique constraint for Email
            builder.HasIndex(r => r.Email).IsUnique();

            builder.Property(r => r.Specialization).HasColumnName("specialization").HasMaxLength(255);

            // Relationships
            builder.HasMany(r => r.ReviewerAssignments)
                   .WithOne(ra => ra.Reviewer)
                   .HasForeignKey(ra => ra.ReviewerId)
                   .HasConstraintName("fk_reviewers_reviewer_assignment");
        }
    }

}
