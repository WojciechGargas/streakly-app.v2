using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Streakly.Core.Entities;
using Streakly.Core.ValueObjects;
using Streakly.Core.ValueObjects.User;

namespace Streakly.Infrastructure.DAL.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(x => x.UserId);

        builder.Property(x => x.UserId)
            .HasConversion(
                x => x.Value,
                x => new UserId(x))
            .ValueGeneratedNever()
            .HasColumnName("id");

        builder.Property(x => x.Email)
            .HasConversion(
                x => x.Value,
                x => new Email(x))
            .HasColumnName("email")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Username)
            .HasConversion(
                x => x.Value,
                x => new Username(x))
            .HasColumnName("username")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.Password)
            .HasConversion(
                x => x.Value,
                x => new Password(x))
            .HasColumnName("password")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Fullname)
            .HasConversion(
                x => x.Value,
                x => new Fullname(x))
            .HasColumnName("fullname")
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(x => x.Role)
            .HasColumnName("role")
            .HasConversion<string>()
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(x => x.LastLoggedAtUtc)
            .HasColumnName("last_logged_at_utc");
        
        builder.Property(x => x.IsEmailConfirmed)
            .HasColumnName("is_email_confirmed")
            .IsRequired();

        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder.HasIndex(x => x.Username)
            .IsUnique();
    }
}