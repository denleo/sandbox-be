using Sandbox.Utility.Ordering;
using Sandbox.Utility.Pagination;
using Sandbox.Wordbook.Domain.Enums;

namespace Sandbox.Wordbook.Domain.Abstractions;

public interface ITranslationRepository
{
    void CreateTranslation(Translation translation);

    void DeleteTranslation(Translation translation);
    Task<Translation?> GetByIdAsync(Guid id, CancellationToken token = default);

    Task<PagedList<Translation>> GetUserTranslationsAsync(
        Guid userId,
        PaginationOptions paginationOptions,
        OrderOptions orderOptions,
        TranslationLanguage? sourceLanguage,
        TranslationLanguage? targetLanguage,
        string? word, string[]? partOfSpeech,
        CancellationToken token = default);

    Task<Translation?> GetByWordAsync(
        Guid userId, string word,
        TranslationLanguage source,
        TranslationLanguage target,
        CancellationToken token = default);
}