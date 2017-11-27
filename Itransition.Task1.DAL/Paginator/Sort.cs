using System;
using System.Linq.Expressions;

namespace Itransition.Task1.DAL.Paginator
{
    internal class Sort<T, TKey>
    {
        public bool Condition { get; set; }
        public Expression<Func<T, TKey>> Expression { get; set; }
        public bool ByDescending { get; set; }
    }
}
