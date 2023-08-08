using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobilePhoneStore.Data;
using MobilePhoneStore.Interfaces;
using MobilePhoneStore.Models;

namespace MobilePhoneStore.Services
{
    public class MobilePhoneServices : IMobilePhoneServices
    {
        private readonly AppDbContext _context;
        public IConfiguration _configuration;
        public MobilePhoneServices(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<IActionResult> GetMobilePhones(ControllerBase controllerBase)
        {
            try
            {
                var mobilePhones = await _context.MobilePhones.Include(m => m.Brand).ToListAsync();
                if (mobilePhones == null || mobilePhones.Count == 0)
                    return controllerBase.NotFound(new { status = "Error", message = "Phones not found" });
                return controllerBase.Ok(mobilePhones);
            }
            catch (Exception ex)
            {
                return controllerBase.BadRequest(new { status = "Error", message = ex.Message });
            }
        }

        public async Task<ActionResult> GetMobilePhone(int id, ControllerBase controllerBase)
        {
            try
            {
                var mobilePhone = await _context.MobilePhones
                    .Include(m => m.Brand)
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (mobilePhone == null)
                {
                    return controllerBase.NotFound(new { status = "Error", message = "Phone not available" });
                }

                return controllerBase.Ok(mobilePhone);
            }
            catch (Exception ex)
            {
                return controllerBase.BadRequest(new { status = "Error", message = ex.Message });
            }
        }

        public async Task<ActionResult> AddMobilePhone(MobilePhone mobilePhone, ControllerBase controllerBase)
        {
            try
            {
                // Check if the brand exists, if not, create it
                var existingBrand = await _context.Brands.FirstOrDefaultAsync(b => b.BrandName.ToLower() == mobilePhone.Brand.BrandName.ToLower());
                if (existingBrand == null)
                {
                    _context.Brands.Add(mobilePhone.Brand);
                    await _context.SaveChangesAsync();
                    existingBrand = mobilePhone.Brand;
                }

                mobilePhone.Brand = existingBrand;

                _context.MobilePhones.Add(mobilePhone);
                await _context.SaveChangesAsync();

                return controllerBase.Ok(new { Status = "Success", Message = "Mobile Phone added successfully" });
            }
            catch (Exception ex)
            {
                return controllerBase.BadRequest(new { status = "Error", message = ex.Message });
            }
        }

        public async Task<IActionResult> UpdateMobilePhone(int id, MobilePhone mobilePhone, ControllerBase controllerBase)
        {
            if (id != mobilePhone.Id)
            {
                return controllerBase.BadRequest();
            }

            var mobile = await _context.MobilePhones.Include(m => m.Brand).Where(a =>a.Id == mobilePhone.Id).ToListAsync();
            if (mobile == null || mobile.Count == 0)
                return controllerBase.BadRequest(new { status = "Error", message = "Mobile is not found for update" });

            if(mobilePhone.Brand.Id != mobilePhone.BrandId)
                return controllerBase.BadRequest(new { status = "Error", message = "BrandId should be same"});

            var brand = await _context.Brands.Where(a => a.Id == mobilePhone.BrandId && a.BrandName == mobilePhone.Brand.BrandName).FirstAsync();
            if (brand == null)
                return controllerBase.BadRequest(new { status = "Error", message = "Brand Id and BrandName is not matching" });

            _context.Entry(mobilePhone).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MobilePhoneExists(id))
                {
                    return controllerBase.NotFound();
                }
                else
                {
                    throw;
                }
            }

            return controllerBase.NoContent();
        }

        private bool MobilePhoneExists(int id)
        {
            return _context.MobilePhones.Any(e => e.Id == id);
        }

        public async Task<IActionResult> BulkInsertMobilePhones(List<MobilePhone> mobilePhones, ControllerBase controllerBase)
        {
            try
            {
                for (int i = 0; i < mobilePhones.Count; i++)
                {
                    var existingBrand = await _context.Brands.FirstOrDefaultAsync(b => b.BrandName.ToLower() == mobilePhones[i].Brand.BrandName.ToLower());
                    if (existingBrand == null)
                    {
                        _context.Brands.Add(mobilePhones[i].Brand);
                        await _context.SaveChangesAsync();
                        existingBrand = mobilePhones[i].Brand;
                    }

                    mobilePhones[i].Brand = existingBrand;

                    _context.MobilePhones.Add(mobilePhones[i]);
                    await _context.SaveChangesAsync();

                }
                //_context.MobilePhones.AddRange(mobilePhones);
                //await _context.SaveChangesAsync();

                return controllerBase.Ok(new { Status = "Success", Message = "Mobile Phones added successfully" });
            }
            catch (Exception ex)
            {
                return controllerBase.BadRequest(new { status = "Error", message = ex.Message });
            }
        }

        public async Task<IActionResult> BulkUpdateMobilePhones(List<MobilePhone> mobilePhones, ControllerBase controllerBase)
        {
            try
            {
                foreach (var phone in mobilePhones)
                {
                    _context.Entry(phone).State = EntityState.Modified;
                }

                await _context.SaveChangesAsync();

                return controllerBase.Ok();
            }
            catch (Exception ex)
            {
                return controllerBase.BadRequest(new { status = "Error", message = ex.Message });
            }
        }
    }
}
