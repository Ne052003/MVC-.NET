using ADSI2026.Handlers;
using Microsoft.AspNetCore.Mvc;
using MVC.Domain.Services.Interfaces;
using MVC.Infrastructure.DTO.Customer;
using MVC.Infrastructure.Models;

namespace ADSI2026.Controllers
{
    [TypeFilter(typeof(CustomExceptionHandler))]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            this._customerService = customerService;
        }

        public async Task<IActionResult> Index()
        {

            //var result = await _customerService.GetAllCustomers();
            return View();
        }

        [HttpGet("GetAllCustomers")]
        public async Task<IActionResult> GetAllCustomers()
        {

            List<Customer> entities = await _customerService.GetAllCustomers();
            return Ok(entities);
        }

        [HttpGet("GetCustomer")]
        public async Task<IActionResult> GetCustomerAsync(string customerID)
        {
            Customer entity = await _customerService.GetCustomerAsync(customerID);
            return Ok(entity);
        }

        [HttpPost("AddCustomer")]
        public async Task<IActionResult> AddCustomer(AddCustomerDto customer)
        {
            bool sucess = await _customerService.AddCustomerAsync(customer);
            return Ok(sucess);

        }
        [HttpPut("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer(UpdateCustomerDto updateCustomer)
        {
            bool sucess = await _customerService.UpdateCustomerAsync(updateCustomer);
            return Ok(sucess);

        }

        [HttpDelete("DeleteCustomer")]
        public async Task<IActionResult> DeleteCustomer(string customerId)
        {
            bool sucess = await _customerService.DeleteCustomerAsync(customerId);
            return Ok(sucess);

        }


    }
}
