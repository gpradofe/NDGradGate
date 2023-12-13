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
    public class PotentialAdvisorMap : IEntityTypeConfiguration<PotentialAdvisor>
    {
        public void Configure(EntityTypeBuilder<PotentialAdvisor> builder)
        {
            builder.ToTable("potential_advisors");

            // Primary Key
            builder.HasKey(pa => pa.Id).HasName("potential_advisors_pkey");

            // Fields
            builder.Property(pa => pa.Id).HasColumnName("id").IsRequired().ValueGeneratedOnAdd();
            builder.Property(pa => pa.FacultyId).HasColumnName("faculty_id").IsRequired();
            builder.Property(pa => pa.ApplicantId).HasColumnName("applicant_id").IsRequired();

            // Unique Constraint
            builder.HasIndex(pa => new { pa.FacultyId, pa.ApplicantId }).IsUnique();

            // Relationships
            builder.HasOne(pa => pa.Faculty)
                   .WithMany(f => f.PotentialAdvisors)
                   .HasForeignKey(pa => pa.FacultyId)
                   .HasConstraintName("potential_advisors_faculty_fk");

            builder.HasOne(pa => pa.Applicant)
                   .WithMany(a => a.PotentialAdvisors)
                   .HasForeignKey(pa => pa.ApplicantId)
                   .HasConstraintName("potential_advisors_applicant_fk");
        }
    }

}
