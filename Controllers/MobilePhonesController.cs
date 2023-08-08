using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobilePhoneStore.Data;
using MobilePhoneStore.Interfaces;
using MobilePhoneStore.Models;

namespace MobilePhoneStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MobilePhonesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMobilePhoneServices _services;
        public MobilePhonesController(AppDbContext context, IMobilePhoneServices mobilePhoneServices)
        {
            _context = context;
            _services = mobilePhoneServices;
        }

        [HttpGet]
        [Route("GetMobilePhones")]
        public async Task<IActionResult> GetMobilePhones()
        {
            return await _services.GetMobilePhones(this);
        }

        [HttpGet]
        [Route("GetMobilePhoneById")]
        public async Task<ActionResult> GetMobilePhone(int id)
        {
            return await _services.GetMobilePhone(id, this);
        }

        [HttpPost]
        [Route("AddMobilePhone")]
        public async Task<ActionResult> PostMobilePhone(MobilePhone mobilePhone)
        { 
            return await _services.AddMobilePhone(mobilePhone, this); 
        }

        [HttpPut]
        [Route("UpdateMobilePhone")]
        public async Task<IActionResult> PutMobilePhone(int id, MobilePhone mobilePhone)
        {   
            return await _services.UpdateMobilePhone(id, mobilePhone, this); ;
        }

        

        [HttpPost]
        [Route("BulkInsert")]
        public async Task<IActionResult> BulkInsertMobilePhones(List<MobilePhone> mobilePhones)
        {
            return await _services.BulkInsertMobilePhones(mobilePhones, this); ;
        }

        [HttpPost("BulkUpdate")]
        public async Task<IActionResult> BulkUpdateMobilePhones(List<MobilePhone> mobilePhones)
        {
            return await _services.BulkUpdateMobilePhones(mobilePhones, this);
        }


    }
}

