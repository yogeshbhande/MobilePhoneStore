using Microsoft.AspNetCore.Mvc;
using MobilePhoneStore.Models;

namespace MobilePhoneStore.Interfaces
{
    public interface ICustomerServices
    {
        public Task<IActionResult> GetAllCustomers(ControllerBase controllerBase);

        public Task<ActionResult> GetCustomerById(int id, ControllerBase controllerBase);

        public Task<ActionResult> AddNewCustomer(Customer customer, ControllerBase controllerBase);

        public Task<IActionResult> UpdateCustomerDetails(int id, Customer customer, ControllerBase controllerBase);

    }
}
