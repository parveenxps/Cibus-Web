using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;


/// <summary>
/// Summary description for clsXmlHelper
/// </summary>
public class clsXmlHelper
{
    public string[] getOrder(string time, string XMLfile)
    {
        //string path = HttpContext.Current.Server.MapPath("");
       
        XDocument doc = XDocument.Load(XMLfile);
        var websites = (from website in doc.Descendants("OrderDetail").Elements("OrderDetail")
                        select new
                        {
                            OrderNumber = website.Element("OrderNumber").Value,
                            Time = website.Element("Time").Value,
                            Status = website.Element("Status").Value,

                        }).ToList();
        var orderNumber = websites.Where(k => k.Status == "Y" &&  Convert.ToDateTime(k.Time) <=  Convert.ToDateTime(time)).Select(k => k.OrderNumber).ToArray();      
        return orderNumber;
    }

    public void Save(string XMLfile, string orderNumber, string time)
    {  
        XDocument XMLDoc = XDocument.Load(XMLfile);
        XElement root = XMLDoc.Root;
        root.Add(new XElement(("OrderDetail"), new XElement("OrderNumber", orderNumber), new XElement("Time", time), new XElement("Status", "Y")));
        XMLDoc.Save(XMLfile);
    }

    public void Delete(string XMLfile, string OrderNumber)
    {
        DataSet dsResult = new DataSet();
        dsResult.ReadXml(XMLfile);
        DataRow[] dr = dsResult.Tables[0].Select("OrderNumber = '" + OrderNumber + "'");

        if (dr != null && dr.Length > 0)
        {
            //-- Remove the matched row from dataset
            dsResult.Tables[0].Rows.Remove(dr[0]);
        }
        //-- Accept the dataset changes
        dsResult.AcceptChanges();
        dsResult.WriteXml(XMLfile);
    }

}
