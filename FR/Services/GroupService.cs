using System;
using System.Collections.Generic;
using System.Linq;
using FR.Api.ViewModels;
using FR.Domain.Interfaces;
using FR.Domain.Models;
using FR.Infrastructure.Specifications;

namespace FR.Api.Services
{
    public class GroupService : IGroupService
    {
        private IRepository<Group> _groupRepository;

        public GroupService(IRepository<Group> groupRepository)
        {
            _groupRepository = groupRepository;

        }

        public GroupViewModel GetTable(string groupName)
        {
            throw new NotImplementedException();
            //Group group = _groupRepository.Find(new GroupSpecification(groupName)).FirstOrDefault();
            //if(group != null)
            //{

            //}
        }

        public IEnumerable<GroupViewModel> GetTables()
        {
            throw new NotImplementedException();
            //var groups = _groupRepository.Get();
            //foreach (var group in groups)
            //{
            //    var groupItems = CreateViewModelsFromResults(group.Results);
            //}
        }

        //private IEnumerable<GroupItemViewModel> CreateViewModelsFromResults(ICollection<Result> results)
        //{
        //    results.
        //}
    }
}
