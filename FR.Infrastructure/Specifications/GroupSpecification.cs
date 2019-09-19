using FR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FR.Infrastructure.Specifications
{
    public class GroupSpecification : Specification<Group>
    {
        private readonly string _name;

        public GroupSpecification(string name)
        {
            _name = name;
        }

        public override Expression<Func<Group, bool>> ToExpression()
        {
            return group => group.Name == _name;
        }
    }
}