using advance_csharp.dto.Response.Products;
using advance_csharp.service.Interfaces;
using advance_csharp.service.Services;

namespace advance_csharp.test.ProductTests
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
            Guid id = new("5A58D7A6-09E9-4F87-984C-00010B110B7E");
            ResponseGetProductById response = await _ProductService.GetById(id);
            Assert.IsNotNull(response);
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