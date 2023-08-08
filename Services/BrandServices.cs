using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobilePhoneStore.Data;
using MobilePhoneStore.Interfaces;

namespace MobilePhoneStore.Services
{
    public class BrandServices : IBrandServices
    {
        private readonly AppDbContext _context;
        public IConfiguration _configuration;
        public BrandServices(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<IActionResult> GetAllBrands(ControllerBase controllerBase)
        {
            try
            {
                var brands = await _context.Brands.ToListAsync();
                if (brands == null || brands.Count == 0)
                    return controllerBase.NotFound(new { status = "Error", message = "Brands not found" });
                return controllerBase.Ok(brands);
            }
            catch (Exception ex)
            {
                return controllerBase.BadRequest(new { status = "Error", message = ex.Message });
            }
        }

        public async Task<IActionResult> GetBrandById(int id, ControllerBase controllerBase)
        {
            try
            {
                var brand = await _context.Brands.FindAsync(id);

                if (brand == null)
                {
                    return controllerBase.NotFound(new { status = "Error", message = "Brand Not found" });
                }

                return controllerBase.Ok(brand);
            }
            catch (Exception ex)
            {
                return controllerBase.BadRequest(new { status = "Error", message = ex.Message });
            }
        }
    }
}
