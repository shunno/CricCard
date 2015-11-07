using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helper;
using Model;
using Model.ViewModel;
using Repository.Pattern.Ef6;

namespace Data.Query
{
    public class OverSearchQuery : QueryObject<Over>
    {
       // public OverSearchQuery SearchOvers(OverViewModel searchViewModel)
        //{
            //if (searchViewModel.KeyWord.IsNotNullOrEmpty())
            //{
            //    And(
            //        item =>
            //            item.Name.Contains(searchViewModel.KeyWord) ||
            //            item.Designation.Contains(searchViewModel.KeyWord));
            //}
            //if (searchViewModel.CityId!=null)
            //{
            //    And(item => item.CityId == searchViewModel.CityId.Value);
            //}
            //if (searchViewModel.CategoryId !=null)
            //{
            //    And(item => item.DoctorCategoryId == searchViewModel.CategoryId.Value);
            //}
            ////SELECT * FROM Places WHERE acos(sin(1.3963) * sin(Lat) + cos(1.3963) * cos(Lat) * cos(Lon - (-0.6981))) * 6371 <= 1000;
            //if (searchViewModel.Latitude != null && searchViewModel.Longitude !=null)
            //{
            //    And(item => SqlFunctions.Acos(SqlFunctions.Sin(searchViewModel.Latitude.Value) 
            //                                * SqlFunctions.Sin(item.Latitude.Value)
            //                                + SqlFunctions.Cos(searchViewModel.Latitude.Value) 
            //                                * SqlFunctions.Cos(searchViewModel.Latitude.Value)
            //                                 * SqlFunctions.Cos(item.Latitude.Value) 
            //                                 * SqlFunctions.Cos (item.Longitude.Value
            //                                 - (searchViewModel.Longitude.Value))) * 6371 <= 1000);
            //}
           // return this;
       // }


    }
}
