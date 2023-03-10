using CustomerProducts.Core.DbContexts;
using CustomerProducts.Core.Entities; 
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CustomerProducts.Core.Services;

public class CustomerProductsRepository : ICustomerProductsRepository 
{
    private readonly ICustomerProductsContext _context;
    private readonly ILogger _logger;

    public CustomerProductsRepository(ILogger logger,ICustomerProductsContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger;
    }

    public void AddProduct(Guid customerId, Product product)
    {
        if (customerId == null)
        {
            throw new ArgumentNullException(nameof(customerId));
        }

        if (product == null)
        {
            throw new ArgumentNullException(nameof(product));
        }

        // always set the customerId to the passed-in customerId
        product.CustomerId = customerId;
        _context.Products.Add(product);
    }

    public void DeleteProduct(Product product)
    {
        _context.Products.Remove(product);
    }

    public async Task<Product> GetProductAsync(Guid customerId, Guid productId)
    {
        if (customerId == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(customerId));
        }

        if (productId == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(productId));
        }

        return await _context.Products
          .Where(c => c.CustomerId == customerId && c.Id == productId).FirstOrDefaultAsync();

    }

    public async Task<IEnumerable<Product>> GetProductsAsync(Guid customerId)
    {
        if (customerId == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(customerId));
        }

        return await _context.Products
                    .Where(c => c.CustomerId == customerId)
                    .OrderBy(c => c.Name).ToListAsync();
    }

    public void UpdateProduct(Product product)
    {
        throw new NotImplementedException();
    }

    public void AddCustomer(Customer customer)
    {
        if (customer == null)
        {
            throw new ArgumentNullException(nameof(customer));
        }

        // the repository fills the id (instead of using identity columns)
        customer.Id = Guid.NewGuid();

        foreach (var product in customer.Products)
        {
            product.Id = Guid.NewGuid();
        }

        _context.Customers.Add(customer);
    }

    public async Task<bool> CustomerExistsAsync(Guid customerId)
    {
        if (customerId == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(customerId));
        }

        return await _context.Customers.AnyAsync(a => a.Id == customerId);
    }

    public void DeleteCustomer(Customer customer)
    {
        if (customer == null)
        {
            throw new ArgumentNullException(nameof(customer));
        }

        _context.Customers.Remove(customer);
    }

    public async Task<Customer> GetCustomerAsync(Guid customerId, bool includeProductDetail = false )
    {
        _logger.Debug("Cusomer Id {cusomerId}", customerId);

        if (customerId == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(customerId));
        }

#pragma warning disable CS8603 // Possible null reference return.
        return includeProductDetail ? await _context.Customers.Include("Products").FirstOrDefaultAsync(a => a.Id == customerId) : await _context.Customers.FirstOrDefaultAsync(a => a.Id == customerId);
#pragma warning restore CS8603 // Possible null reference return.
    }

   
    public async Task<IEnumerable<Customer>> GetCustomersAsync()
    {
        return await _context.Customers.Include("Products").ToListAsync();
    }

    public async Task<IEnumerable<Customer>> GetCustomersAsync(IEnumerable<Guid> customerIds)
    {
        if (customerIds == null)
        {
            throw new ArgumentNullException(nameof(customerIds));
        }

        return await _context.Customers.Where(a => customerIds.Contains(a.Id))
            .OrderBy(a => a.FirstName)
            .OrderBy(a => a.LastName)
            .ToListAsync();
    }

    public void UpdateCustomer(Customer customer)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SaveAsync()
    {
        return await _context.SaveChangesAsync() >= 0;
    }
}

