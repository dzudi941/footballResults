using System;

namespace FR.Domain.Models
{
    public class Result
    {
        public int Id { get; set; }
        public string LeagueTitle { get; set; }
        public int Matchday { get; set; }
        public Group Group { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public DateTime KickoffAt { get; set; }
        public int HomeTeamGoals { get; set; }
        public int AwayTeamGoals { get; set; }
    }
}
