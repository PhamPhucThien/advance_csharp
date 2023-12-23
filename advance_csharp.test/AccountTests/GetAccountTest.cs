using advance_csharp.dto.Response.Accounts;
using advance_csharp.service.Interfaces;
using advance_csharp.service.Services;

namespace advance_csharp.test.AccountTests
{
    [TestClass]
    public class GetAccountTest
    {
        private readonly IAccountService _AccountService;

        public GetAccountTest()
        {
            _AccountService = new AccountService();
        }

        [TestMethod]
        public async Task GetAccountInfoForUser()
        {
            string username = "User";
            ResponseGetAccount response = await _AccountService.Get(username);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Username.Equals(username));
        }

        [TestMethod]
        public async Task GetAccountById()
        {
            Guid id = new("887E4ABE-5C9C-492A-B813-77C8F7E0A52B");
            ResponseGetAccountById response = await _AccountService.GetById(id);
            Assert.IsNotNull(response);
        }
    }
}