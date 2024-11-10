using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessAccessLayer
{
    public class BAL
    {
        DataAccessLayer.DAL newdata= new DataAccessLayer.DAL();
        ErrorLogs er = new ErrorLogs();


        public string insert_series(SeriesList seriesList)
        {
            try
            {

               
                return newdata.SaveSeries(seriesList);

            }
            catch(Exception ex)
            {
                er.LogError(ex, "Error in bal.insert_Series");
                throw;
            }
            finally
            {
                newdata = null;
            }
        }

        public string Update_Series(SeriesList seriesList)
        {
            try
            {


                return newdata.UpdateSeries(seriesList);

            }
            catch (Exception ex)
            {
                er.LogError(ex, "Error in bal.Update_Series");
                throw;
            }
            finally
            {
                newdata = null;
            }
        }

        public DataTable Search_Series(int? apiId, string name, string type, DateTime? start_date, DateTime? end_date)
        {
            try
            {
                return newdata.SearchSeries(apiId, name, type, start_date,end_date);

            }
            catch (Exception ex)
            {
                er.LogError(ex, "Error in bal.Search_Series");
                throw;
            }
        }

        public SeriesList PreloadSeries(int id, int api_id)
        {
            
            try
            {
               
                return newdata.GetSeriesById(id, api_id);
               
            }

            catch (Exception ex)
            {
                er.LogError(ex, "Error in bal.PreloadSeries");
                throw;
            }
           

        }

        public void Delete_Series(int id)
        {
            try
            {
                newdata.DeleteSeries(id);
            }
            catch (Exception ex)
            {
                er.LogError(ex, "Error in bal.DeleteSeries");
                throw;
            }

        }

        public DataTable Bind_Repeater()
        {
            try
            {
                return newdata.BindRepeater();
            }
            catch (Exception ex)
            {
                er.LogError(ex, "Error in bal.Bind_Repeater");
                throw;
            }
        }
    }


   
}
