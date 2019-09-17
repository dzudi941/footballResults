using System;
using System.Collections.Generic;

namespace HTEC.Domain.Interfaces
{
    public interface IRepository<T> : IDisposable
    {
        T Get(object id);
        IEnumerable<T> Get();
        IEnumerable<T> Find(ISpecification<T> specification);
        void Add(T value);
        void AddRange(IEnumerable<T> values);
        void Update(T value);
        void Remove(T value);
    }
}