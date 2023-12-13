using advance_csharp.database.Models;
using advance_csharp.database;
using advance_csharp.dto.Request.Accounts;
using advance_csharp.dto.Response.Accounts;
using advance_csharp.service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace advance_csharp.service.Services
{
    public class AccountService : IAccountService
    {
        public async Task<ResponseDeleteAccount> Delete(Guid Id)
        {
            ResponseDeleteAccount response = new();

            using AdvanceCSharpContext context = new();
            if (context.Accounts != null)
            {
                IQueryable<Account> query = context.Accounts.Where(a => a.Id == Id);
                Account? contain = await query.Select(a => new Account
                {
                    Id = a.Id,
                    Name = a.Name,
                    Username = a.Username,
                    Password = a.Password,
                    IsAvailable = a.IsAvailable,
                    Role = a.Role,
                }).FirstOrDefaultAsync();

                if (contain != null)
                {
                    contain.IsAvailable = false;
                    context.Accounts.Update(contain);
                }

                int i = await context.SaveChangesAsync();
                if (i != 0) response.IsSuccess = true;
            }

            return response;
        }

        public async Task<ResponseGetAccount> Get(string username)
        {
            ResponseGetAccount response = new();

            using AdvanceCSharpContext context = new();
            if (context.Accounts != null)
            {
                IQueryable<Account> query = context.Accounts.Where(a => a.Username == username);
                Account? account = await query.Select(a => new Account
                {
                    Username = a.Username,
                    Name = a.Name,
                    Role = a.Role
                }).FirstOrDefaultAsync();

                if (account != null) { 
                    response.Username = account.Username;
                    response.Name = account.Name;
                    response.Role = account.Role;
                }
            }

            return response;
        }

        public async Task<ResponseGetAccountById> GetById(Guid Id)
        {
            ResponseGetAccountById response = new();

            using AdvanceCSharpContext context = new();
            if (context.Accounts != null)
            {
                IQueryable<Account> query = context.Accounts.Where(a => a.Id == Id);
                Account? account = await query.Select(a => new Account
                {
                    Id = a.Id,
                    Username = a.Username,
                    Name = a.Name,
                    Role = a.Role,
                    IsAvailable = a.IsAvailable
                }).FirstOrDefaultAsync();

                if (account != null)
                {
                    response.Id = account.Id.ToString();
                    response.Username = account.Username;
                    response.Name = account.Name;
                    response.Role = account.Role;
                    response.IsAvailable = account.IsAvailable;
                }
            }

            return response;
        }

        public async Task<ResponseUpdateAccount> Update(string username, RequestUpdateAccount requestUpdateAccount)
        {
            ResponseUpdateAccount response = new();

            using AdvanceCSharpContext context = new();
            if (context.Accounts != null)
            {
                IQueryable<Account> query = context.Accounts.Where(a => a.Username == username);
                Account? contain = await query.Select(a => new Account
                {
                    Id = a.Id,
                    Name = a.Name,
                    Username = a.Username,
                    Password = a.Password,
                    IsAvailable = a.IsAvailable,
                    Role = a.Role,
                }).FirstOrDefaultAsync();

                if (contain != null)
                {
                    if (requestUpdateAccount.Name != null) contain.Name = requestUpdateAccount.Name;
                    if (requestUpdateAccount.Password != null) contain.Password = requestUpdateAccount.Password;
                    context.Accounts.Update(contain);
                }

                int i = await context.SaveChangesAsync();
                if (i != 0) response.IsSuccess = true;
            }

            return response;
        }
    }
}
