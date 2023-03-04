using System.Collections;

namespace Infrastructure.Extensions;

public static class FilterExtension 
{
    public static IEnumerable<T> Pagination<T>(this IQueryable<T> query, int count, int page)
    {
        return query.Skip(count * (page - 1)).Take(count);
    }
}
