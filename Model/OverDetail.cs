using Repository.Pattern.EF6;
using System;
using System.Collections.Generic;

namespace Model
{
    public partial class OverDetail : Entity
    {
        public int OverDetailsID { get; set; }
        public int OverID { get; set; }
        public int BallNumber { get; set; }
        public string Description { get; set; }
        public int RunTaken { get; set; }
        public bool IsWide { get; set; }
        public int BallIndex { get; set; }
        public virtual Over Over { get; set; }


        #region NotMapped

        public  int OverNumber { get; set; }
        #endregion
    }
}
