using AutoMapper;
using Sandbox.Utility.Pagination;

namespace Sandbox.Wordbook.Application.Mappers;

public static class PagedListExtensions
{
    public static PagedList<TResult> MapTo<TSource, TResult>(this PagedList<TSource> pagedList, IMapper mapper)
    {
        return new PagedList<TResult>(
            mapper.Map<IEnumerable<TResult>>(pagedList.Items),
            new PaginationOptions(pagedList.Page, pagedList.PageSize),
            pagedList.TotalCount);
    }
}