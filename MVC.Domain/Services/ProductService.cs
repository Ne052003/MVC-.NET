using Microsoft.EntityFrameworkCore;
using MVC.Domain.Services.Interfaces;
using MVC.Infrastructure.DataContext;
using MVC.Infrastructure.DTO.Product;
using MVC.Infrastructure.DTO.Supplier;
using MVC.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MVC.Domain.Services
{

    public class ProductService : IProductService

    {
        private readonly NorthwindContext _context;
        public ProductService(NorthwindContext northwindContext)
        {
            this._context = northwindContext;
        }

        public async Task<List<ProductDto>> GetAllProducts()
        {
            List<Product> products = await _context.Products
                                                  .Include(x => x.Supplier)
                                                  .Include(x => x.Category)
                                                  .ToListAsync();
            List<ProductDto> result = products.Select(static x => new ProductDto()
            {
                ProductId = x.ProductId,
                CategoryId = x.CategoryId,
                SupplierId = x.SupplierId,
                ProductName = x.ProductName,
                UnitPrice = x.UnitPrice,
                UnitsInStock = x.UnitsInStock,
                Category = x.Category?.CategoryName ?? "CategoryName",
                Supplier = x.Supplier?.CompanyName ?? "SupplierName"
            }).ToList();

            return result;
        }
        public async Task<Product> GetProductAsync(int productId)
        {
            var x = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == productId);

            if (x == null)
            {
                throw new Exception($"Product with id {productId} does not exits");
            }
            ProductDto result = new ProductDto()
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                CategoryId = x.CategoryId,
                SupplierId = x.SupplierId,
                UnitPrice = x.UnitPrice,
                UnitsInStock = x.UnitsInStock
            };
            return x;
        }

        public async Task<bool> AddProductAsync(AddProductDto add)
        {
            Product entity = new Product()
            {
                ProductName = add.ProductName,
                CategoryId = add.CategoryId,
                SupplierId = add.SupplierId,
                UnitPrice = add.UnitPrice,
                UnitsInStock = add.UnitsInStock
            };

            _context.Products.Add(entity);
            bool success = await _context.SaveChangesAsync() > 0;

            return success;
        }
        public async Task<bool> DeleteProductAsync(int productId)
        {
            Product entity = await GetProductAsync(productId);

            _context.Products.Remove(entity);
            bool success = await _context.SaveChangesAsync() > 0;

            return success;
        }

        public async Task<bool> UpdateProduct(UpdateProductDto updateProduct)
        {
            var entity = await GetProductAsync(updateProduct.ProductId);


            entity.ProductId = updateProduct.ProductId;
            entity.UnitsInStock = updateProduct.UnitsInStock;
            entity.ProductName = updateProduct.ProductName;
            entity.SupplierId = updateProduct.SupplierId;
            entity.UnitPrice = updateProduct.UnitPrice;
            entity.CategoryId = updateProduct.CategoryId;


            _context.Products.Update(entity);
            bool success = await _context.SaveChangesAsync() > 0;

            return success;
        }
    }
}
