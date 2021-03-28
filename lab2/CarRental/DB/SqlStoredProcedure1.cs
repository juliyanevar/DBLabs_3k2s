using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class StoredProcedures
{
    [SqlProcedure]
    public static void RentalDate (SqlDateTime StartDate, SqlDateTime EndDate)
    {
        using (SqlConnection con = new SqlConnection("context connection = true"))
        using (SqlCommand cmd = new SqlCommand("select * from [Rental]" +
                                "where [dateOfIssue] between @StartDate and @EndDate;", con))
        {
            cmd.Parameters.AddWithValue("@StartDate", StartDate);
            cmd.Parameters.AddWithValue("@EndDate", EndDate);
            con.Open();

            SqlContext.Pipe.ExecuteAndSend(cmd);
        }
    }
}
