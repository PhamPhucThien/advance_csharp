using advance_csharp.database.Models;
using advance_csharp.database;
using advance_csharp.dto.Response.Orders;
using advance_csharp.service.Interfaces;
using Microsoft.EntityFrameworkCore;
using static advance_csharp.dto.Response.Orders.ResponseGetOrder;

namespace advance_csharp.service.Services
{
    public class OrderService : IOrderService
    {
        public async Task<ResponseAddOrder> Add(string username)
        {
            ResponseAddOrder response = new();

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

                    if (cart != null && context.Products != null && cart.CartRecord != "")
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
                        List<Product>? products = await queryProduct.Select(a => new Product
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Price = a.Price,
                            Unit = a.Unit,
                            Images = a.Images,
                            Category = a.Category,
                            Quantity = a.Quantity,
                            IsAvailable = a.IsAvailable,
                            CreatedAt = a.CreatedAt
                        }).ToListAsync();

                        string newOrderRecord = string.Empty;
                        bool invalidQuantity = false;
                        long total = 0;

                        foreach (string record in records)
                        {
                            if (record != "")
                            {
                                string[] recordInfo = record.Split("&");
                                Guid id = new(recordInfo[0]);
                                int quantity = Int32.Parse(recordInfo[1]);
                                products.ForEach(x =>
                                {
                                    if (x.Id == id)
                                    {
                                        if (x.Quantity >= quantity)
                                        {
                                            string newProduct = x.Id + "&" + x.Name + "&" + x.Price + "&" + quantity;
                                            if (newOrderRecord != string.Empty) newOrderRecord += "/";
                                            newOrderRecord += newProduct;
                                            x.Quantity -= quantity;
                                            total += Int32.Parse(x.Price) * quantity;
                                            if (x.Quantity == 0) x.IsAvailable = false;
                                        }
                                        else
                                        {
                                            if (x.Quantity != 0)
                                            {
                                                invalidQuantity = true;
                                                string updateCart = x.Id + "&" + x.Quantity;
                                                cart.CartRecord = cart.CartRecord.Replace(record, updateCart);
                                            }
                                        }
                                    }
                                });
                            }
                        }
                        if (!invalidQuantity && context.Orders != null)
                        {
                            cart.CartRecord = "";

                            foreach (Product product in products)
                            {
                                context.Products.Update(product);
                            }

                            context.Carts.Update(cart);
                            Order order = new()
                            {
                                AccountId = account.Id,
                                TotalPrice = total,
                                OrderRecord = newOrderRecord
                            };
                            _ = context.Orders.Add(order);
                            response.IsSuccess = true;
                        }
                        else
                        {
                            context.Carts.Update(cart);
                            response.IsSuccess = false;
                        }
                        _ = await context.SaveChangesAsync();
                    }
                }
            }
            return response;
        }

        public async Task<ResponseGetOrder> Get(string username)
        {
            ResponseGetOrder response = new();

            using AdvanceCSharpContext context = new();
            if (context.Accounts != null)
            {
                IQueryable<Account> queryAccount = context.Accounts.Where(a => a.Username == username).AsQueryable();
                Account? account = await queryAccount.Select(a => new Account
                {
                    Id = a.Id,
                }).FirstOrDefaultAsync();

                if (account != null && context.Orders != null)
                {
                    IQueryable<Order> query = context.Orders.Where(a => a.AccountId == account.Id).AsQueryable();
                    List<OrderModel> orders = await query.Select(a => new OrderModel
                    {
                        Id = a.Id,
                        TotalPrice = a.TotalPrice,
                        CreatedAt = a.CreatedAt
                    }).ToListAsync();

                    response.Data = orders;
                }
            }
            return response;
        }

        public async Task<ResponseGetOrderById> GetById(string username, Guid Id)
        {
            ResponseGetOrderById response = new();

            using AdvanceCSharpContext context = new();
            if (context.Accounts != null)
            {
                IQueryable<Account> queryAccount = context.Accounts.Where(a => a.Username == username).AsQueryable();
                Account? account = await queryAccount.Select(a => new Account
                {
                    Id = a.Id,
                }).FirstOrDefaultAsync();

                if (account != null && context.Orders != null)
                {
                    IQueryable<Order> query = context.Orders.Where(a => a.Id == Id).AsQueryable();
                    Order? orders = await query.Select(a => new Order
                    {
                        OrderRecord = a.OrderRecord,
                        TotalPrice = a.TotalPrice
                    }).FirstOrDefaultAsync();

                    if (orders != null)
                    {
                        string[] records = orders.OrderRecord.Split("/");

                        foreach (string record in records)
                        {
                            if (record != "")
                            {
                                string[] recordInfo = record.Split("&");
                                ResponseGetOrderById.ProductInOrder product = new()
                                {
                                    Id = recordInfo[0],
                                    Name = recordInfo[1],
                                    Price = recordInfo[2],
                                    Quantity = recordInfo[3]
                                };
                                response.Data.Add(product);
                            }
                        }
                        response.Total = orders.TotalPrice;
                    }
                }
            }
            return response;
        }
    }
}
