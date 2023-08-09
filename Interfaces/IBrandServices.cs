using Microsoft.AspNetCore.Mvc;
using MobilePhoneStore.Models;

namespace MobilePhoneStore.Interfaces
{
    public interface IBrandServices
    {
        public Task<IActionResult> GetAllBrands(ControllerBase controllerBase);

        public Task<IActionResult> GetBrandById(int id, ControllerBase controllerBase);

        public Task<ActionResult> AddNewBrand(Brand brand, ControllerBase controllerBase);

        public Task<IActionResult> UpdateBrandDetails(int id, Brand brand, ControllerBase controllerBase);

        public Task<IActionResult> DeleteBrand(int id, ControllerBase controllerBase);
    }
}
