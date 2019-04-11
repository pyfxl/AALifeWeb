using AALife.Core.Infrastructure.Kendoui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace AALife.Code.Services
{
    public static class BaseExtensions
    {
        public static IEnumerable<T> ToDataSourceResult<T>(this IEnumerable<T> items, DataSourceRequest request)
        {
            var query = items.AsQueryable();

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
    }
}
