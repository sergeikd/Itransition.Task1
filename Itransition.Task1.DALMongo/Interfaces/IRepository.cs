using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Itransition.Task1.Domain;

namespace Itransition.Task1.DALMongo.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        IEnumerable<T> GetAll();
        T GetSingle(Expression<Func<T, bool>> condition);
        void Add(T entity);
        void Edit(T entity);
        void Delete(T entity);
    }
}
