using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.EfCore.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.RegisteredAt)
            .IsRequired();

        builder.Property(u => u.IsEmailConfirmed)
            .HasDefaultValue(false)
            .IsRequired();

        // Value Object: Email
        builder.OwnsOne(u => u.Email, email =>
        {
            email.Property(e => e.Value)
                .HasColumnName("Email")
                .IsRequired()
                .HasMaxLength(150);

            email.HasIndex(e => e.Value)
                .IsUnique();
        });

        // Value Object: FullName
        builder.OwnsOne(u => u.FullName, fullName =>
        {
            fullName.Property(f => f.FirstName)
                .HasColumnName("FirstName")
                .IsRequired()
                .HasMaxLength(100);

            fullName.Property(f => f.LastName)
                .HasColumnName("LastName")
                .IsRequired()
                .HasMaxLength(100);

            fullName.Property(f => f.MiddleName)
                .HasColumnName("MiddleName")
                .HasMaxLength(100);
        });

        // Value Object: PasswordHash
        builder.OwnsOne(u => u.PasswordHash, password =>
        {
            password.Property(p => p.Value)
                .HasColumnName("PasswordHash")
                .IsRequired()
                .HasMaxLength(300);
        });

        // Value Object: PhoneNumber (nullable)
        builder.OwnsOne(u => u.PhoneNumber, phone =>
        {
            phone.Property(p => p.CountryCode)
                .HasColumnName("PhoneCountryCode")
                .HasMaxLength(3);

            phone.Property(p => p.Number)
                .HasColumnName("PhoneNumber")
                .HasMaxLength(15);
        });
    }
}
