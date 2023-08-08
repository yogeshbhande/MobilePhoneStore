using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobilePhoneStore.Interfaces;
using MobilePhoneStore.Models;

namespace MobilePhoneStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseServices _services;
        public PurchaseController(IPurchaseServices purchaseServices)
        {
            _services = purchaseServices;
        }

        [HttpGet]
        [Route("GetPurchaseDetails")]
        public async Task<IActionResult> GetAllPurchaseDetails()
        {
            return await _services.GetAllPurchaseDetails(this);
        }

        [HttpGet]
        [Route("GetPurchaseDetailsById")]
        public async Task<IActionResult> GetPurchaseDetailsById(int id)
        {
            return await _services.GetPurchaseDetailsById(id, this);
        }

        [HttpPost]
        [Route("SavePurchase")]
        public async Task<IActionResult> SaveNewPurchase(Purchase purchase)
        {
            return await _services.SaveNewPurchase(purchase, this);
        }

        [HttpGet]
        [Route("GetPurchaseDetailsByCustomer")]
        public async Task<IActionResult> GetPurchaseDetailsByCustomer(int customerId)
        {
            return await _services.GetPurchaseDetailsByCustomer(customerId, this);
        }

        [HttpGet]
        [Route("GetPurchaseDetailsByMobile")]
        public async Task<IActionResult> GetPurchaseDetailsByMobile(int mobilePhoneId)
        {
            return await _services.GetPurchaseDetailsByMobile(mobilePhoneId, this);
        }
    }
}
