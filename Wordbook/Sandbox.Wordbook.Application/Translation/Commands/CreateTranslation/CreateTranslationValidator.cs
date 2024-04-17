using FluentValidation;

namespace Sandbox.Wordbook.Application.Translation.Commands.CreateTranslation;

public class CreateTranslationValidator : AbstractValidator<CreateTranslationCommand>
{
    public CreateTranslationValidator()
    {
        RuleFor(x => x.Word).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Translation).NotEmpty().MaximumLength(50);
        RuleFor(x => x.PartOfSpeech).NotEmpty().MaximumLength(30);
        RuleFor(x => x.Transcription).NotEmpty().MaximumLength(50);
        RuleFor(x => x.SourceLang).IsInEnum();
        RuleFor(x => x.TargetLang).IsInEnum();
    }
}