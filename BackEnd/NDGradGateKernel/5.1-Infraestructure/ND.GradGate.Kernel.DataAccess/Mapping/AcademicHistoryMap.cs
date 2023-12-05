using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ND.GradGate.Kernel.Domain.ApplicantData.Mapping
{
    public class AcademicHistoryMap : IEntityTypeConfiguration<AcademicHistory>
    {
        public void Configure(EntityTypeBuilder<AcademicHistory> builder)
        {
            builder.ToTable("academic_history");

            // Key
            builder.HasKey(ah => ah.Id).HasName("pk_academic_history");
            builder.Property(ah => ah.Id).HasColumnName("id").ValueGeneratedOnAdd();

            // Fields
            builder.Property(ah => ah.ApplicantRef).HasColumnName("applicant_ref").IsRequired();
            builder.Property(ah => ah.Institution).HasColumnName("institution");
            builder.Property(ah => ah.Major).HasColumnName("major");
            builder.Property(ah => ah.Gpa).HasColumnName("gpa").HasColumnType("numeric");
            builder.Property(ah => ah.FromDate).HasColumnName("from_date").HasColumnType("date");
            builder.Property(ah => ah.ToDate).HasColumnName("to_date").HasColumnType("date");

            // Relationships
            builder.HasOne(ah => ah.Applicant)
                   .WithMany(a => a.AcademicHistories)
                   .HasForeignKey(ah => ah.ApplicantRef);
        }
    }

}
