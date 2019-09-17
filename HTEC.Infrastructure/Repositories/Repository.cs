using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HTEC.Domain.Interfaces;

namespace HTEC.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T: class
    {
        private readonly TournamentDbContenxt _context;
         
        public Repository(TournamentDbContenxt context)
        {
            _context = context;
        }

        public void Add(T value)
        {
            _context.Add(value);
            _context.SaveChanges();
        }

        public void AddRange(IEnumerable<T> values)
        {
            _context.AddRange(values);
            _context.SaveChanges();
        }

        public void Remove(T value)
        {
            _context.Remove(value);
            _context.SaveChanges();
        }

        public T Get(object id)
        {
            return _context.Find<T>(id);
        }

        public IEnumerable<T> Get()
        {
            return _context.Set<T>();
        }

        public void Update(T value)
        {
            _context.Update(value);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public IEnumerable<T> Find(ISpecification<T> specification)
        {
            return _context.Set<T>().Where(specification.ToExpression());
        }
    }
}