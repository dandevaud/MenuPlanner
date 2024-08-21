// <copyright file="MenuEntityUpdaterTests.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MenuPlanner.Server.Logic.EntityUpdater;
using MenuPlanner.Shared.models;
using MenuPlanner.Shared.models.enums;
using NUnit.Framework;

namespace MenuPlanner.ServerTests.Logic
{
    /// <summary>Class to test the Menu Entity Updater Logic</summary>
    [TestFixture]
    class MenuEntityUpdaterTest
    {

        private MenuEntityUpdater controller;
        private MockDbSetUp mockDb;

        [SetUp]
        public void SetUp()
        {
            mockDb = new MockDbSetUp();

            // controller = new MenuEntityUpdater(mockDb.SetUpDBContext().Object, new PictureHandler());
        }

        /// <summary>
        /// Checks if a menu exists and checks if the Name change works fine
        /// </summary>
        //[Test]
        //Mock DB Context Entity not working --> Test not possible
        public async Task UpdateMenuNameTest()
        {
            var newMenuName = "New Name";
            Menu existingMenu = mockDb.Menus.Find(i => i.Name.Equals("Old Name"));
            Menu menu = new Menu() { Id = existingMenu.Id, Name = newMenuName };

            await controller.UpdateMenuInContext(menu);

            Assert.That(menu.Id, Is.EqualTo(existingMenu.Id));
            Assert.That(mockDb.Menus.Find(i => i.Id == menu.Id).Name, Is.EqualTo(newMenuName));
        }

        /// <summary>
        /// Checks if adding a menu ingredient to the menu works and if the menu ingredient exists in the DB
        /// </summary>
        //[Test]
        //Mock DB Context Entity not working --> Test not possible
        public async Task UpdateMenuIngredientTest()
        {
            var expected = new List<MenuIngredient>();
            expected.AddRange(mockDb.Menus.Find(i => i.Name.Equals("Pouletbrüstli")).Ingredients);

            Menu menu = mockDb.Menus.Find(i => i.Name.Equals("Pouletbrüstli"));

            MenuIngredient menuIngredient = new MenuIngredient()
            {
                Id = Guid.NewGuid(),
                Quantity = new Quantity()
                {
                    QuantityValue = 1d,
                    Unit = UnitEnum.kg
                },
                Ingredient = mockDb.Ingredients.Find(i => i.Name.Equals("Öl")),
                Menu = menu
            };
            menu.Ingredients.Add(menuIngredient);
            expected.Add(menuIngredient);
            var notExpected = mockDb.MenuIngredients
                .Where(i => i.Ingredient.Name.Equals("Rind"))
                .ToList();

            await controller.UpdateMenuInContext(menu);

            var foundMenuIngredient = mockDb.MenuIngredients.Find(i => i.Id == menuIngredient.Id);

            expected.ForEach(i => Assert.That(i, Contains.Value(menu.Ingredients.ToList())));
            Assert.That(menu.Ingredients.ToList().FindAll(i => notExpected.Contains(i)), Is.Empty);
            Assert.That(foundMenuIngredient, Is.Not.Null);
        }

    }
}
