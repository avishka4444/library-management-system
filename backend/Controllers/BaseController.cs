using LinqToDB;
using System.Linq.Expressions;

namespace LibraryManagement.Api.Controllers;

public static class Linq2DbExtensions
{
    public static Task<List<T>> ToListAsync<T>(this IQueryable<T> source)
    {
        return Task.FromResult(source.ToList());
    }

    public static Task<T?> FirstOrDefaultAsync<T>(this IQueryable<T> source) where T : class
    {
        return Task.FromResult(source.FirstOrDefault());
    }

    public static Task<T?> FirstOrDefaultAsync<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate) where T : class
    {
        return Task.FromResult(source.FirstOrDefault(predicate));
    }

    public static Task<bool> AnyAsync<T>(this IQueryable<T> source)
    {
        return Task.FromResult(source.Any());
    }

    public static Task<bool> AnyAsync<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate)
    {
        return Task.FromResult(source.Any(predicate));
    }
}


