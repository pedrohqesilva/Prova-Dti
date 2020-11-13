using Application.Domain.AggregatesModels;
using Domain.Interfaces.Repositories.Bases;

namespace Domain.Specifications
{
    public static class ProductSpecificationExtensions
    {
        public static ISpecification<Product> WithKey(this ISpecification<Product> spec, int id)
        {
            return spec.And(x => x.Id == id);
        }

        public static ISpecification<Product> Valid(this ISpecification<Product> spec)
        {
            return spec.And(x => x.Valid);
        }
    }
}