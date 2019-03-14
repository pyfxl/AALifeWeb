using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;

namespace AALife.Data.Infrastructure.Kendoui
{
    public static class EnumerableExtensions
    {
        public static dynamic GroupByMany<TElement>(this IEnumerable<TElement> elements,
            IEnumerable<Group> groupSelectors)
        {
            if (groupSelectors != null && groupSelectors.Any())
            {
                //create a new list of Kendo Group Selectors 
                var selectors = new List<GroupSelector<TElement>>(groupSelectors.Count());
                foreach (var selector in groupSelectors)
                {
                    //compile the Dynamic Expression Lambda for each one
                    var expression =
                        System.Linq.Dynamic.DynamicExpression.ParseLambda(typeof(TElement), typeof(object), selector.Field);
                    //add it to the list
                    selectors.Add(new GroupSelector<TElement>
                    {
                        Selector = (Func<TElement, object>)expression.Compile(),
                        Field = selector.Field,
                        Aggregates = selector.Aggregates
                    });
                }
                //call the actual group by method
                return elements.GroupByMany(selectors.ToArray());
            }
            else
            {
                return null;
            }
        }

        public static dynamic GroupByMany<TElement>(this IEnumerable<TElement> elements,
            params GroupSelector<TElement>[] groupSelectors)
        {
            if (groupSelectors.Length > 0)
            {
                //get selector
                var selector = groupSelectors.First();
                var nextSelectors = groupSelectors.Skip(1).ToArray(); //reduce the list recursively until zero
                return
                    //group by and return 
                    elements.GroupBy(selector.Selector).Select(
                                g => new GroupResult
                                {
                                    value = g.Key,
                                    aggregates = selector.Aggregates,
                                    hasSubgroups = groupSelectors.Length > 1,
                                    count = g.Count(),
                                    //recursivly group the next selectors
                                    items = g.GroupByMany(nextSelectors),
                                    field = selector.Field
                                });
            }
            //if there are not more group selectors return data
            return elements;
        }

    }

    public static class QueryableExtensions
    {
        public static IQueryable<T> Filter<T>(this IQueryable<T> queryable, Filter filter)
        {
            if (filter != null && filter.Logic != null)
            {
                // Collect a flat list of all filters
                var filters = filter.All();

                // Get all filter values as array (needed by the Where method of Dynamic Linq)
                var values = filters.Select(f => f.Value).ToArray();

                // Create a predicate expression e.g. Field1 = @0 And Field2 > @1
                string predicate = filter.ToExpression(filters);

                // Use the Where method of Dynamic Linq to filter the data
                queryable = queryable.Where(predicate, values);
            }

            return queryable;
        }

        public static IQueryable<T> Sort<T>(this IQueryable<T> queryable, IEnumerable<Sort> sort)
        {
            if (sort != null && sort.Any())
            {
                // Create ordering expression e.g. Field1 asc, Field2 desc
                var ordering = string.Join(",", sort.Select(s => s.ToExpression()));

                // Use the OrderBy method of Dynamic Linq to sort the data
                return queryable.OrderBy(ordering);
            }

            return queryable;
        }

        public static object Aggregate<T>(this IQueryable<T> queryable, IEnumerable<Aggregator> aggregates)
        {
            if ((aggregates != null) && aggregates.Any())
            {
                var objProps = new Dictionary<DynamicProperty, object>();
                var groups = aggregates.GroupBy(g => g.Field);
                Type type = null;
                foreach (var group in groups)
                {
                    var fieldProps = new Dictionary<DynamicProperty, object>();
                    foreach (var aggregate in group)
                    {
                        var prop = typeof(T).GetProperty(aggregate.Field);
                        var param = Expression.Parameter(typeof(T), "s");
                        var selector = (aggregate.Aggregate == "count") && (Nullable.GetUnderlyingType(prop.PropertyType) != null)
                            ? Expression.Lambda(Expression.NotEqual(Expression.MakeMemberAccess(param, prop), Expression.Constant(null, prop.PropertyType)), param)
                            : Expression.Lambda(Expression.MakeMemberAccess(param, prop), param);
                        var mi = aggregate.MethodInfo(typeof(T));
                        if (mi == null)
                        {
                            continue;
                        }

                        var val = queryable.Provider.Execute(Expression.Call(null, mi, (aggregate.Aggregate == "count") && (Nullable.GetUnderlyingType(prop.PropertyType) == null)
                                                   ? new[] { queryable.Expression }
                                                   : new[] { queryable.Expression, Expression.Quote(selector) }));

                        fieldProps.Add(new DynamicProperty(aggregate.Aggregate, typeof(object)), val);
                    }
                    type = System.Linq.Dynamic.DynamicExpression.CreateClass(fieldProps.Keys);
                    var fieldObj = Activator.CreateInstance(type);
                    foreach (var p in fieldProps.Keys)
                    {
                        type.GetProperty(p.Name).SetValue(fieldObj, fieldProps[p], null);
                    }
                    objProps.Add(new DynamicProperty(group.Key, fieldObj.GetType()), fieldObj);
                }

                type = System.Linq.Dynamic.DynamicExpression.CreateClass(objProps.Keys);

                var obj = Activator.CreateInstance(type);

                foreach (var p in objProps.Keys)
                {
                    type.GetProperty(p.Name).SetValue(obj, objProps[p], null);
                }

                return obj;
            }
            return null;
        }
    }

}
