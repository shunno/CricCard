using Repository.Pattern.EF6;
using System;
using System.Collections.Generic;

namespace Model
{
    public partial class Team:Entity
    {
        public Team()
        {
            this.Matches = new List<Match>();
            this.Matches1 = new List<Match>();
            this.Overs = new List<Over>();
        }

        public int TeamID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Match> Matches { get; set; }
        public virtual ICollection<Match> Matches1 { get; set; }
        public virtual ICollection<Over> Overs { get; set; }
    }
}
