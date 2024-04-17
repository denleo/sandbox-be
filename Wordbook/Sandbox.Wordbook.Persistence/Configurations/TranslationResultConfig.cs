using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sandbox.Wordbook.Domain;

namespace Sandbox.Wordbook.Persistence.Configurations;

public class TranslationResultConfig : IEntityTypeConfiguration<TranslationResult>
{
    public void Configure(EntityTypeBuilder<TranslationResult> builder)
    {
        builder.ToTable("TranslationResults");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Transcription).IsRequired(false);
        builder.HasIndex(x => new { x.TranslationId, x.Translation, x.PartOfSpeech }).IsUnique();
    }
}