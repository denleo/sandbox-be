using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sandbox.Wordbook.Domain;

namespace Sandbox.Wordbook.Persistence.Configurations;

public class WordbookUserConfig : IEntityTypeConfiguration<WordbookUser>
{
    public void Configure(EntityTypeBuilder<WordbookUser> builder)
    {
        builder.ToTable("WordbookUsers");

        builder.HasKey(x => x.Id);
        builder.HasIndex(u => u.FirebaseId).IsUnique();
        builder.HasIndex(u => u.Email).IsUnique();

        builder
            .HasMany(x => x.Translations)
            .WithOne()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}