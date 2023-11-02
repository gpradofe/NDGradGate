using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ND.GradGate.Kernel.Domain.ApplicantData.Mapping
{
    public class AcademicHistoryMap : IEntityTypeConfiguration<AcademicHistory>
    {
        public void Configure(EntityTypeBuilder<AcademicHistory> builder)
        {
            builder.ToTable("academic_history");

            builder.HasKey(ah => ah.Id).HasName("pk_academic_history");
            builder.Property(ah => ah.Id).HasColumnName("id").ValueGeneratedOnAdd();

            builder.Property(ah => ah.ApplicantRef).HasColumnName("applicant_ref").IsRequired();
            builder.Property(ah => ah.Institution).HasColumnName("institution").IsRequired();
            builder.Property(ah => ah.Major).HasColumnName("major").IsRequired();
            builder.Property(ah => ah.Gpa).HasColumnName("gpa").IsRequired()
                  .HasPrecision(3, 2); 
            builder.Property(ah => ah.FromDate).HasColumnName("from_date").IsRequired();
            builder.Property(ah => ah.ToDate).HasColumnName("to_date").IsRequired();

            builder.HasIndex(ah => ah.ApplicantRef).HasDatabaseName("idx_academic_history_applicant_ref");

            builder.HasOne(ah => ah.Applicant)
                   .WithMany(a => a.AcademicHistories)
                   .HasForeignKey(ah => ah.ApplicantRef)
                   .HasConstraintName("fk_academic_history_applicants");
        }
    }
}
