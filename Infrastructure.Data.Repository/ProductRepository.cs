using Application.Domain.AggregatesModels;
using Application.Infrastructure.Data.Context;
using Domain.Interfaces.Repositories;
using Infrastructure.Data.Repository.Bases;

namespace Infrastructure.Data.Repository
{
    public class ProductRepository : ReadWriteRepository<Product>, IProductRepository
    {
        public ProductRepository(MyContext context) : base(context)
        {
        }
    }
}