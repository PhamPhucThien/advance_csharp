using advance_csharp.dto.Response.Orders;
using advance_csharp.service.Interfaces;
using advance_csharp.service.Services;

namespace advance_csharp.test.OrderTests
{
    [TestClass]
    public class GetOrderTest
    {
        private readonly IOrderService _OrderService;

        public GetOrderTest()
        {
            _OrderService = new OrderService();
        }

        [TestMethod]
        public async Task GetOrderForUser()
        {
            string username = "User";
            ResponseGetOrder response = await _OrderService.Get(username);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Data.Count >= 0);
        }
    }
}