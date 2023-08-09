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
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerServices _services;
        public CustomerController(ICustomerServices customerServices)
        {
            _services = customerServices;
        }

        [HttpGet]
        [Route("GetCustomer")]
        public async Task<IActionResult> GetAllCustomers()
        {
            return await _services.GetAllCustomers(this);
        }

        [HttpGet]
        [Route("GetCustomerById")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            return await _services.GetCustomerById(id, this);
        }

        [HttpPost]
        [Route("AddCustomer")]
        public async Task<ActionResult> AddNewCustomer(Customer customer)
        {
            return await _services.AddNewCustomer(customer, this);
        }

        [HttpPut]
        [Route("UpdateCustomerDetails")]
        public async Task<IActionResult> UpdateCustomerDetails(int id, Customer customer)
        {
            return await _services.UpdateCustomerDetails(id, customer, this); ;
        }

        [HttpDelete]
        [Route("DeleteCustomerDetails")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            return await _services.DeleteCustomer(id, this);
        }
    }
}
