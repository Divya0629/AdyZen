using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ErrorLogs
    {
        string dataconfig = ConfigurationManager.ConnectionStrings["cs"].ToString();

        public void LogError(Exception ex, string additionalInfo = null)
        {
            using (SqlConnection conn = new SqlConnection(dataconfig))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("LogErrors", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ErrorMessage", ex.Message);
                    cmd.Parameters.AddWithValue("@ErrorProcedure", ex.TargetSite != null ? ex.TargetSite.Name : DBNull.Value.ToString());
                    cmd.Parameters.AddWithValue("@ErrorLine", GetErrorLine(ex.StackTrace));
                    cmd.Parameters.AddWithValue("@StackTrace", ex.StackTrace ?? DBNull.Value.ToString());
                    cmd.Parameters.AddWithValue("@AdditionalInfo", additionalInfo ?? DBNull.Value.ToString());

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public int? GetErrorLine(string stackTrace)
        {
            if (string.IsNullOrEmpty(stackTrace))
                return null;

            int lineIndex = stackTrace.LastIndexOf(":line");
            if (lineIndex > 0)
            {
                string lineNumber = stackTrace.Substring(lineIndex + 5).Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
                if (int.TryParse(lineNumber, out int line))
                {
                    return line;
                }
            }
            return null;
        }
    }
}

