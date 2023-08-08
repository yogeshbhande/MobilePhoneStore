using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobilePhoneStore.Data;
using MobilePhoneStore.Interfaces;
using MobilePhoneStore.Models;
using System;
using System.Linq;

namespace MobilePhoneStore.Services
{
    public class SalesServices : ISalesServices
    {
        private readonly AppDbContext _context;
        public IConfiguration _configuration;
        public SalesServices(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<IActionResult> GetMonthlySalesReport(DateTime fromDate, DateTime toDate, ControllerBase controllerBase)
        {
            try
            {
                var SalesReport = await (from purchase in _context.Purchases
                                         join mobilePhone in _context.MobilePhones
                                         on purchase.MobilePhoneId equals mobilePhone.Id
                                         where purchase.PurchaseDate >= fromDate && purchase.PurchaseDate <= toDate
                                         group new { purchase, mobilePhone } by new { Year = purchase.PurchaseDate.Year, Month = purchase.PurchaseDate.Month } into grouped
                                         select new
                                         {
                                             Year = grouped.Key.Year,
                                             Month = grouped.Key.Month,
                                             TotalSales = grouped.Sum(x => (x.purchase.Quantity * (x.mobilePhone.Price - x.purchase.Discount)))
                                         }).ToListAsync();

                return controllerBase.Ok(SalesReport);
            }
            catch (Exception ex)
            {
                return controllerBase.BadRequest(new { status = "Error", message = ex.Message });
            }
        }

        public async Task<IActionResult> GetBrandWiseMonthlySalesReport(DateTime fromDate, DateTime toDate, ControllerBase controllerBase)
        {
            try
            {
                var BrandWiseSalesReport = await (from p in _context.Purchases
                                                  join m in _context.MobilePhones
                                                  on p.MobilePhoneId equals m.Id
                                                  join brand in _context.Brands on m.BrandId equals brand.Id
                                                  where p.PurchaseDate >= fromDate && p.PurchaseDate <= toDate
                                                  group new { p, m } by new { BrandId = brand.Id, BrandName = brand.BrandName, Year = p.PurchaseDate.Year, Month = p.PurchaseDate.Month } into grouped
                                                  select new
                                                  {
                                                      Brand = grouped.Key.BrandName,
                                                      Year = grouped.Key.Year,
                                                      Month = grouped.Key.Month,
                                                      TotalSales = grouped.Sum(x => (x.p.Quantity * (x.m.Price - x.p.Discount)))
                                                  }).ToListAsync();
                return controllerBase.Ok(BrandWiseSalesReport);
            }
            catch (Exception ex)
            {
                return controllerBase.BadRequest(new { status = "Error", message = ex.Message });
            }

        }

        public async Task<IActionResult> GetProfitLossReport(DateTime fromDate, DateTime toDate, ControllerBase controllerBase)
        {
            try
            {
                var profitLossReport = await (from p in _context.Purchases
                                              join m in _context.MobilePhones
                                              on p.MobilePhoneId equals m.Id
                                              join b in _context.Brands on m.BrandId equals b.Id
                                              where p.PurchaseDate >= fromDate && p.PurchaseDate <= toDate
                                              group new { p, m } by new { PurchaseDate = p.PurchaseDate, MobilePhoneId = m.Id, BrandName = b.BrandName } into grouped
                                              select new
                                              {
                                                  PurchaseDate = grouped.Key.PurchaseDate,
                                                  MobilePhoneId = grouped.Key.MobilePhoneId,
                                                  Brand = grouped.Key.BrandName,
                                                  TotalPrice = grouped.Sum(x => (x.p.Quantity * x.m.Price) - x.p.Discount)
                                              })
                    .ToListAsync();

                return controllerBase.Ok(profitLossReport);
            }
            catch (Exception ex)
            {
                return controllerBase.BadRequest(new { status = "Error", message = ex.Message });
            }
        }

        public async Task<IActionResult> GetProfitLossComparison(DateTime fromDate, DateTime toDate, ControllerBase controllerBase)
        {
            try
            {
                var currentPeriodPurchases = await _context.Purchases
                    .Join(_context.MobilePhones,
                    p => p.MobilePhoneId,
                    m => m.Id,
                    (p, m) => new { p, m })
                    .Join(_context.Brands,
                    x => x.m.BrandId,
                    y => y.Id, (x, b) => new { x, b })
                    .Where(p => p.x.p.PurchaseDate >= fromDate && p.x.p.PurchaseDate <= toDate)
                                                                     .ToListAsync();

                var previousFromDate = fromDate.AddMonths(-1);
                var previousToDate = toDate.AddMonths(-1);

                var previousPeriodPurchases = await _context.Purchases.Join(_context.MobilePhones, p => p.MobilePhoneId, m => m.Id,
                    (p, m) => new { p, m }).Where(p => p.p.PurchaseDate >= previousFromDate && p.p.PurchaseDate <= previousToDate)
                                                                     .ToListAsync();

                var result = currentPeriodPurchases
                    .GroupBy(p => p.b.Id)
                    .Select(group => new
                    {
                        BrandId = group.Key,
                        BrandName = group.FirstOrDefault()?.b?.BrandName,
                        CurrentProfitLoss = group.Sum(p => (p.x.m.Price * p.x.p.Quantity) - p.x.p.Discount),
                        PreviousProfitLoss = previousPeriodPurchases
                            .Where(pp => pp.m.BrandId == group.Key)
                            .Sum(pp => (pp.m.Price * pp.p.Quantity) - pp.p.Discount)
                    })
                    .ToList();
                return controllerBase.Ok(result);
            }catch(Exception ex)
            {
                return controllerBase.BadRequest( new { status = "Error",  message = ex.Message } );
            }
        }
    }
}
