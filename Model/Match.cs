using Repository.Pattern.EF6;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Model
{
    public partial class Match:Entity
    {
        public Match()
        {
            this.Overs = new List<Over>();
        }

        public int MatchID { get; set; }
        public string Name { get; set; }
        public int Team1ID { get; set; }
        public int Team2ID { get; set; }
        public bool IsTeam1Bowl { get; set; }
        public bool IsTeam2Bowl { get; set; }
        public virtual Team Team1 { get; set; }
        public virtual Team Team2 { get; set; }
        public virtual ICollection<Over> Overs { get; set; }


        #region NotMapped
        public string FirstTeamName { get; set; }
        public string SecondTeamName { get; set; }

        public  int TotalRun { get; set; }
        #endregion
    }
}
