using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;
using System.IO;
using System.Collections;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Globalization;


/// <summary>
/// Summary description for clsCommon
/// </summary>
public class clsCommon:clsConnection
{

    // common method to save, updae, delete with stored procedure
    public string commonMethod(NameValueCollection nameVal, string proName)
    {
        SqlCommand cmd = new SqlCommand(proName, con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            foreach (string key in nameVal)
            {
                cmd.Parameters.Add(key, nameVal[key]);
            }           
            if (con.State == ConnectionState.Closed || con.State == ConnectionState.Broken)
                con.Open();
           int i = cmd.ExecuteNonQuery();
           if (i > 0)
               return "Y";
           else
               return "N";
        }
        catch (Exception ex)
        {
            throw ex;
        }

        finally
        {
            cmd.Dispose();
            con.Close();
        }
    }


    // common method to bind priority dropdown
    public void BindPriotyDropDown(DropDownList dpdPriorty, string mode, string id)
    {
        SqlDataAdapter adp = new SqlDataAdapter("sp_priority", con);
        adp.SelectCommand.CommandType = CommandType.StoredProcedure;
        DataSet ds = new DataSet();
        try
        {
            adp.SelectCommand.Parameters.AddWithValue("_mode", mode);
            adp.SelectCommand.Parameters.AddWithValue("_id", id);
            adp.Fill(ds);
            dpdPriorty.Items.Clear();
            dpdPriorty.Items.Add("--priority--");
            for (int i = 1; i <= Convert.ToInt16(ds.Tables[0].Rows[0][0]); i++)
            {
                dpdPriorty.Items.Add(i.ToString());
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    // common method to bind dropdown list
    public void BindDropDown(DropDownList dpdDropDown, string tableName, string textFieldName, string valueFieldName)
    {
        SqlDataAdapter adp = new SqlDataAdapter("spPOS_BindDropDown", con);
        adp.SelectCommand.CommandType = CommandType.StoredProcedure;
        DataSet ds = new DataSet();
        try
        {
            adp.SelectCommand.Parameters.AddWithValue("_tablename", tableName);
            adp.SelectCommand.Parameters.AddWithValue("_textfieldname", textFieldName);
            adp.SelectCommand.Parameters.AddWithValue("_valuefieldname", valueFieldName);
            adp.Fill(ds);
            dpdDropDown.DataTextField = textFieldName;
            dpdDropDown.DataValueField = valueFieldName;
            dpdDropDown.DataSource = ds;
            dpdDropDown.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    // ************ common method to save, updae, delete with stored procedure *************
    public bool ChangeStatus(string tableName, string commandName, string id, string mode)
    {


        SqlCommand cmd = new SqlCommand("sp_changestatus", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("_table", tableName);
            cmd.Parameters.AddWithValue("_command", commandName);
            cmd.Parameters.AddWithValue("_id", id);
            cmd.Parameters.AddWithValue("_by", "Id");
            cmd.Parameters.AddWithValue("_mode", mode);
            if (con.State == ConnectionState.Closed || con.State == ConnectionState.Broken)
                con.Open();
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
                return true;
            else
                return false;
        }
        catch (Exception ex)
        {
            throw ex;
        }

        finally
        {
            cmd.Dispose();
            con.Close();
        }
    }

    // ************ common method to save, updae, delete with stored procedure *************
    public bool ChangeOrderStatus(string tableName, string commandName, string id, string mode)
    {


        SqlCommand cmd = new SqlCommand("sp_changestatus", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("_table", tableName);
            cmd.Parameters.AddWithValue("_command", commandName);
            cmd.Parameters.AddWithValue("_id", id);
            cmd.Parameters.AddWithValue("_by", "OrderNumber");
            cmd.Parameters.AddWithValue("_mode", mode);
            if (con.State == ConnectionState.Closed || con.State == ConnectionState.Broken)
                con.Open();
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
                return true;
            else
                return false;
        }
        catch (Exception ex)
        {
            throw ex;
        }

        finally
        {
            cmd.Dispose();
            con.Close();
        }
    } 

    // common method to add, update , delete with output parametes
    public string commonWithOutPut(NameValueCollection nam, string proName, string outParmeter)
    {
        SqlCommand cmd = new SqlCommand(proName, con);
        cmd.CommandType = CommandType.StoredProcedure;

        try
        {
            foreach (string key in nam)
            {
                cmd.Parameters.Add(key, nam[key]);
            }

            cmd.Parameters.Add(outParmeter, SqlDbType.Int).Direction = ParameterDirection.Output;
            if (con.State == ConnectionState.Closed || con.State == ConnectionState.Broken)
                con.Open();

            cmd.ExecuteNonQuery();
            return Convert.ToString(cmd.Parameters[outParmeter].Value);
        }

        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            cmd.Dispose();
            con.Close();
        }
    }


    public bool commonDelete(string strId, string strTable)
    {

        SqlCommand cmd = new SqlCommand("sp_delete", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("_id", strId);
            cmd.Parameters.AddWithValue("_table", strTable);
            if (con.State == ConnectionState.Closed || con.State == ConnectionState.Broken)
                con.Open();
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {
            cmd.Dispose();
            con.Close();
        }

    }

    // common method to get data and return dataset
    public DataSet getData(NameValueCollection nam, string proName)
    {
        SqlDataAdapter adp = new SqlDataAdapter(proName, con);
        adp.SelectCommand.CommandType = CommandType.StoredProcedure;
        DataSet ds = new DataSet();
        try
        {
            foreach (string key in nam)
            {
                adp.SelectCommand.Parameters.Add(key, nam[key]);
            }
            adp.Fill(ds);
            return ds;            
        }
        catch (Exception ex)
        {
            throw ex;
        }        
    }


    //function to get new member id using Id
    public string getMemberId(string id)
    {
        SqlDataAdapter adp = new SqlDataAdapter("sp_getrecord", con);
        adp.SelectCommand.CommandType = CommandType.StoredProcedure;
        DataSet ds = new DataSet();
        try
        {
            adp.SelectCommand.Parameters.Add("_mode", "getmemberid");
            adp.SelectCommand.Parameters.Add("_id", id);
            adp.SelectCommand.Parameters.Add("_table", "");
            adp.Fill(ds);
            return Convert.ToString(ds.Tables[0].Rows[0][0]);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    // method to return date  in string formate yyyy-mm-dd
    public static string DateFormat(string date)
    {
        DateTime dt = Convert.ToDateTime(date);
        string formatted = dt.ToString("yyyy-MM-dd");
        return formatted;
    }


    // method to return date in string formate mm-dd-yyyy
    public static string DateEdit(string date)
    {
        DateTime dt = Convert.ToDateTime(date);
        string formatted = dt.ToString("MM/dd/yyyy");
        return formatted;
    }

    // method to return date in string formate dd-mm-yyy
    public static string DateToShow(string date)
    {
        DateTime dt = Convert.ToDateTime(date);
        string formatted = dt.ToString("dd-MM-yyyy");
        return formatted;
    }


    // method to check date formate dd-mm-yyyy
    public bool ValidateDayFormatDate(string stringDateValue)
    {
        try
        {
            CultureInfo CultureInfoDateCulture = new CultureInfo("fr-FR");
            DateTime d = DateTime.ParseExact(stringDateValue, "dd-MM-yyyy", CultureInfoDateCulture);
            return true;
        }
        catch
        {
            return false;
        }
    }

    // method to check date formate mm-dd-yyyy
    public bool ValidateMonthFormatDate(string stringDateValue)
    {
        try
        {
            CultureInfo CultureInfoDateCulture = new CultureInfo("en-US");
            DateTime d = DateTime.ParseExact(stringDateValue, "MM-dd-yyyy", CultureInfoDateCulture);
            return true;
        }
        catch
        {
            return false;
        }
    }

    // method to check date formate yyyy-mm-dd
    public bool ValidateYearFormatDate(string stringDateValue)
    {
        try
        {
            CultureInfo CultureInfoDateCulture = new CultureInfo("ja-JP");
            DateTime d = DateTime.ParseExact(stringDateValue, "yyyy-MM-dd", CultureInfoDateCulture);
            return true;
        }
        catch
        {
            return false;
        }
    }

    // common method to get data and return datatable
    public DataTable getDataTable(NameValueCollection nam, string proName)
    {
        SqlDataAdapter adp = new SqlDataAdapter(proName, con);
        adp.SelectCommand.CommandType = CommandType.StoredProcedure;
        DataTable dt = new DataTable();
        try
        {
            foreach (string key in nam)
            {
                adp.SelectCommand.Parameters.Add(key, nam[key]);
            }
            adp.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //method to generate random numbers
    public static string RandomNumberGenrator()
    {
        string randomnumber = "";
        try
        {
            string allowedChars = "1,2,3,4,5,6,7,8,9,0";
            char[] sep = { ',' };
            string[] arr = allowedChars.Split(sep);
            string temp = "";
            Random rand = new Random();
            for (int i = 0; i < 4; i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                randomnumber += temp;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return randomnumber;
    }

    // function to format the string
    public string StringFormat(string textString)
    {
        string FirstChar = textString.Substring(0, 1).ToUpper();
        string remainPart = textString.Substring(1).ToLower();
        string resString = FirstChar + remainPart;
        return resString;
    }

    public static string stringDate(string Date)
    {
        string startDate = "";
        if (Date.Substring(6) == "AM")
        {
            startDate = Date.Substring(0, 5) + ":00";
        }
        else
        {
            int sHour = Convert.ToInt16(Date.Substring(0, 2)) + 12;
            startDate = Convert.ToString(sHour) + ":" + Date.Substring(3, 3) + ":00";
        }
        return startDate;
    }
    
}