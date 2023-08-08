using Microsoft.AspNetCore.Mvc;

namespace MobilePhoneStore.Interfaces
{
    public interface ILoginServices
    {
        public Task<IActionResult> LoginSubmit(string Username, string Password, ControllerBase controllerBase);

    }
}
