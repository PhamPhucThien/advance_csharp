using advance_csharp.dto.Response.Products;
using advance_csharp.service.Interfaces;
using advance_csharp.service.Services;

namespace advance_csharp.test
{
    [TestClass]
    public class GetProductTest
    {
        private readonly IProductService _ProductService;

        public GetProductTest()
        {
            _ProductService = new ProductService();
        }

        [TestMethod]
        public async Task GetProductById()
        {
            Guid id = new("A5267D4A-4030-4EA8-ACEA-00001AED5CA9");
            ResponseGetProductById response = await _ProductService.GetById(id);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Data.Id.Equals(id));
        }

        [TestMethod]
        public async Task GetProductBySearch()
        {
            string search = "Product";
            int page = 1;
            int size = 0;
            ResponseGetProductBySearch response = await _ProductService.GetBySearch(search, page, size);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Data.Count > 0);
        }
    }
}