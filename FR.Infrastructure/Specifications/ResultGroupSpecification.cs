using FR.Domain.Models;
using System;
using System.Linq.Expressions;

namespace FR.Infrastructure.Specifications
{
    public class ResultGroupSpecification : Specification<Result>
    {
        private string _name;
        public ResultGroupSpecification(string name)
        {
            _name = name;
        }

        public override Expression<Func<Result, bool>> ToExpression()
        {
            return result => string.IsNullOrEmpty(_name) ? true : result.Group.Name == _name;
        }
    }
}