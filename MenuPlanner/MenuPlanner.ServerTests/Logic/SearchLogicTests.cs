// <copyright file="SearchLogicTests.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MenuPlanner.Server.Data;
using MenuPlanner.ServerTests;
using MenuPlanner.Shared.models;
using MenuPlanner.Shared.models.Search;
using Moq;
using NUnit.Framework;

namespace MenuPlanner.Server.Logic.Tests
{
    /// <summary>Class to test the Search Logic</summary>
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

        /// <summary>
        /// Checks if the expected searched menus are returned.
        /// </summary>
        //[Test()]
        //Mock DB Context Entity not working --> Test not possible
        public async Task GetAllMenusWithIngredientAndParentIngredientTest()
        {
            //ToListAsync unsupported by Mock
            var ing = await dbContext.Object.Ingredients
                .Where(i => i.Name.Equals("Huhn"))
                .FirstAsync();
            var menuList = searchLogic.SearchMenus(new MenuSearchRequestModel()
            {
                Ingredients = new List<Ingredient>() { ing }
            });
            var expected = mockDb.Menus
                .Where(m =>
                    m.Name.Equals("Chicken Nuggets") || m.Name.Equals("Pouletbrüstli") || m.Name.Equals("Ofen Huhn"))
                .ToList();
            var notExpected = mockDb.Menus
                .Where(m => m.Name.Equals("Steak"))
                .ToList();
            await menuList;

            expected.ForEach(m => Assert.Contains(m, menuList.Result.Result));
            Assert.IsEmpty(menuList.Result.Result.FindAll(m => notExpected.Contains(m)));
        }

        /// <summary>
        /// Checks if the expected searched child ingredients by parent ingredient are returned.
        /// </summary>
        //[Test()]
        //Mock DB Context Entity not working --> Test not possible
        public async Task GetAllChildIngredientsByParentIngredientTest()
        {
            //ToListAsync unsupported by Mock
            var ing = await dbContext.Object.Ingredients
                .Where(i => i.Name.Equals("Huhn"))
                .FirstAsync();
            var ingredientList = searchLogic.SearchIngredients(new IngredientSearchRequestModel()
            {
                ParentIngredients = new List<Ingredient>() { ing }
            });
            var expected = mockDb.Ingredients
                .Where(i => i.Name.Equals("Poulet") || i.Name.Equals("Poulet Geschnetzletes"))
                .ToList();
            var notExpected = mockDb.Ingredients
                .Where(i => i.Name.Equals("Rind"))
                .ToList();

            expected.ForEach(i => Assert.Contains(i, ingredientList.Result.Result));
            Assert.IsEmpty(ingredientList.Result.Result.FindAll(i => notExpected.Contains(i)));
        }

        /// <summary>
        /// Checks if the expected searched parent ingredients by child ingredient are returned.
        /// </summary>
        //[Test()]
        //Mock DB Context Entity not working --> Test not possible
        public async Task GetAllParentIngredientsByChildIngredientTest()
        {
            //ToListAsync unsupported by Mock
            var ing = await dbContext.Object.Ingredients
                .Where(i => i.Name.Equals("Poulet Geschnetzletes"))
                .FirstAsync();
            var ingredientList = searchLogic.SearchIngredients(new IngredientSearchRequestModel()
            {
                ChildIngredients = new List<Ingredient>() { ing }
            });
            var expected = mockDb.Ingredients
                .Where(i => i.Name.Equals("Poulet") || i.Name.Equals("Huhn"))
                .ToList();
            var notExpected = mockDb.Ingredients
                .Where(i => i.Name.Equals("Rind"))
                .ToList();

            expected.ForEach(i => Assert.Contains(i, ingredientList.Result.Result));
            Assert.IsEmpty(ingredientList.Result.Result.FindAll(i => notExpected.Contains(i)));
        }

    }
}
