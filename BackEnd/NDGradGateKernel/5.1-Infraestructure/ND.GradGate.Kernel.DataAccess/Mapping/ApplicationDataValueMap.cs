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
    public class ApplicationDataValueMap : IEntityTypeConfiguration<ApplicationDataValue>
    {
        public void Configure(EntityTypeBuilder<ApplicationDataValue> builder)
        {
            builder.ToTable("application_data_values");

            // Key
            builder.HasKey(adv => adv.Id).HasName("pk_application_data_values");
            builder.Property(adv => adv.Id).HasColumnName("value_id").ValueGeneratedOnAdd();

            // Fields
            builder.Property(adv => adv.ApplicantRef).HasColumnName("applicant_ref").IsRequired();
            builder.Property(adv => adv.AttributeId).HasColumnName("attribute_id").IsRequired();
            builder.Property(adv => adv.Value).HasColumnName("value").HasColumnType("text");

            // Indexes
            builder.HasIndex(adv => adv.ApplicantRef).HasDatabaseName("idx_application_data_values_applicant_ref");
            builder.HasIndex(adv => adv.AttributeId).HasDatabaseName("idx_application_data_values_attribute_id");

            // Relationships
            builder.HasOne(adv => adv.Applicant)
                   .WithMany(a => a.ApplicationDataValues)
                   .HasForeignKey(adv => adv.ApplicantRef)
                   .HasConstraintName("fk_application_data_values_applicants");

            builder.HasOne(adv => adv.Attribute)
                   .WithMany(a => a.ApplicationDataValues)
                   .HasForeignKey(adv => adv.AttributeId)
                   .HasConstraintName("fk_application_data_values_attributes");
        }
    }

}
