using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FR.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FR.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T: class
    {
        private readonly TournamentDbContenxt _context;
         
        public Repository(TournamentDbContenxt context)
        {
            _context = context;
        }

        public int Add(T value)
        {
            _context.Add(value);
            return _context.SaveChanges();
        }

        public int AddRange(IEnumerable<T> values)
        {
            _context.AddRange(values);
            return _context.SaveChanges();
        }

        public int Remove(T value)
        {
            _context.Remove(value);
            return _context.SaveChanges();
        }

        public T Get(object id, params Expression<Func<T, dynamic>>[] includes)
        {
            var entry = _context.Find<T>(id);
            if (entry == null) return null;

            var entityEntry = _context.Entry(entry);
            foreach (var include in includes)
            {
                entityEntry.Reference(include).Load();
            }

            return entityEntry.Entity;
        }

        public IEnumerable<T> Get(params Expression<Func<T, dynamic>>[] includes)
        {
            IQueryable<T> set = _context.Set<T>();
            foreach (var include in includes)
            {
                set = set.Include(include);
            }

            return set;
        }

        public int Update(T value)
        {
            _context.Update(value);
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public IEnumerable<T> Find(ISpecification<T> specification, params Expression<Func<T, dynamic>>[] includes)
        {
            var expression = specification.ToExpression();
            IQueryable<T> set = _context.Set<T>();
            foreach (var include in includes)
            {
                set = set.Include(include);
            }

            return set.Where(expression);
        }
    }
}