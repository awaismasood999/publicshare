using CustomerProducts.Core.DbContexts;

namespace CutomerProjects.Core.UnitTests.Mocks
{
    public class CustomerProductsContextMocks
    {

        private static Mock<ICustomerProductsContext> _mockCustomerProductsContext = new Mock<ICustomerProductsContext>();
        public static Mock<ICustomerProductsContext> GetCustomerProductsDbContext()
        {
            var customers = new List<Customer>
            {
                new Customer
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Test 1",
                    LastName = "Last Name 1",
                    Address = "Bradford",
                    Products = new List<Product>()
                },
            };

            

            var mockCustomerProductsContext = new Mock<ICustomerProductsContext>();
            mockCustomerProductsContext.Setup(x => x.Customers).ReturnsDbSet(customers);
            return mockCustomerProductsContext;
        }
        
        public static Mock<ICustomerProductsContext> AddCustomerProductsDbContext(Customer customer)
        {
            _mockCustomerProductsContext.Setup(x => x.Customers.Add(customer));
            return _mockCustomerProductsContext;
        }

        public static bool VerifyAdd(Customer customer)
        {
            
            _mockCustomerProductsContext.Verify(x => x.Customers.Add(customer), Times.Once);

            return true;
            
        }
    }
}
