// <copyright file="MenuEntityUpdaterTest.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System.Threading.Tasks;
using MenuPlanner.Server.Logic;
using MenuPlanner.Shared.models;
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
        [Test]
        //Mock DB Context Entity not working --> Test not possible
        public async Task UpdateMenuInContextTest()
        {
            var newMenuName = "Pouletbrüstli 2";
            Menu existingMenu = mockDb.Menus.Find(i => i.Name.Equals("Pouletbrüstli"));
            Menu menu = new Menu() { MenuId = existingMenu.MenuId, Name = "Pouletbrüstli 2" };

            await controller.UpdateMenuInContext(menu, existingMenu);

            Assert.AreEqual(menu.MenuId, existingMenu.MenuId);
            Assert.AreEqual(mockDb.Menus.Find(i => i.MenuId == menu.MenuId).Name, newMenuName);
        }

    }
}
