﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CGVakBooks.DataAccess.Repository.IRepository;
using CGVakBooks.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace CGVakBooks.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }
        public void Add(T item)
        {
            _db.Add(item);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T>? query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);

            }
            if (includeProperties != null)
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T>? query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);

            }
            if (includeProperties != null)
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
            return query.FirstOrDefault();
        }

        public void Remove(T item)
        {
            _db.Remove(item);
        }

        public void RemoveChange(IEnumerable<T> items)
        {
            _db.RemoveRange(items);
        }
    }
}
