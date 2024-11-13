using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdyZen
{
    public partial class ReportGen : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.HttpMethod!= "POST")
            {
                LoadData();
            }

        }

        

        protected void btnGenerateReport_Click(object sender, EventArgs e)
        {

            List<string> selectedValues = lstSelectedValues.Items.Cast<ListItem>()
                                        .Where(i => i.Selected)
                                        .Select(i => i.Value)
                                        .ToList();


            if (selectedValues.Count > 0)
            {

                DataTable matrixData = TransformToMatrix(GenerateReport(selectedValues), selectedValues);

                gvReport.DataSource = matrixData;
                gvReport.DataBind();
            }

        }
        private DataTable GenerateReport(List<string> selectedValues)
        {

            string query = @"
            SELECT distinct SeriesYear,  MatchFormat, Gender, COUNT(*) AS SeriesCount
            FROM tbl_Series
            WHERE SeriesYear IN ({0})
            GROUP BY SeriesYear,  MatchFormat, Gender
            Order by SeriesYear, MatchFormat";

            DataTable dt = new DataTable();




            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();

                        string[] paramNames = selectedValues.Select(
                        (s, i) => "@tag" + i.ToString()
                        ).ToArray();

                        string inClause = string.Join(",", paramNames);
                        using (var command = new SqlCommand(string.Format(query, inClause), con))
                        {
                            for (int i = 0; i < paramNames.Length; i++)
                            {

                                command.Parameters.AddWithValue(paramNames[i], selectedValues[i]);
                            }

                            SqlDataAdapter da = new SqlDataAdapter(command);

                            da.Fill(dt);
                            dt.Columns.Add("Gender_text", typeof(string));
                            foreach (DataRow row in dt.Rows) {
                                if (row["Gender"].ToString() == "1")
                                {
                                    row["Gender_text"] = "Mens";
                                }
                                else if (row["Gender"].ToString() == "2")
                                {
                                    row["Gender_text"] = "Women";
                                }
                                else 
                                {
                                    row["Gender_text"] = "Others";
                                }
                            }
                            dt.Columns.Remove("Gender");
                            dt.Columns["Gender_text"].ColumnName = "Gender";
                        }
                    }
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            return dt;
        }

        private void LoadData()
        {
            string query = "SELECT distinct SeriesYear FROM tbl_Series";

            string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string year = reader["SeriesYear"].ToString();
                        ddl_report.Items.Add(new ListItem(year, year));
                    }

                    reader.Close();

                   
                }
            }
        }

        protected void ddl_report_SelectedIndexChanged(object sender, EventArgs e)
        {

            // Add selected items from dropdown to list box
            foreach (ListItem item in ddl_report.Items)
            {
                if (item.Selected)
                {

                    lstSelectedValues.Items.Add(new ListItem(item.Text, item.Value));
                }
            }
        }

        public DataTable TransformToMatrix(DataTable reportData, List<string> years)
        {
            DataTable matrixData = new DataTable();

            matrixData.Columns.Add("MatchFormat");

            foreach (var year in years)
            {
                matrixData.Columns.Add($"Year {year} Mens");
                matrixData.Columns.Add($"Year {year} Women");
                matrixData.Columns.Add($"Year {year} Other");
            }

            var matchFormats = reportData.AsEnumerable()
                .Select(row => row.Field<string>("MatchFormat"))
                .Distinct()
                .ToList();

            foreach (var format in matchFormats)
            {
                DataRow row = matrixData.NewRow();
                row["MatchFormat"] = format;

                foreach (var year in years)
                {
                    var menCount = reportData.AsEnumerable()
                        .Where(r => r.Field<string>("MatchFormat") == format && r.Field<string>("SeriesYear") == year && r.Field<string>("Gender") == "Mens")
                        .Sum(r => r.Field<int>("SeriesCount"));

                    var womenCount = reportData.AsEnumerable()
                        .Where(r => r.Field<string>("MatchFormat") == format && r.Field<string>("SeriesYear") == year && r.Field<string>("Gender") == "Women")
                        .Sum(r => r.Field<int>("SeriesCount"));

                    var otherCount = reportData.AsEnumerable()
                        .Where(r => r.Field<string>("MatchFormat") == format && r.Field<string>("SeriesYear") == year && r.Field<string>("Gender") == "Others")
                        .Sum(r => r.Field<int>("SeriesCount"));

                    row[$"Year {year} Mens"] = menCount;
                    row[$"Year {year} Women"] = womenCount;
                    row[$"Year {year} Other"] = otherCount;
                }

                matrixData.Rows.Add(row);
            }

            return matrixData;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageSeries.aspx");
        }
    }

        
    
}