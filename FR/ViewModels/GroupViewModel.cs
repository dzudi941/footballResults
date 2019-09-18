using FR.Domain.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace FR.Api.ViewModels
{
    public class GroupViewModel
    {
        [DisplayName("leagueTitle")]
        public string LeagueTitle { get; set; }
        [DisplayName("matchday")]
        public int Matchday { get; set; }
        [DisplayName("group")]
        public string Group { get; set; }
        [DisplayName("standing")]
        public IEnumerable<GroupItemViewModel> Standing { get; set; }

        public GroupViewModel(Group group)
        {
            if (group != null)
            {
                var groupItems = CreateTableItemsFromResults(group.Results);
                LeagueTitle = group.LeagueTitle;
                Group = group.Name;
                Matchday = GetMatchday(group.Results);
                Standing = groupItems;
            }
        }

        public GroupViewModel(string groupName, string leagueTitle, ICollection<Result> results)
        {
            var groupItems = CreateTableItemsFromResults(results);
            LeagueTitle = leagueTitle;
            Group = groupName;
            Matchday = GetMatchday(results);
            Standing = groupItems;
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
                if (!items.Exists(x => x.Team == result.HomeTeam))
                {
                    item = new GroupItemViewModel { Team = result.HomeTeam };
                }
                else if (!items.Exists(x => x.Team == result.AwayTeam))
                {
                    item = new GroupItemViewModel { Team = result.AwayTeam };
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
            int homeGoalsAgainst = results.Where(x => x.HomeTeam == team).Sum(x => x.AwayTeamGoals);
            int awayGoalsAgainst = results.Where(x => x.AwayTeam == team).Sum(x => x.HomeTeamGoals);

            return homeGoalsAgainst + awayGoalsAgainst;
        }

        private int GetDraw(string team, ICollection<Result> results)
        {
            int drawHome = results.Count(x => x.HomeTeam == team && x.HomeTeamGoals == x.AwayTeamGoals);
            int drawAway = results.Count(x => x.AwayTeam == team && x.AwayTeamGoals == x.HomeTeamGoals);

            return drawHome + drawAway;
        }

        private int GetLose(string team, ICollection<Result> results)
        {
            int loseHome = results.Count(x => x.HomeTeam == team && x.HomeTeamGoals < x.AwayTeamGoals);
            int loseAway = results.Count(x => x.AwayTeam == team && x.AwayTeamGoals < x.HomeTeamGoals);

            return loseHome + loseAway;
        }

        private int GetWin(string team, ICollection<Result> results)
        {
            int winHome = results.Count(x => x.HomeTeam == team && x.HomeTeamGoals > x.AwayTeamGoals);
            int winAway = results.Count(x => x.AwayTeam == team && x.AwayTeamGoals > x.HomeTeamGoals);

            return winHome + winAway;
        }

        private int GetPlayedGames(string team, ICollection<Result> results)
        {
            int playedGamesHome = results.Count(x => x.HomeTeam == team);
            int playedGamesAway = results.Count(x => x.AwayTeam == team);

            return playedGamesHome + playedGamesAway;
        }

        private int GetGoals(string team, ICollection<Result> results)
        {
            int homeGoals = results.Where(x => x.HomeTeam == team).Sum(x => x.HomeTeamGoals);
            int awayGoals = results.Where(x => x.AwayTeam == team).Sum(x => x.AwayTeamGoals);

            return homeGoals + awayGoals;
        }
    }
}
