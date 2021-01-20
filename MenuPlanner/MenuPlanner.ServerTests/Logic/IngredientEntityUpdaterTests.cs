// <copyright file="IngredientEntityUpdaterTests.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Threading.Tasks;
using MenuPlanner.Server.Logic;
using MenuPlanner.Shared.models;
using NUnit.Framework;

namespace MenuPlanner.ServerTests.Logic
{
    /// <summary>Class to test the Ingredient Entity Updater Logic</summary>
    [TestFixture]
    public class IngredientEntityUpdaterTests
    {
        private IngredientEntityUpdater controller;
        private MockDbSetUp mockDb;

        [SetUp]
        public void SetUp()
        {
            mockDb = new MockDbSetUp();
            controller = new IngredientEntityUpdater(mockDb.SetUpDBContext().Object);
        }

        /// <summary>
        /// Checks if ingredient exists and update or add test using a duplicate.
        /// </summary>
        //[Test]
        //Mock DB Context Entity not working --> Test not possible
        public async Task CheckIfIngredientExistsAndUpdateOrAddTestDuplicate()
        {
            Guid newGuid = Guid.NewGuid();
            Ingredient pouletDuplicate = new Ingredient() { IngredientId = newGuid, Name = "Poulet" };

            await controller.CheckIfIngredientExistsAndUpdateOrAdd(pouletDuplicate);

            Assert.AreNotEqual(pouletDuplicate.IngredientId, newGuid);
            Assert.AreEqual(mockDb.Ingredients.Find(i => i.Name.Equals("Poulet")).IngredientId, pouletDuplicate.IngredientId);
        }

        /// <summary>
        /// Checks if ingredient exists and update or add test Using a new ingredient.
        /// </summary>
        //[Test]
        //Mock DB Context Entity not working --> Test not possible
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
