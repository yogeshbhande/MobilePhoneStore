using Microsoft.AspNetCore.Mvc;

namespace MobilePhoneStore.Interfaces
{
    public interface ISalesServices
    {
        public Task<IActionResult> GetMonthlySalesReport(DateTime fromDate, DateTime toDate, ControllerBase controllerBase);

        public Task<IActionResult> GetBrandWiseMonthlySalesReport(DateTime fromDate, DateTime toDate, ControllerBase controllerBase);

        public Task<IActionResult> GetProfitLossReport(DateTime fromDate, DateTime toDate, ControllerBase controllerBase);

        public Task<IActionResult> GetProfitLossComparison(DateTime fromDate, DateTime toDate, ControllerBase controllerBase);

    }
}
