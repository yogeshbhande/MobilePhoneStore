using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobilePhoneStore.Interfaces;
using MobilePhoneStore.Services;

namespace MobilePhoneStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginServices _services;
        public LoginController(ILoginServices loginServices)
        {
            _services = loginServices;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginSubmit(string Username, string Password)
        {
            return await _services.LoginSubmit(Username, Password, this);
        }
    }
}
