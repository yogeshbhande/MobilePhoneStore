using Microsoft.AspNetCore.Mvc;
using MobilePhoneStore.Models;

namespace MobilePhoneStore.Interfaces
{
    public interface IPurchaseServices
    {
        public Task<IActionResult> GetAllPurchaseDetails(ControllerBase controllerBase);

        public Task<ActionResult> GetPurchaseDetailsById(int id, ControllerBase controllerBase);

        public Task<ActionResult> SaveNewPurchase(Purchase purchase, ControllerBase controllerBase);

        public Task<ActionResult> GetPurchaseDetailsByCustomer(int CustomerId, ControllerBase controllerBase);

        public Task<ActionResult> GetPurchaseDetailsByMobile(int mobilePhoneId, ControllerBase controllerBase);

    }
}
