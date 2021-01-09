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
        private IngredientsController controller;
        private MockDbSetUp mockDb;
        
        [SetUp]
        public void SetUp()
        {
            mockDb = new MockDbSetUp();
           controller = new IngredientsController(mockDb.SetUpDBContext().Object);
          

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
            Assert.AreEqual(mockDb.Ingredients.Find(i => i.Name.Equals("Poulet")),pouletDuplicate.IngredientId);
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
             mockDb.Ingredients.ForEach(i => Assert.AreNotEqual(i.IngredientId, poulet2.IngredientId));
        }
    }
}
