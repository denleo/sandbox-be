using Microsoft.EntityFrameworkCore;
using Sandbox.Utility.Pagination;

namespace Sandbox.Wordbook.Persistence.Common;

public static class QueryableUtility
{
    /// <summary>
    ///     Return materialized pageable model of T items
    /// </summary>
    public static async Task<PagedList<T>> GetPagedAsync<T>(IQueryable<T> query, PaginationOptions options)
    {
        var total = await query.CountAsync();
        var items = await query
            .Skip((options.Page - 1) * options.PageSize)
            .Take(options.PageSize)
            .ToArrayAsync();

        return new PagedList<T>(items, options, total);
    }
}