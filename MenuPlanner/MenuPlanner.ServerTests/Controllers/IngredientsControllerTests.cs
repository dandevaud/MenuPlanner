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
        private List<Ingredient> ingredients = new List<Ingredient>()
        {
            new Ingredient() {IngredientId = Guid.NewGuid(), Name = "Poulet"},
            new Ingredient() {IngredientId = Guid.NewGuid(), Name = "Rind"}
        };
        [SetUp]
        public void SetUp()
        {
            //https://stackoverflow.com/questions/51384331/mocking-dbcontext-in-ef-6-not-working-as-expected
            //https://docs.microsoft.com/en-us/ef/ef6/fundamentals/testing/mocking
           
            var mockDbSet = DbContextMock.GetQueryableMockDbSet(ingredients);
            menuPlannerMock.Setup(c => c.Ingredients).Returns(mockDbSet.Object);
            
            controller = new IngredientsController(menuPlannerMock.Object);
        }

        /// <summary>
        /// Checks if ingredient exists and update or add test using a duplicate.
        /// </summary>
        [Test]
        public async Task CheckIfIngredientExistsAndUpdateOrAddTestDuplicate()
        {
            Guid newGuid = Guid.NewGuid();
            Ingredient pouletDuplicate = new Ingredient() {IngredientId = newGuid, Name = "Poulet"};
           
                await controller.CheckIfIngredientExistsAndUpdateOrAdd(pouletDuplicate);
            

            Assert.AreNotEqual(pouletDuplicate.IngredientId,newGuid);
            Assert.AreEqual(ingredients[0].IngredientId,pouletDuplicate.IngredientId);
        }

        /// <summary>
        /// Checks if ingredient exists and update or add test Using a new ingredient.
        /// </summary>
        [Test]
        public async Task CheckIfIngredientExistsAndUpdateOrAddTestNewIngredient()
        {
            Guid newGuid = Guid.NewGuid();
            Ingredient poulet2 = new Ingredient() { IngredientId = newGuid, Name = "Poulet2" };
            
                await controller.CheckIfIngredientExistsAndUpdateOrAdd(poulet2);
            

             Assert.AreEqual(poulet2.IngredientId, newGuid);
             ingredients.ForEach(i => Assert.AreNotEqual(i.IngredientId, poulet2.IngredientId));
        }
    }
}
