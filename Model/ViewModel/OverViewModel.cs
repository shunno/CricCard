using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class OverDetailViewModel
    {

        public int MatchID { get; set; }
        public int TeamID { get; set; }
        public int OverNumber { get; set; }
        public int BallNumber { get; set; }
        public int RunTaken { get; set; }
        public  string Description { get; set; }

        public  int TotalRun { get; set; }
    }
}
