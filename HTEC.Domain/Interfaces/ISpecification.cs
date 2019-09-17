using System;
using System.Linq.Expressions;

namespace HTEC.Domain.Interfaces
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> ToExpression();
        bool IsSatisfiedBy(T entity);
        ISpecification<T> And(ISpecification<T> specification);
        ISpecification<T> Or(ISpecification<T> specification);
        ISpecification<T> Not();
    }
}
