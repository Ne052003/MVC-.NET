using MVC.Domain.Services.Interfaces;
using MVC.Infrastructure.DataContext;
using MVC.Infrastructure.Models;
using MVC.Infrastructure.DTO.Customer;
using Microsoft.EntityFrameworkCore;

namespace MVC.Domain.Services
{
    public class CustomerService : ICustomerService
    {

        private readonly NorthwindContext _context;

        public CustomerService(NorthwindContext northwindContext)
        {
            this._context = northwindContext;
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            var customers = await _context.Customers.ToListAsync();

            return customers;
        }
        public async Task<Customer> GetCustomerAsync(string customerId)
        {
            var entity = await _context.Customers.FirstOrDefaultAsync(x => x.CustomerId == customerId);

            if (entity == null)
            {
                throw new Exception($"Customer with id {customerId} does not exits");
            }

            return entity;
        }

        public async Task<bool> AddCustomerAsync(AddCustomerDto customer)
        {
            Customer add = new Customer()
            {
                CustomerId = customer.CustomerId,
                CompanyName = customer.CompanyName,
                ContactName = customer.ContactName,
                Country = customer.Country,
                Phone = customer.Phone
            };

            _context.Customers.Add(add);
            bool success = await _context.SaveChangesAsync() > 0;

            return success;
        }

        public async Task<bool> UpdateCustomerAsync(UpdateCustomerDto update)
        {

            var entity = await GetCustomerAsync(update.CustomerId);

            entity.CustomerId = update.CustomerId;
            entity.CompanyName = update.CompanyName;
            entity.ContactName = update.ContactName;
            entity.Country = update.Country;
            entity.Phone = update.Phone;

            _context.Customers.Update(entity);
            bool success = await _context.SaveChangesAsync() > 0;

            return success;
        }

        public async Task<bool> DeleteCustomerAsync(string customerId)
        {

            var entity = await GetCustomerAsync(customerId);

            _context.Customers.Remove(entity);
            bool success = await _context.SaveChangesAsync() > 0;

            return success;
        }



    }
}
