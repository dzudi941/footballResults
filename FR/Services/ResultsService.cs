using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly IRepository<Team> _teamRepository;

        public ResultsService(IRepository<Result> resultsRepository, IRepository<Group> groupRepository, IRepository<Team> teamRepository)
        {
            _resultsRepository = resultsRepository;
            _groupRepository = groupRepository;
            _teamRepository = teamRepository;
        }

        internal void AddResult(int groupId, int homeTeamId, int awayTeamId, ResultViewModel resultVM)
        {
            string[] score = resultVM.Score.Split(':');
            int homeTeamGoals = int.Parse(score[0]);
            int awayTeamGoals = int.Parse(score[1]);

            Result result = new Result
            {
                LeagueTitle = resultVM.LeagueTitle,
                Matchday = resultVM.Matchday,
                Group = _groupRepository.Get(groupId),
                HomeTeam = _teamRepository.Get(homeTeamId),
                AwayTeam = _teamRepository.Get(awayTeamId),
                KickoffAt = resultVM.KickoffAt,
                HomeTeamGoals = homeTeamGoals,
                AwayTeamGoals = awayTeamGoals
            };

            _resultsRepository.Add(result);
        }

        internal IEnumerable<GroupViewModel> Filter(FilterViewModel filter)
        {
            IEnumerable<Result> results = _resultsRepository
                .Find(new ResultDateRangeSpecification(filter.Start, filter.End)
                .And(new ResultGroupSpecification(filter.Group))
                .And(new ResultTeamSpecification(filter.Team)));

            return results.GroupBy(x=> x.Group).Select(x => new GroupViewModel(x.Key.Name, x.Key.LeagueTitle, x.ToList()));
        }

        public void Update(int id, ResultViewModel resultVM)
        {
            Result result = _resultsRepository.Get(id);
            result.LeagueTitle = resultVM.LeagueTitle;
            result.Matchday = resultVM.Matchday;
            result.Group = _groupRepository.Find(new GroupSpecification(resultVM.Group)).First();
            result.HomeTeam = _teamRepository.Find(new TeamSpecification(resultVM.HomeTeam)).First();
            result.AwayTeam = _teamRepository.Find(new TeamSpecification(resultVM.AwayTeam)).First();
            result.KickoffAt = resultVM.KickoffAt;
            result.HomeTeamGoals = resultVM.HomeTeamGoals;
            result.AwayTeamGoals = resultVM.AwayTeamGoals;

            _resultsRepository.Update(result);
        }

        public void Update(List<int> ids, List<ResultViewModel> resultsVM)
        {
            if (ids.Count != resultsVM.Count) return;

            for (int i = 0; i < ids.Count; i++)
            {
                Update(ids[i], resultsVM[i]);
            }
        }

        public IEnumerable<ResultViewModel> Get()
        {
            return _resultsRepository.Get().Select(x => new ResultViewModel(x));
        }

        public ResultViewModel Get(int id)
        {
            return new ResultViewModel(_resultsRepository.Get(id));
        }

        public void Delete(int id)
        {
            _resultsRepository.Remove(_resultsRepository.Get(id));
        }
    }
}