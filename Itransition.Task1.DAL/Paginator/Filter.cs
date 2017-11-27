using System;
using System.Linq.Expressions;

namespace Itransition.Task1.DAL.Paginator
{
    internal class Filter<T>
    {
        public bool Condition { get; set; }
        public Expression<Func<T, bool>> Expression { get; set; }
    }
}