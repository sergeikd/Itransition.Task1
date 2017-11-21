﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using Itransition.Task1.DAL.Interfaces;
using Itransition.Task1.Domain;

namespace Itransition.Task1.DAL.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : Entity
    {
        private readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            if(context == null) throw new ArgumentNullException();
            _context = context;
        }

        public IQueryable<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = _context.Set<T>();
            dbQuery = dbQuery.OrderBy(x => x.Id);

            foreach (var navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include(navigationProperty);
            return dbQuery;
        }
        public IQueryable<T> GetAllOrdered(Expression<Func<T, object>> order, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = _context.Set<T>();
            dbQuery = dbQuery.OrderBy(order);

            foreach (var navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include(navigationProperty);
            return dbQuery;
        }

        public T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            T item = null;
            IQueryable<T> dbQuery = _context.Set<T>();

            foreach (var navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include(navigationProperty);

            item = dbQuery
                .AsNoTracking()
                .FirstOrDefault(where);
            return item;
        }

        public void Add(T entity)
        {
            _context.Entry(entity).State = EntityState.Added;
        }

        public void Edit(T entity)
        {
            _context.Set<T>().AddOrUpdate(entity);
        }
        public void Delete(T item)
        {
            _context.Entry(item).State = EntityState.Deleted;
        }
    }
}
