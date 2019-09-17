using System.Collections.Generic;

namespace HTEC.Domain.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Result> Results { get; set; }
        //public Tournament Tournament { get; set; }
        public string LeagueTitle { get; set; }
    }
}
