using System;
using System.Linq;
using System.Linq.Expressions;
using TkwEF.Core.Extentions;

namespace TkwEF.Core.Extentions
{
    public static class Extensions
    {
        // Сводка:
        //     Указывает направление сортировки элементов списка.
        public enum SortDirection
        {
            // Сводка:
            //     Сортировка в порядке возрастания.Например, от A до Z.
            Ascending = 0,
            //
            // Сводка:
            //     Сортировка в порядке убывания.Например, от Z до A.
            Descending = 1,
        }

        public interface IListFilter
        {
            int Count { get; set; }
            int Start { get; set; }
            Sort Sort { get; set; }
        }

        public class Sort
        {
            public string Field { get; set; }
            public SortDirection Dir { get; set; }
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, IListFilter filter)
        {
            return filter.Count > 0 ? query.Skip(filter.Start).Take(filter.Count) : query;
        }

        public static IQueryable<T> ApplyOrder<T>(this IQueryable<T> query, string name, string methodName)
        {
            var type = typeof(T);
            var param = Expression.Parameter(type, "item");
            var propertyInfo = type.GetProperty(name);
            var expr = Expression.Property(param, propertyInfo);
            var propertyType = propertyInfo.PropertyType;

            var delegateType = typeof(Func<,>).MakeGenericType(typeof(T), propertyType);
            var lambda = Expression.Lambda(delegateType, expr, param);

            return (IQueryable<T>)typeof(Queryable).GetMethods().Single(
                                      method => method.Name == methodName
                                                && method.IsGenericMethodDefinition
                                                && method.GetGenericArguments().Length == 2
                                                && method.GetParameters().Length == 2)
                                      .MakeGenericMethod(typeof(T), propertyType)
                                      .Invoke(null, new object[] { query, lambda });
        }

        public static IQueryable<T> Order<T>(this IQueryable<T> query, Sort sort)
        {
            return sort.Dir == SortDirection.Ascending ?
                query.Order(sort.Field) :
                query.OrderDescending(sort.Field);
        }

        public static IQueryable<T> Order<T>(this IQueryable<T> query, string name)
        {
            return query.ApplyOrder(name, "OrderBy");
        }

        public static IQueryable<T> OrderDescending<T>(this IQueryable<T> query, string name)
        {
            return query.ApplyOrder(name, "OrderByDescending");
        }
        
        /// <summary>
        /// Add to query TOP and LIMIT condition
        /// </summary>
        /// <typeparam name="T">[table] mapping entity Type</typeparam>
        /// <param name="query">Base query</param>
        /// <param name="skip">rows</param>
        /// <param name="take">rows</param>
        /// <returns></returns>
        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int skip, int take)
        {
            return query.Skip(skip).Take(take);
        }
        
        public static IQueryable<T> ApplyOrder<T>(this IQueryable<T> query, IListFilter filter)
        {
            return filter == null
                       ? query
                       : query.ApplyOrder(filter.Sort.Field,
                                          filter.Sort.Dir == SortDirection.Ascending ? "OrderBy" : "OrderByDescending"
                             );
        }

    }
}