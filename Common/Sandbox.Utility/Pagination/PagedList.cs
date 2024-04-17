namespace Sandbox.Utility.Pagination;

public class PagedList<T>
{
    public PagedList(IEnumerable<T> items, PaginationOptions options, int totalCount)
    {
        Page = options.Page;
        PageSize = options.PageSize;
        TotalCount = totalCount;
        Items = items;
    }

    public int Page { get; }
    public int PageSize { get; }
    public int TotalCount { get; }
    public bool HasNext => Page * PageSize < TotalCount;
    public IEnumerable<T> Items { get; }    
}