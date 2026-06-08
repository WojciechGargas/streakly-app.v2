using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Streakly.Infrastructure.Auth;

namespace Streakly.Infrastructure.DAL.Configurations;

internal sealed class RevokedTokenConfiguration : IEntityTypeConfiguration<RevokedToken>
{
    public void Configure(EntityTypeBuilder<RevokedToken> builder)
    {
        builder.ToTable("revoked_tokens");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();

        builder.Property(x => x.TokenId)
            .HasColumnName("token_id")
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(x => x.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        builder.Property(x => x.ExpiresAtUtc)
            .HasColumnName("expires_at_utc")
            .IsRequired();

        builder.Property(x => x.RevokedAtUtc)
            .HasColumnName("revoked_at_utc")
            .IsRequired();

        builder.HasIndex(x => x.TokenId)
            .IsUnique();

        builder.HasIndex(x => x.ExpiresAtUtc);
    }
}