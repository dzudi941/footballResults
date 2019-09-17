using System.ComponentModel;

namespace HTEC.Api.ViewModels
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
        public int Points { get; set; }
        [DisplayName("goals")]
        public int Goals { get; set; }
        [DisplayName("goalsAgainst")]
        public int GoalsAgainst { get; set; }
        [DisplayName("goalDifference")]
        public int GoalDifference { get; set; }
        [DisplayName("win")]
        public int Win { get; set; }
        [DisplayName("lose")]
        public int Lose { get; set; }
        [DisplayName("draw")]
        public int Draw { get; set; }
    }
}