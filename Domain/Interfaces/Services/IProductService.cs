using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Domain.AggregatesModels;

namespace Domain.Interfaces
{
    public interface IProductService
    {
        Task<Product> GetProduct(int id, CancellationToken cancellationToken);

        Task<IEnumerable<Product>> GetProducts(CancellationToken cancellationToken);

        Task<Product> InsertProduct(Product product, CancellationToken cancellationToken);

        Task<Product> UpdateProduct(int id, Product product, CancellationToken cancellationToken);

        Task DeleteProduct(int id, CancellationToken cancellationToken);
    }
}