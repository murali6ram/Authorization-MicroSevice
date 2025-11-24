using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UnitOfWork.Property;

namespace UnitOfWork.Extensions;

public static class DbSetExtensions
{
    public static IQueryable<TR> PrepareQuery<T, TR>(this IQueryable<T> query,
            Expression<Func<T, TR>> selector,
            Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null,
            bool disableTracking = true) where T : class
    {
        if (disableTracking)
            query = query.AsNoTracking();

        if (includes != null)
            query = includes(query);

        if (expression != null)
            query = query.Where(expression);

        if (orderBy != null)
            query = orderBy(query);

        return query.Select(selector);
    }

    public static async Task<Paged<TR>> ToPagedAsync<TR>(this IQueryable<TR> query,
        PagedParameters pagedParameters)
    {
        int page = pagedParameters.PageNumber < 0 ? 1 : pagedParameters.PageNumber;
        return new Paged<TR>
        {
            CurrentPage = page,
            PageSize = pagedParameters.PageSize,
            Items = await ((IOrderedQueryable<TR>)query).SkipAndTakeToListAsync(page, pagedParameters.PageSize),
            TotalItems = await query.CountAsync()
        };
    }

    public static async Task<IEnumerable<TR>> SkipAndTakeToListAsync<TR>(this IOrderedQueryable<TR> query, int page, int size)
    {
        return await query
            .Skip((page - 1) * size)
            .Take(size).ToListAsync();
    }
}
