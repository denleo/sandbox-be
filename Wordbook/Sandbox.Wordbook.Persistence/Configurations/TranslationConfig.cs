using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sandbox.Wordbook.Domain;
using Sandbox.Wordbook.Domain.Enums;

namespace Sandbox.Wordbook.Persistence.Configurations;

public class TranslationConfig : IEntityTypeConfiguration<Translation>
{
    public void Configure(EntityTypeBuilder<Translation> builder)
    {
        builder.ToTable("Translations");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.SourceLang).HasConversion(new EnumToStringConverter<TranslationLanguage>());
        builder.Property(x => x.TargetLang).HasConversion(new EnumToStringConverter<TranslationLanguage>());
        builder.HasIndex(x => new { x.UserId, x.Word, x.SourceLang, x.TargetLang }).IsUnique();

        builder
            .HasMany(x => x.TranslationResults)
            .WithOne()
            .HasForeignKey(x => x.TranslationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}