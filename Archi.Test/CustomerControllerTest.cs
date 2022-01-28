using Archi.Test.Models;
using System;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArchiAPI.Controllers;
using ArchiLibrary.Models;
using ArchiLibrary.Filter;
using Archi.Test.MockDbContext;
using Assert = NUnit.Framework.Assert;

namespace Archi.Test
{
    public class CustomerControllerTest
    {
        private MockDbContexte _dbContext;
        private PizzasController _pizzaModel;
        private CustomersController _customerModel;


        [SetUp]
        public void Test()
        {
            // le nouveau dbContext que l'on utilise pour le test
            _dbContext = MockDbContexte.GetDbContexte();
            _pizzaModel = new PizzasController(_dbContext);
            _customerModel = new CustomersController(_dbContext);
        }
        [Test]
        public async Task Test1()
        {
            var Actionresult = await _customerModel.GetAll(new Params());
            var result = Actionresult as ObjectResult;
            var values = ((IEnumerable<object>)(result).Value);

            // assertiions : test si les deux objets sont égaux 
            Assert.AreEqual((int)System.Net.HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(_dbContext.Customers.Count(), values.Count());
        }

        // test2
        [Test]
        public async Task Test2()
        {
            var actionResult = await _pizzaModel.GetAll(new Params());
            var result = actionResult.Result as ObjectResult;
            var values = (IEnumerable<object>)(result).Value;
            Assert.AreEqual((int)System.Net.HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(_dbContext.Pizzas.Count(), values.Count());
        }
    }
}
