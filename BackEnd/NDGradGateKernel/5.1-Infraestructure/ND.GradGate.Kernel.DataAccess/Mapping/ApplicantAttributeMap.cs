using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ND.GradGate.Kernel.Domain.EAV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.GradGate.Kernel.DataAccess.Mapping
{
    public class ApplicantAttributeMap : IEntityTypeConfiguration<ApplicantAttribute>
    {
        public void Configure(EntityTypeBuilder<ApplicantAttribute> builder)
        {
            builder.ToTable("applicant_attributes");

            // Key
            builder.HasKey(aa => aa.Id).HasName("pk_applicant_attributes");
            builder.Property(aa => aa.Id).HasColumnName("attribute_id").ValueGeneratedOnAdd();

            // Fields
            builder.Property(aa => aa.ApplicantId).HasColumnName("applicant_id").IsRequired();
            builder.Property(aa => aa.Attribute).HasColumnName("attribute").IsRequired();
            builder.Property(aa => aa.Value).HasColumnName("value").IsRequired();

            // Relationships
            builder.HasOne(aa => aa.Applicant)
                   .WithMany(a => a.ApplicantAttributes)
                   .HasForeignKey(aa => aa.ApplicantId);
        }
    }

}
