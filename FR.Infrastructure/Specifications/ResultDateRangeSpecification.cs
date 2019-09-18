using FR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FR.Infrastructure.Specifications
{
    public class ResultDateRangeSpecification : Specification<Result>
    {
        private DateTime _start;
        private DateTime _end;

        public ResultDateRangeSpecification(DateTime start, DateTime end)
        {
            _start = start;
            _end = end;
        }

        public override Expression<Func<Result, bool>> ToExpression()
        {
            return result => result.KickoffAt > _start && result.KickoffAt < _end;
        }
    }
}
