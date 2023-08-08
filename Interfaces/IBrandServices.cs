using Microsoft.AspNetCore.Mvc;

namespace MobilePhoneStore.Interfaces
{
    public interface IBrandServices
    {
        public Task<IActionResult> GetAllBrands(ControllerBase controllerBase);

        public Task<IActionResult> GetBrandById(int id, ControllerBase controllerBase);
    }
}
