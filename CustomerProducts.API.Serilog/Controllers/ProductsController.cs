
using AutoMapper;
using CustomerProducts.API.Models;
using CustomerProducts.Core.Entities;
using CustomerProducts.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomerProducts.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/customer/{customerId}/products")]
[ApiVersion("2.0")]
[Authorize]
public class ProductsController : ControllerBase
{
    private readonly ICustomerProductsRepository _customerProductRepository;
    private readonly IMapper _mapper;

    public ProductsController(ICustomerProductsRepository customerProductRepository,
        IMapper mapper)
    {
        _customerProductRepository = customerProductRepository ??
            throw new ArgumentNullException(nameof(customerProductRepository));
        _mapper = mapper ??
            throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProdcutDto>>> GetProductsForCustomer(Guid customerId)
    {
        if (!await _customerProductRepository.CustomerExistsAsync(customerId))
        {
            return NotFound();
        }

        var productsForCustomerFromRepo = await _customerProductRepository.GetProductsAsync(customerId);
        return Ok(_mapper.Map<IEnumerable<ProdcutDto>>(productsForCustomerFromRepo));
    }

    [HttpGet("{productId}")]
    public async Task<ActionResult<ProdcutDto>> GeProductForCustomer(Guid customerId, Guid productId)
    {
        if (!await _customerProductRepository.CustomerExistsAsync(customerId))
        {
            return NotFound();
        }

        var productForCustomerFromRepo = await _customerProductRepository.GetProductAsync(customerId, productId);

        if (productForCustomerFromRepo == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<ProdcutDto>(productForCustomerFromRepo));
    }


    [HttpPost]
    public async Task<ActionResult<ProdcutDto>> CreateProductForCustomer(
            Guid customerId, ProductForCreationDto product)
    {
        if (!await _customerProductRepository.CustomerExistsAsync(customerId))
        {
            return NotFound();
        }

        var productEntity = _mapper.Map<Product>(product);
        _customerProductRepository.AddProduct(customerId, productEntity);
        await _customerProductRepository.SaveAsync();

        var productToReturn = _mapper.Map<ProdcutDto>(productEntity);
        return Ok(productToReturn);
    }


    [HttpPut("{productId}")]
    public async Task<IActionResult> UpdateProductForCustomer(Guid customerId,
      Guid productId,
      ProdcutDto prodcut)
    {
        if (!await _customerProductRepository.CustomerExistsAsync(customerId))
        {
            return NotFound();
        }

        var productForCustomerFromRepo = await _customerProductRepository.GetProductAsync(customerId, productId);

        if (productForCustomerFromRepo == null)
        {
            return NotFound();
        }

        _mapper.Map(prodcut, productForCustomerFromRepo);

        _customerProductRepository.UpdateProduct(productForCustomerFromRepo);

        await _customerProductRepository.SaveAsync();

        return NoContent();
    }

    [HttpDelete("{productId}")]
    public async Task<ActionResult> DeleteProductForCustomer(Guid customerId, Guid productId)
    {
        if (!await _customerProductRepository.CustomerExistsAsync(customerId))
        {
            return NotFound();
        }

        var productForCustomerFromRepo = await _customerProductRepository.GetProductAsync(customerId, productId);

        if (productForCustomerFromRepo == null)
        {
            return NotFound();
        }

        _customerProductRepository.DeleteProduct(productForCustomerFromRepo);

        await _customerProductRepository.SaveAsync();

        return NoContent();
    }

}