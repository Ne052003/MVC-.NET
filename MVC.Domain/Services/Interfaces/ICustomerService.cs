using Microsoft.EntityFrameworkCore;
using MVC.Infrastructure.DTO.Customer;
using MVC.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Domain.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<bool> AddCustomerAsync(AddCustomerDto customer);
        Task<List<Customer>> GetAllCustomers();
        Task<Customer> GetCustomerAsync(string customerId);
        Task<bool> DeleteCustomerAsync(string customerId);
        Task<bool> UpdateCustomerAsync(UpdateCustomerDto update);
    }
}
