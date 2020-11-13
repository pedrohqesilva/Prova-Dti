using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Domain.AggregatesModels;
using Application.Infrastructure.Data.Context;
using Domain.Interfaces;
using Xunit;

namespace Tests.Services
{
    public class ProductServiceTest
    {
        private readonly MyContext _context;
        private readonly IProductService _productService;

        private CancellationToken cancellationToken = CancellationToken.None;

        public ProductServiceTest(
            MyContext context,
            IProductService productService)
        {
            _context = context;
            _productService = productService;
        }

        private async Task Mock()
        {
            await _context.Database.EnsureDeletedAsync();

            var product1 = new Product { Id = 1, Name = "Product1", Amount = 10, UnitPrice = 19.90, Valid = true };
            var product2 = new Product { Id = 2, Name = "Product2", Amount = 20, UnitPrice = 29.90, Valid = true };
            var product3 = new Product { Id = 3, Name = "Product3", Amount = 30, UnitPrice = 39.90, Valid = true };
            var product4 = new Product { Id = 4, Name = "Product4", Amount = 40, UnitPrice = 49.90, Valid = false };

            await _context.Products.AddAsync(product1, cancellationToken);
            await _context.Products.AddAsync(product2, cancellationToken);
            await _context.Products.AddAsync(product3, cancellationToken);
            await _context.Products.AddAsync(product4, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        [Fact]
        public async void Get_should_return_all_products()
        {
            await Mock();

            var products = await _productService.GetProducts(cancellationToken);

            Assert.NotNull(products);
            Assert.Equal(3, products.Count());
        }

        [Fact]
        public async void Specific_get_should_return_the_product()
        {
            await Mock();

            var product = await _productService.GetProduct(2, cancellationToken);

            Assert.NotNull(product);
            Assert.Equal(2, product.Id);
            Assert.Equal("Product2", product.Name);
            Assert.Equal(20, product.Amount);
            Assert.Equal(29.90, product.UnitPrice);
            Assert.True(product.Valid);
        }

        [Fact]
        public async void Specific_get_should_not_return_a_invalid_product()
        {
            await Mock();

            var product = await _productService.GetProduct(4, cancellationToken);

            Assert.Null(product);
        }

        [Fact]
        public async void Product_should_be_added()
        {
            await Mock();

            var product = new Product { Name = "Product5", Amount = 50, UnitPrice = 59.90 };
            var result = await _productService.InsertProduct(product, cancellationToken);

            Assert.NotNull(product);
            Assert.Equal(5, result.Id);
            Assert.Equal("Product5", result.Name);
            Assert.Equal(50, result.Amount);
            Assert.Equal(59.90, result.UnitPrice);
            Assert.True(result.Valid);
        }

        [Fact]
        public async void Product_should_be_updated()
        {
            await Mock();

            var product = new Product { Name = "Product1 after Update", Amount = 101, UnitPrice = 191.90 };
            var result = await _productService.UpdateProduct(1, product, cancellationToken);

            Assert.NotNull(product);
            Assert.Equal(1, result.Id);
            Assert.Equal("Product1 after Update", result.Name);
            Assert.Equal(101, result.Amount);
            Assert.Equal(191.90, result.UnitPrice);
            Assert.True(result.Valid);
        }

        [Fact]
        public async void Product_should_be_deleted()
        {
            await Mock();

            var productsBeforeDelete = _context.Products.Where(w => w.Valid).ToList().Count;
            await _productService.DeleteProduct(1, cancellationToken);
            var productsAfterDelete = _context.Products.Where(w => w.Valid).ToList().Count;

            Assert.Equal(3, productsBeforeDelete);
            Assert.Equal(2, productsAfterDelete);
        }
    }
}