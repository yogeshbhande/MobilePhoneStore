using Microsoft.AspNetCore.Mvc;
using MobilePhoneStore.Models;

namespace MobilePhoneStore.Interfaces
{
    public interface IMobilePhoneServices
    {
        public Task<IActionResult> GetMobilePhones(ControllerBase controllerBase);

        public Task<ActionResult> GetMobilePhone(int id, ControllerBase controllerBase);

        public Task<ActionResult> AddMobilePhone(MobilePhone mobilePhone, ControllerBase controllerBase);

        public Task<IActionResult> UpdateMobilePhone(int id, MobilePhone mobilePhone, ControllerBase controllerBase);

        public Task<IActionResult> BulkInsertMobilePhones(List<MobilePhone> mobilePhones, ControllerBase controllerBase);

        public Task<IActionResult> BulkUpdateMobilePhones(List<MobilePhone> mobilePhones, ControllerBase controllerBase);

    }
}
