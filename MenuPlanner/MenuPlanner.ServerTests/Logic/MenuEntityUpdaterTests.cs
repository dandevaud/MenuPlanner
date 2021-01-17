// <copyright file="MenuEntityUpdaterTest.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MenuPlanner.Server.Logic;
using MenuPlanner.Shared.models;
using MenuPlanner.Shared.models.enums;
using NUnit.Framework;

namespace MenuPlanner.ServerTests.Logic
{
    [TestFixture]
    class MenuEntityUpdaterTest
    {

        private MenuEntityUpdater controller;
        private MockDbSetUp mockDb;

        [SetUp]
        public void SetUp()
        {
            mockDb = new MockDbSetUp();
            controller = new MenuEntityUpdater(mockDb.SetUpDBContext().Object);
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
            Menu menu = new Menu() { MenuId = existingMenu.MenuId, Name = newMenuName };

            await controller.UpdateMenuInContext(menu, existingMenu);

            Assert.AreEqual(menu.MenuId, existingMenu.MenuId);
            Assert.AreEqual(mockDb.Menus.Find(i => i.MenuId == menu.MenuId).Name, newMenuName);
        }

        /// <summary>
        /// Checks if adding a menu ingredient to the menu works
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

            await controller.UpdateMenuInContext(menu, mockDb.Menus.Find(i => i.Name.Equals("Pouletbrüstli")));

            expected.ForEach(i => Assert.Contains(i, menu.Ingredients.ToList()));
            Assert.IsEmpty(menu.Ingredients.ToList().FindAll(i => notExpected.Contains(i)));
        }

    }
}
