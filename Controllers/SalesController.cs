using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobilePhoneStore.Interfaces;

namespace MobilePhoneStore.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISalesServices _services;
        public SalesController(ISalesServices salesServices)
        {
            _services = salesServices;
        }

        [HttpGet]
        [Route("MonthlySalesReport")]
        public async Task<IActionResult> GetMonthlySalesReport(DateTime fromDate, DateTime toDate)
        {
            return await _services.GetMonthlySalesReport(fromDate, toDate, this);
        }

        [HttpGet]
        [Route("BrandWiseMonthlySalesReport")]
        public async Task<IActionResult> GetBrandWiseMonthlySalesReport(DateTime fromDate, DateTime toDate)
        {
            return await _services.GetBrandWiseMonthlySalesReport(fromDate, toDate, this);
        }

        [HttpGet]
        [Route("GetProfitLossReport")]
        public async Task<IActionResult> GetProfitLossReport(DateTime fromDate, DateTime toDate)
        {
            return await _services.GetProfitLossReport(fromDate, toDate, this);
        }

        [HttpGet]
        [Route("GetProfitLossComparison")]
        public async Task<IActionResult> GetProfitLossComparison(DateTime fromDate, DateTime toDate)
        {
            return await _services.GetProfitLossComparison(fromDate, toDate, this);
        }


    }
}
