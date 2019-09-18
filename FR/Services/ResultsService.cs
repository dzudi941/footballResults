using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FR.Api.ViewModels;
using FR.Domain.Interfaces;
using FR.Domain.Models;

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

        //internal void Add(int groupId, ResultViewModel resultVM)
        //{
        //    throw new NotImplementedException();
        //}

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
    }
}
