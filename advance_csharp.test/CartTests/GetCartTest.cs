using advance_csharp.dto.Response.Carts;
using advance_csharp.service.Interfaces;
using advance_csharp.service.Services;

namespace advance_csharp.test.CartTests
{
    [TestClass]
    public class GetCartTest
    {
        private readonly ICartService _CartService;

        public GetCartTest()
        {
            _CartService = new CartService();
        }

        [TestMethod]
        public async Task GetCartForUser()
        {
            string username = "User";
            ResponseGetCart response = await _CartService.Get(username);
            Assert.IsNotNull(response);
        }
    }
}