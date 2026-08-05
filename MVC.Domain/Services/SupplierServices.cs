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
                Phone = Supplier.Phone
            };

            _context.Suppliers.Add(add);
            bool success = await _context.SaveChangesAsync() > 0;

            return success;
        }

        public async Task<bool> UpdateSupplierAsync(UpdateSupplierDto update)
        {

            var entity = await GetSupplierAsync(update.SupplierId);

            Supplier supplierDB = new()
            {
                SupplierId = entity.SupplierId,
                CompanyName = update.CompanyName,
                ContactName = update.ContactName,
                Address = update.Address,
                Country = update.Country,
                Phone = update.Phone
            };

            _context.Suppliers.Update(supplierDB);
            bool success = await _context.SaveChangesAsync() > 0;

            return success;
        }

        public async Task<bool> DeleteSupplierAsync(int SupplierId)
        {

            var entity = await GetSupplierAsync(SupplierId);
            Supplier supplierDB = new()
            {
                SupplierId = entity.SupplierId,
                CompanyName = entity.CompanyName,
                ContactName = entity.ContactName,
                Address = entity.Address,
                Country = entity.Country,
                Phone = entity.Phone
            };

            _context.Suppliers.Remove(supplierDB);
            bool success = await _context.SaveChangesAsync() > 0;

            return success;
        }
    }
}
