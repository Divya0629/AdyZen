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

                GenerateReport(selectedValues);
            }

        }
        private void GenerateReport(List<string> selectedValues)
        {

            string query = @"
            SELECT distinct SeriesYear,  MatchFormat, Gender, COUNT(*) AS SeriesCount
            FROM tbl_Series
            WHERE SeriesYear IN ({0})
            GROUP BY SeriesYear,  MatchFormat, Gender";

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
                        }

                        gvReport.DataSource = dt;
                        gvReport.DataBind();
                    }
                }
            }
            catch (Exception ex) {
                throw ex;
            }
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
    }

        
    
}