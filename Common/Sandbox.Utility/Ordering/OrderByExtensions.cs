using System.Linq.Expressions;
using System.Reflection;

namespace Sandbox.Utility.Ordering;

public static class OrderByExtensions
{
    public static IOrderedQueryable<T> OrderByProp<T>(this IQueryable<T> source, string propertyName, Order order)
    {
        var propInfo = typeof(T).GetProperty(propertyName,
            BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

        if (propInfo is null)
            throw new ArgumentException($"Property {propertyName} does not exist on type {typeof(T).Name}");

        var propAccessor = GetExpression<T>(propInfo.Name);

        return order switch
        {
            Order.Asc => source.OrderBy(propAccessor),
            Order.Desc => source.OrderByDescending(propAccessor),
            _ => source.OrderByDescending(propAccessor)
        };
    }

    private static Expression<Func<T, object>> GetExpression<T>(string propertyName)
    {
        var parameter = Expression.Parameter(typeof(T));
        var property = Expression.Property(parameter, propertyName);
        var propAsObject = Expression.Convert(property, typeof(object));

        return Expression.Lambda<Func<T, object>>(propAsObject, parameter);
    }
}