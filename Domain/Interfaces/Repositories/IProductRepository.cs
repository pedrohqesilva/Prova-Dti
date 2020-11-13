using Application.Domain.AggregatesModels;
using Domain.Interfaces.Repositories.Bases;

namespace Domain.Interfaces.Repositories
{
    public interface IProductRepository : IReadWriteRepository<Product>
    {
    }
}