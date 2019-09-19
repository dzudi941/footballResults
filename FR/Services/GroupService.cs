using FR.Api.ViewModels;
using FR.Domain.Interfaces;
using FR.Domain.Models;
using FR.Infrastructure.Specifications;
using System.Collections.Generic;
using System.Linq;

namespace FR.Api.Services
{
    public class GroupService
    {
        private IRepository<Group> _groupRepository;

        public GroupService(IRepository<Group> groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public GroupViewModel Get(string groupName)
        {
            Group group = _groupRepository.Find(new GroupSpecification(groupName), x=> x.Results).FirstOrDefault();
            if (group == null) return null;

            return new GroupViewModel(group);
        }

        public IEnumerable<GroupViewModel> Get()
        {
            List<GroupViewModel> groupsVM = new List<GroupViewModel>();
            var groups = _groupRepository.Get(x=> x.Results);

            return groups.Select(x => new GroupViewModel(x));
        }

        public int AddGroup(string name, string leagueName)
        {
            Group group = _groupRepository.Find(new GroupSpecification(name)).FirstOrDefault();
            if (group != null) return group.Id;

            Group newGroup = new Group
            {
                Name = name,
                LeagueTitle = leagueName
            };

            _groupRepository.Add(newGroup);
            return newGroup.Id;
        }
    }
}