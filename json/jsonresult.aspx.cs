using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
//using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using jsonProperty;
using System.Text;
using System.Configuration;
using System.Timers;

public partial class json_jsonresult : System.Web.UI.Page
{

    #region CLASSESS OBJECT
    // public variables
    public Timer timer1;
    clsCommon objCommon = new clsCommon();
    clsencrypt objEncrypt = new clsencrypt();
    ClsMailDetails objMail = new ClsMailDetails();
    clsXmlHelper objXml = new clsXmlHelper();
    #endregion

    #region PAGE LOAD

    protected void Page_Load(object sender, EventArgs e)
    {
        //*********** Initialize timer evernt ******************
        if (!IsPostBack)
            IntTimer();
        //*********************************************************

        #region WORKING FOR JSON URL
        //*********** working for json urls *****************************

        if (!string.IsNullOrEmpty(GetUrlValue("method")))
        {
            switch (GetUrlValue("method"))
            {
                case "orderbytable": // view oredry by table number                    
                    Response.Write(JsonOrderByTable(GetUrlValue("method"), GetUrlValue("table")));
                    break;

                case "viewopentickets": //view all order which are in processing                    
                    Response.Write(JsonViewAllOrders(GetUrlValue("method")));
                    break;

                case "viewfinalizeorders": // view all order which are finalized                   
                    Response.Write(JsonViewAllOrders(GetUrlValue("method")));
                    break;

                case "viewpickuporders": // view all pick-up and booking orders                   
                    Response.Write(JsonViewAllOrders(GetUrlValue("method")));
                    break;

                case "viewalltickets": // view all order which are in processing and completed                    
                    Response.Write(JsonViewAllOrders(GetUrlValue("method")));
                    break;

                case "checkorder": // check order is saved or send to kitchen true for saved and false for kitchen                    
                    Response.Write(CheckOrder(GetUrlValue("table")));
                    break;

                case "order": // save order items                    
                    Response.Write(JsonAddOrderItems(GetUrlValue("ordernumber"), GetUrlValue("itemdetails"), GetUrlValue("itemremarks"), GetUrlValue("itemtoppings"), GetUrlValue("lastitemname")));
                    break;

                case "sendtokitchen": // save order items                    
                    Response.Write(JsonAddItemsKitchen(GetUrlValue("ordernumber"), GetUrlValue("itemdetails"), GetUrlValue("itemremarks"), GetUrlValue("itemtoppings"), GetUrlValue("lastitemname"), GetUrlValue("waitingtime")));
                    break;

                case "orderitems": // get order item's detail by order number
                    Response.Write(JsonGetOrderItemsByOrderNumber(GetUrlValue("ordernumber")));
                    break;

                case "orderitemsedit": // get order item's detail by order number
                    Response.Write(JsonGetOrderItemsByOrderNumber(GetUrlValue("ordernumber")));
                    break;

                case "orderitemsbytable": // get order item's detail by table number                   
                    Response.Write(JsonGetOrderItemsByTableNumber(GetUrlValue("tablenumber")));
                    break;

                case "finalize": // get order item's detail by table number                    
                    Response.Write(JsonGetOrderItemsByTableNumberForFinalize(GetUrlValue("tablenumber")));
                    break;

                case "pickuporderdetails": // get order item's detail by table number                    
                    Response.Write(JsonGetPickUpOrderDetails(GetUrlValue("ordernumber")));
                    break;

                case "orderfinalize": // set order status to finalize by table number                    
                    Response.Write(OrderFinalize(GetUrlValue("tablenumber")));
                    break;

                case "orderfinalizebyphone": // set order status to finalize by phone number                    
                    Response.Write(OrderFinalizeByPhone(GetUrlValue("phonenumber")));
                    break;

                case "orderfinalizebyorder": // set order status to finalize by order number                    
                    Response.Write(OrderFinalizeByOrder(GetUrlValue("ordernumber")));
                    break;

                case "orderontable": // get order item's detail by table number                    
                    Response.Write(JsonOrderOnTable(GetUrlValue("tablenumber")));
                    break;

                case "orderonphone": // get order item's detail by table number                    
                    Response.Write(JsonOrderOnPhoneNumber(GetUrlValue("phonenumber")));
                    break;

                case "newsplitorder": // save new order number for split
                    Response.Write(AddOrder(GetUrlValue("table"), GetUrlValue("loginid")));
                    break;

                case "splitorder": // split order items                    
                    Response.Write(JsonAddOrderItemsSplit(GetUrlValue("ordernumber1"), GetUrlValue("itemdetails1"), GetUrlValue("itemremarks"), GetUrlValue("itemtoppings1"), GetUrlValue("ordernumber2"), GetUrlValue("itemdetails2"), GetUrlValue("itemtoppings2")));
                    break;

                case "dinin": // save order                     
                    Response.Write(JsonAddOrder(GetUrlValue("table"), GetUrlValue("customername"), GetUrlValue("customerphone"), GetUrlValue("loginid")));
                    break;

                case "neworder": // save order                     
                    Response.Write(JsonAddNewOrder(GetUrlValue("table"), GetUrlValue("loginid")));
                    break;

                case "neworderpickup": // save order                     
                    Response.Write(JsonAddNewOrderPickUp(GetUrlValue("phonenumber"), GetUrlValue("loginid")));
                    break;

                case "booking": // save order  
                    Response.Write(JsonAddOrderBookingPickup(GetUrlValue("table"), GetUrlValue("loginid"), GetUrlValue("method"), GetUrlValue("deliverydate"), GetUrlValue("deliverytime"), GetUrlValue("name"), GetUrlValue("email"), GetUrlValue("mobile"), GetUrlValue("guest")));
                    break;

                case "pickup": // save order  
                    Response.Write(JsonAddOrderBookingPickup(GetUrlValue("table"), GetUrlValue("loginid"), GetUrlValue("method"), GetUrlValue("deliverydate"), GetUrlValue("deliverytime"), GetUrlValue("name"), GetUrlValue("email"), GetUrlValue("mobile"), GetUrlValue("guest")));
                    break;

                case "quickorder": // method to add order number for quick services
                    Response.Write(JsonAddOrderQuickService(GetUrlValue("login")));
                    break;

                case "offers": // get todays offer                   
                    Response.Write(JsonOffers(GetUrlValue("method")));
                    break;

                case "category": // get category
                    Response.Write(JsonCategory(GetUrlValue("method")));
                    break;

                case "quickcategory": // get quick category
                    Response.Write(JsonQuickCategory(GetUrlValue("method")));
                    break;

                case "subcategory": // get sub-category
                    Response.Write(JsonSubCategory(GetUrlValue("method"), GetUrlValue("category")));
                    break;

                case "quicksubcategory": // get quick sub-category
                    Response.Write(JsonQuickSubCategory(GetUrlValue("method"), GetUrlValue("category")));
                    break;

                case "items": // get items

                    Response.Write(JsonItems(GetUrlValue("method"), GetUrlValue("subcategory")));
                    break;

                case "quickitems": // get quick items                    
                    Response.Write(JsonQuickItems(GetUrlValue("method"), GetUrlValue("subcategory")));
                    break;

                case "forgotpassword": // get items                   
                    Response.Write(JsonForgotPassord(GetUrlValue("method"), GetUrlValue("userid")));
                    break;

                case "searchitems": // get searched items                   
                    Response.Write(JsonSearchItems(GetUrlValue("method"), GetUrlValue("searchtext")));
                    break;

                case "searchdirectory": // get searched directory                    
                    Response.Write(JsonSearchDirectory(GetUrlValue("method")));
                    break;

                case "itemstoppings": // get item toppings by item id                
                    Response.Write(JsonItemToppings(GetUrlValue("method"), GetUrlValue("item")));
                    break;

                case "itemstoppingsbyname": // get item toppings by item name                
                    Response.Write(JsonItemToppingsByName(GetUrlValue("method"), GetUrlValue("item")));
                    break;


                case "itemscatsubcatbyname": // get item toppings by item name                
                    Response.Write(JsonItemCatSubcatByName(GetUrlValue("method"), GetUrlValue("item")));
                    break;

                case "login-subadmin": // login sub-admin (waiter)                    
                    Response.Write(JsonSubAdminLogin(GetUrlValue("username"), GetUrlValue("password")));
                    break;

                case "logout": // logout users                   
                    Response.Write(LogOut(GetUrlValue("loginid")));
                    break;

                case "restaurantdetails": // restaurant details                    
                    Response.Write(JsonRestaurantDetails(GetUrlValue("method")));
                    break;

                case "hourreportable": // get report by table                    
                    Response.Write(JsonReportByTable(GetUrlValue("method"), GetUrlValue("starttime"), GetUrlValue("endtime"), GetUrlValue("date")));
                    break;

                case "tabledetails": // get table details for booking status
                    Response.Write(JsonTableDetail(GetUrlValue("method")));
                    break;

                case "pickupfinal": // send thanks mail to user for placing order                    
                    Response.Write(JsonSendThanksMail(GetUrlValue("time"), GetUrlValue("ordernumber")));
                    break;

                case "ordercancel": // cancel the order by order number
                    Response.Write(JsonOrderCancel(GetUrlValue("ordernumber")));
                    break;

                case "cancelbytable": // cancel the order by table number
                    Response.Write(JsonOrderCancelByTablePhone(GetUrlValue("method"), GetUrlValue("by")));
                    break;

                case "cancelbyphone": // cancel the order by phone number for pick-up and reserve
                    Response.Write(JsonOrderCancelByTablePhone(GetUrlValue("method"), GetUrlValue("by")));
                    break;

                case "itemstatus": // method to get items on table number for item status                    
                    Response.Write(JsonItemStatus(GetUrlValue("method"), GetUrlValue("tablenumber")));
                    break;

                case "itemstatusorder": // method to get items on order number for item status                   
                    Response.Write(JsonItemStatusOrderNumber(GetUrlValue("method"), GetUrlValue("ordernumber")));
                    break;

                case "changeitemstatus": // method to change items                    
                    Response.Write(JsonItemStatusChange(GetUrlValue("ordernumber"), GetUrlValue("itemstatus")));
                    break;

                case "getfeedback": // get the feedback fields
                    Response.Write(JsonFeedback(GetUrlValue("method"), GetUrlValue("id"), GetUrlValue("empname"), GetUrlValue("custname"), GetUrlValue("contact"), GetUrlValue("custemail"), GetUrlValue("tablenumber"), GetUrlValue("rating"), GetUrlValue("hearfrom"), GetUrlValue("comment"), GetUrlValue("finalrating")));
                    break;

                case "setfeedback": // save feed back rating
                    Response.Write(JsonFeedback(GetUrlValue("method"), GetUrlValue("id"), GetUrlValue("empname"), GetUrlValue("custname"), GetUrlValue("contact"), GetUrlValue("custemail"), GetUrlValue("tablenumber"), GetUrlValue("rating"), GetUrlValue("hearfrom"), GetUrlValue("comment"), GetUrlValue("finalrating")));
                    break;

                case "checkitemstatus": // check the item status
                    Response.Write(CheckItemStatus(GetUrlValue("ordernumber")));
                    break;

                case "sendtokitchenedit": // save order items for edit send to kitchen                  
                    Response.Write(JsonAddItemsKitchenEdit(GetUrlValue("ordernumber"), GetUrlValue("itemdetails"), GetUrlValue("itemremarks"), GetUrlValue("itemtoppings"), GetUrlValue("lastitemname"), GetUrlValue("waitingtime"), GetUrlValue("deleteditems")));
                    break;

                case "picktodinein": // convert the reserve orders to the dinin
                    Response.Write(jsonPickupToDineIn(GetUrlValue("ordernumber"), GetUrlValue("tablenumber")));
                    break;

                case "orderstatusontable": // get the order status on the table number
                    Response.Write(JsonOrderStatusByTable(GetUrlValue("tablenumber")));
                    break;

                case "sendregistermail": // send register mail to the users
                    Response.Write(SendThanksMailRegister(GetUrlValue("name"), GetUrlValue("email"), GetUrlValue("phone"), GetUrlValue("message")));
                    break;

                case "checkout": // check order by tbale numbers
                    Response.Write(OrderFinalizeWithPayment(GetUrlValue("tablenumber"), GetUrlValue("loginid")));
                    break;

                case "finalizebyphone": // get order item's detail by phone number                    
                    Response.Write(JsonGetOrderItemsByPhoneNumberForFinalize(GetUrlValue("phonenumber")));
                    break;

                case "checkoutbyphone": // checkout by phone number
                    Response.Write(OrderFinalizeWithPaymentByPhone(GetUrlValue("phonenumber"), GetUrlValue("loginid")));
                    break;

                case "checktable": // check the table number
                    Response.Write(jsonCheckTable(GetUrlValue("tablenumber")));
                    break;
            }
        }
        //*************************************************************************************
        #endregion
    }

    #endregion

    #region METHOD VIEW ORDER BY TABLE NUMBER IN JSON FORMATE

    // function to add order number and return order number
    private string JsonOrderByTable(string requestMethod, string tableNumber)
    {
        if (!string.IsNullOrEmpty(requestMethod))
        {
            // Generate list for order items

            List<orderbytable> listItem = new List<orderbytable>();
            DataTable dtItem = new DataTable();
            NameValueCollection nam = new NameValueCollection();
            nam.Add("_table", tableNumber);
            dtItem = objCommon.getDataTable(nam, "sp_vieworderbytable");

            dtItem.Columns.Add(new DataColumn("ToppingId", typeof(string)));
            dtItem.Columns.Add(new DataColumn("ToppingName", typeof(string)));
            dtItem.Columns.Add(new DataColumn("ToppingPrice", typeof(string)));

            //set topping name and toppping price
            foreach (DataRow row in dtItem.Rows)
            {
                string[] arrToppings = Convert.ToString(row["Toppings"]).Split(',');
                string getToppingName = "", getToppingPrice = "", setToppingId = "", setToppingName = "", setToppingPrice = "";
                foreach (string tId in arrToppings)
                {
                    GetToppingDetails(out getToppingName, out getToppingPrice, tId);
                    setToppingId += tId + ",";
                    setToppingName += getToppingName + ",";
                    setToppingPrice += getToppingPrice + ",";
                }

                setToppingId = setToppingId.Substring(0, setToppingId.Length - 1);
                setToppingName = setToppingName.Substring(0, setToppingName.Length - 1);
                setToppingPrice = setToppingPrice.Substring(0, setToppingPrice.Length - 1);

                row["ToppingId"] = setToppingId;
                row["ToppingName"] = setToppingName;
                row["ToppingPrice"] = setToppingPrice;
            }

            listItem = (from DataRow row in dtItem.Rows
                        select new orderbytable
                        {
                            ItemId = row["Id"].ToString(),
                            ItemName = row["ItemName"].ToString(),
                            ItemQty = row["ItemQty"].ToString(),
                            EachPrice = row["Price"].ToString(),
                            TotalPrice = row["totalprice"].ToString(),
                            ToppingId = row["ToppingId"].ToString(),
                            ToppingName = row["ToppingName"].ToString(),
                            ToppingPrice = row["ToppingPrice"].ToString()


                        }).ToList();

            // Generate list for order number

            List<ordernumber> listOrder = new List<ordernumber>();
            DataTable dtOrder = new DataTable();
            NameValueCollection namOrder = new NameValueCollection();
            namOrder.Add("_table", tableNumber);
            dtOrder = objCommon.getDataTable(namOrder, "sp_ordernobytable");
            listOrder = (from DataRow row in dtOrder.Rows
                         select new ordernumber
                         {
                             OrderNumber = row["OrderNumber"].ToString()

                         }).ToList();

            System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string strJsonRe = oSerializer.Serialize(listItem);
            strJsonRe = strJsonRe.Remove(0, 1);
            strJsonRe = strJsonRe.Substring(0, strJsonRe.Length - 1);

            strJsonRe = "{\"" + requestMethod + "\":{\"ItemDetails\":[" + strJsonRe + "],";

            string reOrder = oSerializer.Serialize(listOrder);
            reOrder = reOrder.Remove(0, 2);
            reOrder = reOrder.Substring(0, reOrder.Length - 1);
            strJsonRe = strJsonRe + reOrder + "}";
            return strJsonRe;
        }
        else
            return "";
    }

    #endregion

    #region METHOD GET TOPPING-NAME & TOPPING-PRICE

    // function to add order number and return order number
    private void GetToppingDetails(out string toppingName, out string toppingPrice, string id)
    {
        try
        {
            NameValueCollection nam = new NameValueCollection();
            DataSet ds = new DataSet();
            nam.Add("_toppingname", id);
            ds = objCommon.getData(nam, "sp_toppings");
            toppingName = Convert.ToString(ds.Tables[0].Rows[0][0]);
            toppingPrice = Convert.ToString(ds.Tables[0].Rows[0][1]);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    #endregion

    #region METHOD VIEW ORDERS IN JSON FORMATE

    // function to view all types of ordes
    private string JsonViewAllOrders(string requestMethod)
    {
        string strJsonRe = "";
        if (!string.IsNullOrEmpty(requestMethod))
        {
            System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            DataTable dt = new DataTable();
            NameValueCollection nam = new NameValueCollection();
            nam.Add("_mode", GetUrlValue("method"));
            nam.Add("_loginid", GetUrlValue("loginid"));
            dt = objCommon.getDataTable(nam, "sp_viewallorders");

            switch (requestMethod)
            {
                case "viewopentickets": //for open tickets 
                    List<viewopenticket> listopen = new List<viewopenticket>();
                    listopen = (from DataRow row in dt.Rows
                                select new viewopenticket
                                {
                                    OrderNumber = row["OrderNumber"].ToString(),
                                    Type = row["OrderType"].ToString().Trim(),
                                    TableNumber = row["TableNumber"].ToString(),
                                    Date = Convert.ToDateTime(row["OrderDate"]).ToShortDateString(),
                                    WaitingTime = row["WaitingTime"].ToString(),
                                    Status = row["IsProcess"].ToString()
                                }).ToList();

                    strJsonRe = "{\"" + requestMethod + "\":" + oSerializer.Serialize(listopen) + "}";
                    break;
                case "viewpickuporders": // for pick up & booking orders

                    dt.Columns.Add(new DataColumn("KitchenTime", typeof(string)));

                    //set offer image
                    foreach (DataRow row in dt.Rows)
                    {
                        if (Convert.ToString(row["SendKitchenTime"]) != "0")
                        {
                            string[] arrSendTime = Convert.ToString(row["SendKitchenTime"]).Split(' ');
                            string date = arrSendTime[0].ToString();
                            string[] arrDate = date.Split('-');
                            date = clsCommon.DateFormat(Convert.ToString(arrDate[1] + "-" + arrDate[0] + "-" + arrDate[2]));

                            string time = arrSendTime[1].ToString();
                            row["KitchenTime"] = date + " " + time;

                        }
                        else
                            row["KitchenTime"] = row["SendKitchenTime"].ToString();
                    }

                    List<viewpickuporders> listpickup = new List<viewpickuporders>();
                    listpickup = (from DataRow row in dt.Rows
                                  select new viewpickuporders
                                  {
                                      OrderNumber = row["OrderNumber"].ToString(),
                                      Type = row["OrderType"].ToString(),
                                      TableNumber = row["TableNumber"].ToString(),
                                      Date = Convert.ToDateTime(row["DeliveryDate"]).ToShortDateString(),
                                      Time = row["DeliveryTime"].ToString(),
                                      sendtokitchentime = row["KitchenTime"].ToString(),
                                      NoOfGues = row["NumberOfGuest"].ToString(),
                                      CustomerPhone = row["PhoneNumber"].ToString(),
                                      CustomerName = row["Name"].ToString(),
                                      Status = row["IsProcess"].ToString()

                                  }).ToList();
                    strJsonRe = "{\"" + requestMethod + "\":" + oSerializer.Serialize(listpickup) + "}";
                    break;
                case "viewalltickets": // for all orders orders
                    List<viewallcloseorders> listall = new List<viewallcloseorders>();
                    listall = (from DataRow row in dt.Rows
                               select new viewallcloseorders
                               {
                                   OrderNumber = row["OrderNumber"].ToString(),
                                   Type = row["OrderType"].ToString(),
                                   TableNumber = row["TableNumber"].ToString(),
                                   Date = Convert.ToDateTime(row["OrderDate"]).ToShortDateString(),
                                   WaitingTime = row["WaitingTime"].ToString(),
                                   NoOfGues = row["NumberOfGuest"].ToString(),
                                   CustomerDetails = row["Name"].ToString(),
                                   Status = row["IsProcess"].ToString()

                               }).ToList();
                    strJsonRe = "{\"" + requestMethod + "\":" + oSerializer.Serialize(listall) + "}";
                    break;

                case "viewfinalizeorders": // for finalized orders
                    string result = "";
                    foreach (DataRow row in dt.Rows)
                    {
                        //    //********* get all order number and merge on current table ********************
                        //NameValueCollection nanValO = new NameValueCollection();
                        //DataTable dtO = new DataTable();
                        //nanValO.Add("_mode", "finalizeorder");
                        //string table = row["TableNumber"].ToString();
                        //nanValO.Add("_id", row["TableNumber"].ToString());
                        //dtO = objCommon.getDataTable(nanValO, "spPOS_General");
                        string orderNumber = "";
                        Int32 count = 0;

                        //foreach (DataRow rowO in dtO.Rows)
                        //{
                        //if (rowO["TableNumber"].ToString() == "0")
                        //{
                        orderNumber = row["OrderNumber"].ToString() + ",";
                        string strName = "No Details";

                        if (!string.IsNullOrEmpty(row["PhoneNumber"].ToString()))
                            strName = row["PhoneNumber"].ToString();

                        if (strName == "(null)")
                            strName = "No Details";

                        result += "{\"OrderNumber\":\"" + orderNumber.Remove(orderNumber.Length - 1, 1) + "\",\"Type\":\"" + row["OrderType"].ToString() + "\",\"TableNumber\":\"" + row["TableNumber"].ToString() + "\",\"Date\":\"" + Convert.ToDateTime(row["OrderDate"]).ToShortDateString() + "\",\"WaitingTime\":\"" + row["WaitingTime"].ToString() + "\",\"NoOfGues\":\"" + row["NumberOfGuest"].ToString() + "\",\"CustomerDetails\":\"" + strName + "\",\"Status\":\"N\"}" + ",";
                        count++;
                        //}
                        //else
                        //{
                        //   orderNumber += rowO["OrderNumber"].ToString() + ",";
                        //}
                        //}

                        //******************************************************************************
                        if (count == 0)
                        {
                            string strName1 = "No Details";

                            if (!string.IsNullOrEmpty(row["PhoneNumber"].ToString()))
                                strName1 = row["PhoneNumber"].ToString();

                            if (strName1 == "(null)")
                                strName1 = "No Details";

                            result += "{\"OrderNumber\":\"" + orderNumber.Remove(orderNumber.Length - 1, 1) + "\",\"Type\":\"" + row["OrderType"].ToString() + "\",\"TableNumber\":\"" + row["TableNumber"].ToString() + "\",\"Date\":\"" + Convert.ToDateTime(row["OrderDate"]).ToShortDateString() + "\",\"WaitingTime\":\"" + row["WaitingTime"].ToString() + "\",\"NoOfGues\":\"" + row["NumberOfGuest"].ToString() + "\",\"CustomerDetails\":\"" + strName1 + "\",\"Status\":\"N\"}" + ",";
                        }
                        strJsonRe = "{\"" + requestMethod + "\":[" + result.Remove(result.Length - 1, 1) + "]}";

                    }
                    break;
            }

        }
        return strJsonRe;
    }

    #endregion

    #region METHOD ADD ORDER & RETURN JSON ORDER NUMBER

    // function to add order number and return order number
    private string JsonAddOrder(string tablenumber, string customerName, string customerPhone, string loginId)
    {
        if (string.IsNullOrEmpty(customerName))
            customerName = "";
        if (string.IsNullOrEmpty(customerPhone))
            customerPhone = "";

        string strJsonRe = "";
        NameValueCollection nam = new NameValueCollection();
        nam.Add("_ordernumber", "0");
        nam.Add("_tablenumber", tablenumber);
        nam.Add("_ordertype", "Dine In");
        nam.Add("_issplit", "0");
        nam.Add("_customername", customerName);
        nam.Add("_customerphone", customerPhone);
        nam.Add("_loginid", loginId);
        string ordernumber = Convert.ToString(objCommon.commonWithOutPut(nam, "sp_order", "_outordernumber"));

        if (Convert.ToInt16(ordernumber) > 0)
            strJsonRe = "{\"OrderNumber\":\"" + ordernumber + "\"}";

        else if (Convert.ToInt16(ordernumber) == 0)
            strJsonRe = "{\"Error\":\"This table number does not exist\"}";

        else
            strJsonRe = "{\"Error\":\"The table number you entered is already occupied\"}";

        return strJsonRe;
    }

    #endregion

    #region METHOD ADD AND RETURN NEW ORDER NUMBER

    // function to add order number and return order number
    private string JsonAddNewOrder(string tablenumber, string loginId)
    {
        string strJsonRe = "";
        NameValueCollection nam = new NameValueCollection();
        nam.Add("_tablenumber", tablenumber);
        nam.Add("_loginid", loginId);
        string ordernumber = Convert.ToString(objCommon.commonWithOutPut(nam, "sp_neworder", "_outordernumber"));
        if (Convert.ToInt16(ordernumber) > 0)
            strJsonRe = "{\"OrderNumber\":\"" + ordernumber + "\"}";
        else
            strJsonRe = "{\"Error\":\"This table number is in process\"}";
        return strJsonRe;
    }

    #endregion

    #region METHOD ADD AND RETURN NEW ORDER NUMBER FOR PICK-UP

    // function to add order number and return order number for pick up
    private string JsonAddNewOrderPickUp(string phonenumber, string loginId)
    {
        string strJsonRe = "";
        NameValueCollection nam = new NameValueCollection();
        nam.Add("_phonenumber", phonenumber);
        nam.Add("_loginid", loginId);
        string ordernumber = Convert.ToString(objCommon.commonWithOutPut(nam, "sp_neworderpickup", "_outordernumber"));
        if (Convert.ToInt16(ordernumber) > 0)
            strJsonRe = "{\"OrderNumber\":\"" + ordernumber + "\"}";
        else
            strJsonRe = "{\"Error\":\"This table number is in process\"}";
        return strJsonRe;
    }

    #endregion

    #region METHOD ADD ORDER FOR BOOKING / PICK UP & RETURN JSON ORDER NUMBER

    // function to add order number and return order number
    private string JsonAddOrderBookingPickup(string tablenumber, string loginId, string orderType, string deliveryDate, string deliveryTime, string CutomerName, string customerEmail, string customerMobile, string noOfGuest)
    {
        string strJsonRe = "";
        NameValueCollection nam = new NameValueCollection();
        nam.Add("_ordernumber", "0");

        if (string.IsNullOrEmpty(tablenumber))
            tablenumber = "0";

        nam.Add("_tablenumber", tablenumber);

        if (orderType == "booking")
            nam.Add("_ordertype", "Booking");
        else
            nam.Add("_ordertype", "Pick Up");

        nam.Add("_deliverydate", clsCommon.DateFormat(deliveryDate));
        nam.Add("_deliverytime", deliveryTime);
        nam.Add("_loginid", loginId);
        nam.Add("_customername", CutomerName);
        nam.Add("_customeremail", customerEmail);
        nam.Add("_cutomermobile", customerMobile);
        nam.Add("_numberofguest", noOfGuest);

        string ordernumber = Convert.ToString(objCommon.commonWithOutPut(nam, "sp_order_booking_pickup", "_outordernumber"));

        if (Convert.ToInt16(ordernumber) > 0)
            strJsonRe = "{\"OrderNumber\":\"" + ordernumber + "\"}";
        else if (Convert.ToInt16(ordernumber) == -1)
            strJsonRe = "{\"Error\":\"This table number does not exist!\"}";
        else if (Convert.ToInt16(ordernumber) == -2)
            strJsonRe = "{\"Error\":\"An order number already exists with same details!\"}";
        else
            strJsonRe = "{\"Error\":\"This table number is already book\"}";

        return strJsonRe;
    }

    #endregion

    #region METHOD ADD ORDER FOR QUICK SERVICE & RETURN JSON ORDER NUMBER

    // function to add order number and return order number for quick service
    private string JsonAddOrderQuickService(string loginId)
    {
        try
        {
            NameValueCollection nam = new NameValueCollection();
            nam.Add("_loginid", loginId);
            string ordernumber = Convert.ToString(objCommon.commonWithOutPut(nam, "spPOS_AddQuickOrder", "_outorderno"));
            return "{\"OrderNumber\":\"" + ordernumber + "\"}";
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    #endregion

    #region METHOD ADD ORDER & RETURN ORDER NUMBER FOR SPLIT METHOD

    // function to add order number for split order and return order number
    private string AddOrder(string tablenumber, string loginId)
    {
        string strJsonRe = "";

        NameValueCollection nam = new NameValueCollection();
        nam.Add("_ordernumber", "0");
        nam.Add("_tablenumber", tablenumber);
        if (!string.IsNullOrEmpty(GetUrlValue("date")) && !string.IsNullOrEmpty(GetUrlValue("time")))
        {
            nam.Add("_ordertype", "Pick Up");
            nam.Add("_deliverydate", GetUrlValue("date"));
            nam.Add("_deliverytime", GetUrlValue("time"));
        }
        else
        {
            nam.Add("_ordertype", "Dine In");
            nam.Add("_deliverydate", "0000-00-00");
            nam.Add("_deliverytime", "");
        }
        nam.Add("_issplit", "Y");
        nam.Add("_customername", "");
        nam.Add("_customerphone", "");
        nam.Add("_loginid", loginId);

        string ordernumber = Convert.ToString(objCommon.commonWithOutPut(nam, "sp_order", "_outordernumber"));
        if (Convert.ToInt16(ordernumber) > 0)
            strJsonRe = "{\"OrderNumber\":\"" + ordernumber + "\"}";
        else
            strJsonRe = "{\"Error\":\"This table number is in process\"}";
        return strJsonRe;
    }

    #endregion

    #region METHOD ADD SPLIT ORDER ITEMS

    // function to save order
    private string JsonAddOrderItemsSplit(string orderNo1, string itemDetails1, string itemRemarks, string itemToppings1, string orderNo2, string itemDetails2, string itemToppings2)
    {
        //remove duplicate order items
        ResetOrderItems(orderNo1);
        ResetOrderItems(orderNo2);

        // save items details (orderno, item, qty, remarks)
        itemDetails1 = itemDetails1.Remove(0, 1);
        itemDetails1 = itemDetails1.Remove(itemDetails1.Length - 2, 2);
        string[] arrItemDet = itemDetails1.Split(';');

        string[] arrItemRemarks = null;
        if (!string.IsNullOrEmpty(itemRemarks))
        {
            itemRemarks = itemRemarks.Remove(0, 1);
            itemRemarks = itemRemarks.Remove(itemRemarks.Length - 2, 2);
            arrItemRemarks = itemRemarks.Split(';');
        }


        foreach (string itemDet in arrItemDet)
        {
            string strName = "", strQty = "", strRemakrs = "";
            string[] arrItem = itemDet.Split('=');
            strName = arrItem[0].ToString();
            strQty = arrItem[1].ToString();

            if (!string.IsNullOrEmpty(itemRemarks))
            {
                foreach (string itemRem in arrItemRemarks)
                {
                    string[] arrItemRem = itemRem.Split('=');
                    if (arrItemRem[0].ToString() == arrItem[0].ToString())
                    {
                        strRemakrs = arrItemRem[1].ToString();
                        break;
                    }
                }
            }

            // SAVE ORDER ITEMS INTO DATA BASE
            NameValueCollection nam = new NameValueCollection();
            nam.Add("_orderid", orderNo1);
            nam.Add("_itemname", strName);
            nam.Add("_itemqty", strQty);
            nam.Add("_toppings", "");
            nam.Add("_remarks", strRemakrs);
            string result = objCommon.commonMethod(nam, "sp_orderitem");
        }

        // ************************ save second order number *********************************    


        // save items details (orderno, item, qty, remarks)
        itemDetails2 = itemDetails2.Remove(0, 1);
        itemDetails2 = itemDetails2.Remove(itemDetails2.Length - 2, 2);
        string[] arrItemDet_2 = itemDetails2.Split(';');

        string[] arrItemRemarks_2 = null;
        if (!string.IsNullOrEmpty(itemRemarks))
        {
            itemRemarks = itemRemarks.Remove(0, 1);
            itemRemarks = itemRemarks.Remove(itemRemarks.Length - 2, 2);
            arrItemRemarks_2 = itemRemarks.Split(';');
        }


        foreach (string itemDet_2 in arrItemDet_2)
        {
            string strName_2 = "", strQty_2 = "", strRemakrs_2 = "";
            string[] arrItem_2 = itemDet_2.Split('=');
            strName_2 = arrItem_2[0].ToString();
            strQty_2 = arrItem_2[1].ToString();

            if (!string.IsNullOrEmpty(itemRemarks))
            {
                foreach (string itemRem_2 in arrItemRemarks_2)
                {
                    string[] arrItemRem_2 = itemRem_2.Split('=');
                    if (arrItemRemarks_2[0].ToString() == arrItem_2[0].ToString())
                    {
                        strRemakrs_2 = arrItemRem_2[1].ToString();
                        break;
                    }
                }
            }

            // SAVE ORDER ITEMS INTO DATA BASE
            NameValueCollection nam = new NameValueCollection();
            nam.Add("_orderid", orderNo2);
            nam.Add("_itemname", strName_2);
            nam.Add("_itemqty", strQty_2);
            nam.Add("_toppings", "");
            nam.Add("_remarks", strRemakrs_2);
            string result = objCommon.commonMethod(nam, "sp_orderitem");
        }

        return "{\"Status\":\"Success\"}";

    }

    #endregion

    #region METHOD ADD ORDER ITEMS FOR SAVE ORDER

    // function to save order
    private string JsonAddOrderItems(string orderNo, string itemDetails, string itemRemarks, string itemToppings, string lastItemName)
    {
        string jsonResult = "";

        try
        {
            //REMOVE THE DUPLICATE ORDER ITEMS
            ResetOrderItems(orderNo);


            // save items details (orderno, item, qty, remarks)
            itemDetails = itemDetails.Remove(0, 1);
            itemDetails = itemDetails.Remove(itemDetails.Length - 2, 2);
            string[] arrItemDet = itemDetails.Split(';');

            string[] arrItemRemarks = null;
            if (!string.IsNullOrEmpty(itemRemarks))
            {
                itemRemarks = itemRemarks.Remove(0, 1);
                itemRemarks = itemRemarks.Remove(itemRemarks.Length - 2, 2);
                arrItemRemarks = itemRemarks.Split(';');
            }


            foreach (string itemDet in arrItemDet)
            {
                string strName = "", strQty = "", strRemakrs = "";
                string[] arrItem = itemDet.Split('=');
                strName = arrItem[0].ToString();
                strQty = arrItem[1].ToString();

                if (!string.IsNullOrEmpty(itemRemarks))
                {
                    foreach (string itemRem in arrItemRemarks)
                    {
                        string[] arrItemRem = itemRem.Split('=');
                        if (arrItemRem[0].ToString() == arrItem[0].ToString())
                        {
                            strRemakrs = arrItemRem[1].ToString();
                            break;
                        }
                    }
                }

                // SAVE ORDER ITEMS INTO DATA BASE
                NameValueCollection nam = new NameValueCollection();
                nam.Add("_orderid", orderNo);
                nam.Add("_itemname", strName);
                nam.Add("_itemqty", strQty);
                nam.Add("_toppings", "");
                nam.Add("_remarks", strRemakrs);
                nam.Add("_lastitem", lastItemName);
                string result = objCommon.commonMethod(nam, "sp_orderitem");
            }

            if (!string.IsNullOrEmpty(itemToppings))
            {
                // update order items for toppinss
                itemToppings = itemToppings.Remove(0, 1);
                itemToppings = itemToppings.Remove(itemToppings.Length - 4, 4);

                string[] arrToppings = itemToppings.Split(')');

                foreach (string topping in arrToppings)
                {
                    string topping1 = "";

                    // remove ; sign from starting position
                    if (topping.Substring(0, 1) == ";")
                        topping1 = topping.Remove(0, 1);
                    else
                        topping1 = topping;

                    // remove ; from the last position
                    if (topping1.Substring(topping1.Length - 1, 1) == ";")
                        topping1 = topping1.Remove(topping1.Length - 1, 1);


                    if (topping1.Substring(topping1.Length - 1, 1) != ")")
                        topping1 = topping1 + ")";


                    string itemName = topping1.Substring(0, topping1.IndexOf('='));
                    string topping2 = topping1.Remove(0, topping1.IndexOf('=') + 2);
                    topping2 = topping2.Remove(topping2.Length - 1, 1);

                    string[] arrTopping2 = topping2.Split(';');

                    string itemTopping = "";
                    foreach (string topping3 in arrTopping2)
                    {
                        itemTopping += topping3.Remove(0, topping3.IndexOf('=') + 1) + ";";
                    }

                    itemTopping = itemTopping.Remove(itemTopping.Length - 1, 1);

                    // UPDATE ORDER ITEMS INTO DATA BASE FOR TOPPINGS  
                    NameValueCollection nam = new NameValueCollection();
                    nam.Add("_orderid", orderNo);
                    nam.Add("_itemname", itemName);
                    nam.Add("_itemqty", "0");
                    nam.Add("_toppings", itemTopping);
                    nam.Add("_remarks", "");
                    nam.Add("_lastitem", "");
                    string result = objCommon.commonMethod(nam, "sp_orderitem");
                }
            }

            jsonResult = "{\"Status\":\"Success\"}";
        }
        catch (Exception ex)
        {
            jsonResult = "{\"Error\":\"Item not saved, please try again\"}";
        }
        return jsonResult;

    }

    #endregion

    #region METHOD ADD ORDER ITEMS FOR SEND TO KITCHEN

    // function to save order
    private string JsonAddItemsKitchen(string orderNo, string itemDetails, string itemRemarks, string itemToppings, string lastItemName, string waitingTime)
    {
        string jsonResult = "";

        try
        {
            //int i = UpdateWaitingTime(orderNo, waitingTime);

            //if (i == 0)
            //{
            //REMOVE THE DUPLICATE ORDER ITEMS
            ResetOrderItems(orderNo);


            // save items details (orderno, item, qty, remarks)
            itemDetails = itemDetails.Remove(0, 1);
            itemDetails = itemDetails.Remove(itemDetails.Length - 2, 2);
            string[] arrItemDet = itemDetails.Split(';');

            string[] arrItemRemarks = null;
            if (!string.IsNullOrEmpty(itemRemarks))
            {
                itemRemarks = itemRemarks.Remove(0, 1);
                itemRemarks = itemRemarks.Remove(itemRemarks.Length - 2, 2);
                arrItemRemarks = itemRemarks.Split(';');
            }


            foreach (string itemDet in arrItemDet)
            {
                string strName = "", strQty = "", strRemakrs = "";
                string[] arrItem = itemDet.Split('=');
                strName = arrItem[0].ToString();
                strQty = arrItem[1].ToString();

                if (!string.IsNullOrEmpty(itemRemarks))
                {
                    foreach (string itemRem in arrItemRemarks)
                    {
                        string[] arrItemRem = itemRem.Split('=');
                        if (arrItemRem[0].ToString() == arrItem[0].ToString())
                        {
                            strRemakrs = arrItemRem[1].ToString();
                            break;
                        }
                    }
                }

                // SAVE ORDER ITEMS INTO DATA BASE
                NameValueCollection nam = new NameValueCollection();
                nam.Add("_orderid", orderNo);
                nam.Add("_itemname", strName);
                nam.Add("_itemqty", strQty);
                nam.Add("_toppings", "");
                nam.Add("_remarks", strRemakrs);
                nam.Add("_lastitem", lastItemName);
                string result = objCommon.commonMethod(nam, "sp_orderitem");
            }

            if (!string.IsNullOrEmpty(itemToppings))
            {
                // update order items for toppinss
                itemToppings = itemToppings.Remove(0, 1);
                itemToppings = itemToppings.Remove(itemToppings.Length - 4, 4);

                string[] arrToppings = itemToppings.Split(')');

                foreach (string topping in arrToppings)
                {
                    string topping1 = "";

                    // remove ; sign from starting position
                    if (topping.Substring(0, 1) == ";")
                        topping1 = topping.Remove(0, 1);
                    else
                        topping1 = topping;

                    // remove ; from the last position
                    if (topping1.Substring(topping1.Length - 1, 1) == ";")
                        topping1 = topping1.Remove(topping1.Length - 1, 1);


                    if (topping1.Substring(topping1.Length - 1, 1) != ")")
                        topping1 = topping1 + ")";


                    string itemName = topping1.Substring(0, topping1.IndexOf('='));
                    string topping2 = topping1.Remove(0, topping1.IndexOf('=') + 2);
                    topping2 = topping2.Remove(topping2.Length - 1, 1);

                    string[] arrTopping2 = topping2.Split(';');

                    string itemTopping = "";
                    foreach (string topping3 in arrTopping2)
                    {
                        itemTopping += topping3.Remove(0, topping3.IndexOf('=') + 1) + ";";
                    }

                    itemTopping = itemTopping.Remove(itemTopping.Length - 1, 1);

                    // UPDATE ORDER ITEMS INTO DATA BASE FOR TOPPINGS  
                    NameValueCollection nam = new NameValueCollection();
                    nam.Add("_orderid", orderNo);
                    nam.Add("_itemname", itemName);
                    nam.Add("_itemqty", "0");
                    nam.Add("_toppings", itemTopping);
                    nam.Add("_remarks", "");
                    nam.Add("_lastitem", "");
                    string result = objCommon.commonMethod(nam, "sp_orderitem");
                }
            }

            UpdateWaitingTime(orderNo, waitingTime);
            //}

            jsonResult = "{\"Status\":\"Success\"}";
        }
        catch (Exception ex)
        {
            jsonResult = "{\"Error\":\"Item not saved, please try again\"}";
        }
        return jsonResult;

    }

    #endregion

    #region METHOD REMOVE DUPLICATE ORDER ITEMS

    // function to resetorderitems order
    private void ResetOrderItems(string orderNo)
    {
        NameValueCollection nam = new NameValueCollection();
        nam.Add("_orderno", orderNo);
        objCommon.commonMethod(nam, "sp_resetorderitems");
    }

    #endregion

    #region METHOD CANCEL ORDER

    // function to resetorderitems order
    private string JsonOrderCancel(string orderNo)
    {
        string jsonResult = "";
        try
        {
            NameValueCollection nam = new NameValueCollection();
            nam.Add("_orderno", orderNo);
            objCommon.commonMethod(nam, "sp_ordercancel");
            jsonResult = "{\"Status\":\"Success\"}";
        }
        catch (Exception ex)
        {
            jsonResult = "{\"Error\":\"Please try again\"}";
        }
        return jsonResult;
    }

    #endregion

    #region METHOD CANCEL ORDER BY TABLE NUMBER AND PHONE NUMBER

    // function to resetorderitems order
    private string JsonOrderCancelByTablePhone(string requestMethod, string by)
    {
        string jsonResult = "";
        try
        {
            NameValueCollection namOrder = new NameValueCollection();
            DataTable dt = new DataTable();

            if (requestMethod == "cancelbytable")
                namOrder.Add("_mode", "ordernobytable");
            else
                namOrder.Add("_mode", "ordernobyphone");

            namOrder.Add("_id", by);
            dt = objCommon.getDataTable(namOrder, "spPOS_GetOrderNoForCancel");

            foreach (DataRow row in dt.Rows)
            {
                NameValueCollection nam = new NameValueCollection();
                nam.Add("_orderno", row["OrderNumber"].ToString());
                objCommon.commonMethod(nam, "sp_ordercancel");
            }
            jsonResult = "{\"Status\":\"Success\"}";
        }
        catch (Exception ex)
        {
            jsonResult = "{\"Error\":\"Please try again\"}";
        }
        return jsonResult;
    }

    #endregion

    #region METHOD MAKE FINALIZE ORDER BY TABLE NUMBER

    // function to set order status finalize by table number
    private string OrderFinalize(string tableNumber)
    {
        string jsonResult = "";

        try
        {
            // change the status of order from processing to finalize and get customer details
            NameValueCollection nam = new NameValueCollection();
            DataSet ds = new DataSet();
            nam.Add("_mode", "orderfinalize");
            nam.Add("_id", tableNumber);
            ds = objCommon.getData(nam, "spPOS_General");

            /*
            // save the cusotmer details in member table
            if (Convert.ToString(ds.Tables[0].Rows[0]["OrderType"]) == "Dine In" && !string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["Name"])) && !string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["Email"])))
            {
                SaveMemberInforamtion(Convert.ToString(ds.Tables[0].Rows[0]["Name"]), Convert.ToString(ds.Tables[0].Rows[0]["PhoneNumber"]), Convert.ToString(ds.Tables[0].Rows[0]["Email"]));
            } */


            jsonResult = "{\"Status\":\"Success\"}";
        }
        catch (Exception ex)
        {
            jsonResult = "{\"Error\":\"Please try again\"}";
        }

        return jsonResult;
    }

    #endregion

    #region METHOD MAKE FINALIZE ORDER BY ORDER NUMBER

    // function to set order status finalize by order number
    public string OrderFinalizeByOrder(string ordernumber)
    {
        string jsonResult = "";

        try
        {
            NameValueCollection nam = new NameValueCollection();
            nam.Add("_mode", "orderfinalizebyorderno");
            nam.Add("_id", ordernumber);
            objCommon.commonMethod(nam, "spPOS_General");
            jsonResult = "{\"Status\":\"Success\"}";
        }
        catch (Exception ex)
        {
            jsonResult = "{\"Error\":\"Please try again\"}";
        }

        return jsonResult;
    }

    #endregion

    # region METHOD GET OFFERS

    // function to get cagegory and conert into json, return in string value
    private string JsonOffers(string requestMethod)
    {
        if (!string.IsNullOrEmpty(requestMethod))
        {
            List<offers> list = new List<offers>();
            DataTable dt = new DataTable();
            NameValueCollection nam = new NameValueCollection();
            dt = objCommon.getDataTable(nam, "spPOS_GetOfffer");

            dt.Columns.Add(new DataColumn("OfferImage", typeof(string)));

            //set offer image
            foreach (DataRow row in dt.Rows)
            {
                if (string.IsNullOrEmpty(row["ItemImage"].ToString()))
                    row["OfferImage"] = "nooffer.png";
                else
                    row["OfferImage"] = row["ItemImage"].ToString();
            }


            list = (from DataRow row in dt.Rows
                    select new offers
                    {
                        OfferId = row["ItemId"].ToString(),
                        OfferName = row["OfferName"].ToString(),
                        OfferPrice = row["OfferPrice"].ToString(),
                        OfferImage = row["OfferImage"].ToString()

                    }).ToList();

            System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            NameValueCollection nam1 = new NameValueCollection();
            nam1.Add("_mode", "displaydetail");
            nam1.Add("_id", "0");
            nam1.Add("_table", "");
            DataSet ds = new DataSet();
            ds = objCommon.getData(nam1, "sp_getrecord");
            string strJsonRe = "{\"" + requestMethod + "\":" + oSerializer.Serialize(list) + ",\"Restaurant\" : \"" + Convert.ToString(ds.Tables[0].Rows[0][0]) + "\",\"HotelLogo\":\"" + Convert.ToString(ds.Tables[0].Rows[0][1]) + "\",\"Currency\":\"" + Convert.ToString(ds.Tables[0].Rows[0][2]) + "\",\"Split\":\"True\"}";
            return strJsonRe;
        }
        else
            return "";
    }

    #endregion

    #region METHOD GET ITEMS

    // function to get sub cagegory and conert into json, return in string value
    private string JsonItems(string requestMethod, string subCatId)
    {
        if (!string.IsNullOrEmpty(requestMethod))
        {
            List<Items> list = new List<Items>();
            DataTable dt = new DataTable();
            NameValueCollection nam = new NameValueCollection();
            nam.Add("_mode", "items-ipad");
            nam.Add("_id", subCatId);
            dt = objCommon.getDataTable(nam, "spPOS_General");
            list = (from DataRow row in dt.Rows
                    select new Items
                    {
                        ItemId = row["Id"].ToString(),
                        ItemName = row["ItemName"].ToString(),
                        ItemPrice = row["Price"].ToString(),
                        ItemImage = row["ItemImage"].ToString(),
                        ShortDescription = row["ShortDescription"].ToString(),
                        ShortName = row["ShortName"].ToString()

                    }).ToList();

            System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string strJsonRe = "{\"" + requestMethod + "\":" + oSerializer.Serialize(list) + "}";
            return strJsonRe;
        }
        else
            return "";
    }

    #endregion

    #region METHOD GET QUICK ITEMS

    // function to get quick items and conert into json, return in string value
    private string JsonQuickItems(string requestMethod, string subCatId)
    {
        if (!string.IsNullOrEmpty(requestMethod))
        {
            List<Items> list = new List<Items>();
            DataTable dt = new DataTable();
            NameValueCollection nam = new NameValueCollection();
            nam.Add("_mode", "items");
            nam.Add("_id", subCatId);
            dt = objCommon.getDataTable(nam, "spPOS_QuickServices");
            list = (from DataRow row in dt.Rows
                    select new Items
                    {
                        ItemId = row["Id"].ToString(),
                        ItemName = row["ItemName"].ToString(),
                        ItemPrice = row["Price"].ToString(),
                        ItemImage = row["ItemImage"].ToString(),
                        ShortDescription = row["ShortDescription"].ToString(),
                        ShortName = row["ShortName"].ToString()

                    }).ToList();

            System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string strJsonRe = "{\"" + requestMethod + "\":" + oSerializer.Serialize(list) + "}";
            return strJsonRe;
        }
        else
            return "";
    }

    #endregion

    #region METHOD GET LAST ITEM DETAILS

    // function to get last item details
    private string GetLastItems(string lastItemName)
    {
        DataTable dt = new DataTable();
        NameValueCollection nam = new NameValueCollection();
        nam.Add("_itemname", lastItemName);
        dt = objCommon.getDataTable(nam, "sp_lastitemdetails");

        string result = "";

        foreach (DataRow row in dt.Rows)
        {
            result = "{\"ItemId\":\"" + row["ItemId"].ToString() + "\"},{\"ItemName\":\"" + row["ItemName"].ToString() + "\"},{\"CatId\":\"" + row["CatId"].ToString() + "\"},{\"CatName\":\"" + row["CategoryName"].ToString() + "\"},{\"SubCatId\":\"" + row["SubCatId"].ToString() + "\"},{\"SubCatName\":\"" + row["SubCategoryName"].ToString() + "\"}";
        }

        return result;
    }

    #endregion

    #region METHOD GET LAST ITEM DETAILS

    // function to get last item details
    private string GetLastItemsForFinalize(string lastItemName)
    {
        DataTable dt = new DataTable();
        NameValueCollection nam = new NameValueCollection();
        nam.Add("_itemname", lastItemName);
        dt = objCommon.getDataTable(nam, "sp_lastitemdetails");

        string result = "";

        foreach (DataRow row in dt.Rows)
        {
            result = "{\"ItemId\":\"" + row["ItemId"].ToString() + "\"},{\"ItemName\":\"" + row["ItemName"].ToString() + "\"}";
        }

        return result;
    }

    #endregion

    #region METHOD GET LAST ITEM NAME BY ORDER NUMBER

    // function to get last item name by order number
    private string GetLastItemName(string orderNumber)
    {
        DataTable dt = new DataTable();
        NameValueCollection nam = new NameValueCollection();
        nam.Add("_mode", "getlastitem");
        nam.Add("_id", orderNumber);
        nam.Add("_table", ""); // use table name to pass item name 
        dt = objCommon.getDataTable(nam, "sp_getrecord");
        return Convert.ToString(dt.Rows[0][0]);
    }

    #endregion

    #region METHOD FORGOT PASSWORD

    // function to send email for forgot password
    private string JsonForgotPassord(string requestMethod, string userId)
    {
        string jsonResult = "";
        if (!string.IsNullOrEmpty(requestMethod))
        {
            //get user id by memberid
            NameValueCollection nam = new NameValueCollection();
            nam.Add("_mode", "userid");
            nam.Add("_name", userId);
            DataTable dt = new DataTable();
            dt = objCommon.getDataTable(nam, "spPOS_GetByName");

            // save notification
            NameValueCollection namNotification = new NameValueCollection();
            namNotification.Add("_senderid", dt.Rows[0]["Id"].ToString());
            namNotification.Add("_receiverid", "0");
            namNotification.Add("_notification", "Forgot Password");
            objCommon.commonMethod(namNotification, "sp_notification");
            jsonResult = "{\"Status\":\"Your message has been sent successfully\"}";
            return jsonResult;
        }
        else
            return jsonResult;

    }

    #endregion

    #region METHOD FOR LOGOUT

    // function to send email for forgot password
    private string LogOut(string userId)
    {
        try
        {
            NameValueCollection nam = new NameValueCollection();
            nam.Add("_mode", "logout");
            nam.Add("_id", userId);
            DataSet ds = new DataSet();
            ds = objCommon.getData(nam, "spPOS_General");
            return "{\"Status\":\"Success\"}";
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    #endregion

    #region METHOD GET SEARCH DIRECTORY

    // function to get search directory
    private string JsonSearchDirectory(string requestMethod)
    {
        if (!string.IsNullOrEmpty(requestMethod))
        {
            string strJsonResult = "";

            List<Items> list = new List<Items>();
            DataTable dt = new DataTable();
            NameValueCollection nam = new NameValueCollection();
            nam.Add("_mode", "searchdirectory");
            nam.Add("_id", "0");
            nam.Add("_table", "");
            dt = objCommon.getDataTable(nam, "sp_getrecord");

            string items = "";
            foreach (DataRow row in dt.Rows)
            {
                items += "\"" + row["OfferName"].ToString() + "\"" + ",";
            }

            items = items.Remove(items.Length - 1, 1);

            strJsonResult = "{\"" + requestMethod + "\":[" + items + "]}";

            return strJsonResult;
        }
        else
            return "";
    }

    #endregion

    #region METHOD SEARCH ITEMS
    // function to get sub cagegory and conert into json, return in string value
    private string JsonSearchItems(string requestMethod, string searchText)
    {
        if (!string.IsNullOrEmpty(requestMethod))
        {
            List<SearchItems> list = new List<SearchItems>();
            DataTable dt = new DataTable();
            NameValueCollection nam = new NameValueCollection();
            nam.Add("_searchtext", searchText);
            dt = objCommon.getDataTable(nam, "sp_searchitems");
            list = (from DataRow row in dt.Rows
                    select new SearchItems
                    {
                        ItemId = row["Id"].ToString(),
                        ItemName = row["ItemName"].ToString(),
                        ItemPrice = row["Price"].ToString(),
                        CatetoryId = row["CategoryId"].ToString(),
                        SubCatetoryId = row["SubCatId"].ToString(),
                        IsOffer = row["IsOffer"].ToString()

                    }).ToList();
            System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string strJsonRe = "{\"" + requestMethod + "\":" + oSerializer.Serialize(list) + "}";
            return strJsonRe;
        }
        else
            return "";
    }

    #endregion

    #region METHOD GET ITEMS TOPPINGS BY ITEM ID

    // function to get item toppings by item id and convert into json, return in string value
    private string JsonItemToppings(string requestMethod, string itemId)
    {
        if (!string.IsNullOrEmpty(requestMethod))
        {
            List<ItemsToppings> list = new List<ItemsToppings>();
            DataTable dt = new DataTable();
            NameValueCollection nam = new NameValueCollection();
            nam.Add("_id", itemId);
            nam.Add("_by", "ItemId");
            nam.Add("_table", "itemtopping");
            dt = objCommon.getDataTable(nam, "sp_getgeneral");
            list = (from DataRow row in dt.Rows
                    select new ItemsToppings
                    {
                        ToppingId = row["Id"].ToString(),
                        ToppingName = row["ToppingName"].ToString(),
                        ToppingPrice = row["ToppingPrice"].ToString()

                    }).ToList();
            System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string strJsonRe = "{\"" + requestMethod + "\":" + oSerializer.Serialize(list) + "}";
            return strJsonRe;
        }
        else
            return "";
    }

    #endregion

    #region METHOD GET ITEMS TOPPINGS BY ITEM NAME

    // function to get item toppings by item name and convert into json, return in string value
    private string JsonItemToppingsByName(string requestMethod, string itemname)
    {
        if (!string.IsNullOrEmpty(requestMethod))
        {
            List<ItemsToppings> list = new List<ItemsToppings>();
            DataTable dt = new DataTable();
            NameValueCollection nam = new NameValueCollection();
            nam.Add("_mode", "itemtoppings");
            nam.Add("_itemname", itemname);
            dt = objCommon.getDataTable(nam, "spPOS_GetByItemName");
            list = (from DataRow row in dt.Rows
                    select new ItemsToppings
                    {
                        ToppingId = row["Id"].ToString(),
                        ToppingName = row["ToppingName"].ToString(),
                        ToppingPrice = row["ToppingPrice"].ToString()

                    }).ToList();
            System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string strJsonRe = "{\"" + requestMethod + "\":" + oSerializer.Serialize(list) + "}";
            return strJsonRe;
        }
        else
            return "";
    }

    #endregion

    #region METHOD GET ITEM'S  CAT SUB-CAT DETAILS BY ITEM NAME

    // function to get item cat sub-cat details by item name and convert into json, return in string value
    private string JsonItemCatSubcatByName(string requestMethod, string itemname)
    {
        if (!string.IsNullOrEmpty(requestMethod))
        {
            List<ItemsCatSubCat> list = new List<ItemsCatSubCat>();
            DataTable dt = new DataTable();
            NameValueCollection nam = new NameValueCollection();
            nam.Add("_mode", "itemtcatsubcat");
            nam.Add("_itemname", itemname);
            dt = objCommon.getDataTable(nam, "spPOS_GetByItemName");
            list = (from DataRow row in dt.Rows
                    select new ItemsCatSubCat
                    {
                        CatId = row["CatId"].ToString(),
                        CatName = row["CategoryName"].ToString(),
                        SubCatId = row["SubCatId"].ToString(),
                        SubCatName = row["SubCategoryName"].ToString()

                    }).ToList();
            System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string strJsonRe = "{\"" + requestMethod + "\":" + oSerializer.Serialize(list) + "}";
            return strJsonRe;
        }
        else
            return "";
    }

    #endregion

    #region METHOD CHECK ORDER IS SAVED OR SEND TO KITCHEN

    // function to get sub cagegory and conert into json, return in string value
    private string CheckOrder(string tableNumber)
    {
        string Error = "";
        if (Convert.ToString(tableNumber) != String.Empty)
        {
            string status = "";
            NameValueCollection nam = new NameValueCollection();
            nam.Add("_tablenumber", tableNumber);

            int result = Convert.ToInt16(objCommon.commonWithOutPut(nam, "sp_checkorder", "_result"));

            if (result > 0)
                status = "True";
            else
                status = "False";
            return "{\"Status\":\"" + status + "\"}";
        }
        else
            Error = "Please enter an ongoing order";
        return "{\"Error\":\"" + Error + "\"}";
    }

    #endregion

    #region METHOD SUB ADMIN LOGIN

    // function to get cagegory and conert into json, return in string value
    private string JsonSubAdminLogin(string userName, string userPassword)
    {
        string strJsonRe = "";
        if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(userPassword))
        {
            List<settings> list = new List<settings>();
            NameValueCollection nam = new NameValueCollection();
            nam.Add("_username", userName);
            nam.Add("_password", objEncrypt.Encrypt(userPassword));
            string loginId = objCommon.commonWithOutPut(nam, "sp_subadmin", "_loginid");

            if (Convert.ToInt16(loginId) > 0)
            {

                NameValueCollection namData = new NameValueCollection();
                DataTable dt = new DataTable();
                dt = objCommon.getDataTable(namData, "sp_getsettings");

                dt.Columns.Add(new DataColumn("LoginId", typeof(string)));

                foreach (DataRow row in dt.Rows)
                {
                    row["LoginId"] = loginId;
                }
                string userType = "", EmployeeName = "";
                GetUserDetails(out userType, out EmployeeName, loginId);
                list = (from DataRow row in dt.Rows
                        select new settings
                        {

                            Currency = row["CurrencyFormat"].ToString(),
                            TaxRate = row["TaxRate"].ToString(),
                            Discount = row["DiscountRate"].ToString(),
                            LoginId = row["LoginId"].ToString(),
                            UserType = userType,
                            EmployeeName = EmployeeName

                        }).ToList();

                System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                strJsonRe = oSerializer.Serialize(list);

                strJsonRe = strJsonRe.Remove(strJsonRe.Length - 2, 2) + "," + CardSurcharge() + "}]";

                strJsonRe = "{\"LoginInfo\":" + strJsonRe + "}";
            }
            else
            {
                strJsonRe = "{\"Error\":\"Invalid username/ password !\"}";
            }
        }
        else
            strJsonRe = "";

        return strJsonRe;
    }

    #endregion

    #region METHOD RESTAURANT DETAILS

    // function to get restaurant details into json, return in string value
    private string JsonRestaurantDetails(string requestMethod)
    {
        string strJsonRe = "";
        try
        {
            List<restaurantdetails> list = new List<restaurantdetails>();
            DataTable dt = new DataTable();

            NameValueCollection nam = new NameValueCollection();
            nam.Add("_mode", "restaurantdetails");
            nam.Add("_id", "0");
            dt = objCommon.getDataTable(nam, "spPOS_General");

            list = (from DataRow row in dt.Rows
                    select new restaurantdetails
                    {
                        ResturantName = row["CompanyName"].ToString(),
                        ResturantPhone = row["CompanyNumber"].ToString(),
                        OwnerName = row["OwnerName"].ToString(),
                        ResturantAddress1 = row["CompanyAddress"].ToString(),
                        ResturantAddress2 = row["Address2"].ToString(),
                        ResturantFax = row["FaxNumber"].ToString(),
                        EmailAddress = row["EmailAddress"].ToString(),
                        ResturantLogo = row["CompanyLogo"].ToString(),
                        City = row["City"].ToString(),
                        State = row["State"].ToString(),
                        Country = row["Country"].ToString()

                    }).ToList();

            System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            strJsonRe = "{\"" + requestMethod + "\":" + oSerializer.Serialize(list) + "}";

        }
        catch (Exception ex)
        {
            strJsonRe = "";
        }

        return strJsonRe;
    }

    #endregion

    #region METHOD GET SUB CATEGORY

    // function to get sub cagegory and conert into json, return in string value
    private string JsonSubCategory(string requestMethod, string catId)
    {
        if (!string.IsNullOrEmpty(requestMethod))
        {
            List<SubCategory> list = new List<SubCategory>();
            DataTable dt = new DataTable();
            NameValueCollection nam = new NameValueCollection();
            nam.Add("_mode", "subcategory-ipad");
            nam.Add("_id", catId);
            dt = objCommon.getDataTable(nam, "spPOS_General");
            list = (from DataRow row in dt.Rows
                    select new SubCategory
                    {
                        SubCatId = row["Id"].ToString(),
                        SubCatName = row["SubCategoryName"].ToString(),
                        SubCatImage = row["SubCategoryImage"].ToString()

                    }).ToList();
            System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string strJsonRe = "{\"" + requestMethod + "\":" + oSerializer.Serialize(list) + "}";
            return strJsonRe;
        }
        else
            return "";
    }

    #endregion

    #region METHOD GET QUICK SUB CATEGORY

    // function to get sub cagegory and conert into json, return in string value
    private string JsonQuickSubCategory(string requestMethod, string catId)
    {
        if (!string.IsNullOrEmpty(requestMethod))
        {

            string strJsonRe = "";

            string sameSubCat = CheckSameSubCategory(catId);

            if (sameSubCat == "Y") // get items if same sub-category
            {

                List<Items> list = new List<Items>();
                DataTable dt = new DataTable();
                NameValueCollection nam = new NameValueCollection();
                nam.Add("_mode", "items-samesubcat");
                nam.Add("_id", catId);
                dt = objCommon.getDataTable(nam, "spPOS_QuickServices");
                list = (from DataRow row in dt.Rows
                        select new Items
                        {
                            ItemId = row["Id"].ToString(),
                            ItemName = row["ItemName"].ToString(),
                            ItemPrice = row["Price"].ToString(),
                            ItemImage = row["ItemImage"].ToString(),
                            ShortDescription = row["ShortDescription"].ToString(),
                            ShortName = row["ShortName"].ToString()

                        }).ToList();

                System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                strJsonRe = "{\"quickitems\":" + oSerializer.Serialize(list) + "}";
            }
            else // get sub-category
            {
                List<SubCategory> list = new List<SubCategory>();
                DataTable dt = new DataTable();
                NameValueCollection nam = new NameValueCollection();
                nam.Add("_mode", "subcategory");
                nam.Add("_id", catId);
                dt = objCommon.getDataTable(nam, "spPOS_QuickServices");
                list = (from DataRow row in dt.Rows
                        select new SubCategory
                        {
                            SubCatId = row["Id"].ToString(),
                            SubCatName = row["SubCategoryName"].ToString(),
                            SubCatImage = row["SubCategoryImage"].ToString()

                        }).ToList();
                System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                strJsonRe = "{\"quicsubcategory\":" + oSerializer.Serialize(list) + "}";

            }

            return strJsonRe;
        }
        else
            return "";
    }

    #endregion

    # region METHOD GET CATEGORY

    // function to get cagegory and convert into json, return in string value
    private string JsonCategory(string requestMethod)
    {
        if (!string.IsNullOrEmpty(requestMethod))
        {
            List<Category> list = new List<Category>();
            DataTable dt = new DataTable();
            NameValueCollection nam = new NameValueCollection();
            nam.Add("_mode", "category-ipad");
            nam.Add("_id", "0");
            dt = objCommon.getDataTable(nam, "spPOS_General");
            list = (from DataRow row in dt.Rows
                    select new Category
                    {
                        CatId = row["Id"].ToString(),
                        CatName = row["CategoryName"].ToString()

                    }).ToList();
            System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string strJsonRe = "{\"" + requestMethod + "\":" + oSerializer.Serialize(list) + "}";
            return strJsonRe;
        }
        else
            return "";
    }

    #endregion

    # region METHOD GET QUICK CATEGORY

    // function to get cagegory for quick service and convert into json, return in string value
    private string JsonQuickCategory(string requestMethod)
    {
        if (!string.IsNullOrEmpty(requestMethod))
        {
            List<Category> list = new List<Category>();
            DataTable dt = new DataTable();
            NameValueCollection nam = new NameValueCollection();
            nam.Add("_mode", "category");
            nam.Add("_id", "0");
            dt = objCommon.getDataTable(nam, "spPOS_QuickServices");
            list = (from DataRow row in dt.Rows
                    select new Category
                    {
                        CatId = row["Id"].ToString(),
                        CatName = row["CategoryName"].ToString()

                    }).ToList();
            System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string strJsonRe = "{\"" + requestMethod + "\":" + oSerializer.Serialize(list) + "}";
            return strJsonRe;
        }
        else
            return "";
    }

    #endregion

    #region GET ORDER ITEM DETAILS BY ORDER NUMBER

    protected string JsonGetOrderItemsByOrderNumber(string orderNumber)
    {
        string jsonResult = "";
        try
        {
            NameValueCollection namVal = new NameValueCollection();
            DataTable dt = new DataTable();
            namVal.Add("_mode", "orderitems");
            namVal.Add("_id", orderNumber);
            namVal.Add("_table", "");
            dt = objCommon.getDataTable(namVal, "sp_getrecord");

            if (dt.Rows.Count > 0)
            {
                string itemDetails = "", itemRemarks = "", itemToppings = "", itemToppingsFinal = "", itemPrice = "";

                foreach (DataRow row in dt.Rows)
                {

                    itemDetails += "\"" + row["ItemName"] + "\":\"" + row["ItemQty"].ToString() + "\",";

                    if (!string.IsNullOrEmpty(row["Remarks"].ToString()))
                        itemRemarks += "\"" + row["ItemName"] + "\":\"" + row["Remarks"].ToString() + "\",";

                    string[] arrToppings = null;
                    if (!string.IsNullOrEmpty(row["Toppings"].ToString()))
                    {
                        arrToppings = row["Toppings"].ToString().Split(';');
                        int count = 1;
                        foreach (string toppings in arrToppings)
                        {
                            string[] arrTop = toppings.Split(',');
                            string topp = "";
                            foreach (string t in arrTop)
                            {
                                topp += "{\"" + t + "\":\"" + GetToppingPrice(t) + "\"},";
                            }

                            itemToppings += "\"" + row["ItemName"].ToString() + count.ToString() + "\":[" + topp.Remove(topp.Length - 1, 1) + "],";
                            count += 1;
                            topp = "";
                        }

                        itemToppingsFinal += "\"" + row["ItemName"] + "\":{" + itemToppings.Remove(itemToppings.Length - 1, 1) + "},";
                        itemToppings = "";
                    }
                }

                if (!string.IsNullOrEmpty(itemDetails))
                {
                    itemDetails = itemDetails.Remove(itemDetails.Length - 1, 1);
                    jsonResult = "{\"orderitems\":[{\"itemdetails\":{" + itemDetails + "}," + GetItemPrice(orderNumber);
                }

                if (!string.IsNullOrEmpty(itemRemarks))
                {
                    itemRemarks = itemRemarks.Remove(itemRemarks.Length - 1, 1);
                    jsonResult = jsonResult + ",\"itemremakrs\":{" + itemRemarks + "}";
                }

                if (!string.IsNullOrEmpty(itemToppingsFinal))
                {
                    itemToppingsFinal = itemToppingsFinal.Remove(itemToppingsFinal.Length - 1, 1);
                    jsonResult = jsonResult + ",\"itemtoppings\":{" + itemToppingsFinal.Remove(itemToppingsFinal.Length - 1, 1) + "}";
                }

                if (!string.IsNullOrEmpty(itemToppingsFinal))
                {
                    if (!string.IsNullOrEmpty(GetLastItems(GetLastItemName(orderNumber))))
                    {
                        jsonResult = jsonResult + "}}," + GetLastItems(GetLastItemName(orderNumber));

                        if (GetUrlValue("method") == "orderitemsedit")
                            jsonResult = jsonResult + "," + ItemStatusOrderNumber(orderNumber) + "]}";
                        else
                            jsonResult = jsonResult + "]}";
                    }
                    else
                        jsonResult = jsonResult + "}}]}";
                }
                else
                {
                    if (!string.IsNullOrEmpty(GetLastItems(GetLastItemName(orderNumber))))
                    {
                        jsonResult = jsonResult + "}," + GetLastItems(GetLastItemName(orderNumber));

                        if (GetUrlValue("method") == "orderitemsedit")
                            jsonResult = jsonResult + "," + ItemStatusOrderNumber(orderNumber) + "]}";
                        else
                            jsonResult = jsonResult + "]}";

                    }
                    else
                        jsonResult = jsonResult + "}]}";
                }
            }
            else
                jsonResult = "{\"Error\":\"Sorry, this order number not in process\"}";

            return jsonResult;

        }
        catch (Exception ex)
        {
            jsonResult = "{\"Error\":\"Invalid order number\"}";
        }
        return jsonResult;
    }

    #endregion

    #region METHOD TO UPDATE WAITING TIME BY ORDER NUMBER
    // function to get sub cagegory and conert into json, return in string value
    private int UpdateWaitingTime(string orderNumber, string time)
    {
        try
        {
            NameValueCollection nam = new NameValueCollection();
            nam.Add("_ordernumber", orderNumber);
            nam.Add("_waitingtime", time);
            return Convert.ToInt16(objCommon.commonWithOutPut(nam, "sp_updatewaitingtime", "_result"));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion

    #region GET ORDER ITEM DETAILS BY TABLE NUMBER

    protected string JsonGetOrderItemsByTableNumber(string tableNumber)
    {
        string jsonResult = "";
        try
        {
            NameValueCollection namVal = new NameValueCollection();

            DataTable dt = new DataTable();
            namVal.Add("_mode", "orderitemsbytable");
            namVal.Add("_id", tableNumber);
            namVal.Add("_table", "");

            dt = objCommon.getDataTable(namVal, "sp_getrecord");

            if (dt.Rows.Count > 0)
            {
                string itemDetails = "", itemRemarks = "", itemToppings = "", itemToppingsFinal = "", itemPrice = "";

                foreach (DataRow row in dt.Rows)
                {

                    itemDetails += "\"" + row["ItemName"] + "\":\"" + row["ItemQty"].ToString() + "\",";

                    if (!string.IsNullOrEmpty(row["Remarks"].ToString()))
                        itemRemarks += "\"" + row["ItemName"] + "\":\"" + row["Remarks"].ToString() + "\",";

                    string[] arrToppings = null;
                    if (!string.IsNullOrEmpty(row["Toppings"].ToString()))
                    {
                        arrToppings = row["Toppings"].ToString().Split(';');
                        int count = 1;
                        foreach (string toppings in arrToppings)
                        {
                            string[] arrTop = toppings.Split(',');
                            string topp = "";

                            foreach (string t in arrTop)
                            {
                                topp += "{\"" + t + "\":\"" + GetToppingPrice(t) + "\"},";
                            }

                            itemToppings += "\"" + row["ItemName"].ToString() + count.ToString() + "\":[" + topp.Remove(topp.Length - 1, 1) + "],";
                            count += 1;
                            topp = "";
                        }

                        itemToppingsFinal += "\"" + row["ItemName"] + "\":{" + itemToppings.Remove(itemToppings.Length - 1, 1) + "},";
                        itemToppings = "";
                    }
                }

                if (!string.IsNullOrEmpty(itemDetails))
                {
                    itemDetails = itemDetails.Remove(itemDetails.Length - 1, 1);
                    jsonResult = "{\"orderitems\":[{\"itemdetails\":{" + itemDetails + "}," + GetItemPrice(GetOrderNumberByTable(tableNumber)) + "";
                }

                if (!string.IsNullOrEmpty(itemRemarks))
                {
                    itemRemarks = itemRemarks.Remove(itemRemarks.Length - 1, 1);
                    jsonResult = jsonResult + ",\"itemremakrs\":{" + itemRemarks + "}";
                }

                if (!string.IsNullOrEmpty(itemToppingsFinal))
                {
                    itemToppingsFinal = itemToppingsFinal.Remove(itemToppingsFinal.Length - 1, 1);
                    jsonResult = jsonResult + ",\"itemtoppings\":{" + itemToppingsFinal.Remove(itemToppingsFinal.Length - 1, 1) + "}";
                }

                if (!string.IsNullOrEmpty(itemToppingsFinal))
                    jsonResult = jsonResult + "}},{\"ordernumber\":\"" + GetOrderNumberByTable(tableNumber) + "\"}," + GetLastItems(GetLastItemName(GetOrderNumberByTable(tableNumber))) + "]}";
                else
                    jsonResult = jsonResult + "},{\"ordernumber\":\"" + GetOrderNumberByTable(tableNumber) + "\"}," + GetLastItems(GetLastItemName(GetOrderNumberByTable(tableNumber))) + " ]}";
            }
            else
                jsonResult = "{\"Error\":\"Sorry, this table number not in process\"}";

            return jsonResult;

        }
        catch (Exception ex)
        {
            jsonResult = "{\"Error\":\"Invalid order number\"}";
        }
        return jsonResult;
    }

    #endregion

    #region GET ORDER ITEM DETAILS BY TABLE NUMBER FOR FINALIZE

    protected string JsonGetOrderItemsByTableNumberForFinalize(string tableNumber)
    {
        //string jsonResult = "{\"finalize\":[[{\"itemdetails\":{\"Chicken Plate\":\"1\"},\"itemprice\":{\"Chicken Plate\":\"120\"}},{\"ItemId\":\"1\"},{\"ItemName\":\"Chicken Plate\"},{\"OrderNumber\":\"176\"}],[{\"itemdetails\":{\"Chicken Plate\":\"1\"},\"itemprice\":{\"Chicken Plate\":\"120\"}},{\"ItemId\":\"1\"},{\"ItemName\":\"Chicken Plate\"},{\"OrderNumber\":\"177\"}],[{\"itemdetails\":{\"Chicken Plate\":\"1\"},\"itemprice\":{\"Chicken Plate\":\"120\"}},{\"ItemId\":\"1\"},{\"ItemName\":\"Chicken Plate\"},{\"OrderNumber\":\"178\"}]]}";


        string jsonFinalResult = "";
        try
        {

            NameValueCollection namOrder = new NameValueCollection();
            DataTable dtOrder = new DataTable();
            namOrder.Add("_mode", "finalizeordernumber");
            namOrder.Add("_id", tableNumber);
            dtOrder = objCommon.getDataTable(namOrder, "spPOS_General");

            foreach (DataRow rowOrder in dtOrder.Rows)
            {
                string orderNumber = rowOrder["OrderNumber"].ToString();
                string jsonResult = "";

                NameValueCollection namVal = new NameValueCollection();
                DataTable dt = new DataTable();
                namVal.Add("_mode", "orderitems");
                namVal.Add("_id", orderNumber);
                namVal.Add("_table", "");
                dt = objCommon.getDataTable(namVal, "sp_getrecord");

                if (dt.Rows.Count > 0)
                {
                    string itemDetails = "", itemRemarks = "", itemToppings = "", itemToppingsFinal = "", itemPrice = "";

                    foreach (DataRow row in dt.Rows)
                    {

                        itemDetails += "\"" + row["ItemName"] + "\":\"" + row["ItemQty"].ToString() + "\",";

                        if (!string.IsNullOrEmpty(row["Remarks"].ToString()))
                            itemRemarks += "\"" + row["ItemName"] + "\":\"" + row["Remarks"].ToString() + "\",";

                        string[] arrToppings = null;
                        if (!string.IsNullOrEmpty(row["Toppings"].ToString()))
                        {
                            arrToppings = row["Toppings"].ToString().Split(';');
                            int count = 1;
                            foreach (string toppings in arrToppings)
                            {
                                string[] arrTop = toppings.Split(',');
                                string topp = "";
                                foreach (string t in arrTop)
                                {
                                    topp += "{\"" + t + "\":\"" + GetToppingPrice(t) + "\"},";
                                }

                                itemToppings += "\"" + row["ItemName"].ToString() + count.ToString() + "\":[" + topp.Remove(topp.Length - 1, 1) + "],";
                                count += 1;
                                topp = "";
                            }

                            itemToppingsFinal += "\"" + row["ItemName"] + "\":{" + itemToppings.Remove(itemToppings.Length - 1, 1) + "},";
                            itemToppings = "";
                        }
                    }

                    if (!string.IsNullOrEmpty(itemDetails))
                    {
                        itemDetails = itemDetails.Remove(itemDetails.Length - 1, 1);
                        jsonResult = "[{\"itemdetails\":{" + itemDetails + "}," + GetItemPrice(orderNumber);
                    }

                    if (!string.IsNullOrEmpty(itemRemarks))
                    {
                        itemRemarks = itemRemarks.Remove(itemRemarks.Length - 1, 1);
                        jsonResult = jsonResult + ",\"itemremakrs\":{" + itemRemarks + "}";
                    }

                    if (!string.IsNullOrEmpty(itemToppingsFinal))
                    {
                        itemToppingsFinal = itemToppingsFinal.Remove(itemToppingsFinal.Length - 1, 1);
                        jsonResult = jsonResult + ",\"itemtoppings\":{" + itemToppingsFinal.Remove(itemToppingsFinal.Length - 1, 1) + "}";
                    }

                    if (!string.IsNullOrEmpty(itemToppingsFinal))
                    {
                        if (!string.IsNullOrEmpty(GetLastItemsForFinalize(GetLastItemName(orderNumber))))
                            jsonResult = jsonResult + "}}," + GetLastItemsForFinalize(GetLastItemName(orderNumber)) + ",{\"OrderNumber\":\"" + orderNumber + "\"}" + "]";
                        else
                            jsonResult = jsonResult + "}}]";
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(GetLastItemsForFinalize(GetLastItemName(orderNumber))))
                            jsonResult = jsonResult + "}," + GetLastItemsForFinalize(GetLastItemName(orderNumber)) + ",{\"OrderNumber\":\"" + orderNumber + "\"}" + "]";
                        else
                            jsonResult = jsonResult + "}]";
                    }

                    jsonFinalResult += jsonResult + ",";
                }
                else
                    jsonFinalResult = "{\"Error\":\"Sorry, this order number not in process\"}";
            }

            return "{\"finalize\":[" + jsonFinalResult.Remove(jsonFinalResult.Length - 1, 1) + "]}";

        }
        catch (Exception ex)
        {
            jsonFinalResult = "{\"Error\":\"Invalid order number\"}";
        }

        return jsonFinalResult;



    }

    #endregion

    #region GET ORDER ITEM DETAILS FOR CURRENT ORDER NUMBER

    protected string JsonGetPickUpOrderDetails(string orderNumber)
    {
        string jsonFinalResult = "";
        try
        {
            string jsonResult = "";

            NameValueCollection namVal = new NameValueCollection();
            DataTable dt = new DataTable();
            namVal.Add("_mode", "orderitems");
            namVal.Add("_id", orderNumber);
            namVal.Add("_table", "");
            dt = objCommon.getDataTable(namVal, "sp_getrecord");

            if (dt.Rows.Count > 0)
            {
                string itemDetails = "", itemRemarks = "", itemToppings = "", itemToppingsFinal = "", itemPrice = "";

                foreach (DataRow row in dt.Rows)
                {

                    itemDetails += "\"" + row["ItemName"] + "\":\"" + row["ItemQty"].ToString() + "\",";

                    if (!string.IsNullOrEmpty(row["Remarks"].ToString()))
                        itemRemarks += "\"" + row["ItemName"] + "\":\"" + row["Remarks"].ToString() + "\",";

                    string[] arrToppings = null;
                    if (!string.IsNullOrEmpty(row["Toppings"].ToString()))
                    {
                        arrToppings = row["Toppings"].ToString().Split(';');
                        int count = 1;
                        foreach (string toppings in arrToppings)
                        {
                            string[] arrTop = toppings.Split(',');
                            string topp = "";
                            foreach (string t in arrTop)
                            {
                                topp += "{\"" + t + "\":\"" + GetToppingPrice(t) + "\"},";
                            }

                            itemToppings += "\"" + row["ItemName"].ToString() + count.ToString() + "\":[" + topp.Remove(topp.Length - 1, 1) + "],";
                            count += 1;
                            topp = "";
                        }

                        itemToppingsFinal += "\"" + row["ItemName"] + "\":{" + itemToppings.Remove(itemToppings.Length - 1, 1) + "},";
                        itemToppings = "";
                    }
                }

                if (!string.IsNullOrEmpty(itemDetails))
                {
                    itemDetails = itemDetails.Remove(itemDetails.Length - 1, 1);
                    jsonResult = "[{\"itemdetails\":{" + itemDetails + "}," + GetItemPrice(orderNumber);
                }

                if (!string.IsNullOrEmpty(itemRemarks))
                {
                    itemRemarks = itemRemarks.Remove(itemRemarks.Length - 1, 1);
                    jsonResult = jsonResult + ",\"itemremakrs\":{" + itemRemarks + "}";
                }

                if (!string.IsNullOrEmpty(itemToppingsFinal))
                {
                    itemToppingsFinal = itemToppingsFinal.Remove(itemToppingsFinal.Length - 1, 1);
                    jsonResult = jsonResult + ",\"itemtoppings\":{" + itemToppingsFinal.Remove(itemToppingsFinal.Length - 1, 1) + "}";
                }

                if (!string.IsNullOrEmpty(itemToppingsFinal))
                {
                    if (!string.IsNullOrEmpty(GetLastItemsForFinalize(GetLastItemName(orderNumber))))
                        jsonResult = jsonResult + "}}," + GetLastItemsForFinalize(GetLastItemName(orderNumber)) + ",{\"OrderNumber\":\"" + orderNumber + "\"}" + "]";
                    else
                        jsonResult = jsonResult + "}}]";
                }
                else
                {
                    if (!string.IsNullOrEmpty(GetLastItemsForFinalize(GetLastItemName(orderNumber))))
                        jsonResult = jsonResult + "}," + GetLastItemsForFinalize(GetLastItemName(orderNumber)) + ",{\"OrderNumber\":\"" + orderNumber + "\"}" + "]";
                    else
                        jsonResult = jsonResult + "}]";
                }

                jsonFinalResult += jsonResult + ",";
            }
            else
                jsonFinalResult = "{\"Error\":\"Sorry, this order number not in process\"}";


            return "{\"pickuporderdetails\":" + jsonFinalResult.Remove(jsonFinalResult.Length - 1, 1) + "}";

        }
        catch (Exception ex)
        {
            jsonFinalResult = "{\"Error\":\"Invalid order number\"}";
        }

        return jsonFinalResult;



    }

    #endregion

    #region GET ORDER NUMBERS ON TABLE

    protected string JsonOrderOnTable(string tableNumber)
    {
        string jsonResult = "";
        try
        {
            NameValueCollection nam = new NameValueCollection();
            nam.Add("_tablenumber", tableNumber);
            nam.Add("_orderno", "0");
            DataSet ds = new DataSet();
            ds = objCommon.getData(nam, "spPOS_TotalByTable");

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int count = 0; count < ds.Tables[0].Rows.Count; count++)
                {
                    string strOrderNo = Convert.ToString(ds.Tables[0].Rows[count][0]);
                    string strTotal = Convert.ToString(ds.Tables[0].Rows[count][1]);

                    NameValueCollection namToping = new NameValueCollection();
                    namToping.Add("_tablenumber", "0");
                    namToping.Add("_orderno", strOrderNo);
                    DataSet dsTopping = new DataSet();
                    dsTopping = objCommon.getData(namToping, "spPOS_TotalByTable");
                    if (dsTopping.Tables[0].Rows.Count > 0)
                    {
                        for (int countT = 0; countT < dsTopping.Tables[0].Rows.Count; countT++)
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(dsTopping.Tables[0].Rows[countT][0])))
                            {
                                string[] toppingname = Convert.ToString(dsTopping.Tables[0].Rows[countT][0]).Split(';');
                                foreach (string st in toppingname)
                                {
                                    string toppingPrice = GetToppingPrice(st);
                                    strTotal = Convert.ToString(Convert.ToDouble(strTotal) + Convert.ToDouble(toppingPrice));
                                }
                            }
                        }
                    }

                    jsonResult += "{\"OrderNo\":\"" + strOrderNo + "\",\"SubTotal\":\"" + strTotal + "\",\"OrderStatus\":\"" + GetOrderStatus(strOrderNo) + "\"}" + ",";

                }
            }
            else
                jsonResult = "";

            jsonResult = jsonResult.Remove(jsonResult.Length - 1, 1);
            jsonResult = "{\"orderontable\":[" + jsonResult + "]}";
        }
        catch (Exception ex)
        {
            jsonResult = "";
        }
        return jsonResult;
    }

    #endregion

    #region GET ORDER NUMBERS ON TABLE

    protected string JsonOrderOnPhoneNumber(string phoneNumber)
    {
        string jsonResult = "";
        try
        {
            NameValueCollection nam = new NameValueCollection();
            nam.Add("_phonenumber", phoneNumber);
            nam.Add("_orderno", "");
            DataSet ds = new DataSet();
            ds = objCommon.getData(nam, "spPOS_TotalByPhone");

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int count = 0; count < ds.Tables[0].Rows.Count; count++)
                {
                    string strOrderNo = Convert.ToString(ds.Tables[0].Rows[count][0]);
                    string strTotal = Convert.ToString(ds.Tables[0].Rows[count][1]);

                    NameValueCollection namToping = new NameValueCollection();
                    namToping.Add("_phonenumber", "");
                    namToping.Add("_orderno", strOrderNo);
                    DataSet dsTopping = new DataSet();
                    dsTopping = objCommon.getData(namToping, "spPOS_TotalByPhone");
                    if (dsTopping.Tables[0].Rows.Count > 0)
                    {
                        for (int countT = 0; countT < dsTopping.Tables[0].Rows.Count; countT++)
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(dsTopping.Tables[0].Rows[countT][0])))
                            {
                                string[] toppingname = Convert.ToString(dsTopping.Tables[0].Rows[countT][0]).Split(';');
                                foreach (string st in toppingname)
                                {
                                    string toppingPrice = GetToppingPrice(st);
                                    strTotal = Convert.ToString(Convert.ToDouble(strTotal) + Convert.ToDouble(toppingPrice));
                                }
                            }
                        }
                    }

                    jsonResult += "{\"OrderNo\":\"" + strOrderNo + "\",\"SubTotal\":\"" + strTotal + "\"}" + ",";

                }
            }
            else
                jsonResult = "";

            jsonResult = jsonResult.Remove(jsonResult.Length - 1, 1);
            jsonResult = "{\"orderontable\":[" + jsonResult + "]}";
        }
        catch (Exception ex)
        {
            jsonResult = "";
        }
        return jsonResult;
    }

    #endregion

    #region METHOD TO GET ITEM PRICE BY ORDER NUMBER
    private string GetItemPrice(string orderNumber)
    {

        string finalPrice = "";

        try
        {
            NameValueCollection nam = new NameValueCollection();
            nam.Add("_ordernumber", orderNumber);
            nam.Add("_toppingname", "");
            DataSet ds = new DataSet();
            ds = objCommon.getData(nam, "sp_price");

            int[] price = new int[Convert.ToInt16(ds.Tables[0].Rows.Count)];
            for (int count = 0; count < Convert.ToInt16(ds.Tables[0].Rows.Count); count++)
            {
                double sum = 0;
                string item = Convert.ToString(ds.Tables[0].Rows[count][0]);
                string[] toppingname = Convert.ToString(ds.Tables[0].Rows[count][2]).Split(';');
                int itemPrice = Convert.ToInt16(ds.Tables[0].Rows[count][1]);
                price[count] = itemPrice;
                double total = sum + itemPrice;

                finalPrice += "\"" + item + "\":\"" + total + "\",";
            }
            return "\"itemprice\":{" + finalPrice.Remove(finalPrice.Length - 1, 1) + "}";
        }
        catch (Exception ex)
        {
            return "";
        }

    }
    #endregion

    #region METHOD TO GET CURRENT ORDER NUMBER BY TABLE NUMBER
    private string GetOrderNumberByTable(string tableNumber)
    {
        try
        {
            NameValueCollection nam = new NameValueCollection();
            nam.Add("_mode", "ordernumber");
            nam.Add("_id", tableNumber);
            nam.Add("_table", "");
            DataSet ds = new DataSet();
            ds = objCommon.getData(nam, "sp_getrecord");
            return Convert.ToString(ds.Tables[0].Rows[0][0]);
        }
        catch (Exception ex)
        {
            return "";
        }

    }
    #endregion

    #region METHOD TO GET TOPPING PRICE BY TOPPING NAME
    private string GetToppingPrice(string toppingName)
    {
        try
        {
            NameValueCollection nam = new NameValueCollection();
            nam.Add("_ordernumber", "0");
            nam.Add("_toppingname", toppingName);
            DataSet ds = new DataSet();
            ds = objCommon.getData(nam, "sp_price");

            return Convert.ToString(ds.Tables[0].Rows[0][0]);
        }
        catch (Exception ex)
        {
            return "";
        }

    }
    #endregion

    #region METHOD TO GET TABLE REPORT HOURLY BASES RETURN JSON RESULT

    // method to get json result for table report on hourly bases
    protected string JsonReportByTable(string requestMethod, string startTime, string endTime, string strDate)
    {
        string strJsonResult = "";

        try
        {
            List<ReportTable> reportTable = new List<ReportTable>();
            DataTable dt = getReportByTable(startTime, endTime, strDate);
            reportTable = (from DataRow row in dt.Rows
                           select new ReportTable
                           {
                               SrNo = row["Sr.No"].ToString(),
                               TableNumber = row["TableNumber"].ToString(),
                               Occupied = row["Occupied"].ToString(),
                               HourlySales = row["HourlySales"].ToString()

                           }).ToList();
            System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            strJsonResult = "{\"" + requestMethod + "\":" + oSerializer.Serialize(reportTable) + "}";
        }
        catch (Exception ex)
        {
            strJsonResult = "";
        }

        return strJsonResult;

    }

    #endregion

    #region METHOD GET HOURLY REPORT BY TABLE

    // return datatable for json method
    protected DataTable getReportByTable(string startTime, string endTime, string strDate)
    {
        NameValueCollection nam = new NameValueCollection();
        nam.Add("_mode", "table");
        nam.Add("_starttime", startTime);
        nam.Add("_endtime", endTime);
        nam.Add("_date", strDate);
        DataSet ds = new DataSet();
        ds = objCommon.getData(nam, "spPOS_Report");
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("Sr.No", typeof(string)));
        dt.Columns.Add(new DataColumn("TableNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("Occupied", typeof(string)));
        dt.Columns.Add(new DataColumn("HourlySales", typeof(string)));

        for (int count = 1; count <= ((Convert.ToInt16(ds.Tables[0].Rows.Count)) & (Convert.ToInt16(ds.Tables[1].Rows.Count))); count++)
        {
            DataRow dr = dt.NewRow();
            dr["Sr.No"] = Convert.ToString(count);
            dr["TableNumber"] = Convert.ToString(ds.Tables[1].Rows[count - 1][0]);
            dr["Occupied"] = Convert.ToString(ds.Tables[0].Rows[count - 1][0]);
            dr["HourlySales"] = Convert.ToString(ds.Tables[1].Rows[count - 1][0]);
            dt.Rows.Add(dr);
        }
        return dt;
    }

    #endregion

    #region METHOD TO GET USERTYPE BY USERID
    private void GetUserDetails(out string userType, out string employeeName, string userId)
    {
        try
        {
            NameValueCollection nam = new NameValueCollection();
            nam.Add("_mode", "getuserdetails");
            nam.Add("_id", userId);
            DataSet ds = new DataSet();
            ds = objCommon.getData(nam, "spPOS_General");
            userType = Convert.ToString(ds.Tables[0].Rows[0][0]);
            employeeName = Convert.ToString(ds.Tables[0].Rows[0][1]);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    #endregion

    #region METHOD GET VALUE FROM URL
    // function to get query string value retun a sring value
    protected string GetUrlValue(string key)
    {
        if (!string.IsNullOrEmpty(key))
            return Request.QueryString[key];
        else
            return "";
    }
    #endregion

    #region METHOD TO GET THE TABLE DETAILS

    // json method to get tables details for booking status
    private string JsonTableDetail(string requestedMethod)
    {
        string strJson = "";
        string tableno = "";
        try
        {

            System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<tabledetail> listTable = new List<tabledetail>();
            NameValueCollection nam = new NameValueCollection();
            nam.Add("_mode", "tabledetails");
            nam.Add("_id", "0");
            DataTable dtTable = new DataTable();
            dtTable = objCommon.getDataTable(nam, "spPOS_General");

            foreach (DataRow row in dtTable.Rows)
            {
                string status = "";
                if (Convert.ToInt16(row["Booked"]) == 0)
                    status = "Free";
                else
                {
                    status = GetTableStatus(row["TableNumber"].ToString());
                }
                tableno = row["TableNumber"].ToString();
                strJson += "{\"TableNumber\":\"" + row["TableNumber"].ToString() + "\",\"TableStatus\":\"" + status + "\"}" + ",";
            }

            strJson = "{\"tabledetails\":[" + strJson.Remove(strJson.Length - 1, 1) + "]}";


        }
        catch (Exception ex)
        {
            strJson = ex.Message.ToString() + ex.StackTrace.ToString() + tableno;
        }
        return strJson;
    }
    #endregion

    #region METHOD TO RETURN ORDER ITEM TOTAL TAX
    // method to return total tax on the amount total of orders
    private double GetTotalTax(double dblAmount)
    {
        try
        {
            NameValueCollection nam = new NameValueCollection();
            nam.Add("_mode", "taxrate");
            nam.Add("_id", "0");
            DataSet ds = new DataSet();
            ds = objCommon.getData(nam, "spPOS_General");

            double dblTotalTax = 0;

            if (ds.Tables[0].Rows.Count > 0)
                dblTotalTax = Convert.ToDouble(dblAmount * Convert.ToDouble(ds.Tables[0].Rows[0][0]) / 100);

            return dblTotalTax;

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    #endregion

    #region METHOD TO RETURN  TABLE STATUS
    // method to return table status basis on table number
    private string GetTableStatus(string tableNumber)
    {
        try
        {
            NameValueCollection nam = new NameValueCollection();
            nam.Add("_mode", "tablestatus");
            nam.Add("_id", tableNumber);
            DataSet ds = new DataSet();
            ds = objCommon.getData(nam, "spPOS_General");
            if (ds.Tables[0].Rows.Count > 0)
                return Convert.ToString(ds.Tables[0].Rows[0][0]);
            else
                return "Free";

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    #endregion

    #region METHOD SEND THANKS MAIL FOR PLACE ORDER
    // method to send thanks mail for place order
    private string JsonSendThanksMail(string strPreparationTime, string orderNumber)
    {

        string strDate = "";
        string strTime = "";
        string oldstr = "";

        string jsonResult = "";

        try
        {

            if (!string.IsNullOrEmpty(strPreparationTime))
            {
                strPreparationTime = strPreparationTime.Replace('_', ' ');


                if (CheckOrderType(orderNumber) == "Pick Up")
                {

                    string[] arrSendTime = strPreparationTime.Split(' ');
                    string date = arrSendTime[0].ToString();
                    string[] arrDate = date.Split('-');
                    date = Convert.ToString(arrDate[1] + "-" + arrDate[0] + "-" + arrDate[2]);
                    string time = arrSendTime[1].ToString();



                    //********* save order number and time ot xml file **************
                    string xmlPath = Server.MapPath("../XML/OrderDetail.xml");
                    objXml.Save(xmlPath, orderNumber, date + " " + time);
                    //**************************************************************  
                }


                // update waiting time
                NameValueCollection namUpd = new NameValueCollection();
                namUpd.Add("_ordernumber", orderNumber);
                namUpd.Add("_time", strPreparationTime);
                objCommon.commonMethod(namUpd, "spPOS_UpdateWaitingTime");
            }

            # region Mail Coding
            /*
            //get details for order
            NameValueCollection nam = new NameValueCollection();
            DataSet ds = new DataSet();
            nam.Add("_mode", "pickitemdetails");
            nam.Add("_id", orderNumber);
            ds = objCommon.getData(nam, "spPOS_General");

            ////save the membere information after getting detail from order table
            //SaveMemberInforamtion(Convert.ToString(ds.Tables[1].Rows[0]["Name"]), Convert.ToString(ds.Tables[1].Rows[0]["PhoneNumber"]), Convert.ToString(ds.Tables[1].Rows[0]["Email"]));



            //html message to send mail


            // get item details on current order number
            string strItemDetails = "";
            int srno = 1;
            double subtotal = 0;
            for (int count = 0; count < ds.Tables[2].Rows.Count; count++)
            {
                strItemDetails += @"<tr>
                                    <td style='padding:10px; '>" + srno.ToString() + @"</td>
                                      <td style='padding:10px;'>" + Convert.ToString(ds.Tables[2].Rows[count]["ItemName"]) + @"</td>
                                      <td style='padding:10px; text-align:right;'>" + Convert.ToString(ds.Tables[2].Rows[count]["ItemQty"]) + @"</td>
                                      <td style='padding:10px; text-align:right;'>" + Convert.ToString(ds.Tables[2].Rows[count]["Price"]) + @"</td>
                                      <td style='padding:10px; text-align:right;'>" + Convert.ToString(ds.Tables[2].Rows[count]["Total"]) + @"</td>
                                      </tr>";
                subtotal += Convert.ToDouble(ds.Tables[2].Rows[count]["Total"]);
                srno += 1;
            }

            string strMessage1 = @"<table width='650' border='0' align='center' style='font-family:Arial, Helvetica, sans-serif; font-size:13px; border:1px solid #CCCCCC;'>
  <tr>
    <td style='padding:5px; font-size:14px;'><strong>Hi " + Convert.ToString(ds.Tables[1].Rows[0]["Name"]) + @"</strong></td>
  </tr>
  <tr>
    <td style='padding:5px; border-bottom:1px solid #CCCCCC; width:620px; margin:auto;'>Thanks for placing order " + Convert.ToString(ds.Tables[0].Rows[0]["CompanyName"]) + @". This email contains important information regarding
Your recent purchase-plese save it for reference;</td>
  </tr>
  <tr>
    <td style='padding:47px 0px 0px 10px;'><strong>" + Convert.ToString(ds.Tables[0].Rows[0]["CompanyName"]) + @" Restaurant Address</strong> " + Convert.ToString(ds.Tables[0].Rows[0]["CompanyName"]) + @"</td>
  </tr>
  <tr>
    <td style='padding:40px 0px 0px 10px;'><strong>Guest Name :</strong>" + Convert.ToString(ds.Tables[1].Rows[0]["Name"]) + @"</td>
  </tr>
  <tr>
    <td  style='padding:10px;'><strong>Guest Mobile:</strong> " + Convert.ToString(ds.Tables[1].Rows[0]["PhoneNumber"]) + @"</td>
  </tr>
  <tr>
    <td><table width='650' border='0' align='center' style=' font-family:Arial, Helvetica, sans-serif; font-size:12px; border:1px solid #d0d0d0;'>
  <tr>
    <td width='172' style='padding:10px;'><strong>Order Number: </strong>" + orderNumber + @"</td>
    <td width='286' style='padding:10px;'><strong>Time of Order:</strong> " + Convert.ToString(ds.Tables[1].Rows[0]["OrderDate"]) + @"</td>
    <td colspan='3' style='padding:10px;'><strong>Final Total :</strong> " + Convert.ToString(subtotal + GetTotalTax(subtotal)) + @"</td>
    </tr>
  <tr>
  <td style='padding:10px;'><strong>Sr. No.</strong></td>
  <td style='padding:10px; '><strong>Item name</strong></td>
  <td width='43' style='padding:10px; '><strong>Qty</strong></td>
  <td width='79' style='padding:10px;'><strong>Each Price</strong></td>
  <td width='46' style='padding:10px;'><strong>Total</strong></td>
  </tr>";

            string strMessage2 = @"</table></td>
  </tr>
  <tr>
    <td style='padding:20px 0px 29px 10px; border-bottom:1px solid #CCCCCC;'>
    <table width='650' border='0' style=' font-family:Arial, Helvetica, sans-serif;'>
  <tr>
    <td width='116' style='padding:10px;'><strong>Subtotal:</strong></td>
    <td width='524' style='font-family:Arial, Helvetica, sans-serif; font-size:12px;'>$" + subtotal.ToString() + @"</td>
  </tr>
  <tr>
    <td style='padding:10px;'><strong>Discount:</strong></td>
    <td style='font-family:Arial, Helvetica, sans-serif; font-size:12px;'>$0</td>
  </tr>
  <tr>
    <td style='padding:10px;'><strong>Tax:</strong></td>
    <td style='font-family:Arial, Helvetica, sans-serif; font-size:12px;'>$" + GetTotalTax(subtotal).ToString() + @"</td>
  </tr>  
</table>  
    
    
    </td>
  </tr>
  <tr>
    <td style='padding:48px 0px 0px 10px;'><strong>Net Price:</strong> $" + Convert.ToString(subtotal) + @" Tax <strong>(S.T+VAT)</strong>: $" + GetTotalTax(subtotal).ToString() + @"</td>
  </tr>
  <tr>
    <td style='padding:20px 0px 38px 10px; border-bottom:1px solid #CCCCCC;'><strong>Pick up Time: " + Convert.ToString(ds.Tables[1].Rows[0]["DeliveryTime"]) + @"</strong></td>
  </tr>
  <tr>
    <td style='padding:10px; font-size:16px;'><strong>Congratulations, You are now special member of our restaurent.</strong></td>
  </tr>
  <tr>
    <td style='padding:110px 0px 10px 10px; font-size:11px; color:#666666'>&We have tracked your phone number and cotact details and you are requests to turn up and recieve your order within the stipulated time period facing any legal proccess against yourself.</td>
  </tr>
  
</table>";

            //string strFinalMessage = strMessage1 + strItemDetails + strMessage2;

           // bool result = objMail.SendThanksMail(ds.Tables[1].Rows[0]["Email"].ToString(), "Thanks for your order!", strFinalMessage);

           // if (result)
            //    jsonResult = "{\"Status\":\"Success\"}";
            //else
             //   jsonResult = "{\"Error\":\"Sorry, please try again\"}";
            */

            #endregion

            jsonResult = "{\"Status\":\"Success\"}";

        }
        catch (Exception ex)
        {
            jsonResult = ex.Message.ToString() + ex.StackTrace.ToString();
        }

        return jsonResult;

    }


    #endregion

    #region METHOD TO SAVE MEMBER INFORMATION

    private bool SaveMemberInforamtion(string strName, string strContactNo, string strEmail)
    {
        bool result;
        try
        {
            NameValueCollection nam = new NameValueCollection();
            nam.Add("_id", "0");
            nam.Add("_membername", strName);
            nam.Add("_contactno", strContactNo);
            nam.Add("_gender", "Male/Female");
            nam.Add("_dateofbirth", "");
            nam.Add("_martialstatus", "");
            nam.Add("_marriageanniversary", "");
            nam.Add("_spoucename", "");
            nam.Add("_discount", "0");
            nam.Add("_address", "");
            nam.Add("_city", "");
            nam.Add("_state", "");
            nam.Add("_country", "");
            nam.Add("_emailaddress", strEmail);
            string memberId = objCommon.commonWithOutPut(nam, "sp_customersetup", "_outmemberid");
            result = true;
        }
        catch (Exception ex)
        {
            result = false;
        }
        return result;
    }

    #endregion

    #region METHOD GET ITEMS FOR ITEMS STATUS ON TABLE BASIS

    // function to get item status on table basis
    private string JsonItemStatus(string requestMethod, string tableNumber)
    {
        string strJsonFinal = "";
        if (!string.IsNullOrEmpty(requestMethod))
        {
            NameValueCollection namOrder = new NameValueCollection();
            DataTable dtOrder = new DataTable();
            namOrder.Add("_mode", "getfinalorders");
            namOrder.Add("_id", tableNumber);
            dtOrder = objCommon.getDataTable(namOrder, "spPOS_General");

            if (dtOrder.Rows.Count > 0)
            {
                foreach (DataRow orderRow in dtOrder.Rows)
                {

                    List<ItemsStatus> list = new List<ItemsStatus>();
                    DataTable dt = new DataTable();
                    NameValueCollection nam = new NameValueCollection();
                    nam.Add("_mode", "itemstatus");
                    nam.Add("_id", orderRow["OrderNumber"].ToString());
                    dt = objCommon.getDataTable(nam, "spPOS_General");
                    list = (from DataRow row in dt.Rows
                            select new ItemsStatus
                            {
                                ItemId = row["Id"].ToString(),
                                ItemName = row["ItemName"].ToString(),
                                ItemStatus = row["ItemStatus"].ToString(),

                            }).ToList();

                    System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                    string strJsonRe = oSerializer.Serialize(list);

                    strJsonRe = strJsonRe.Remove(0, 1);
                    strJsonFinal += "[[" + strJsonRe.Remove(strJsonRe.Length - 1, 1) + "]," + "{\"OrderNumber\":\"" + orderRow["OrderNumber"].ToString() + "\"}]" + ",";
                }

                strJsonFinal = "{\"" + requestMethod + "\":[" + strJsonFinal.Remove(strJsonFinal.Length - 1, 1) + "]}";
            }
            else
                strJsonFinal = "{\"Error\":\"No record on this table number!\"}";

            return strJsonFinal;
        }
        else
            return "";
    }

    #endregion

    #region METHOD GET ITEMS FOR ITEMS STATUS ON ORDERNUMBER BASIS

    // function to get item status on ordernumber basis
    private string JsonItemStatusOrderNumber(string requestMethod, string orderNumber)
    {
        string strJsonFinal = "";
        if (!string.IsNullOrEmpty(requestMethod))
        {
            List<ItemsStatus> list = new List<ItemsStatus>();
            DataTable dt = new DataTable();
            NameValueCollection nam = new NameValueCollection();
            nam.Add("_mode", "itemstatus");
            nam.Add("_id", orderNumber);
            dt = objCommon.getDataTable(nam, "spPOS_General");
            list = (from DataRow row in dt.Rows
                    select new ItemsStatus
                    {
                        ItemId = row["Id"].ToString(),
                        ItemName = row["ItemName"].ToString(),
                        ItemStatus = row["ItemStatus"].ToString(),

                    }).ToList();

            System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string strJsonRe = oSerializer.Serialize(list);
            strJsonRe = strJsonRe.Remove(0, 1);
            strJsonFinal = "{\"" + requestMethod + "\":[[[" + strJsonRe.Remove(strJsonRe.Length - 1, 1) + "]," + "{\"OrderNumber\":\"" + orderNumber + "\"}]]}";

            return strJsonFinal;
        }
        else
            return "";
    }

    #endregion

    #region METHOD TO CHANGE ITEMS STATUS

    // function to change status on ordernumber basis
    private string JsonItemStatusChange(string orderNumber, string itemStatus)
    {
        string jsonResult = "";
        try
        {
            itemStatus = itemStatus.Remove(0, 1);
            itemStatus = itemStatus.Remove(itemStatus.Length - 1, 1);
            itemStatus = itemStatus.Remove(itemStatus.Length - 1, 1);

            string[] arrItem = itemStatus.Split(';');

            foreach (string item in arrItem)
            {

                string[] arrStatus = item.Split('=');

                int status = 0;

                if (Convert.ToString(arrStatus[1]) == "Y")
                    status = 1;

                NameValueCollection nam = new NameValueCollection();
                nam.Add("_itemid", arrStatus[0].ToString());
                nam.Add("_ordernumber", orderNumber);
                nam.Add("_status", status.ToString());
                objCommon.commonMethod(nam, "spPOS_ChangeItemStatus");
            }

            NameValueCollection namO = new NameValueCollection();
            namO.Add("_itemid", "0");
            namO.Add("_ordernumber", orderNumber);
            namO.Add("_status", "");
            objCommon.commonMethod(namO, "spPOS_ChangeItemStatus");

            jsonResult = "{\"Status\":\"Success\"}";
        }
        catch (Exception ex)
        {
            jsonResult = "{\"Error\":\"Sorry, please try again\"}";
        }
        return jsonResult;
    }

    #endregion

    #region METHOD TO ADD UPDATE FEEDBACK

    // function to get item status on ordernumber basis
    private string JsonFeedback(string requestMethod, string id, string employeeName, string customerName, string contactNumber, string customerEmail, string tableNumber, string rating, string hearAboutus, string comment, string finalRating)
    {
        string strJsonFinal = "";

        try
        {
            if (!string.IsNullOrEmpty(requestMethod))
            {

                NameValueCollection nam = new NameValueCollection();
                DataSet ds = new DataSet();

                if (string.IsNullOrEmpty(tableNumber))
                    tableNumber = "0";

                if (string.IsNullOrEmpty(id))
                    id = "0";

                if (!string.IsNullOrEmpty(rating))
                {
                    rating = rating.Remove(0, 1);
                    rating = rating.Remove(rating.Length - 1, 1);
                    rating = rating.Remove(rating.Length - 1, 1);
                }

                if (!string.IsNullOrEmpty(hearAboutus))
                {
                    hearAboutus = hearAboutus.Remove(0, 1);
                    hearAboutus = hearAboutus.Remove(hearAboutus.Length - 1, 1);
                    hearAboutus = hearAboutus.Remove(hearAboutus.Length - 1, 1);
                }

                if (string.IsNullOrEmpty(rating))
                    rating = "";

                if (string.IsNullOrEmpty(hearAboutus))
                    hearAboutus = "";

                if (string.IsNullOrEmpty(finalRating))
                    finalRating = "0";

                if (string.IsNullOrEmpty(comment))
                    comment = "";

                if (string.IsNullOrEmpty(employeeName))
                    employeeName = "";

                if (string.IsNullOrEmpty(customerEmail))
                    customerEmail = "";

                if (string.IsNullOrEmpty(customerName))
                    customerName = "";

                if (string.IsNullOrEmpty(contactNumber))
                    contactNumber = "";

                nam.Add("_id", id);
                nam.Add("_employeename", employeeName);
                nam.Add("_customername", customerName);
                nam.Add("_contactNo", contactNumber);
                nam.Add("_customeremail", customerEmail);
                nam.Add("_tablenumber", tableNumber);
                nam.Add("_rating", rating);
                nam.Add("_hearaboutus", hearAboutus);
                nam.Add("_comment", comment);
                nam.Add("_finalrating", finalRating);
                ds = objCommon.getData(nam, "spPOS_AddUpdateFeedbackListing");

                if (requestMethod == "getfeedback")
                {
                    strJsonFinal = "{\"feedbackfields\":[{\"rating\":[" + ds.Tables[0].Rows[0]["Rating"].ToString() + "]},{\"hearabout\":[" + ds.Tables[1].Rows[0]["About"].ToString() + "]},{\"Id\":\"" + ds.Tables[2].Rows[0]["Id"] + "\"}]}";
                }
                else if (requestMethod == "setfeedback")
                {
                    strJsonFinal = "{\"Status\":\"Success\"}";
                }
            }
            else
                return "";
        }
        catch (Exception ex)
        {
            strJsonFinal = ex.Message.ToString() + ex.StackTrace.ToString();
        }
        return strJsonFinal;
    }

    #endregion

    #region METHOD TO CHECK SAME SUBCATEGORY

    // function to check same sub-category
    private string CheckSameSubCategory(string id)
    {
        try
        {
            NameValueCollection nam = new NameValueCollection();
            DataTable dt = new DataTable();
            nam.Add("_mode", "checksamesubcat");
            nam.Add("_id", id);
            dt = objCommon.getDataTable(nam, "spPOS_General");
            return Convert.ToString(dt.Rows[0]["IsSameSubCategory"]);
        }
        catch (Exception ex)
        {
            return "0";
        }

    }

    #endregion

    #region METHOD TO CHECK ALL ITEMS STATUS

    // function to check same sub-category
    private string CheckItemStatus(string orderNo)
    {
        try
        {
            NameValueCollection nam = new NameValueCollection();
            DataTable dt = new DataTable();
            nam.Add("_orderno", orderNo);
            dt = objCommon.getDataTable(nam, "spPOS_CheckItemStataus");
            if (Convert.ToInt16(dt.Rows[0]["CountId"]) > 0)
                return "{\"Status\":\"N\"}";
            else
                return "{\"Status\":\"Y\"}";
        }
        catch (Exception ex)
        {
            return "";
        }

    }

    #endregion

    #region METHOD RETURN ITEM STATUS

    // function to get item status on ordernumber basis
    private string ItemStatusOrderNumber(string orderNumber)
    {
        try
        {

            string result = "";

            DataTable dt = new DataTable();
            NameValueCollection nam = new NameValueCollection();
            nam.Add("_mode", "itemstatus");
            nam.Add("_id", orderNumber);
            dt = objCommon.getDataTable(nam, "spPOS_General");

            foreach (DataRow row in dt.Rows)
            {
                result += "\"" + row["ItemName"].ToString() + "\":\"" + row["ItemStatus"].ToString() + "\"" + ",";
            }

            result = "{\"Status\":{" + result.Remove(result.Length - 1, 1) + "}}";
            return result;
        }
        catch (Exception ex)
        {
            return "";
        }

    }

    #endregion

    #region METHOD ADD ORDER ITEMS FOR SEND TO KITCHEN EDIT

    // function to save order items for edit
    private string JsonAddItemsKitchenEdit(string orderNo, string itemDetails, string itemRemarks, string itemToppings, string lastItemName, string waitingTime, string delelteditems)
    {
        string jsonResult = "";

        try
        {
            //int i = UpdateWaitingTime(orderNo, waitingTime);

            //if (i == 0)
            //{
            //REMOVE THE DUPLICATE ORDER ITEMS
            ResetOrderItems(orderNo);


            // save items details (orderno, item, qty, remarks)
            itemDetails = itemDetails.Remove(0, 1);
            itemDetails = itemDetails.Remove(itemDetails.Length - 2, 2);
            string[] arrItemDet = itemDetails.Split(';');

            string[] arrItemRemarks = null;
            if (!string.IsNullOrEmpty(itemRemarks))
            {
                itemRemarks = itemRemarks.Remove(0, 1);
                itemRemarks = itemRemarks.Remove(itemRemarks.Length - 2, 2);
                arrItemRemarks = itemRemarks.Split(';');
            }


            foreach (string itemDet in arrItemDet)
            {
                string strName = "", strQty = "", strRemakrs = "";
                string[] arrItem = itemDet.Split('=');
                strName = arrItem[0].ToString();
                strQty = arrItem[1].ToString();

                if (!string.IsNullOrEmpty(itemRemarks))
                {
                    foreach (string itemRem in arrItemRemarks)
                    {
                        string[] arrItemRem = itemRem.Split('=');
                        if (arrItemRem[0].ToString() == arrItem[0].ToString())
                        {
                            strRemakrs = arrItemRem[1].ToString();
                            break;
                        }
                    }
                }

                // SAVE ORDER ITEMS INTO DATA BASE
                NameValueCollection nam = new NameValueCollection();
                nam.Add("_orderid", orderNo);
                nam.Add("_itemname", strName);
                nam.Add("_itemqty", strQty);
                nam.Add("_toppings", "");
                nam.Add("_remarks", strRemakrs);
                nam.Add("_lastitem", lastItemName);
                string result = objCommon.commonMethod(nam, "sp_orderitem");
            }

            if (!string.IsNullOrEmpty(itemToppings))
            {
                // update order items for toppinss
                itemToppings = itemToppings.Remove(0, 1);
                itemToppings = itemToppings.Remove(itemToppings.Length - 4, 4);

                string[] arrToppings = itemToppings.Split(')');

                foreach (string topping in arrToppings)
                {
                    string topping1 = "";

                    // remove ; sign from starting position
                    if (topping.Substring(0, 1) == ";")
                        topping1 = topping.Remove(0, 1);
                    else
                        topping1 = topping;

                    // remove ; from the last position
                    if (topping1.Substring(topping1.Length - 1, 1) == ";")
                        topping1 = topping1.Remove(topping1.Length - 1, 1);


                    if (topping1.Substring(topping1.Length - 1, 1) != ")")
                        topping1 = topping1 + ")";


                    string itemName = topping1.Substring(0, topping1.IndexOf('='));
                    string topping2 = topping1.Remove(0, topping1.IndexOf('=') + 2);
                    topping2 = topping2.Remove(topping2.Length - 1, 1);

                    string[] arrTopping2 = topping2.Split(';');

                    string itemTopping = "";
                    foreach (string topping3 in arrTopping2)
                    {
                        itemTopping += topping3.Remove(0, topping3.IndexOf('=') + 1) + ";";
                    }

                    itemTopping = itemTopping.Remove(itemTopping.Length - 1, 1);

                    // UPDATE ORDER ITEMS INTO DATA BASE FOR TOPPINGS  
                    NameValueCollection nam = new NameValueCollection();
                    nam.Add("_orderid", orderNo);
                    nam.Add("_itemname", itemName);
                    nam.Add("_itemqty", "0");
                    nam.Add("_toppings", itemTopping);
                    nam.Add("_remarks", "");
                    nam.Add("_lastitem", "");
                    string result = objCommon.commonMethod(nam, "sp_orderitem");
                }
            }

            UpdateWaitingTime(orderNo, waitingTime);

            //save deleted items
            string[] arrDeletedItems = null;
            if (!string.IsNullOrEmpty(delelteditems))
            {
                delelteditems = delelteditems.Remove(0, 1);
                delelteditems = itemRemarks.Remove(itemRemarks.Length - 2, 2);
                arrDeletedItems = delelteditems.Split(';');

                foreach (string delItems in arrDeletedItems)
                {
                    string[] arrDelItem = delItems.Split('=');
                    SaveDeletedItems(arrDelItem[0].ToString(), orderNo, arrDelItem[1].ToString());
                }
            }



            jsonResult = "{\"Status\":\"Success\"}";
        }
        catch (Exception ex)
        {
            jsonResult = "{\"Error\":\"Item not saved, please try again\"}";
        }
        return jsonResult;

    }

    #endregion

    #region METHOD TO SAVE DELETED ITEMS

    // function to save deleted items
    private void SaveDeletedItems(string strItemName, string strOrderNo, string strReason)
    {
        try
        {
            NameValueCollection nam = new NameValueCollection();
            nam.Add("_orderno", strItemName);
            nam.Add("_itemname", strOrderNo);
            nam.Add("_reason", strReason);
            objCommon.commonMethod(nam, "spPOS_AddDeltedItems");
        }
        catch (Exception ex)
        {

        }

    }

    #endregion

    #region METHOD TO CONVERT PICK UP ORDER INTO DINE IN

    // function to convert pick up order into dine in
    private string jsonPickupToDineIn(string orderNumber, string tableNumber)
    {
        try
        {
            string strJsonRe = "";
            NameValueCollection nam = new NameValueCollection();
            nam.Add("_tablenumber", tableNumber);
            nam.Add("_ordernumber", orderNumber);
            string ordernumber = Convert.ToString(objCommon.commonWithOutPut(nam, "spPOS_PickupToDinIn", "_outordernumber"));
            if (Convert.ToInt16(ordernumber) > 0)
                strJsonRe = "{\"Status\":\"Success\"}";
            return strJsonRe;
        }
        catch (Exception ex)
        {
            return "{\"Error\":\"" + ex.Message.ToString() + "\"}";
        }

    }

    #endregion

    #region GET ORDER STATUS ON TABLE
    // function to get order status on particular table number
    protected string JsonOrderStatusByTable(string tableNumber)
    {
        string jsonResult = "";
        try
        {
            NameValueCollection nam = new NameValueCollection();
            nam.Add("_mode", "tableorderstatus");
            nam.Add("_id", tableNumber);
            DataTable dt = new DataTable();
            dt = objCommon.getDataTable(nam, "spPOS_General");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string strStatus = "SEND";
                    switch (row["WaitingTime"].ToString())
                    {
                        case "-1":
                            strStatus = "SAVE";
                            break;
                        case "-2":
                            strStatus = "FINAL";
                            break;
                    }

                    jsonResult += "{\"OrderNo\":\"" + row["OrderNumber"].ToString() + "\",\"Status\":\"" + strStatus + "\"}" + ",";

                }


                jsonResult = jsonResult.Remove(jsonResult.Length - 1, 1);
                jsonResult = "{\"orderstatusontable\":[" + jsonResult + "]}";
            }
            else
                jsonResult = "{\"Error\":\"No record on this table number!\"}";
        }
        catch (Exception ex)
        {
            jsonResult = "{\"Error\":\"Some error please try again!\"}";
        }
        return jsonResult;
    }

    #endregion

    # region METHOD TO GET CARD SURCHARGE

    // function to get card surcharge
    private string CardSurcharge()
    {
        string result = "";
        DataTable dt = new DataTable();
        NameValueCollection nam = new NameValueCollection();
        nam.Add("_mode", "surcharge");
        nam.Add("_id", "0");
        dt = objCommon.getDataTable(nam, "spPOS_General");
        foreach (DataRow row in dt.Rows)
        {
            result += "\"" + row["Name"].ToString() + "\":\"" + row["Surcharge"].ToString() + "\"" + ",";
        }
        return result.Remove(result.Length - 1, 1);
    }


    #endregion

    #region METHOD TO SEND THANKS MAIL FOR REGISTRATION

    // function to send thanks mail for registeration
    private string SendThanksMailRegister(string strName, string strEmail, string strPhone, string strMsg)
    {
        try
        {
            strMsg = strMsg.Replace('-', ' ');
            strName = strName.Replace('-', ' ');

            NameValueCollection nam = new NameValueCollection();
            nam.Add("_username", strName);
            nam.Add("_useremail", strEmail);
            nam.Add("_userphone", strPhone);
            objCommon.commonMethod(nam, "spPOS_AddRegisterUsers");
            objMail.SendThanksMail(strEmail, "Thanks for use POS", "Dear user pleas contact if need any assitance");
            return "{\"Status\":\"Success\"}";
        }
        catch (Exception ex)
        {
            return "{\"Error\":\"" + ex.Message.ToString() + "\"}";
        }

    }

    #endregion

    #region METHOD MAKE FINALIZE ORDER WITH PAYMENT BY TABLE NUMBER

    // function to set order status finalize by table number with payment and save payment details
    private string OrderFinalizeWithPayment(string tableNumber, string loginid)
    {
        string jsonResult = "";

        try
        {
            // change the status of order from processing to finalize and get customer details
            NameValueCollection nam = new NameValueCollection();
            DataSet ds = new DataSet();
            nam.Add("_mode", "orderfinalizewithpayment");
            nam.Add("_id", tableNumber);
            ds = objCommon.getData(nam, "spPOS_General");

            // save the cusotmer details in member table
            if (Convert.ToString(ds.Tables[0].Rows[0]["OrderType"]) == "Dine In" && !string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["Name"])) && !string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["Email"])))
            {
                //SaveMemberInforamtion(Convert.ToString(ds.Tables[0].Rows[0]["Name"]), Convert.ToString(ds.Tables[0].Rows[0]["PhoneNumber"]), Convert.ToString(ds.Tables[0].Rows[0]["Email"]));
            }


            jsonResult = "{\"Status\":\"Success\"}";
        }
        catch (Exception ex)
        {
            jsonResult = "{\"Error\":\"Please try again\"}";
        }

        return jsonResult;
    }

    #endregion

    #region METHOD MAKE FINALIZE ORDER WITH PAYMENT BY PHONE NUMBER

    // function to set order status finalize by phone number with payment and save payment details
    private string OrderFinalizeWithPaymentByPhone(string phoneNumber, string loginid)
    {
        string jsonResult = "";

        try
        {
            // change the status of order from processing to finalize and get customer details
            NameValueCollection nam = new NameValueCollection();
            DataSet ds = new DataSet();
            nam.Add("mode", "setfinilized");
            nam.Add("phone", phoneNumber);
            ds = objCommon.getData(nam, "spPOS_GeneralByPhone");
            jsonResult = "{\"Status\":\"Success\"}";
        }
        catch (Exception ex)
        {
            jsonResult = "{\"Error\":\"Please try again\"}";
        }

        return jsonResult;
    }

    #endregion

    #region GET ORDER ITEM DETAILS BY PHONE NUMBER FOR FINALIZE
    // method to get finalize details by phone number
    protected string JsonGetOrderItemsByPhoneNumberForFinalize(string PhoneNumber)
    {
        string jsonFinalResult = "";

        try
        {

            NameValueCollection namOrder = new NameValueCollection();
            DataTable dtOrder = new DataTable();
            namOrder.Add("mode", "getfinilized");
            namOrder.Add("phone", PhoneNumber);
            dtOrder = objCommon.getDataTable(namOrder, "spPOS_GeneralByPhone");

            foreach (DataRow rowOrder in dtOrder.Rows)
            {
                string orderNumber = rowOrder["OrderNumber"].ToString();
                string jsonResult = "";

                NameValueCollection namVal = new NameValueCollection();
                DataTable dt = new DataTable();
                namVal.Add("_mode", "orderitems");
                namVal.Add("_id", orderNumber);
                namVal.Add("_table", "");
                dt = objCommon.getDataTable(namVal, "sp_getrecord");

                if (dt.Rows.Count > 0)
                {
                    string itemDetails = "", itemRemarks = "", itemToppings = "", itemToppingsFinal = "", itemPrice = "";

                    foreach (DataRow row in dt.Rows)
                    {

                        itemDetails += "\"" + row["ItemName"] + "\":\"" + row["ItemQty"].ToString() + "\",";

                        if (!string.IsNullOrEmpty(row["Remarks"].ToString()))
                            itemRemarks += "\"" + row["ItemName"] + "\":\"" + row["Remarks"].ToString() + "\",";

                        string[] arrToppings = null;
                        if (!string.IsNullOrEmpty(row["Toppings"].ToString()))
                        {
                            arrToppings = row["Toppings"].ToString().Split(';');
                            int count = 1;
                            foreach (string toppings in arrToppings)
                            {
                                string[] arrTop = toppings.Split(',');
                                string topp = "";
                                foreach (string t in arrTop)
                                {
                                    topp += "{\"" + t + "\":\"" + GetToppingPrice(t) + "\"},";
                                }

                                itemToppings += "\"" + row["ItemName"].ToString() + count.ToString() + "\":[" + topp.Remove(topp.Length - 1, 1) + "],";
                                count += 1;
                                topp = "";
                            }

                            itemToppingsFinal += "\"" + row["ItemName"] + "\":{" + itemToppings.Remove(itemToppings.Length - 1, 1) + "},";
                            itemToppings = "";
                        }
                    }

                    if (!string.IsNullOrEmpty(itemDetails))
                    {
                        itemDetails = itemDetails.Remove(itemDetails.Length - 1, 1);
                        jsonResult = "[{\"itemdetails\":{" + itemDetails + "}," + GetItemPrice(orderNumber);
                    }

                    if (!string.IsNullOrEmpty(itemRemarks))
                    {
                        itemRemarks = itemRemarks.Remove(itemRemarks.Length - 1, 1);
                        jsonResult = jsonResult + ",\"itemremakrs\":{" + itemRemarks + "}";
                    }

                    if (!string.IsNullOrEmpty(itemToppingsFinal))
                    {
                        itemToppingsFinal = itemToppingsFinal.Remove(itemToppingsFinal.Length - 1, 1);
                        jsonResult = jsonResult + ",\"itemtoppings\":{" + itemToppingsFinal.Remove(itemToppingsFinal.Length - 1, 1) + "}";
                    }

                    if (!string.IsNullOrEmpty(itemToppingsFinal))
                    {
                        if (!string.IsNullOrEmpty(GetLastItemsForFinalize(GetLastItemName(orderNumber))))
                            jsonResult = jsonResult + "}}," + GetLastItemsForFinalize(GetLastItemName(orderNumber)) + ",{\"OrderNumber\":\"" + orderNumber + "\"}" + "]";
                        else
                            jsonResult = jsonResult + "}}]";
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(GetLastItemsForFinalize(GetLastItemName(orderNumber))))
                            jsonResult = jsonResult + "}," + GetLastItemsForFinalize(GetLastItemName(orderNumber)) + ",{\"OrderNumber\":\"" + orderNumber + "\"}" + "]";
                        else
                            jsonResult = jsonResult + "}]";
                    }

                    jsonFinalResult += jsonResult + ",";
                }
                else
                    jsonFinalResult = "{\"Error\":\"Sorry, this order number not in process\"}";
            }

            return "{\"finalize\":[" + jsonFinalResult.Remove(jsonFinalResult.Length - 1, 1) + "]}";

        }
        catch (Exception ex)
        {
            jsonFinalResult = "{\"Error\":\"Invalid order number\"}";
        }

        return jsonFinalResult;
    }

    #endregion

    #region CALL TIMER EVENT FOR AUTOMATIC SEND TO KITCHEN

    public void IntTimer()
    {
        const double interval1miute = 30 * 1000;
        Timer checkfortime = new Timer(interval1miute);
        checkfortime.Elapsed += new ElapsedEventHandler(checkfortime_Elapsed);
        checkfortime.Enabled = true;
    }

    void checkfortime_Elapsed(object sender, ElapsedEventArgs e)
    {

        // throw new NotImplementedException();
        string formatedtime = DateTime.Now.ToString();

        string xmlPath = Server.MapPath("../XML/OrderDetail.xml");
        var orderNumber = objXml.getOrder(formatedtime, xmlPath);

        foreach (string order in orderNumber)
        {
             string db = StringFormat(order);
            RawPrinterHelper.SendStringToPrinter(ConfigurationManager.AppSettings["printer_name"], db);
            objXml.Delete(xmlPath, order);
            ChangeStatusPickup(order);
        }
    }
    #endregion

    #region METHOD TO SEND PRINT COMMAND

    private string StringFormat(string strOrderNumber)
    {
        StringBuilder sb = new StringBuilder();

        DataTable dt = new DataTable();
        NameValueCollection nam = new NameValueCollection();
        nam.Add("_mode", "getitemtopping");
        nam.Add("_name", strOrderNumber);
        dt = objCommon.getDataTable(nam, "spPOS_GetByName");


        TestTableBuilder.TableBuilder tb = new TestTableBuilder.TableBuilder();
        tb.AddRow("Order No.", "" + strOrderNumber, "Table No. ", "0");
        tb.AddRow("S.No", "Item Name", "Qty", "Remarks");

        int count = 1;
        foreach (DataRow row in dt.Rows)
        {
            // add item 
            tb.AddRow(count.ToString(), row["ItemName"], row["ItemQty"], row["Remarks"]);

            string toppings = row["Toppings"].ToString();

            if (!string.IsNullOrEmpty(toppings))
            {
                // add toppings
                string[] arrToppings = toppings.Split(';');
                foreach (string t in arrToppings)
                {
                    tb.AddRow("", ">> " + t, "1", "");
                }
            }
            count++;
        }





        foreach (TestTableBuilder.ITextRow tr in tb)
        {
            tr.Output(sb);
        }
        return sb.ToString();
    }


    #endregion

    #region METHOD SET ORDER STATUS DONE AFTER AUTOMATICALLY SEND TO KITCHEN

    // function to set order status done after automaticlly  send to kitchen
    public string ChangeStatusPickup(string ordernumber)
    {
        string jsonResult = "";

        try
        {
            NameValueCollection nam = new NameValueCollection();
            nam.Add("_mode", "sendtokitchenpickup");
            nam.Add("_id", ordernumber);
            objCommon.commonMethod(nam, "spPOS_General");
            jsonResult = "{\"Status\":\"Success\"}";
        }
        catch (Exception ex)
        {
            jsonResult = "{\"Error\":\"Please try again\"}";
        }

        return jsonResult;
    }

    #endregion

    #region METHOD GET ORDER STATUS BY ORDER NUMBER

    // function to get order status by order number
    public string GetOrderStatus(string ordernumber)
    {
        try
        {
            NameValueCollection nam = new NameValueCollection();
            nam.Add("_mode", "orderstatus");
            nam.Add("_id", ordernumber);
            DataTable dt = new DataTable();
            dt = objCommon.getDataTable(nam, "spPOS_General");
            return Convert.ToString(dt.Rows[0]["status"]);
        }
        catch (Exception ex)
        {
            return "Pending";
        }


    }

    #endregion

    #region METHOD MAKE FINALIZE ORDER BY PHONE NUMBER

    // function to set order status finalize by phone number
    private string OrderFinalizeByPhone(string phoneNumber)
    {
        string jsonResult = "";

        try
        {
            // change the status of order from processing to finalize and get customer details
            NameValueCollection nam = new NameValueCollection();
            DataSet ds = new DataSet();
            nam.Add("mode", "orderfinalizebyphone");
            nam.Add("phone", phoneNumber);
            ds = objCommon.getData(nam, "spPOS_GeneralByPhone");

            /*
            // save the cusotmer details in member table
            if (Convert.ToString(ds.Tables[0].Rows[0]["OrderType"]) == "Dine In" && !string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["Name"])) && !string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["Email"])))
            {
                SaveMemberInforamtion(Convert.ToString(ds.Tables[0].Rows[0]["Name"]), Convert.ToString(ds.Tables[0].Rows[0]["PhoneNumber"]), Convert.ToString(ds.Tables[0].Rows[0]["Email"]));
            } */


            jsonResult = "{\"Status\":\"Success\"}";
        }
        catch (Exception ex)
        {
            jsonResult = "{\"Error\":\"Please try again\"}";
        }

        return jsonResult;
    }

    #endregion

    #region METHOD TO CHCECK ONLY TABLE

    // function to convert pick up order into dine in
    private string jsonCheckTable(string tableNumber)
    {
        try
        {
            string strJsonRe = "";
            NameValueCollection nam = new NameValueCollection();
            nam.Add("_tablenumber", tableNumber);
            string result = Convert.ToString(objCommon.commonWithOutPut(nam, "spPOS_CheckTable", "_outresult"));

            if (Convert.ToInt16(result) > 0)
                strJsonRe = "{\"Status\":\"Success\"}";
            else if (Convert.ToInt16(result) < 0)
                strJsonRe = "{\"Error\":\"This table number does not exist!\"}";
            else
                strJsonRe = "{\"Error\":\"This table number is already book\"}";
            return strJsonRe;
        }
        catch (Exception ex)
        {
            return "{\"Error\":\"" + ex.Message.ToString() + "\"}";
        }

    }

    #endregion

    #region METHOD TO CHCECK ORDER TYPE

    // function to check order type by order number
    private string CheckOrderType(string orderNumber)
    {
        try
        {
            NameValueCollection nam = new NameValueCollection();
            nam.Add("_mode", "checkordertype");
            nam.Add("_itemname", orderNumber);
            DataTable dt = new DataTable();
            dt = objCommon.getDataTable(nam, "spPOS_GetByItemName");
            if (dt.Rows.Count > 0)
                return dt.Rows[0][0].ToString();
            else
                return "";
        }
        catch (Exception ex)
        {
            return "{\"Error\":\"" + ex.Message.ToString() + "\"}";
        }

    }

    #endregion


}
