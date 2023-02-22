using OMS.DAL.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OMS.Model.ViewModel
{
    public class HomeStatisticsModel
    {
        public HomeModel Instock { get; set; } = new HomeModel();
        public HomeModel MonthSales { get; set; } = new HomeModel();
        public HomeModel YearSales { get; set; } = new HomeModel();
        public HomeModel Disposed { get; set; } = new HomeModel();
        public HomeModel TotalCustomers { get; set; } = new HomeModel();
        public HomeModel Batches { get; set; } = new HomeModel();
        public HomeModel Tenders { get; set; } = new HomeModel();
        public List<SalesTargetAnalyzerModel> salesTargetAnalyzerModel { get; set; }
        public List<SalesByAreaModel> salesByAreaModel { get; set; }
    }

    public class HomeModel
    {
        public string Value1 { get; set; } = " ";
        public string Value2 { get; set; } = " ";
    }
    public class HomeSalesAgentTargetModel
    {
        public string SalesAgentName { get; set; }
        public decimal Amount { get; set; }
        public decimal Target { get; set; }
        public string Percentage { get; set; }
    }
    public class SalesTargetAnalyzerModel
    {
        public string Product { get; set; }
        public decimal TargetQty { get; set; }
        public decimal ActualQty { get; set; }
        public string Percentage { get; set; }
    }
    public class SalesByAreaModel
    {
        public string Area { get; set; }
        public decimal Amount { get; set; }
    }
}