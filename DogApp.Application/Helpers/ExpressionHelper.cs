using System.ComponentModel;
using System.Linq.Expressions;

namespace DogApp.Application.Helpers
{
    public static class ExpressionHelper
    {
        public static Expression<Func<T, object>>? CreateSortedExpression<T>(string propertyName)
        {

            var propByName = TypeDescriptor.GetProperties(typeof(T)).Find(propertyName, true);
            if (propByName is null)
                return null;

            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyName);

            var orderExpression = Expression.Lambda<Func<T, object>>(Expression.Convert(property, typeof(object)), parameter);

            return orderExpression;
        }

    }
}
