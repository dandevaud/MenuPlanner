using MenuPlanner.Server.Controllers;
using NUnit.Framework;
using MenuPlanner.ServerTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenuPlanner.Server.SqlImplementation;
using MenuPlanner.Shared.models;
using Microsoft.EntityFrameworkCore;
using Moq;


namespace MenuPlanner.ServerTests.Controllers
{
    [TestFixture]
    public class IngredientsControllerTests
    {
        Mock<MenuPlannerContext> menuPlannerMock = new Mock<MenuPlannerContext>();
        private IngredientsController controller;
        [SetUp]
        public void SetUp()
        {
            //https://stackoverflow.com/questions/51384331/mocking-dbcontext-in-ef-6-not-working-as-expected
            //https://docs.microsoft.com/en-us/ef/ef6/fundamentals/testing/mocking
            var ingredients = new List<Ingredient>()
            {
                new Ingredient() {IngredientId = Guid.NewGuid(), Name = "Poulet"},
                new Ingredient() {IngredientId = Guid.NewGuid(), Name = "Rind"}
            };

            var mockDbSet = DbContextMock.GetQueryableMockDbSet(ingredients);
            menuPlannerMock.Setup(m => m.Ingredients).Returns(mockDbSet);
            
            controller = new IngredientsController(menuPlannerMock.Object);
        }

        [Test]
        public void CheckIfIngredientExistsAndUpdateOrAddTest()
        {
            Guid newGuid = Guid.NewGuid();
            Ingredient pouletDuplicate = new Ingredient() {IngredientId = newGuid, Name = "Poulet"};
            controller.CheckIfIngredientExistsAndUpdateOrAdd(pouletDuplicate);
            Assert.AreNotEqual(pouletDuplicate.IngredientId,newGuid);
            Assert.Pass();
         }
    }
}
