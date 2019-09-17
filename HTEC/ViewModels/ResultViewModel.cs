using System;
using System.ComponentModel.DataAnnotations;

namespace HTEC.Api.ViewModels
{
    public class ResultViewModel
    {
        public string LeagueTitle { get; set; }
        public int Matchday { get; set; }
        public string Group { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm:ss}")]
        public DateTime KickoffAt { get; set; }
        //public string KickoffAt { get; set; }
        //public DateTime KickoffAtAsDT => DateTime.Parse(KickoffAt);
        public string Score { get; set; }
    }
}