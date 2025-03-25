
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StaffManagement.Data.Configurations
{
    public class TrainerConfigurations : IEntityTypeConfiguration<Models.Trainer>
    {
        public void Configure(EntityTypeBuilder<Trainer> builder)
        {
            builder.ToTable("Trainers");
            builder.HasKey(t => t.Id);

            // Store FullName as a single complex column
            builder.OwnsOne(t => t.Name, name =>
            {
                name.Property(n => n.FirstName)
                    .HasColumnName("FirstName")
                    .HasMaxLength(100)
                    .IsRequired();

                name.Property(n => n.LastName)
                    .HasColumnName("LastName")
                    .HasMaxLength(100)
                    .IsRequired();
            });

            // Store Contact Info as a complex type
            builder.OwnsOne(t => t.Contact, contact =>
            {
                contact.Property(c => c.PhoneNumber)
                    .HasColumnName("PhoneNumber")
                    .HasMaxLength(20)
                    .IsRequired();

                contact.Property(c => c.Email)
                    .HasColumnName("Email")
                    .HasMaxLength(100)
                    .IsRequired();
            });

            //!!Note : specializations and certifications are still value objects but stored in a separate table
            builder.OwnsMany(t => t.Specializations, specialization =>
            {
                specialization.ToTable("TrainerSpecializations"); // Separate table

                specialization.Property(s => s.Name)
                             .HasColumnName("SpecializationName")
                             .HasMaxLength(100)
                             .IsRequired();
            });

            builder.Navigation(t => t.Specializations).UsePropertyAccessMode(PropertyAccessMode.Field);


            // Store Certifications as a separate table
            builder.OwnsMany(t => t.Certifications, certification =>
            {
                certification.ToTable("TrainerCertifications"); // Separate table


                certification.Property(c => c.Name)
                       .HasColumnName("CertificationName")
                       .HasMaxLength(100)
                       .IsRequired();

                certification.Property(c => c.DateEarned)
                       .HasColumnName("DateEarned")
                       .IsRequired();
            });


            builder.Navigation(t => t.Certifications).UsePropertyAccessMode(PropertyAccessMode.Field);


            // Store Current Clients as a separate table

            builder.OwnsMany(s => s.CurrentClients, client =>
            {
                client.ToTable("TrainerClients"); // Separate table
                client.Property(c => c.Id)
                      .HasColumnName("ClientId")
                      .IsRequired();
            });
            builder.Navigation(t => t.CurrentClients).UsePropertyAccessMode(PropertyAccessMode.Field);


            // Store Employment Type as a string
            builder.Property(t => t.EmploymentType)
                   .HasConversion<string>()
                   .HasMaxLength(50);

            // Store Money (Hourly Rate) as decimal(10,2)
            builder.OwnsOne(t => t.HourlyRate, money =>
            {
                money.Property(m => m.Amount)
                     .HasColumnName("HourlyRate")
                     .HasColumnType("decimal(10,2)")
                     .IsRequired();

                money.Property(m => m.Currency)
                     .HasColumnName("Currency")
                     .HasMaxLength(3)
                     .IsRequired();
            });

            builder.Property(t => t.IsActive).IsRequired();
            builder.Property(t => t.HireDate).IsRequired();


            builder.HasMany<PrivateSession>()
           .WithOne()
           .HasForeignKey(s => s.TrainerId)
           .OnDelete(DeleteBehavior.Cascade);

            builder.Ignore(t => t.Events);

        }
    }
}
