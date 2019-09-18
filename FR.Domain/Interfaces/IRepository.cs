using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FR.Domain.Interfaces
{
    public interface IRepository<T> : IDisposable
    {
        T Get(object id, params Expression<Func<T, dynamic>>[] includes);
        IEnumerable<T> Get(params Expression<Func<T, dynamic>>[] includes);
        IEnumerable<T> Find(ISpecification<T> specification, params Expression<Func<T, dynamic>>[] includes);
        void Add(T value);
        void AddRange(IEnumerable<T> values);
        void Update(T value);
        void Remove(T value);
    }
}