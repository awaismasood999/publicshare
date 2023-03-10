using CustomerProducts.Core.Entities; 

namespace CustomerProducts.Core.Services;

public interface ICustomerProductsRepository
{
    Task<IEnumerable<Product>> GetProductsAsync(Guid productId);
    Task<Product> GetProductAsync(Guid customerId, Guid productId);
    void AddProduct(Guid customerId, Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(Product product);
    Task<IEnumerable<Customer>> GetCustomersAsync();
    Task<Customer> GetCustomerAsync(Guid customerId, bool includeProductDeatil = false);
    Task<IEnumerable<Customer>> GetCustomersAsync(IEnumerable<Guid> customerIds);
    void AddCustomer(Customer customer);
    void DeleteCustomer(Customer customer);
    void UpdateCustomer(Customer customer);
    Task<bool> CustomerExistsAsync(Guid customerId);
    Task<bool> SaveAsync();
}

