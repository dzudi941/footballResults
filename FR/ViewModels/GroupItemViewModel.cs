namespace FR.Api.ViewModels
{
    public class GroupItemViewModel
    {
        public int Rank { get; set; }
        public string Team { get; set; }
        public int PlayedGames { get; set; }
        public int Points => Win * 3 + Draw;
        public int Goals { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalDifference => Goals - GoalsAgainst;
        public int Win { get; set; }
        public int Lose { get; set; }
        public int Draw { get; set; }

        public GroupItemViewModel(string teamName)
        {
            Team = teamName;
        }
    }
}