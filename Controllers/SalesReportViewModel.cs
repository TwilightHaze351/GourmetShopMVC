using System;
using System.Collections.Generic;

namespace GourmetShopWebApp.ViewModels
{
    public class SalesReportViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalSales { get; set; }
        // Using dynamic for simplicity; you could also create a dedicated model for each report item.
        public IEnumerable<dynamic> ReportData { get; set; }
    }
}