using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Runtime.InteropServices.ComTypes;


namespace DataAccessLayer
{
    public class DAL
    {
        string dataconfig = ConfigurationManager.ConnectionStrings["cs"].ToString();
        public SqlConnection conn;
        public SqlCommand cmd;
        public string SaveSeries(SeriesList series_details)
        {
           
                conn = new SqlConnection(dataconfig);
                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "prdTbLSeriesInsert";
                conn.Open();


                try
                {

                    cmd.Parameters.AddWithValue("@SeriesName", series_details.SeriesName);
                    cmd.Parameters.AddWithValue("@SeriesType", series_details.SeriesType);
                    cmd.Parameters.AddWithValue("@SeriesStatus", series_details.SeriesStatus);
                    cmd.Parameters.AddWithValue("@MatchStatus", series_details.MatchStatus);
                    cmd.Parameters.AddWithValue("@MatchFormat", series_details.MatchFormat);
                    cmd.Parameters.AddWithValue("@SeriesMatchType", series_details.SeriesMatchtype);
                    cmd.Parameters.AddWithValue("@Gender", series_details.Gender);
                    cmd.Parameters.AddWithValue("@SeriesYear", series_details.Year);
                    cmd.Parameters.AddWithValue("@TrophyType", series_details.TrophyType);
                    cmd.Parameters.AddWithValue("@SeriesStartDate",series_details.startdate);
                    cmd.Parameters.AddWithValue("@SeriesEndDate", series_details.enddate);
                    cmd.Parameters.AddWithValue("@IsActive", series_details.IsActive);
                    cmd.Parameters.AddWithValue("@SeriesDescription", series_details.Description);
                    return cmd.ExecuteNonQuery().ToString();

                }
                catch (Exception ex)
                {
                        ErrorLogs er = new ErrorLogs();
                        er.LogError(ex, "Error in Data.SaveSeries");
                        throw;
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            

        }

        public string UpdateSeries(SeriesList series_det)
        {

            conn = new SqlConnection(dataconfig);
            cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "prdTbLSeriesUpdate";
            conn.Open();

            try
            {

                cmd.Parameters.AddWithValue("@SeriesID", series_det.SeriesID);
                cmd.Parameters.AddWithValue("@SeriesAPI_ID", series_det.SeriesAPI_ID);
                cmd.Parameters.AddWithValue("@SeriesName", series_det.SeriesName);
                cmd.Parameters.AddWithValue("@SeriesType", series_det.SeriesType);
                cmd.Parameters.AddWithValue("@SeriesStatus", series_det.SeriesStatus);
                cmd.Parameters.AddWithValue("@MatchStatus", series_det.MatchStatus);
                cmd.Parameters.AddWithValue("@MatchFormat", series_det.MatchFormat);
                cmd.Parameters.AddWithValue("@SeriesMatchType", series_det.SeriesMatchtype);
                cmd.Parameters.AddWithValue("@Gender", series_det.Gender);
                cmd.Parameters.AddWithValue("@SeriesYear", series_det.Year);
                cmd.Parameters.AddWithValue("@TrophyType", series_det.TrophyType);
                cmd.Parameters.AddWithValue("@SeriesStartDate", series_det.startdate);
                cmd.Parameters.AddWithValue("@SeriesEndDate", series_det.enddate);
                cmd.Parameters.AddWithValue("@IsActive", series_det.IsActive);
                cmd.Parameters.AddWithValue("@SeriesDescription", series_det.Description);
                return cmd.ExecuteNonQuery().ToString();

            }
            catch (Exception ex)
            {
                ErrorLogs er = new ErrorLogs();
                er.LogError(ex, "Error in Data.UpdateSeries");
                throw;
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
        }

        public SeriesList GetSeriesById(int SeriesID, int SeriesAPI_ID)
        {
           
            SeriesList series = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(dataconfig))
                {
                    connection.Open();

                    

                    string query = "SELECT * FROM tbl_Series WHERE SeriesID = @SeriesID and SeriesAPI_ID= @SeriesAPI_ID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SeriesID", SeriesID);
                        command.Parameters.AddWithValue("@SeriesAPI_ID", SeriesAPI_ID);
                      

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int sdateindex = reader.GetOrdinal("SeriesStartDate");
                            int edateindex = reader.GetOrdinal("SeriesEndDate");


                            if (reader.Read())
                            {
                              
                            series = new SeriesList
                                {
                                    SeriesName = reader["SeriesName"].ToString(),
                                    SeriesType = reader["SeriesType"].ToString(),
                                    SeriesStatus = reader["SeriesStatus"].ToString(),
                                    MatchStatus = reader["MatchStatus"].ToString(),
                                    MatchFormat = reader["MatchFormat"].ToString(),
                                    SeriesMatchtype = Convert.ToInt32(reader["SeriesMatchType"].ToString()),
                                    Gender = Convert.ToInt32(reader["Gender"].ToString()),
                                    Year = reader["SeriesYear"].ToString(),
                                    TrophyType = Convert.ToInt32(reader["TrophyType"].ToString()),
                                    startdate = (DateTime?)reader["SeriesStartDate"],
                                    enddate = (DateTime?)reader["SeriesEndDate"],
                                    IsActive = Convert.ToInt32(reader["IsActive"].ToString()),
                                    Description = reader["SeriesDescription"].ToString()
                                };
                            }
                        }
                    }

                }
            }

            catch (Exception ex)
            {
                ErrorLogs er = new ErrorLogs();
                er.LogError(ex, "Error in Fetching Record by ID");
                throw;

            }
            return series;
            
        }

        public DataTable SearchSeries(int? id, string name, string type, DateTime? st_date, DateTime? end_date)
        {

            DataTable dataTable = new DataTable();

            using (conn = new SqlConnection(dataconfig))
            {
                try
                {
                    conn.Open();
                    cmd = new SqlCommand("prdTbLSeriesSearch", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@seriesAPI_ID", (object)id ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@SeriesName", string.IsNullOrEmpty(name) ? (object)DBNull.Value : name);
                    cmd.Parameters.AddWithValue("@SeriesType", string.IsNullOrEmpty(type) ? (object)DBNull.Value : type);
                    cmd.Parameters.AddWithValue("@SeriesStartDate", st_date ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@SeriesEndDate", end_date ?? (object)DBNull.Value);


                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    ErrorLogs er = new ErrorLogs();
                    er.LogError(ex, "Error in Data.SearchSeries");
                    throw;
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }

            return dataTable;
        }

        public void DeleteSeries(int id)
        {

            using (SqlConnection conn = new SqlConnection(dataconfig))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM tbl_Series WHERE SeriesID = @SeriesID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SeriesID", id);
                        cmd.ExecuteNonQuery();
                    }
                }

                catch (Exception ex)
                {
                    ErrorLogs er = new ErrorLogs();
                    er.LogError(ex, "Error in Data.DeleteSeries");
                    throw;
                }
                

            }
        }

        public DataTable BindRepeater()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(dataconfig))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT SeriesID, SeriesAPI_ID, SeriesName, SeriesType, SeriesMatchType, Gender, SeriesYear ,TrophyType, SeriesStartDate,SeriesEndDate FROM tbl_Series";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        adapter.Fill(dt);

                    }
                }
                catch (Exception ex)
                {
                    ErrorLogs er = new ErrorLogs();
                    er.LogError(ex, "Error in Data.BindRepeater");
                    throw;
                }
                finally
                {
                    conn.Close();
                }

            }
            return dt;
        }

    }
        
        
    
}


