using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ND.GradGate.Kernel.Domain.ApplicantData;

public class ApplicantMap : IEntityTypeConfiguration<Applicant>
{
    public void Configure(EntityTypeBuilder<Applicant> builder)
    {
        builder.ToTable("applicants");

        // Key
        builder.HasKey(a => a.Ref).HasName("pk_applicants");
        builder.Property(a => a.Ref).HasColumnName("ref").ValueGeneratedOnAdd().HasColumnType("int");
        builder.Ignore(a => a.Id);

        // Fields
        builder.Property(a => a.LastName).HasColumnName("last_name").IsRequired().HasColumnType("varchar");
        builder.Property(a => a.FirstName).HasColumnName("first_name").IsRequired().HasColumnType("varchar");
        builder.Property(a => a.Email).HasColumnName("email").IsRequired().HasColumnType("varchar");
        builder.Property(a => a.Sex).HasColumnName("sex").HasColumnType("char").HasMaxLength(1);
        builder.Property(a => a.Ethnicity).HasColumnName("ethnicity").HasColumnType("varchar");
        builder.Property(a => a.CitizenshipCountry).HasColumnName("citizenship_country").HasColumnType("varchar");
        builder.Property(a => a.AreaOfStudy).HasColumnName("area_of_study").HasColumnType("varchar");
        builder.Property(a => a.ApplicationStatus).HasColumnName("application_status").HasColumnType("varchar");
        builder.Property(a => a.DepartmentRecommendation)
               .HasColumnName("department_recommendation")
               .HasColumnType("varchar")
               .IsRequired(false); 

        // Indexes
        builder.HasIndex(a => a.Email).HasDatabaseName("idx_applicants_email");

        // Relationships
        builder.HasMany(a => a.AcademicHistories)
               .WithOne(ah => ah.Applicant)
               .HasForeignKey(ah => ah.ApplicantRef)
               .HasConstraintName("fk_academic_history_applicants");

        builder.HasMany(a => a.ApplicationDataValues)
               .WithOne(adv => adv.Applicant)
               .HasForeignKey(adv => adv.ApplicantRef)
               .HasConstraintName("fk_application_data_values_applicants");

        builder.HasMany(a => a.ApplicantAdvisorLinks)
               .WithOne(aal => aal.Applicant)
               .HasForeignKey(aal => aal.ApplicantRef)
               .HasConstraintName("fk_applicant_advisors_link_applicants");

        builder.HasMany(a => a.ReviewerAssignments)
               .WithOne(ra => ra.Applicant)
               .HasForeignKey(ra => ra.ApplicantRef)
               .HasConstraintName("fk_reviewer_assignment_applicants");
    }
}
