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
    public class ApplicantAdvisorLinkMap : IEntityTypeConfiguration<ApplicantAdvisorLink>
    {
        public void Configure(EntityTypeBuilder<ApplicantAdvisorLink> builder)
        {
            builder.ToTable("applicant_advisors_link");
            builder.Ignore(a => a.Id);
            // Key
            builder.HasKey(aal => new { aal.ApplicantRef, aal.AdvisorId }).HasName("pk_applicant_advisors_link");

            // Fields
            builder.Property(aal => aal.ApplicantRef).HasColumnName("applicant_ref").IsRequired();
            builder.Property(aal => aal.AdvisorId).HasColumnName("advisor_id").IsRequired();

            // Relationships
            builder.HasOne(aal => aal.Applicant)
                   .WithMany(a => a.ApplicantAdvisorLinks)
                   .HasForeignKey(aal => aal.ApplicantRef)
                   .HasConstraintName("fk_applicant_advisors_link_applicants");
        }
    }

}
