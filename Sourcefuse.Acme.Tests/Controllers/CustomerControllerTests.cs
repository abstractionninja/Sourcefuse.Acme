using Microsoft.AspNetCore.Mvc;
using Sourcefuse.Acme.Common.Interfaces;
using Sourcefuse.Acme.Tests;
using Sourcefuse.Acme.WebApi.Models;

namespace Sourcefuse.Acme.WebApi.Controllers.Tests
{
    [TestClass()]
    public class CustomerControllerTests
    {
        private readonly TestRepoService _repoService = new();

        private readonly CustomerController _customerController;
        public CustomerControllerTests() 
        { 

            _customerController = new CustomerController(_repoService);
        }

        private static TResult TestActionResultOk<TResult>(IActionResult actionresult)
        {
            Assert.IsInstanceOfType(actionresult, typeof(OkObjectResult));
            var result = (OkObjectResult)actionresult;

            Assert.IsTrue(result.StatusCode == 200);

            Assert.IsNotNull(result.Value);

            return (TResult)result.Value;  
        }

        [TestMethod()]
        public async Task GetTest()
        {
            var results = await _customerController.Get();
            var customers = await _repoService.GetCustomers();

            Assert.IsTrue(!results.Except(customers).Any() && !customers.Except(results).Any());
        }

        [TestMethod()]
        public async Task GetById()
        {

            var customer = await _repoService.AddCustomer(new TestCustomer
            {
                FirstName = "Hank",
                LastName = "IsCool",
                Email = "hank@abstraction.ninja"
            });

            var actionresult = await _customerController.Get(customer.Id);

            var value = TestActionResultOk<ICustomer>(actionresult);

            Assert.IsTrue(value.Id.Equals(customer.Id ) && value.Email.Equals(customer.Email));

        }

        [TestMethod()]
        public async Task PostTest()
        {
            var actionresult = await _customerController.Post(new CustomerModel
            {
                FirstName = "Hank",
                LastName = "Post Test",
                Email = "hank@post.test"
            });

            var value = TestActionResultOk<ICustomer>(actionresult);

            var customer = await _repoService.GetCustomer(value.Id);

            Assert.IsNotNull(customer);
        }

        [TestMethod()]
        public async Task UpdateTest()
        {
            var customer = await _repoService.AddCustomer(new TestCustomer
            {
                FirstName = "Hank",
                LastName = "Update Test",
                Email = "hank@update.test"
            });

            var model = new CustomerModel {
                FirstName = "Hank",
                LastName = "Updated",
                Email = "hank@update.test"
            };

            var actionresult = await _customerController.Update(model, customer.Id);

            var result = TestActionResultOk<ICustomer>(actionresult);

            Assert.IsNotNull(result);

            Assert.IsTrue(result.LastName.Equals("Updated"));
        }
    }
}