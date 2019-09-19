using System.Collections.Generic;
using System.Linq;
using FR.Api.ViewModels;
using FR.Domain.Interfaces;
using FR.Domain.Models;
using FR.Infrastructure.Specifications;

namespace FR.Api.Services
{
    public class ResultsService
    {
        private readonly IRepository<Result> _resultsRepository;
        private readonly IRepository<Group> _groupRepository;

        public ResultsService(IRepository<Result> resultsRepository, IRepository<Group> groupRepository)
        {
            _resultsRepository = resultsRepository;
            _groupRepository = groupRepository;
        }

        internal int AddResult(int groupId, ResultViewModel resultVM)
        {
            string[] score = resultVM.Score.Split(':');
            int homeTeamGoals = int.Parse(score[0]);
            int awayTeamGoals = int.Parse(score[1]);

            Result result = new Result
            {
                LeagueTitle = resultVM.LeagueTitle,
                Matchday = resultVM.Matchday,
                Group = _groupRepository.Get(groupId),
                HomeTeam = resultVM.HomeTeam,
                AwayTeam = resultVM.AwayTeam,
                KickoffAt = resultVM.KickoffAt,
                HomeTeamGoals = homeTeamGoals,
                AwayTeamGoals = awayTeamGoals
            };

            return _resultsRepository.Add(result);
        }

        internal IEnumerable<ResultViewModel> Filter(FilterViewModel filter)
        {
            IEnumerable<Result> results = _resultsRepository
                .Find(new ResultDateRangeSpecification(filter.Start, filter.End)
                .And(new ResultGroupSpecification(filter.Group))
                .And(new ResultTeamSpecification(filter.Team)),
                x=> x.Group);

            var aa = results.ToList();
            var bb = results.GroupBy(x => x.Group);

            return results.Select(x => new ResultViewModel(x));
        }

        public bool Update(int id, int groupId, ResultViewModel resultVM)
        {
            Result result = _resultsRepository.Get(id);
            if (result == null) return false;

            result.LeagueTitle = resultVM.LeagueTitle;
            result.Matchday = resultVM.Matchday;
            result.Group = _groupRepository.Get(groupId);
            result.HomeTeam = resultVM.HomeTeam;
            result.AwayTeam = resultVM.AwayTeam;
            result.KickoffAt = resultVM.KickoffAt;
            result.HomeTeamGoals = resultVM.HomeTeamGoals();
            result.AwayTeamGoals = resultVM.AwayTeamGoals();

            return _resultsRepository.Update(result) > 0;
        }

        public bool Update(List<ResultViewModel> resultsVM)
        {
            bool updated = true;
            foreach (var resultVM in resultsVM)
            {
                var group = _groupRepository.Find(new GroupSpecification(resultVM.Group)).FirstOrDefault();
                if (group == null)
                {
                    group = new Group { Name = resultVM.Group, LeagueTitle = resultVM.LeagueTitle };
                    _groupRepository.Add(group);
                }
                updated = updated && Update(resultVM.Id, group.Id, resultVM);
            }

            return updated;
        }

        public IEnumerable<ResultViewModel> Get()
        {
            return _resultsRepository.Get(x => x.Group).Select(x => new ResultViewModel(x));
        }

        public ResultViewModel Get(int id)
        {
            Result result = _resultsRepository.Get(id, x => x.Group);
            if (result == null) return null;

            return new ResultViewModel(result);
        }

        public bool Delete(int id)
        {
            var result = _resultsRepository.Get(id);
            if (result == null) return false;

            return _resultsRepository.Remove(result) > 0;
        }

        #region Static methods
        public static int GetMatchday(IEnumerable<Result> results)
        {
            if (results.Count() == 0) return 0;

            return results.Max(x => x.Matchday);
        }

        public static int GetGoalsAgainst(string team, IEnumerable<Result> results)
        {
            int homeGoalsAgainst = results.Where(x => x.HomeTeam == team).Sum(x => x.AwayTeamGoals);
            int awayGoalsAgainst = results.Where(x => x.AwayTeam == team).Sum(x => x.HomeTeamGoals);

            return homeGoalsAgainst + awayGoalsAgainst;
        }

        public static int GetDraw(string team, IEnumerable<Result> results)
        {
            int drawHome = results.Count(x => x.HomeTeam == team && x.HomeTeamGoals == x.AwayTeamGoals);
            int drawAway = results.Count(x => x.AwayTeam == team && x.AwayTeamGoals == x.HomeTeamGoals);

            return drawHome + drawAway;
        }

        public static int GetLose(string team, IEnumerable<Result> results)
        {
            int loseHome = results.Count(x => x.HomeTeam == team && x.HomeTeamGoals < x.AwayTeamGoals);
            int loseAway = results.Count(x => x.AwayTeam == team && x.AwayTeamGoals < x.HomeTeamGoals);

            return loseHome + loseAway;
        }

        public static int GetWin(string team, IEnumerable<Result> results)
        {
            int winHome = results.Count(x => x.HomeTeam == team && x.HomeTeamGoals > x.AwayTeamGoals);
            int winAway = results.Count(x => x.AwayTeam == team && x.AwayTeamGoals > x.HomeTeamGoals);

            return winHome + winAway;
        }

        public static int GetPlayedGames(string team, IEnumerable<Result> results)
        {
            int playedGamesHome = results.Count(x => x.HomeTeam == team);
            int playedGamesAway = results.Count(x => x.AwayTeam == team);

            return playedGamesHome + playedGamesAway;
        }

        public static int GetGoals(string team, IEnumerable<Result> results)
        {
            int homeGoals = results.Where(x => x.HomeTeam == team).Sum(x => x.HomeTeamGoals);
            int awayGoals = results.Where(x => x.AwayTeam == team).Sum(x => x.AwayTeamGoals);

            return homeGoals + awayGoals;
        }
        #endregion
    }
}