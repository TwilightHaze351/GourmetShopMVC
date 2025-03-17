using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GourmetShopWebApp.Data;
using GourmetShopWebApp.ViewModels;

namespace GourmetShop.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reports/SalesReport
        public IActionResult SalesReport()
        {
            return View();
        }

        // POST: Reports/SalesReport
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SalesReport(DateTime startDate, DateTime endDate)
        {
            // Query OrderItems based on the Order's date (adjust this query if you have an Order table)
            var reportData = await _context.OrderItems
                .Where(oi => oi.Order.OrderDate >= startDate && oi.Order.OrderDate <= endDate)
                .GroupBy(oi => new { oi.ProductId, oi.Product.ProductName })
                .Select(g => new
                {
                    ProductId = g.Key.ProductId,
                    ProductName = g.Key.ProductName,
                    TotalQuantity = g.Sum(oi => oi.Quantity),
                    TotalSales = g.Sum(oi => oi.Quantity * oi.UnitPrice)
                })
                .ToListAsync();

            // Calculate the total sales over all products.
            decimal totalSales = reportData.Sum(r => r.TotalSales);

            var viewModel = new SalesReportViewModel
            {
                StartDate = startDate,
                EndDate = endDate,
                TotalSales = totalSales,
                ReportData = reportData
            };

            return View("SalesReportResults", viewModel);
        }
    }
}