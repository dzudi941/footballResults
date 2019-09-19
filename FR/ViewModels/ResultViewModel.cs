using System;
using System.ComponentModel.DataAnnotations;
using FR.Domain.Models;

namespace FR.Api.ViewModels
{
    public class ResultViewModel
    {
        private int _homeTeamGoals;
        private int _awayTeamGoals;
        public int Id { get; set; }
        public string LeagueTitle { get; set; }
        public int Matchday { get; set; }
        public string Group { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm:ss}")]
        public DateTime KickoffAt { get; set; }
        public string Score
        {
            get
            {
                return $"{_homeTeamGoals}:{_awayTeamGoals}";
            }
            set
            {
                string[] scores = value.Split(':');
                if(scores.Length > 0) int.TryParse(scores[0], out _homeTeamGoals);
                if (scores.Length > 1) int.TryParse(scores[1], out _awayTeamGoals);
            }
        }

        public int HomeTeamGoals() => _homeTeamGoals;
        public int AwayTeamGoals() => _awayTeamGoals;


        public ResultViewModel() { }

        public ResultViewModel(Result result)
        {
            Id = result.Id;
            LeagueTitle = result.LeagueTitle;
            Matchday = result.Matchday;
            Group = result.Group.Name;
            HomeTeam = result.HomeTeam;
            AwayTeam = result.AwayTeam;
            KickoffAt = result.KickoffAt;
            _homeTeamGoals = result.HomeTeamGoals;
            _awayTeamGoals = result.AwayTeamGoals;
        }
    }
}