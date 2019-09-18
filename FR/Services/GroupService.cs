using FR.Api.ViewModels;
using FR.Domain.Interfaces;
using FR.Domain.Models;
using FR.Infrastructure.Specifications;
using System.Collections.Generic;
using System.Linq;

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
            Group group = _groupRepository.Find(new GroupSpecification(groupName)).FirstOrDefault();

            return CreateGroupVM(group);
        }

        public IEnumerable<GroupViewModel> GetTables()
        {
            List<GroupViewModel> groupsVM = new List<GroupViewModel>();
            var groups = _groupRepository.Get();

            return groups.Select(x => CreateGroupVM(x));
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
            return _groupRepository.Find(new GroupSpecification(name)).First().Id;
        }

        private GroupViewModel CreateGroupVM(Group group)
        {
            if (group == null) return null;

            var groupItems = CreateTableItemsFromResults(group.Results);
            GroupViewModel groupVM = new GroupViewModel
            {
                LeagueTitle = group.LeagueTitle,
                Group = group.Name,
                Matchday = GetMatchday(group.Results),
                Standing = groupItems
            };

            return groupVM;
        }

        private int GetMatchday(ICollection<Result> results)
        {
            return results.Max(x => x.Matchday);
        }

        private IEnumerable<GroupItemViewModel> CreateTableItemsFromResults(ICollection<Result> results)
        {
            List<GroupItemViewModel> items = new List<GroupItemViewModel>();
            foreach (var result in results)
            {
                GroupItemViewModel item = null;
                if (!items.Exists(x=>x.Team == result.HomeTeam.Name))
                {
                    item = new GroupItemViewModel { Team = result.HomeTeam.Name };
                }
                else if (!items.Exists(x => x.Team == result.AwayTeam.Name))
                {
                    item = new GroupItemViewModel { Team = result.AwayTeam.Name };
                }

                if (item != null) items.Add(item);
            }

            foreach (var item in items)
            {
                item.PlayedGames = GetPlayedGames(item.Team, results);
                item.Goals = GetGoals(item.Team, results);
                item.GoalsAgainst = GetGoalsAgainst(item.Team, results);
                item.Win = GetWin(item.Team, results);
                item.Lose = GetLose(item.Team, results);
                item.Draw = GetDraw(item.Team, results);
            }
            items.Sort((a, b) => {
                if (a.Points > b.Points) return 1;
                else if (a.Points < b.Points) return -1;
                else if (a.Goals > b.Goals) return 1;
                else if (a.Goals < b.Goals) return -1;
                else if (a.GoalDifference > b.GoalDifference) return 1;
                else if (a.GoalDifference < b.GoalDifference) return -1;
                else return 0;
            });

            for (int i = 0; i < items.Count; i++)
            {
                items[i].Rank = i + 1;
            }

            return items;
        }

        private int GetGoalsAgainst(string team, ICollection<Result> results)
        {
            int homeGoalsAgainst = results.Where(x => x.HomeTeam.Name == team).Sum(x => x.AwayTeamGoals);
            int awayGoalsAgainst = results.Where(x => x.AwayTeam.Name == team).Sum(x => x.HomeTeamGoals);

            return homeGoalsAgainst + awayGoalsAgainst;
        }

        private int GetDraw(string team, ICollection<Result> results)
        {
            int drawHome = results.Count(x => x.HomeTeam.Name == team && x.HomeTeamGoals == x.AwayTeamGoals);
            int drawAway = results.Count(x => x.AwayTeam.Name == team && x.AwayTeamGoals == x.HomeTeamGoals);

            return drawHome + drawAway;
        }

        private int GetLose(string team, ICollection<Result> results)
        {
            int loseHome = results.Count(x => x.HomeTeam.Name == team && x.HomeTeamGoals < x.AwayTeamGoals);
            int loseAway = results.Count(x => x.AwayTeam.Name == team && x.AwayTeamGoals < x.HomeTeamGoals);

            return loseHome + loseAway;
        }

        private int GetWin(string team, ICollection<Result> results)
        {
            int winHome = results.Count(x => x.HomeTeam.Name == team && x.HomeTeamGoals > x.AwayTeamGoals);
            int winAway = results.Count(x => x.AwayTeam.Name == team && x.AwayTeamGoals > x.HomeTeamGoals);

            return winHome + winAway;
        }

        private int GetPlayedGames(string team, ICollection<Result> results)
        {
            int playedGamesHome = results.Count(x => x.HomeTeam.Name == team);
            int playedGamesAway = results.Count(x => x.AwayTeam.Name == team);

            return playedGamesHome + playedGamesAway;
        }

        private int GetGoals(string team, ICollection<Result> results)
        {
            int homeGoals = results.Where(x => x.HomeTeam.Name == team).Sum(x => x.HomeTeamGoals);
            int awayGoals = results.Where(x => x.AwayTeam.Name == team).Sum(x => x.AwayTeamGoals);

            return homeGoals + awayGoals;
        }
    }
}