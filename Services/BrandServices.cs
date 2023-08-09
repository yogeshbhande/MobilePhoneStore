using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobilePhoneStore.Data;
using MobilePhoneStore.Interfaces;
using MobilePhoneStore.Models;

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

        public async Task<ActionResult> AddNewBrand(Brand brand, ControllerBase controllerBase)
        {
            try
            {
                _context.Brands.Add(brand);
                await _context.SaveChangesAsync();
                return controllerBase.Ok(new { status = "Success", message = "Brand added successfully" });
            }
            catch (Exception ex)
            {
                return controllerBase.BadRequest(new { status = "Error", message = ex.Message });
            }
        }

        public async Task<IActionResult> UpdateBrandDetails(int id, Brand brand, ControllerBase controllerBase)
        {

            if (id != brand.Id)
            {
                return controllerBase.BadRequest();
            }

            _context.Entry(brand).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return controllerBase.NotFound();
                }
                else
                {
                    throw;
                }
            }

            return controllerBase.Ok(new { Status = "Success", Message = "Brand details updated successfully" });
        }

        private bool CustomerExists(int id)
        {
            return _context.Brands.Any(e => e.Id == id);
        }

        public async Task<IActionResult> DeleteBrand(int id, ControllerBase controllerBase)
        {
            try
            {
                var brand = await _context.Brands.FindAsync(id);
                if (brand == null)
                {
                    return controllerBase.NotFound(new { Status = "Error", Message = "Brand Not Available" });
                }

                _context.Brands.Remove(brand);
                await _context.SaveChangesAsync();

                return controllerBase.Ok(new
                {
                    Message = "Brand deleted Successfully."
                });
            }
            catch (Exception ex)
            {
                return controllerBase.BadRequest(ex.Message);
            }
        }
    }
}
