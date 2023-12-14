using advance_csharp.database.Models;
using advance_csharp.database;
using advance_csharp.dto.Request.Carts;
using advance_csharp.dto.Response.Carts;
using advance_csharp.service.Interfaces;
using Microsoft.EntityFrameworkCore;
using static advance_csharp.dto.Response.Carts.ResponseGetCart;

namespace advance_csharp.service.Services
{
    public class CartService : ICartService
    {
        public async Task<ResponseGetCart> Get(string username)
        {
            ResponseGetCart response = new();

            using AdvanceCSharpContext context = new();
            if (context.Accounts != null)
            {
                IQueryable<Account> queryAccount = context.Accounts.Where(a => a.Username == username);
                Account? account = await queryAccount.Select(a => new Account
                {
                    Id = a.Id,
                }).FirstOrDefaultAsync();

                if (account != null && context.Carts != null)
                {
                    IQueryable<Cart> queryCart = context.Carts.Where(a => a.AccountId == account.Id);
                    Cart? cart = await queryCart.Select(a => new Cart
                    {
                        CartRecord = a.CartRecord
                    }).FirstOrDefaultAsync();

                    if (cart != null && context.Products != null)
                    {
                        string[] records = cart.CartRecord.Split('/');
                        List<Guid> codes = new();

                        foreach (string record in records)
                        {
                            if (record != "")
                            {
                                string[] recordInfo = record.Split("&");
                                codes.Add(new Guid(recordInfo[0]));
                            }
                        }

                        IQueryable<Product> queryProduct = context.Products.Where(a => codes.Contains(a.Id));
                        List<ProductModelForCart>? products = await queryProduct.Select(a => new ProductModelForCart
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Price = a.Price,
                            Unit = a.Unit,
                            Images = a.Images,
                            Category = a.Category
                        }).ToListAsync();

                        long TotalPrice = 0;

                        foreach (string record in records)
                        {
                            if (record != "")
                            {
                                string[] recordInfo = record.Split("&");
                                Guid id = new(recordInfo[0]);
                                products.ForEach(x =>
                                {
                                    if (x.Id == id) { 
                                        x.Quantity = Int32.Parse(recordInfo[1]);
                                        TotalPrice += (x.Quantity * Int64.Parse(x.Price));
                                    }
                                });
                            }
                        }
                        response.Data = products;
                        response.TotalPrice = TotalPrice;
                    }    
                }
            }

            return response;
        }

        public async Task<ResponseChangeOnCart> Add(string username, RequestModifyCart request)
        {
            ResponseChangeOnCart response = new();

            if (request.Quantity > 0)
            {
                using AdvanceCSharpContext context = new();
                if (context.Accounts != null)
                {
                    IQueryable<Account> queryAccount = context.Accounts.Where(a => a.Username == username);
                    Account? account = await queryAccount.Select(a => new Account
                    {
                        Id = a.Id,
                    }).FirstOrDefaultAsync();

                    if (account != null && context.Carts != null)
                    {
                        IQueryable<Cart> queryCart = context.Carts.Where(a => a.AccountId == account.Id);
                        Cart? cart = await queryCart.Select(a => new Cart
                        {
                            Id = a.Id,
                            AccountId = a.AccountId,
                            CartRecord = a.CartRecord
                        }).FirstOrDefaultAsync();

                        if (cart != null && context.Products != null)
                        {
                            if (cart.CartRecord != "") cart.CartRecord += "/";
                            IQueryable<Product> queryProduct = context.Products.Where(a => a.Id == request.ProductId);
                            Product? product = await queryProduct.Select(a => new Product
                            {
                                Id = a.Id,
                                Quantity = a.Quantity
                            }).FirstOrDefaultAsync();

                            if (product != null && product.Quantity >= request.Quantity)
                            {
                                string newProduct = request.ProductId.ToString() + "&" + request.Quantity.ToString();
                                cart.CartRecord += newProduct;
                                context.Carts.Update(cart);
                            }
                        }
                    }

                    int i = await context.SaveChangesAsync();
                    if (i != 0) response.IsSuccess = true;
                }
            }
            
            return response;
        }

        public async Task<ResponseChangeOnCart> Update(string username, RequestModifyCart request)
        {
            ResponseChangeOnCart response = new();

            if (request.Quantity > 0)
            {
                using AdvanceCSharpContext context = new();
                if (context.Accounts != null)
                {
                    IQueryable<Account> queryAccount = context.Accounts.Where(a => a.Username == username);
                    Account? account = await queryAccount.Select(a => new Account
                    {
                        Id = a.Id,
                    }).FirstOrDefaultAsync();

                    if (account != null && context.Carts != null)
                    {
                        IQueryable<Cart> queryCart = context.Carts.Where(a => a.AccountId == account.Id);
                        Cart? cart = await queryCart.Select(a => new Cart
                        {
                            Id = a.Id,
                            AccountId = a.AccountId,
                            CartRecord = a.CartRecord
                        }).FirstOrDefaultAsync();

                        if (cart != null && cart.CartRecord != "" && context.Products != null)
                        {
                            string[] records = cart.CartRecord.Split('/');

                            IQueryable<Product> queryProduct = context.Products.Where(a => a.Id == request.ProductId && a.IsAvailable == true);
                            ProductModelForCart? product = await queryProduct.Select(a => new ProductModelForCart
                            {
                                Id = a.Id,
                                Name = a.Name,
                                Price = a.Price,
                                Quantity = a.Quantity,
                                Unit = a.Unit,
                                Images = a.Images,
                                Category = a.Category
                            }).FirstOrDefaultAsync();

                            if (product != null)
                            {
                                foreach (string record in records)
                                {
                                    if (record != "")
                                    {
                                        string[] recordInfo = record.Split("&");
                                        Guid id = new(recordInfo[0]);
                                        if (id.Equals(product.Id) && product.Quantity >= request.Quantity)
                                        {
                                            string updateProduct = product.Id.ToString() + "&" + request.Quantity.ToString();
                                            cart.CartRecord = cart.CartRecord.Replace(record, updateProduct);
                                        }
                                    }
                                }
                            }
                            context.Carts.Update(cart);
                        }
                    }
                   
                    int i = await context.SaveChangesAsync();
                    if (i != 0) response.IsSuccess = true;
                }
            }

            return response;
        }

        public async Task<ResponseChangeOnCart> Delete(string username, Guid ProductId)
        {
            ResponseChangeOnCart response = new();

            using AdvanceCSharpContext context = new();
            if (context.Accounts != null)
            {
                IQueryable<Account> queryAccount = context.Accounts.Where(a => a.Username == username);
                Account? account = await queryAccount.Select(a => new Account
                {
                    Id = a.Id,
                }).FirstOrDefaultAsync();

                if (account != null && context.Carts != null)
                {
                    IQueryable<Cart> queryCart = context.Carts.Where(a => a.AccountId == account.Id);
                    Cart? cart = await queryCart.Select(a => new Cart
                    {
                        Id = a.Id,
                        AccountId = a.AccountId,
                        CartRecord = a.CartRecord
                    }).FirstOrDefaultAsync();

                    if (cart != null && cart.CartRecord != "" && context.Products != null)
                    {
                        string[] records = cart.CartRecord.Split('/');

                        foreach (string record in records)
                        {
                            if (record.Contains(ProductId.ToString()))
                            {
                                cart.CartRecord = cart.CartRecord.Replace(record, "");
                                cart.CartRecord = cart.CartRecord.Replace("//", "/");
                                cart.CartRecord = cart.CartRecord.TrimStart('/').TrimEnd('/');
                            }
                        }
                        context.Carts.Update(cart);

                    }
                }

                int i = await context.SaveChangesAsync();
                if (i != 0) response.IsSuccess = true;
            }

            return response;
        }
    }
}
