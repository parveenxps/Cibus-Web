using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Summary description for clsConnection
/// </summary>
public abstract class clsConnection
{
    //declare mysql connection  as protected
    protected SqlConnection con = new SqlConnection();
	
    // construnctor to set connection string
    public clsConnection()
	{
        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["cn_admin"].ConnectionString;		
	}
}