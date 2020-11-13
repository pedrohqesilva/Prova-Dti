using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Domain.AggregatesModels;
using Application.ViewModels;
using AutoMapper;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ProvaDti.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public ProductsController(
            ILogger<ProductsController> logger,
            IMapper mapper,
            IProductService productService)
        {
            _logger = logger;
            _mapper = mapper;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
        {
            var products = await _productService.GetProducts(cancellationToken);
            var result = _mapper.Map<IEnumerable<ProductResponse>>(products);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id, CancellationToken cancellationToken)
        {
            var product = await _productService.GetProduct(id, cancellationToken);
            var result = _mapper.Map<ProductResponse>(product);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> InsertProduct(ProductRequest productRequest, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(productRequest);
            product = await _productService.InsertProduct(product, cancellationToken);
            var result = _mapper.Map<ProductResponse>(product);
            return CreatedAtAction("GetProduct", new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductRequest productRequest, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(productRequest);
            product = await _productService.UpdateProduct(id, product, cancellationToken);
            var result = _mapper.Map<ProductResponse>(product);
            return CreatedAtAction("GetProduct", new { id = result.Id }, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id, CancellationToken cancellationToken)
        {
            await _productService.DeleteProduct(id, cancellationToken);
            return NoContent();
        }
    }
}