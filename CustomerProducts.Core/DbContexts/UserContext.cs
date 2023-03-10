using CustomerProducts.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CustomerProducts.Core.DbContexts;

public class UserContext : DbContext
{
    public UserContext(DbContextOptions<UserContext> options)
       : base(options)
    {
    }

    // base DbContext constructor ensures that Books and Authors are not null after
    // having been constructed.  Compiler warning ("uninitialized non-nullable property")
    // can safely be ignored with the "null-forgiving operator" (= null!)

    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      
        modelBuilder.Entity<User>().HasData(
            new User("test", "user", "superuser", "password"){
              Id  = Guid.Parse("c2ba458e-ce25-484b-9830-c1b93118ad5f")
            }
            ) ;

        base.OnModelCreating(modelBuilder);
    }
}


