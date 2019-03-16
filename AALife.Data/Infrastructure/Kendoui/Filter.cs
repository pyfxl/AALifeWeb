﻿using AALife.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AALife.Data.Infrastructure.Kendoui
{
    /// <summary>
    /// Represents a filter expression of Kendo DataSource.
    /// </summary>
    public class Filter
    {
        private object _tempValue;

        /// <summary>
        /// Gets or sets the name of the sorted field (property). Set to <c>null</c> if the <c>Filters</c> property is set.
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Gets or sets the filtering operator. Set to <c>null</c> if the <c>Filters</c> property is set.
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// Gets or sets the filtering value. Set to <c>null</c> if the <c>Filters</c> property is set.
        /// </summary>
        public object Value
        {
            get
            {
                if (_tempValue.IsNumber())
                {
                    return Convert.ToDecimal(_tempValue);
                }
                if (_tempValue.IsDateTime())
                {
                    return Convert.ToDateTime(_tempValue);
                }
                return _tempValue;
            }
            set
            {
                _tempValue = value;
            }
        }

        /// <summary>
        /// Gets or sets the filtering logic. Can be set to "or" or "and". Set to <c>null</c> unless <c>Filters</c> is set.
        /// </summary>
        public string Logic { get; set; }

        /// <summary>
        /// Gets or sets the child filter expressions. Set to <c>null</c> if there are no child expressions.
        /// </summary>
        public IEnumerable<Filter> Filters { get; set; }

        /// <summary>
        /// Mapping of Kendo DataSource filtering operators to Dynamic Linq
        /// </summary>
        private static readonly IDictionary<string, string> operators = new Dictionary<string, string>
        {
            {"eq", "="},
            {"neq", "!="},
            {"lt", "<"},
            {"lte", "<="},
            {"gt", ">"},
            {"gte", ">="},
            {"startswith", "StartsWith"},
            {"endswith", "EndsWith"},
            {"contains", "Contains"},
            {"doesnotcontain", "DoesNotContain"},
            {"isnull", "="},
            {"isnotnull", "!="},
            {"isempty", ""},
            {"isnotempty", "!"},
            {"isnullorempty", ""},
            {"isnotnullorempty", "!"}
        };

        /// <summary>
        /// Get a flattened list of all child filter expressions.
        /// </summary>
        public IList<Filter> All()
        {
            var filters = new List<Filter>();

            Collect(filters);

            return filters;
        }

        private void Collect(IList<Filter> filters)
        {
            if (Filters != null && Filters.Any())
            {
                foreach (Filter filter in Filters)
                {
                    filters.Add(filter);

                    filter.Collect(filters);
                }
            }
            else
            {
                filters.Add(this);
            }
        }

        /// <summary>
        /// Converts the filter expression to a predicate suitable for Dynamic Linq e.g. "Field1 = @1 and Field2.Contains(@2)"
        /// </summary>
        /// <param name="filters">A list of flattened filters.</param>
        public string ToExpression(IList<Filter> filters)
        {
            if (Filters != null && Filters.Any())
            {
                return "(" + String.Join(" " + Logic + " ", Filters.Select(filter => filter.ToExpression(filters)).ToArray()) + ")";
            }

            int index = filters.IndexOf(this);

            string comparison = operators[Operator];

            //original code below (case sensitive) commented
            //if (comparison == "StartsWith" || comparison == "EndsWith" || comparison == "Contains")
            //{
            //    return String.Format("{0}.{1}(@{2})", Field, comparison, index);
            //}

            if (Operator == "isnotnull" || Operator == "isnull")
            {
                return String.Format("{0} {1} null", Field, comparison);
            }
            if (Operator == "isempty" || Operator == "isnotempty" || Operator == "isnullorempty" || Operator == "isnotnullorempty")
            {
                return String.Format("{1}string.IsNullOrEmpty({0})", Field, comparison);
            }

            //we ignore case
            if (comparison == "Contains")
            {
                return String.Format("{0}.IndexOf(@{1}) >= 0", Field, index);
            }
            if (comparison == "DoesNotContain")
            {
                return String.Format("{0}.IndexOf(@{1}) < 0", Field, index);
            }
            if (comparison == "=" && Value is String)
            {
                //string only
                comparison = "Equals";
                //numeric values use standard "=" char
            }
            if (comparison == "StartsWith" || comparison == "EndsWith" || comparison == "Equals")
            {
                return String.Format("{0}.{1}(@{2})", Field, comparison, index);
            }

            return String.Format("{0} {1} @{2}", Field, comparison, index);
        }

    }
}