using CustomerProducts.Core.DbContexts;
using CustomerProducts.Core.Services;
using CutomerProjects.Core.UnitTests.Mocks;
using System.Collections.Generic;
using System.Linq;

namespace CutomerProjects.Core.UnitTests
{
    public class CustomerProductsRepositoryUnitTests
    {
        private IMock<ICustomerProductsContext> _mockCustomerProductsContext;
        private ICustomerProductsRepository _customerProductsRepository;

        [SetUp]
        public void Setup()
        {
            _mockCustomerProductsContext = new Mock<ICustomerProductsContext>();
            
        }

        [Test]
        public void ShouldExpectACustomer()
        {
             // Init
            _mockCustomerProductsContext = CustomerProductsContextMocks.GetCustomerProductsDbContext();
            var dbContextObject = _mockCustomerProductsContext.Object;
            int expected = 1;

            // act
            _customerProductsRepository = new CustomerProductsRepository(null, dbContextObject);
            int actual = _customerProductsRepository.GetCustomersAsync().Result.Count();


            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void AddMockCustomerName()
        {
            // Init
            var customer = new Customer { FirstName = "" };
            _mockCustomerProductsContext = CustomerProductsContextMocks.AddCustomerProductsDbContext(customer);
            var dbContextObject = _mockCustomerProductsContext.Object;
           
            // act
            _customerProductsRepository = new CustomerProductsRepository(null, dbContextObject);
            _customerProductsRepository.AddCustomer(customer);

            // verify
            CustomerProductsContextMocks.VerifyAdd(customer);

        }
    }
}