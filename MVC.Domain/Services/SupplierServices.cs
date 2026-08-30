using Microsoft.EntityFrameworkCore;
using MVC.Domain.Services.Interfaces;
using MVC.Infrastructure.DataContext;
using MVC.Infrastructure.DTO.Supplier;
using MVC.Infrastructure.Models;
using System.Diagnostics.Metrics;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MVC.Domain.Services
{
    public class SupplierServices : ISupplierServices
    {


        private readonly NorthwindContext _context;

        public SupplierServices(NorthwindContext northwindContext)
        {
            this._context = northwindContext;
        }

        public async Task<List<SupplierDto>> GetAllSuppliers()
        {
            var suppliers = await _context.Suppliers.ToListAsync();

            List<SupplierDto> result = suppliers.Select(x => new SupplierDto()
            {
                SupplierId = x.SupplierId,
                Address = x.Address,
                City = x.City,
                CompanyName = x.CompanyName,
                ContactName = x.ContactName,
                Country = x.Country,
                Phone = x.Phone,
            }).ToList();

            return result;
        }
        public async Task<SupplierDto> GetSupplierAsync(int SupplierId)
        {
            var x = await _context.Suppliers.FirstOrDefaultAsync(x => x.SupplierId == SupplierId);

            if (x == null)
            {
                throw new Exception($"Supplier with id {SupplierId} does not exits");
            }
            SupplierDto result = new SupplierDto()
            {
                SupplierId = x.SupplierId,
                Address = x.Address,
                City = x.City,
                CompanyName = x.CompanyName,
                ContactName = x.ContactName,
                Country = x.Country,
                Phone = x.Phone,
            };
            return result;
        }

        public async Task<bool> AddSupplierAsync(AddSupplierDto Supplier)
        {
            Supplier add = new Supplier()
            {
                CompanyName = Supplier.CompanyName,
                ContactName = Supplier.ContactName,
                Address = Supplier.Address,
                Country = Supplier.Country,
                City= Supplier.City,
                Phone = Supplier.Phone
            };

            _context.Suppliers.Add(add);
            bool success = await _context.SaveChangesAsync() > 0;

            return success;
        }

        public async Task<bool> UpdateSupplierAsync(UpdateSupplierDto update)
        {

            var supplier = await _context.Suppliers.FirstOrDefaultAsync(s => s.SupplierId == update.SupplierId);
            if (supplier == null)
            {
                throw new Exception($"Supplier with id {update.SupplierId} does not exist");
            }

            supplier.CompanyName = update.CompanyName;
            supplier.ContactName = update.ContactName;
            supplier.Address = update.Address;
            supplier.Country = update.Country;
            supplier.City = update.City;
            supplier.Phone = update.Phone;

            _context.Suppliers.Update(supplier);
            bool success = await _context.SaveChangesAsync() > 0;

            return success;
        }

        public async Task<bool> DeleteSupplierAsync(int SupplierId)
        {

            var supplier = await _context.Suppliers
                                 .Include(s => s.Products)
                                 .FirstOrDefaultAsync(s => s.SupplierId == SupplierId);
            if (supplier == null)
                throw new Exception($"Supplier with id {SupplierId} does not exist");

            _context.Suppliers.Remove(supplier);
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Error deleting supplier: " + (ex.InnerException?.Message ?? ex.Message), ex);
            }
        }
    }
}
