using MVC.Infrastructure.DTO.Supplier;
using MVC.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Domain.Services.Interfaces
{
    public interface ISupplierServices
    {
        Task<bool> AddSupplierAsync(AddSupplierDto Supplier);
        Task<bool> DeleteSupplierAsync(int SupplierId);
        Task<List<SupplierDto>> GetAllSuppliers();
        Task<SupplierDto> GetSupplierAsync(int SupplierId);
        Task<bool> UpdateSupplierAsync(UpdateSupplierDto update);
    }
}
