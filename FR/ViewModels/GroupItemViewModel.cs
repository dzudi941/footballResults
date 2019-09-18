using System.ComponentModel;

namespace FR.Api.ViewModels
{
    public class GroupItemViewModel
    {
        [DisplayName("rank")]
        public int Rank { get; set; }
        [DisplayName("team")]
        public string Team { get; set; }
        [DisplayName("playedGames")]
        public int PlayedGames { get; set; }
        [DisplayName("points")]
        public int Points => Win * 3 + Draw;
        [DisplayName("goals")]
        public int Goals { get; set; }
        [DisplayName("goalsAgainst")]
        public int GoalsAgainst { get; set; }
        [DisplayName("goalDifference")]
        public int GoalDifference => Goals - GoalsAgainst;
        [DisplayName("win")]
        public int Win { get; set; }
        [DisplayName("lose")]
        public int Lose { get; set; }
        [DisplayName("draw")]
        public int Draw { get; set; }
    }
}