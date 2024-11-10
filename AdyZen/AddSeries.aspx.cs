using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using BusinessAccessLayer;
using DataAccessLayer;



namespace AdyZen
{
    public partial class AddSeries : System.Web.UI.Page
    {
        BAL bal1 = new BAL();
        SeriesList sl = new SeriesList();
        protected string PageMode;
        protected int SeriesId, API_Id;
        ErrorLogs er= new ErrorLogs();
        

        protected void Page_Init(object sender, EventArgs e)
        {
            string encryptedMode = Request.QueryString["Mode"];
            string encryptedSeriesId = Request.QueryString["Sid"];
            string encryptedSeriesAPI_Id = Request.QueryString["api_id"];

            // Decrypt mode and series ID
            PageMode = QueryStringHelper.Decrypt(encryptedMode);

            if (PageMode == "E" && !string.IsNullOrEmpty(encryptedSeriesId))
            {
                // Decrypt the SeriesId
                SeriesId = int.Parse(QueryStringHelper.Decrypt(encryptedSeriesId));
                API_Id = int.Parse(QueryStringHelper.Decrypt(encryptedSeriesAPI_Id));

            }
           

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            sid.Text = SeriesId.ToString();
            txtapi_id.Text = API_Id.ToString();
            lblmsg.Visible = false;

                if (PageMode == "E" && Request.HttpMethod != "POST")
                {
                    idRow.Visible = true;
                    LoadSeriesData(SeriesId, API_Id); 
                }

                else if (PageMode == "A")
                {
                    idRow.Visible= false;
                }

        }

        protected void save_series_Click(object sender, EventArgs e)
        {

            if (PageMode == "A")
            {
                sl.SeriesName = seriesName.Text;
                sl.SeriesType = dropseriestype.SelectedValue;
                sl.SeriesStatus = series_status.SelectedValue;
                sl.MatchFormat = match_format.SelectedValue;
                sl.SeriesMatchtype = match_type.SelectedIndex;
                sl.Gender = gender.SelectedIndex;
                sl.Year = year.Text;
                sl.TrophyType = trophy_type.SelectedIndex;
                sl.MatchStatus = match_status.SelectedValue;
                sl.startdate = DateTime.ParseExact(start_date.Text, "yyyy-MM-dd", null);
                sl.enddate = DateTime.ParseExact(end_date.Text, "yyyy-MM-dd", null);
                System.Diagnostics.Debug.WriteLine(sl.startdate);
                sl.IsActive = active_status.SelectedIndex;
                sl.Description = txtDesc.Text;

                try
                {
                    string result = bal1.insert_series(sl);
                    lblmsg.Visible = true;
                    if (result != null)
                    {
                        lblmsg.Text = "Inserted Succesfully";
                    }
                    else
                    {
                        lblmsg.Text = "Some Error Occured";
                    }
                }
               
                catch (Exception ex)
                {
                    er.LogError(ex, "Error in Saving");
                    throw;
                }
            

                finally
                {
                    sl = null;
                }
            }
            else if (PageMode == "E" )
            {
                
                sl.SeriesAPI_ID = Convert.ToInt32(txtapi_id.Text);
                sl.SeriesID = Convert.ToInt32(sid.Text);
                sl.SeriesName=seriesName.Text;
                sl.SeriesType = dropseriestype.SelectedValue;
                sl.SeriesStatus = series_status.SelectedValue;
                sl.MatchFormat = match_format.SelectedValue;
                sl.SeriesMatchtype = match_type.SelectedIndex;
                sl.Gender = gender.SelectedIndex;
                sl.Year = year.Text;
                sl.TrophyType = trophy_type.SelectedIndex;
                sl.MatchStatus = match_status.SelectedValue;
                sl.startdate = DateTime.ParseExact(start_date.Text, "yyyy-MM-dd", null);
                sl.enddate = DateTime.ParseExact(end_date.Text, "yyyy-MM-dd", null);
                sl.IsActive = active_status.SelectedIndex;
                sl.Description = txtDesc.Text;

                try
                {
                    string result = bal1.Update_Series(sl);
                    lblmsg.Visible = true;
                    if (result != null)
                    {
                        lblmsg.Text = "Updated Succesfully";
                    }
                    else
                    {
                        lblmsg.Text = "Some Error Occured";
                    }


                }
                catch (Exception ex)
                {
                    er.LogError(ex, "Error in Updating");
                    throw;
                }

                finally
                {
                    sl = null;
                }
            }


        }

        protected void refresh_Click(object sender, EventArgs e)
        {
            seriesName.Text = "";
            year.Text = "";
            start_date.Text = "";
            end_date.Text = "";
            dropseriestype.SelectedValue = "Select";
            series_status.SelectedValue = "Scheduled";
            match_status.SelectedValue = "Scheduled";
            match_format.SelectedValue = "Select";
            match_type.SelectedValue = "-1";
            gender.SelectedValue = "Select";
            active_status.SelectedValue = "Yes";
            trophy_type.SelectedValue = "Select";
            txtDesc.Text = "";
            lblmsg.Visible = false;
        }

        private void LoadSeriesData(int seriesId, int api_id)
        {
            // Get the series data from the database using the BAL
            SeriesList sList = bal1.PreloadSeries(seriesId, api_id);
            try
            {
                if (sList != null)
                {
                    seriesName.Text = sList.SeriesName;
                    dropseriestype.SelectedValue = sList.SeriesType.ToString();
                    series_status.SelectedValue = sList.SeriesStatus.ToString();
                    match_status.SelectedValue = sList.MatchStatus.ToString();
                    match_format.SelectedValue = sList.MatchFormat.ToString();
                    match_type.SelectedIndex = sList.SeriesMatchtype;
                    gender.SelectedIndex = sList.Gender;
                    year.Text = sList.Year;
                    trophy_type.SelectedIndex = sList.TrophyType;
                    start_date.Text = DateTime.ParseExact(sList.startdate.ToString(), "dd-MM-yyyy HH:mm:ss", null).ToString("yyyy-MM-dd");
                    end_date.Text = DateTime.ParseExact(sList.enddate.ToString(), "dd-MM-yyyy HH:mm:ss", null).ToString("yyyy-MM-dd");
                    active_status.SelectedIndex = sList.IsActive;
                    txtDesc.Text = sList.Description.ToString();
                }
            }

            catch (Exception ex)
            {
                er.LogError(ex, "Error in Filling");
                throw;

            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageSeries.aspx");
        }
    }
}
