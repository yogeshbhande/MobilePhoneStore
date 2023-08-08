using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobilePhoneStore.Interfaces;

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
    }
}
