using Repository.Pattern.EF6;
using System;
using System.Collections.Generic;

namespace Model
{
    public partial class Over:Entity
    {
        public Over()
        {
            this.OverDetails = new List<OverDetail>();
        }

        public int OverID { get; set; }
        public int MatchID { get; set; }
        public int TeamID { get; set; }
        public int OverNumber { get; set; }
        public virtual Match Match { get; set; }
        public virtual Team Team { get; set; }
        public virtual ICollection<OverDetail> OverDetails { get; set; }
    }
}
