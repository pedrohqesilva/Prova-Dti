using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Domain.AggregatesModels;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Domain.Specifications;
using Domain.Specifications.Bases;

namespace Application.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> GetProduct(int id, CancellationToken cancellationToken)
        {
            var spec = SpecificationBuilder<Product>.Create()
                .WithKey(id)
                .Valid();

            var result = await _productRepository.FirstOrDefaultAsync(spec, cancellationToken);
            return result;
        }

        public async Task<IEnumerable<Product>> GetProducts(CancellationToken cancellationToken)
        {
            var spec = SpecificationBuilder<Product>.Create()
                .Valid();

            var result = await _productRepository.SearchAsync(spec, cancellationToken);
            return result;
        }

        public async Task<Product> InsertProduct(Product product, CancellationToken cancellationToken)
        {
            var result = _productRepository.Add(product);
            await _productRepository.SaveChanges(cancellationToken);
            return result;
        }

        public async Task<Product> UpdateProduct(int id, Product product, CancellationToken cancellationToken)
        {
            var oldProduct = await GetProduct(id, cancellationToken);

            oldProduct.Name = product.Name;
            oldProduct.Amount = product.Amount;
            oldProduct.UnitPrice = product.UnitPrice;

            await _productRepository.SaveChanges(cancellationToken);
            return oldProduct;
        }

        public async Task DeleteProduct(int id, CancellationToken cancellationToken)
        {
            var product = await GetProduct(id, cancellationToken);
            _productRepository.Remove(product);
            await _productRepository.SaveChanges(cancellationToken);
        }
    }
}