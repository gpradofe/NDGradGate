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
    public class FacultyAdvisorMap : IEntityTypeConfiguration<FacultyAdvisor>
    {
        public void Configure(EntityTypeBuilder<FacultyAdvisor> builder)
        {
            builder.ToTable("faculty_advisors");

            // Key
            builder.HasKey(fa => fa.Id).HasName("pk_faculty_advisors");
            builder.Property(fa => fa.Id).HasColumnName("advisor_id").ValueGeneratedOnAdd();

            // Fields
            builder.Property(fa => fa.Name).HasColumnName("name").IsRequired();

            // Relationships
            builder.HasMany(fa => fa.ApplicantAdvisorLinks)
                   .WithOne(aal => aal.FacultyAdvisor)
                   .HasForeignKey(aal => aal.AdvisorId)
                   .HasConstraintName("fk_applicant_advisors_link_faculty_advisors");
        }
    }

}
