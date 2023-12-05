using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ND.GradGate.Kernel.Domain.ApplicantData;

public class ApplicantMap : IEntityTypeConfiguration<Applicant>
{
    public void Configure(EntityTypeBuilder<Applicant> builder)
    {
        builder.ToTable("applicants");

        // Key
        builder.HasKey(a => a.Id).HasName("applicants_pk");
        builder.Property(a => a.Id).HasColumnName("applicant_id").ValueGeneratedOnAdd();

        // Fields
        builder.Property(a => a.FirstName).HasColumnName("first_name").IsRequired();
        builder.Property(a => a.LastName).HasColumnName("last_name").IsRequired();
        builder.Property(a => a.Email).HasColumnName("email").IsRequired();
        builder.Property(a => a.Sex).HasColumnName("sex");
        builder.Property(a => a.Ethnicity).HasColumnName("ethnicity");
        builder.Property(a => a.Country).HasColumnName("country");
        builder.Property(a => a.Field).HasColumnName("field");
        builder.Property(a => a.Decision).HasColumnName("decision");

        // Relationships
        builder.HasMany(a => a.AcademicHistories)
               .WithOne(ah => ah.Applicant)
               .HasForeignKey(ah => ah.ApplicantRef);

        builder.HasMany(a => a.ApplicantAttributes)
               .WithOne(aa => aa.Applicant)
               .HasForeignKey(aa => aa.ApplicantId);

        builder.HasMany(a => a.PotentialAdvisors)
               .WithOne(pa => pa.Applicant)
               .HasForeignKey(pa => pa.ApplicantId);

        builder.HasMany(a => a.ReviewerAssignments)
               .WithOne(ra => ra.Applicant)
               .HasForeignKey(ra => ra.ApplicantId);

        builder.HasMany(a => a.Comments)
               .WithOne(c => c.Applicant)
               .HasForeignKey(c => c.ApplicantId);
    }
}
