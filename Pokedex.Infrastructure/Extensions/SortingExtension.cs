using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;


namespace Pokedex.Infrastructure.Extensions
{
    public static class SortingExtension
    {
        public static IQueryable<T> ApplySorting<T>(this IQueryable<T> query, string? sortBy, bool descending)
        {
            if (string.IsNullOrWhiteSpace(sortBy))
                return query;

            return query.OrderBy($"{sortBy} {(descending ? "descending" : "ascending")}");
        }

    }
}
