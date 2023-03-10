using CustomerProducts.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerProducts.Core.DbContexts;

public class CustomerProductsContext : DbContext, ICustomerProductsContext
{
    public CustomerProductsContext(DbContextOptions<CustomerProductsContext> options)
       : base(options)
    {
    }

    // base DbContext constructor ensures that Books and Authors are not null after
    // having been constructed.  Compiler warning ("uninitialized non-nullable property")
    // can safely be ignored with the "null-forgiving operator" (= null!)

    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;

    public Task<int> SaveChangesAsync()
    {
       return base.SaveChangesAsync();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // seed the database with dummy data
        modelBuilder.Entity<Customer>().HasData(
            new Customer("Abdullah", "Awais", "Bradford")
            {
                Id = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35")
            },
            new Customer("Abulhadi", "Ali", "Leeds")
            {
                Id = Guid.Parse("da2fd609-d754-4feb-8acd-c4f9ff13ba96")
            },
            new Customer("Shahzad", "Ahmed", "Manchester")
            {
                Id = Guid.Parse("2902b665-1190-4c70-9915-b9c2d7680450")
            },
            new Customer("Ali", "Umair", "Leeds")
            {
                Id = Guid.Parse("102b566b-ba1f-404c-b2df-e2cde39ade09")
            },
            new Customer("Sumair", "Rana", "London")
            {
                Id = Guid.Parse("5b3621c0-7b12-4e80-9c8b-3398cba7ee05")
            },
            new Customer("Junaid", "Masood", "Hull")
            {
                Id = Guid.Parse("2aadd2df-7caf-45ab-9355-7f6332985a87")
            },
            new Customer("Saleem", "Anwar", "Manchester")
            {
                Id = Guid.Parse("2ee49fe3-edf2-4f91-8409-3eb25ce6ca51")
            }
            );

        modelBuilder.Entity<Product>().HasData(
           new Product("HP Laptop")
           {
               Id = Guid.Parse("5b1c2b4d-48c7-402a-80c3-cc796ad49c6b"),
               CustomerId = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
               Description = "HP Laptop 16 GB memory with 500GB hard disk"
           },
           new Product("Keyboard")
           {
               Id = Guid.Parse("d8663e5e-7494-4f81-8739-6e0de1bea7ee"),
               CustomerId = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
               Description = "Microsoft."
           },
           new Product("Headset")
           {
               Id = Guid.Parse("d173e20d-159e-4127-9ce9-b0ac2564ad97"),
               CustomerId = Guid.Parse("da2fd609-d754-4feb-8acd-c4f9ff13ba96"),
               Description = "Plantronic."
           },
           new Product("Asus Laptop")
           {
               Id = Guid.Parse("40ff5488-fdab-45b5-bc3a-14302d59869a"),
               CustomerId = Guid.Parse("2902b665-1190-4c70-9915-b9c2d7680450"),
               Description = "Asus Laptop 32 GB memory with 500GB hard disk."
           }
           );

        //// fix to allow sorting on DateTimeOffset when using Sqlite, based on
        //// https://blog.dangl.me/archive/handling-datetimeoffset-in-sqlite-with-entity-framework-core/
        //if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
        //{
        //    // Sqlite does not have proper support for DateTimeOffset via Entity Framework Core, see the limitations
        //    // here: https://docs.microsoft.com/en-us/ef/core/providers/sqlite/limitations#query-limitations
        //    // To work around this, when the Sqlite database provider is used, all model properties of type DateTimeOffset
        //    // use the DateTimeOffsetToBinaryConverter
        //    // Based on: https://github.com/aspnet/EntityFrameworkCore/issues/10784#issuecomment-415769754 
        //    foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        //    {
        //        var properties = entityType.ClrType.GetProperties()
        //            .Where(p => p.PropertyType == typeof(DateTimeOffset)
        //                || p.PropertyType == typeof(DateTimeOffset?));
        //        foreach (var property in properties)
        //        {
        //            modelBuilder.Entity(entityType.Name)
        //                .Property(property.Name)
        //                .HasConversion(new DateTimeOffsetToBinaryConverter());
        //        }
        //    }
        //}

        base.OnModelCreating(modelBuilder);
    }
}


