using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for posProperty
/// </summary>
namespace jsonProperty
{
    //category class  to get set category fields for json response
    public class Category
    {
        public string CatId { get; set; }
        public string CatName { get; set; }
    }

    public class SubCategory
    {
        public string SubCatId { get; set; }
        public string SubCatName { get; set; }
        public string SubCatImage { get; set; }

    }

    public class Items
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemPrice { get; set; }
        public string ItemImage { get; set; }
        public string ShortDescription { get; set; }
        public string ShortName { get; set; }
    }

    public class ItemsToppings
    {
        public string ToppingId { get; set; }
        public string ToppingName { get; set; }
        public string ToppingPrice { get; set; }
    }

    public class ItemsCatSubCat
    {
        public string CatId { get; set; }
        public string CatName { get; set; }
        public string SubCatId { get; set; }
        public string SubCatName { get; set; }
    }

    public class ItemsStatus
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemStatus { get; set; }
    }

    public class FeedbackDetails
    {
        public string Name { get; set; }
    }

    public class FeedbackRateUs
    {
        public string Name { get; set; }
    }

    public class FeedbackHearAboutUs
    {
        public string Name { get; set; }
    }

    public class SearchItems
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemPrice { get; set; }
        public string CatetoryId { get; set; }
        public string SubCatetoryId { get; set; }
        public string IsOffer { get; set; }
    }


    public class offers
    {
        public string OfferId { get; set; }
        public string OfferName { get; set; }
        public string OfferPrice { get; set; }
        public string OfferImage { get; set; }
    }

    public class restaurant
    {
        public string Name { get; set; }
    }

    public class viewopenticket
    {
        public string OrderNumber { get; set; }
        public string Type { get; set; }
        public string TableNumber { get; set; }
        public string Date { get; set; }
        public string WaitingTime { get; set; }
        public string Status { get; set; }
    }

    public class viewpickuporders
    {
        public string OrderNumber { get; set; }
        public string Type { get; set; }
        public string TableNumber { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string sendtokitchentime { get; set; }
        public string NoOfGues { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerName { get; set; }
        public string Status { get; set; }
    }

    public class viewallcloseorders
    {
        public string OrderNumber { get; set; }
        public string Type { get; set; }
        public string TableNumber { get; set; }
        public string Date { get; set; }
        public string WaitingTime { get; set; }
        public string NoOfGues { get; set; }
        public string CustomerDetails { get; set; }
        public string Status { get; set; }
    }

    public class orderbytable
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemQty { get; set; }
        public string EachPrice { get; set; }
        public string TotalPrice { get; set; }
        public string ToppingId { get; set; }
        public string ToppingName { get; set; }
        public string ToppingPrice { get; set; }
    }

    public class ordernumber
    {
        public string OrderNumber { get; set; }
    }

    public class settings
    {
        public string Currency { get; set; }
        public string TaxRate { get; set; }
        public string Discount { get; set; }
        public string LoginId { get; set; }
        public string UserType { get; set; }
        public string EmployeeName { get; set; }
    }

    public class restaurantdetails
    {
        public string ResturantName { get; set; }
        public string ResturantPhone { get; set; }
        public string OwnerName { get; set; }
        public string ResturantAddress1 { get; set; }
        public string ResturantAddress2 { get; set; }
        public string ResturantFax { get; set; }
        public string EmailAddress { get; set; }
        public string ResturantLogo { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

    }

    public class ReportTable
    {
        public string SrNo { get; set; }
        public string TableNumber { get; set; }
        public string Occupied { get; set; }
        public string HourlySales { get; set; }

    }

    public class tabledetail
    {
        public string TableNumber { get; set; }
        public string TableStatus { get; set; }
    }

}

