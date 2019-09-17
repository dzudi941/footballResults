using HTEC.Domain.Models;
using System.Collections.Generic;
using System.ComponentModel;

namespace HTEC.Api.ViewModels
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
    }
}
