using Microsoft.EntityFrameworkCore;
using Sandbox.Utility.Ordering;
using Sandbox.Utility.Pagination;
using Sandbox.Wordbook.Domain;
using Sandbox.Wordbook.Domain.Abstractions;
using Sandbox.Wordbook.Domain.Enums;
using Sandbox.Wordbook.Persistence.Common;

namespace Sandbox.Wordbook.Persistence.Repositories;

public class TranslationRepository : ITranslationRepository
{
    private readonly WordbookContext _context;

    public TranslationRepository(WordbookContext context)
    {
        _context = context;
    }

    public void CreateTranslation(Translation translation)
    {
        _context.Translations.Add(translation);
    }

    public void DeleteTranslation(Translation translation)
    {
        _context.Translations.Remove(translation);
    }

    public Task<Translation?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        return _context.Translations
            .Include(x => x.TranslationResults)
            .FirstOrDefaultAsync(x => x.Id == id, token);
    }

    public Task<Translation?> GetByWordAsync(
        Guid userId, string word,
        TranslationLanguage source,
        TranslationLanguage target,
        CancellationToken token = default)
    {
        return _context.Translations
            .Include(x => x.TranslationResults)
            .FirstOrDefaultAsync(x =>
                x.UserId == userId
                && x.Word == word
                && x.SourceLang == source
                && x.TargetLang == target, token);
    }

    public Task<PagedList<Translation>> GetUserTranslationsAsync(
        Guid userId,
        PaginationOptions paginationOptions,
        OrderOptions orderOptions,
        TranslationLanguage? sourceLanguage,
        TranslationLanguage? targetLanguage,
        string? word, string[]? partOfSpeech,
        CancellationToken token = default)
    {
        var query = _context.Translations
            .Include(x => x.TranslationResults)
            .Where(t => t.UserId == userId);

        query = ApplyFilters(query, sourceLanguage, targetLanguage, word, partOfSpeech);
        query = query.OrderByProp(orderOptions.OrderBy, orderOptions.Order);

        return QueryableUtility.GetPagedAsync(query, paginationOptions);
    }

    private static IQueryable<Translation> ApplyFilters(
        IQueryable<Translation> query,
        TranslationLanguage? sourceLang,
        TranslationLanguage? targetLang,
        string? word, string[]? partOfSpeech)
    {
        if (sourceLang.HasValue)
            query = query.Where(x => x.SourceLang == sourceLang.Value);

        if (targetLang.HasValue)
            query = query.Where(x => x.TargetLang == targetLang.Value);

        if (!string.IsNullOrEmpty(word))
            query = query.Where(x => x.Word.ToLower().Contains(word.ToLower()));

        if (partOfSpeech is { Length: > 0 })
        {
            partOfSpeech = partOfSpeech
                .Select(x => x.ToLower())
                .ToArray();

            query = query.Where(x =>
                x.TranslationResults.Any(v => partOfSpeech.Contains(v.PartOfSpeech.ToLower())));
        }

        return query;
    }
}