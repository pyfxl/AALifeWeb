using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace AALife.Core.Infrastructure.Kendoui
{
    public static class KendoExtensions
    {

        public static IQueryable<T> ToKendoDataSource<T>(this IQueryable<T> items, DataSourceRequest request)
        {
            var query = items;

            if (request.Filter != null)
            {
                query = query.Filter(request.Filter);
            }

            if (request.Sort != null)
            {
                query = query.Sort(request.Sort);
            }
            else
            {
                query = query.OrderBy("Id desc");
            }

            return query.Skip(request.Skip).Take(request.Take);
        }

        public static IEnumerable<T> ToKendoDataSource<T>(this IEnumerable<T> items, DataSourceRequest request)
        {
            var query = items.AsQueryable();

            return query.ToKendoDataSource<T>(request);
        }

        public static DataSourceResult ToKendoDataSource<T>(this IEnumerable<T> items)
        {
            var result = new DataSourceResult
            {
                Total = items.Count(),
                Data = items.ToList()
            };

            return result;
        }

    }
}
