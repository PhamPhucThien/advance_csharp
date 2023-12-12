using advance_csharp.database;
using advance_csharp.database.Models;
using advance_csharp.dto.Request.Products;
using advance_csharp.dto.Response.Products;
using advance_csharp.service.Interfaces;
using Microsoft.EntityFrameworkCore;
using static advance_csharp.dto.Request.Products.RequestAddProduct;
using static advance_csharp.dto.Request.Products.RequestUpdateProduct;

namespace advance_csharp.service.Services
{
    public class ProductService : IProductService
    {
        public async Task<ResponseAddProduct> Add(RequestAddProduct requestAddProduct)
        {
            ResponseAddProduct response = new();

            using AdvanceCSharpContext context = new();
            if (context.Products != null)
            {
                foreach (AddProductModel product in requestAddProduct.Data)
                {
                    bool tryParse = Int32.TryParse(product.Price, out _);
                    if (tryParse)
                    {
                        Product contain = new()
                        {
                            Id = Guid.NewGuid(),
                            Name = product.Name,
                            Price = product.Price,
                            Quantity = product.Quantity,
                            Images = product.Images,
                            Category = product.Category
                        };
                        context.Products.Add(contain);
                    }
                    else
                    {
                        break;
                    }
                    
                }

                response.NumberOfSuccess = await context.SaveChangesAsync();
            }

            return response;
        }

        public async Task<ResponseGetProductById> GetById(Guid id)
        {
            ResponseGetProductById getProductById = new();

            using AdvanceCSharpContext context = new();
            if (context.Products != null)
            {
                IQueryable<Product> query = context.Products.Where(a => a.Id == id);
                Product? product = await query.Select(a => new Product
                {
                    Id = a.Id,
                    Name = a.Name,
                }).FirstOrDefaultAsync();

                if (product != null) getProductById.Data = product;
            }

            return getProductById;
        }

        public async Task<ResponseGetProductBySearch> GetBySearch(string? search, int page, int size)
        {
            ResponseGetProductBySearch getProductBySearch = new();

            if (page <= 0) { page = 1; }
            if (size <= 0) { size = 1; }

            using AdvanceCSharpContext context = new();
            if (context.Products != null)
            {
                IQueryable<Product>? query = context.Products;

                if (search != null && search.Length != 0)
                {
                    query = query.Where(a => a.Name.Contains(search));
                }

                query = query.OrderBy(a => a.Name)
                    .Skip(size * (page - 1))
                    .Take(size);

                getProductBySearch.Data = await query.Select(a => new Product
                {
                    Id = a.Id,
                    Name = a.Name,
                }).ToListAsync();
            }
            return getProductBySearch;
        }

        public async Task<ResponseUpdateProduct> Update(RequestUpdateProduct requestUpdateProduct)
        {
            ResponseUpdateProduct response = new();

            using AdvanceCSharpContext context = new();
            if (context.Products != null)
            {
                foreach (UpdateProductModel product in requestUpdateProduct.Data)
                {
                    IQueryable<Product> query = context.Products.Where(a => a.Id == product.Id);
                    Product? contain = await query.Select(a => new Product
                    {
                        Id = a.Id,
                        Name = a.Name,
                        Price = a.Price,
                        Quantity = a.Quantity,
                        Unit = a.Unit,
                        Images = a.Images,
                        Category = a.Category,
                        CreatedAt = a.CreatedAt,
                        IsAvailable = a.IsAvailable,
                    }).FirstOrDefaultAsync();

                    if (contain != null)
                    {
                        if (product.Name != null) contain.Name = product.Name;
                        if (product.Price != null)
                        {
                            bool tryParse = Int32.TryParse(product.Price, out _);
                            if (tryParse)
                            {
                                contain.Price = product.Price;
                            } else
                            {
                                break;
                            }
                        }
                        if (product.Quantity != null) contain.Quantity = product.Quantity.Value;
                        if (product.Images != null) contain.Images = product.Images;
                        if (product.Category != null) contain.Category = product.Category;
                        if (product.IsAvailable != null) contain.IsAvailable = product.IsAvailable.Value;

                        context.Products.Update(contain);
                    }
                }

                response.NumberOfSuccess = await context.SaveChangesAsync();
            }

            return response;
        }
    }
}
