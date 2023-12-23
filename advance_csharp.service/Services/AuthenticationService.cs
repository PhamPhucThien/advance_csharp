using advance_csharp.database;
using advance_csharp.database.Models;
using advance_csharp.dto.Request.Authentication;
using advance_csharp.dto.Response.Authentication;
using advance_csharp.service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace advance_csharp.service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;

        public AuthenticationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<ResponseLogin> Login(RequestLogin login)
        {
            ResponseLogin response = new();

            using AdvanceCSharpContext context = new();
            if (context.Accounts != null)
            {
                IQueryable<Account> query = context.Accounts.
                    Where(a => a.Username == login.Username
                    && a.Password == login.Password
                    && a.IsAvailable == true).AsQueryable();

                Account? account = await query.Select(a => new Account
                {
                    Id = a.Id,
                    Username = a.Username,
                    Role = a.Role
                }).FirstOrDefaultAsync();

                if (account != null)
                {
                    response.Jwt = CreateToken(account);
                }
            }

            return response;
        }

        public async Task<ResponseRegister> Register(RequestRegister register)
        {

            ResponseRegister response = new();

            using AdvanceCSharpContext context = new();
            if (context.Accounts != null && context.Carts != null)
            {
                IQueryable<Account> query = context.Accounts.
                    Where(a => a.Username == register.Username).AsQueryable();

                Account? account = await query.Select(a => new Account
                {
                    Id = a.Id,
                    Username = a.Username,
                    Role = a.Role
                }).FirstOrDefaultAsync();

                if (account == null)
                {
                    Account createAccount = new()
                    {
                        Username = register.Username,
                        Name = register.Name,
                        Password = register.Password,
                        Role = "User",
                        IsAvailable = true,
                    };
                    Cart createCart = new()
                    {
                        AccountId = createAccount.Id,
                        CartRecord = ""
                    };

                    _ = context.Accounts.Add(createAccount);
                    _ = context.Carts.Add(createCart);
                    int i = await context.SaveChangesAsync();
                    if (i != 0)
                    {
                        response.IsSuccess = true;
                    }
                }
            }

            return response;

        }

        private string CreateToken(Account account)
        {
            List<Claim> claims = new()
                    {
                        new Claim(ClaimTypes.Name, account.Username),
                        new Claim(ClaimTypes.Role, account.Role)
                    };

            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_configuration.GetSection("JwtSettings:Key").Value));

            SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha512Signature);

            JwtSecurityToken token = new(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            string jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return "Bearer " + jwt;
        }
    }
}
