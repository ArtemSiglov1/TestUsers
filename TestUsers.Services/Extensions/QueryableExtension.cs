using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestUsers.Services.Models;

namespace TestUsers.Services.Extensions
{
    public static class QueryableExtension
    {
        public static IQueryable<TT> GetPage<T,TT>(this IQueryable<T> query, PageRequest page, Expression<Func<T, TT>> selector)
        {
            return query.Skip((page.Page - 1) * page.PageSize)
                 .Take(page.PageSize)
                 .Select(selector); ;
        }
    }
}
