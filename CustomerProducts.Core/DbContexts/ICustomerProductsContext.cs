using CustomerProducts.Core.Entities;
using Microsoft.EntityFrameworkCore;


namespace CustomerProducts.Core.DbContexts
{
    public interface ICustomerProductsContext
    {
        DbSet<Customer> Customers { get; set; }

        DbSet<Product> Products { get; set; }

        Task<int> SaveChangesAsync();
    }
}