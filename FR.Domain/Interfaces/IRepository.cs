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
        int Add(T value);
        int AddRange(IEnumerable<T> values);
        int Update(T value);
        int Remove(T value);
    }
}