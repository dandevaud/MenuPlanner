using NUnit.Framework;
using MenuPlanner.Server.Logic;
// <copyright file="SearchLogicTests.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenuPlanner.Server.Controllers;
using MenuPlanner.Server.SqlImplementation;
using MenuPlanner.ServerTests;
using Moq;

namespace MenuPlanner.Server.Logic.Tests
{
    [TestFixture()]
    public class SearchLogicTests
    {
        private MockDbSetUp mockDb;
        private SearchLogic searchLogic;
        private Mock<MenuPlannerContext> dbContext;

            [SetUp]
        public void SetUp()
        {
            mockDb = new MockDbSetUp();
            dbContext = mockDb.SetUpDBContext();
            searchLogic = new SearchLogic(dbContext.Object);
           

        }
      
        //[Test()]
        //Mock DB Get Entity not working
        public async Task GetAllMenusWithIngredientAndParentIngredientTest()
        {

            //ToListAsync unsupported by Mock
            var ing = await dbContext.Object.Ingredients
                .Where(i => i.Name.Equals("Poulet Geschnetzletes"))
                .FirstAsync();
            var menuList = searchLogic.GetAllMenusWithIngredientAndParentIngredient(ing);
            var expected = mockDb.Menus
                .Where(m =>
                    m.Name.Equals("Chicken Nuggets") || m.Name.Equals("Pouletbrüstli") || m.Name.Equals("Ofen Huhn"))
                .ToList();
            var notExpected = mockDb.Menus
                .Where(m => m.Name.Equals("Steak"))
                .ToList();
            await menuList;

            expected.ForEach(m => Assert.Contains(m,menuList.Result));
            Assert.IsEmpty(menuList.Result.FindAll(m => notExpected.Contains(m)));
            
        }
    }
}
