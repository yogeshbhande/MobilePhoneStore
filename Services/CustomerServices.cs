using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobilePhoneStore.Data;
using MobilePhoneStore.Interfaces;
using MobilePhoneStore.Models;

namespace MobilePhoneStore.Services
{
    public class CustomerServices : ICustomerServices
    {
        private readonly AppDbContext _context;
        public IConfiguration _configuration;
        public CustomerServices(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<ActionResult> AddNewCustomer(Customer customer, ControllerBase controllerBase)
        {
            try
            {
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
                return controllerBase.Ok(new { status = "Success", message = "Customer added successfully" });
            }
            catch (Exception ex)
            {
                return controllerBase.BadRequest(new { status = "Error", message = ex.Message });
            }
        }

        public async Task<IActionResult> GetAllCustomers(ControllerBase controllerBase)
        {
            try
            {
                var customers = await _context.Customers.ToListAsync();
                if (customers == null || customers.Count == 0)
                    return controllerBase.NotFound(new {status = "Error", message = "Customers not found" });
                return controllerBase.Ok(customers);
            }
            catch (Exception ex)
            {
                return controllerBase.BadRequest(new { status = "Error", message = ex.Message });
            }
        }

        public async Task<ActionResult> GetCustomerById(int id, ControllerBase controllerBase)
        {
            try
            {
                var customer = await _context.Customers.FindAsync(id);

                if (customer == null)
                {
                    return controllerBase.NotFound(new { status = "Error", message = "Customer Not found" });
                }

                return controllerBase.Ok(customer);
            }
            catch (Exception ex)
            {
                return controllerBase.BadRequest(new { status = "Error", message = ex.Message });
            }
        }

        public async Task<IActionResult> UpdateCustomerDetails(int id, Customer customer, ControllerBase controllerBase)
        {

            if (id != customer.Id)
            {
                return controllerBase.BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return controllerBase.NotFound();
                }
                else
                {
                    throw;
                }
            }

            return controllerBase.Ok(new { Status = "Success", Message = "Customer details update successfully" });
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
