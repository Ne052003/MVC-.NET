using ADSI2026.Handlers;
using Microsoft.AspNetCore.Mvc;
using MVC.Domain.Services;
using MVC.Domain.Services.Interfaces;
using MVC.Infrastructure.DTO.Customer;
using MVC.Infrastructure.DTO.Product;
using MVC.Infrastructure.DTO.Supplier;

namespace ADSI2026.Controllers
{
    [TypeFilter(typeof(CustomExceptionHandler))]
    public class ProductController : Controller
    {
        #region Properties
        private readonly IProductService _productService;
        #endregion

        #region Builder
        public ProductController(IProductService productService)
        {
            this._productService = productService;
        }
        #endregion

        #region Views
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region Services

        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            List<ProductDto> entities = await _productService.GetAllProducts();
            return Ok(entities);
        }

        [HttpGet("GetProduct")]
        public async Task<IActionResult> GetProductAsync(int productId)
        {
            var entity = await _productService.GetProductAsync(productId);
            return Ok(entity);
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(AddProductDto product)
        {
            bool sucess = await _productService.AddProductAsync(product);
            return Ok(sucess);

        }

        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProduct
            )
        {
            bool sucess = await _productService.UpdateProduct(updateProduct);
            return Ok(sucess);

        }

        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            bool sucess = await _productService.DeleteProductAsync(productId);
            return Ok(sucess);

        }
        #endregion
    }

}