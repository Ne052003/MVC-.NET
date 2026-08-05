using MVC.Infrastructure.DTO.Product;
using MVC.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Domain.Services.Interfaces
{
    public interface IProductService
    {
        Task<bool> AddProductAsync(AddProductDto add);
        Task<bool> UpdateProduct(UpdateProductDto updateProduct);
        Task<List<ProductDto>> GetAllProducts();
        Task<bool> DeleteProductAsync(int productId);
        Task<Product> GetProductAsync(int productId);
    }
}
