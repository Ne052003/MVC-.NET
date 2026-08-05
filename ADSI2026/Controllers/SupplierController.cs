using Microsoft.AspNetCore.Mvc;
using MVC.Domain.Services.Interfaces;
using MVC.Infrastructure.DTO.Supplier;

namespace ADSI2026.Controllers
{
    public class SupplierController: Controller
    {
        private readonly ISupplierServices _supplierServices;

        public SupplierController(ISupplierServices supplierService)
        {
            this._supplierServices = supplierService;
        }

        public IActionResult SuppliersView()
        {

            //var result = await _SupplierService.GetAllSuppliers();
            return View();
        }

        [HttpGet("GetAllSuppliers")]
        public async Task<IActionResult> GetAllSuppliers()
        {

            List<SupplierDto> entities = await _supplierServices.GetAllSuppliers();
            return Ok(entities);
        }

        [HttpGet("GetSupplier")]
        public async Task<IActionResult> GetSupplierAsync([FromQuery] int SupplierID)
        {
            SupplierDto entity = await _supplierServices.GetSupplierAsync(SupplierID);
            return Ok(entity);
        }

        [HttpPost("AddSupplier")]
        public async Task<IActionResult> AddSupplier([FromBody] AddSupplierDto Supplier)
        {
            bool sucess = await _supplierServices.AddSupplierAsync(Supplier);
            return Ok(sucess);

        }
        [HttpPost("UpdateSupplier")]
        public async Task<IActionResult> UpdateSupplier([FromBody] UpdateSupplierDto updateSupplier)
        {
            bool sucess = await _supplierServices.UpdateSupplierAsync(updateSupplier);
            return Ok(sucess);

        }
    }
}
