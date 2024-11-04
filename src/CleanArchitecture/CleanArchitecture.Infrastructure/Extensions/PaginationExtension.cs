using System.Linq.Expressions;

namespace CleanArchitecture.Infrastructure.Extensions;

public static class PaginationExtension 
{
    public static IQueryable<T> OrderBypropertyOrField<T>(
        this IQueryable<T> queryable,
        string propertyOrFieldName,
        bool ascending = true 
    )
    {
        var elementType = typeof(T);
        var orderByMethodName = ascending ? "OrderBy" : "OrderByDescending";

        var parameterExpression = Expression.Parameter(elementType);
        var propertyOrFieldExpression = Expression
        .PropertyOrField(parameterExpression, propertyOrFieldName);

        var selector = Expression.Lambda(propertyOrFieldExpression, parameterExpression);

        var orderBYExpression = Expression.Call(
            typeof(Queryable), 
            orderByMethodName,
            new [] { elementType, propertyOrFieldExpression.Type },
            queryable.Expression,
            selector
        );

        return queryable.Provider.CreateQuery<T>(orderBYExpression);
    }
}