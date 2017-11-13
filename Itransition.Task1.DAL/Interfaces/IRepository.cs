using System;
using System.Linq;
using System.Linq.Expressions;
using Itransition.Task1.Domain;

namespace Itransition.Task1.DAL.Interfaces
{
    public  interface IRepository<T> where T : Entity
    {
        IQueryable<T> GetAll(params Expression<Func<T, object>>[] navigationProperties);
        T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        void Add(T entity);
        void Edit(T entity);
        void Delete(T entity);
    }
}
