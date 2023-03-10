
using AutoMapper;
using CustomerProducts.API.Models;
using CustomerProducts.Core.Services;
using CustomerProducts.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomerProducts.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/customers")]
[ApiVersion("1.0")]
[Authorize]
public class CustomersController : ControllerBase
{
    private readonly ICustomerProductsRepository _customerProductRepository;
    private readonly IMapper _mapper;

    public CustomersController(
        ICustomerProductsRepository customerProductRepository,
        IMapper mapper)
    {
        _customerProductRepository = customerProductRepository ??
            throw new ArgumentNullException(nameof(customerProductRepository));
        _mapper = mapper ??
            throw new ArgumentNullException(nameof(mapper));
    }

    /// <summary>
    /// Get all customers
    /// </summary>
    /// <returns>All customers</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomers()
    {
        // get customers from repo
        var customersFromRepo = await _customerProductRepository
            .GetCustomersAsync();

        // return them
        return Ok(_mapper.Map<IEnumerable<CustomerDto>>(customersFromRepo));
    }

    /// <summary>
    /// Get single customer
    /// </summary>
    /// <param name="customerId"></param>
    /// <returns>Customer detail</returns>

    [HttpGet("{customerId}", Name = "GetCustomer")]
    public async Task<ActionResult<CustomerDto>> GetCustomer(Guid customerId)
    {
        // get customer from repo
        var customerFromRepo = await _customerProductRepository.GetCustomerAsync(customerId);

        if (customerFromRepo == null)
        {
            return NotFound();
        }

        // return customer
        return Ok(_mapper.Map<CustomerDto>(customerFromRepo));
    }

    //[HttpGet("t/{customerId}", Name = "GetCustomerv2")]
    //[ApiVersion("2.0")]
    //public async Task<ActionResult<CustomerDto>> GetCustomerv2(Guid customerId, bool includeProductDetail)
    //{
    //    // get customer from repo
    //    var customerFromRepo = await _customerProductRepository.GetCustomerAsync(customerId, includeProductDetail);

    //    if (customerFromRepo == null)
    //    {
    //        return NotFound();
    //    }

    //    // return customer
    //    return Ok(_mapper.Map<CustomerDto>(customerFromRepo));
    //}

    /// <summary>
    /// Create customer
    /// </summary>
    /// <param name="customer"></param>
    /// <returns>create new customer</returns>
    [HttpPost("createcustomer",Name = "CreateCustomer")]
    public async Task<ActionResult<CustomerDto>> CreateCustomer(CustomerForCreationDto customer)
    {
        var customerEntity = _mapper.Map<Customer>(customer);

        _customerProductRepository.AddCustomer(customerEntity);
        await _customerProductRepository.SaveAsync();

        var customerToReturn = _mapper.Map<CustomerDto>(customerEntity);

        return CreatedAtRoute("GetCustomer",
            new { customerId = customerToReturn.Id },
            customerToReturn);
    }
}
