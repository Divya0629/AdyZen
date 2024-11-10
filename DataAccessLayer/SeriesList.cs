using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class SeriesList
    {
        public int? SeriesAPI_ID=null;

        public int? SeriesID;
        public string SeriesName { set; get; }
        public string SeriesType { set; get; }
        public string SeriesStatus { set; get; }
        public string MatchStatus { set; get; }
        public string MatchFormat { set; get; }
        public int SeriesMatchtype { set; get; }
        public int Gender { set; get; }
        public string Year { set; get; }
        public int TrophyType { set; get; }
        public DateTime? startdate { set; get; }
        public DateTime? enddate { set; get; }
        public int IsActive { set; get; } 
        public string Description { set; get; }


    }
}
