using FR.Domain.Models;
using System;
using System.Linq.Expressions;

namespace FR.Infrastructure.Specifications
{
    public class ResultTeamSpecification : Specification<Result>
    {
        private readonly string _name;

        public ResultTeamSpecification(string name)
        {
            _name = name;
        }

        public override Expression<Func<Result, bool>> ToExpression()
        {
            return result => string.IsNullOrEmpty(_name) ? true : (result.HomeTeam.Name == _name || result.AwayTeam.Name == _name); 
        }
    }
}
