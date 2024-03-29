﻿using System.Collections.Generic;

namespace FR.Domain.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Result> Results { get; set; }
        public string LeagueTitle { get; set; }
    }
}
