using Application.Domain.AggregatesModels;
using Application.ViewModels;
using AutoMapper;

namespace Application.Mapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductRequest, Product>()
                .ForMember(src => src.Id, opts => opts.Ignore())
                .ForMember(src => src.Valid, opts => opts.Ignore());

            CreateMap<Product, ProductResponse>();
        }
    }
}