using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using BusinessAccessLayer;
using System.Web.Script.Serialization;
using System.Web.Services;
using Newtonsoft.Json;

namespace AdyZen
{
    public partial class Default : System.Web.UI.Page
    {
        BAL seriesBAL = new BAL();
        SeriesList sl = new SeriesList();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                seriesBAL.Bind_Repeater();
            }
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            string enc = QueryStringHelper.Encrypt("A");
            Response.Redirect($"AddSeries.aspx?Mode={Server.UrlEncode(enc)}");
        }

        protected void Refresh_Click(object sender, EventArgs e)
        {
            series_id.Text = "";
            series_Name.Text = "";
            series_type.SelectedValue = "Select";
            start_date.Text = "";
            end_date.Text = "";
        }
        protected string GetEncryptedUrl(string mode, int seriesId, int sapid)
        {
            string encryptedMode = QueryStringHelper.Encrypt(mode);
            string encryptedSeriesId = QueryStringHelper.Encrypt(seriesId.ToString());
            string encryptedSeriesAPI_Id = QueryStringHelper.Encrypt(sapid.ToString());

            return $"AddSeries.aspx?Mode={Server.UrlEncode(encryptedMode)}&Sid={Server.UrlEncode(encryptedSeriesId)}&api_id={Server.UrlEncode(encryptedSeriesAPI_Id)}";
        }

        protected void search_Click(object sender, EventArgs e)
        {
            
            if (int.TryParse(series_id.Text, out int parsedSeriesID))
            {
                sl.SeriesAPI_ID = parsedSeriesID;
            }
            sl.SeriesName = (series_Name.Text).ToString();
            sl.SeriesType = series_type.SelectedValue=="Select"? "": series_type.SelectedValue;
            sl.startdate= string.IsNullOrEmpty(start_date.Text) ? (DateTime?)null : DateTime.ParseExact(start_date.Text, "yyyy-MM-dd", null);
            sl.enddate = string.IsNullOrEmpty(end_date.Text) ? (DateTime?)null : DateTime.ParseExact(end_date.Text, "yyyy-MM-dd", null);
            DataTable dt = seriesBAL.Search_Series(sl.SeriesAPI_ID, sl.SeriesName,sl.SeriesType, sl.startdate, sl.enddate);

            rptSeries.DataSource = dt;
            rptSeries.DataBind();
        }

        protected void Delete_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DeleteItem")
            {
                // Get the ID from CommandArgument
                int id = Convert.ToInt32(e.CommandArgument);

                // Delete the item from the database
                seriesBAL.Delete_Series(id);

                // Rebind the Repeater to refresh the data
                DataTable dt = seriesBAL.Bind_Repeater();
                rptSeries.DataSource = dt;
                rptSeries.DataBind();
            }
        }

        protected void Rep_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReportGen.aspx");
        }

        public string GetGenderText(int value)
        {
            switch (value)
            {
                case 1:
                    return "Mens";
                case 2:
                    return "Womens";
                case 3:
                    return "Others";
                default:
                    return "Unknown Option";
            }
        }

        public string GetTrophyText(int value)
        {
            switch (value)
            {
                case 1:
                    return "Asia Cup";
                case 2:
                    return "ICC WC T20";
                case 3:
                    return "ICC WC ODI";
                case 4:
                    return "IPL";
                default:
                    return "Unknown Option";
            }
        }

        public string GetMatchText(int value)
        {
            switch (value)
            {
                case 1:
                    return "ODI";
                case 2:
                    return "TEST";
                case 3:
                    return "T20I";
                case 4:
                    return "LIST A";
                case 5:
                    return "T20(Domestic)";
                case 6:
                    return "First Class";
                default:
                    return "Unknown Option";
            }
        }
    }
}