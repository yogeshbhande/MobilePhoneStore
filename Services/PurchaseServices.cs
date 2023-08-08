using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobilePhoneStore.Data;
using MobilePhoneStore.Interfaces;
using MobilePhoneStore.Models;

namespace MobilePhoneStore.Services
{
    public class PurchaseServices : IPurchaseServices
    {
        private readonly AppDbContext _context;
        public IConfiguration _configuration;
        public PurchaseServices(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<IActionResult> GetAllPurchaseDetails(ControllerBase controllerBase)
        {
            try
            {
                var purchases = await _context.Purchases.ToListAsync();
                if (purchases == null || purchases.Count == 0)
                    return controllerBase.NotFound(new { status = "Error",message = "Purchase not found" });
                return controllerBase.Ok(purchases);
            }
            catch (Exception ex)
            {
                return controllerBase.BadRequest(new { status = "Error", message = ex.Message });
            }
        }

        public async Task<ActionResult> GetPurchaseDetailsByCustomer(int customerId, ControllerBase controllerBase)
        {
            try
            {
                var purchase = await _context.Purchases.Where(a => a.CustomerId == customerId).ToListAsync();

                if (purchase == null || purchase.Count == 0)
                {
                    return controllerBase.NotFound(new { status = "Error", message = "Purchase details not found" });
                }

                return controllerBase.Ok(purchase);
            }
            catch (Exception ex)
            {
                return controllerBase.BadRequest(new { status = "Error", message = ex.Message });
            }
        }

        public async Task<ActionResult> GetPurchaseDetailsById(int id, ControllerBase controllerBase)
        {
            try
            {
                var purchase = await _context.Purchases.FindAsync(id);

                if (purchase == null)
                {
                    return controllerBase.NotFound(new { status = "Error", message = "Purchase details Not found" });
                }

                return controllerBase.Ok(purchase);
            }
            catch (Exception ex)
            {
                return controllerBase.BadRequest(new { status = "Error", message = ex.Message });
            }
        }

        public async Task<ActionResult> GetPurchaseDetailsByMobile(int mobilePhoneId, ControllerBase controllerBase)
        {
            try
            {
                var purchase = await _context.Purchases.Where(a => a.MobilePhoneId == mobilePhoneId).ToListAsync();

                if (purchase == null || purchase.Count == 0)
                {
                    return controllerBase.NotFound(new { status = "Error", message = "Purchase details Not found" });
                }

                return controllerBase.Ok(purchase);
            }
            catch (Exception ex)
            {
                return controllerBase.BadRequest(new { status = "Error", message = ex.Message });
            }
        }

        public async Task<ActionResult> SaveNewPurchase(Purchase purchase, ControllerBase controllerBase)
        {
            try
            {
                _context.Purchases.Add(purchase);
                await _context.SaveChangesAsync();
                return controllerBase.Ok(new { Status = "Success", Message = "Purchase Save successfully" });
            }
            catch (Exception ex)
            {
                return controllerBase.BadRequest(new { status = "Error", message = ex.Message });
            }
        }
    }
}
