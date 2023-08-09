using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobilePhoneStore.Interfaces;
using MobilePhoneStore.Models;
using MobilePhoneStore.Services;

namespace MobilePhoneStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandServices _services;
        public BrandController(IBrandServices brandServices)
        {
            _services = brandServices;
        }

        [HttpGet]
        [Route("GetBrands")]
        public async Task<IActionResult> GetAllBrands()
        {
            return await _services.GetAllBrands(this);
        }

        [HttpGet]
        [Route("GetBrandById")]
        public async Task<IActionResult> GetBrandById(int id)
        {
            return await _services.GetBrandById(id, this);
        }

        [HttpPost]
        [Route("AddBrand")]
        public async Task<ActionResult> AddNewCustomer(Brand brands)
        {
            return await _services.AddNewBrand(brands, this);
        }

        [HttpPut]
        [Route("UpdateBrandDetails")]
        public async Task<IActionResult> UpdateCustomerDetails(int id, Brand brands)
        {
            return await _services.UpdateBrandDetails(id, brands, this);
        }

        [HttpDelete]
        [Route("DeleteBrand")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            return await _services.DeleteBrand(id, this);
        }
    }
}
