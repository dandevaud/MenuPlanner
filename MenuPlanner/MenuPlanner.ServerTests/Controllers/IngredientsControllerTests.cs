using MenuPlanner.Server.Controllers;
using NUnit.Framework;
using MenuPlanner.ServerTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenuPlanner.Shared.models;
using Moq;

namespace MenuPlanner.ServerTests.Controllers
{
    [TestFixture()]
    public class IngredientsControllerTests
    {
        public void SetUp()
        {
            //https://stackoverflow.com/questions/51384331/mocking-dbcontext-in-ef-6-not-working-as-expected
            var ingredients = new List<Ingredient>()
            {
                new Ingredient() {IngredientId = Guid.NewGuid(), Name = "Poulet"},
                new Ingredient() {IngredientId = Guid.NewGuid(), Name = "Rind"}
            }.AsQueryable();
            
        }

        [Test()]
        public void CheckIfIngredientExistsAndUpdateOrAddTest()
        {
            Assert.Fail();
        }
    }
}
