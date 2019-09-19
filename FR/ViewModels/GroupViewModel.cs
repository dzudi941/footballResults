using FR.Api.Services;
using FR.Domain.Models;
using System.Collections.Generic;

namespace FR.Api.ViewModels
{
    public class GroupViewModel
    {
        public string LeagueTitle { get; set; }
        public int Matchday { get; set; }
        public string Group { get; set; }
        public IEnumerable<GroupItemViewModel> Standing { get; set; }

        public GroupViewModel() { }

        public GroupViewModel(Group group)
        {
            if (group != null)
            {
                var groupItems = CreateGroupItemsFromResults(group.Results);
                LeagueTitle = group.LeagueTitle;
                Group = group.Name;
                Matchday = ResultsService.GetMatchday(group.Results);
                Standing = groupItems;
            }
        }

        public GroupViewModel(string groupName, string leagueTitle, IEnumerable<Result> results)
        {
            var groupItems = CreateGroupItemsFromResults(results);
            LeagueTitle = leagueTitle;
            Group = groupName;
            Matchday = ResultsService.GetMatchday(results);
            Standing = groupItems;
        }

        private IEnumerable<GroupItemViewModel> CreateGroupItemsFromResults(IEnumerable<Result> results)
        {
            List<GroupItemViewModel> groupItemsVM = new List<GroupItemViewModel>();
            foreach (var result in results)
            {
                if (!groupItemsVM.Exists(x => x.Team == result.HomeTeam))
                {
                    GroupItemViewModel item = new GroupItemViewModel(result.HomeTeam);
                    groupItemsVM.Add(item);
                }
                if (!groupItemsVM.Exists(x => x.Team == result.AwayTeam))
                {
                    GroupItemViewModel item = new GroupItemViewModel(result.AwayTeam);
                    groupItemsVM.Add(item);
                }
            }

            foreach (var item in groupItemsVM)
            {
                item.PlayedGames = ResultsService.GetPlayedGames(item.Team, results);
                item.Goals = ResultsService.GetGoals(item.Team, results);
                item.GoalsAgainst = ResultsService.GetGoalsAgainst(item.Team, results);
                item.Win = ResultsService.GetWin(item.Team, results);
                item.Lose = ResultsService.GetLose(item.Team, results);
                item.Draw = ResultsService.GetDraw(item.Team, results);
            }

            groupItemsVM.Sort((a, b) => {
                if (a.Points > b.Points) return 1;
                else if (a.Points < b.Points) return -1;
                else if (a.Goals > b.Goals) return 1;
                else if (a.Goals < b.Goals) return -1;
                else if (a.GoalDifference > b.GoalDifference) return 1;
                else if (a.GoalDifference < b.GoalDifference) return -1;
                else return 0;
            });
            groupItemsVM.Reverse();

            for (int i = 0; i < groupItemsVM.Count; i++)
            {
                groupItemsVM[i].Rank = i + 1;
            }

            return groupItemsVM;
        }
    }
}