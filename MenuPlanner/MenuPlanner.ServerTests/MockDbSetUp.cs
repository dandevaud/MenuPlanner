// <copyright file="MockDbSetUp.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Collections.Generic;
using Duende.IdentityServer.Extensions;
using MenuPlanner.Server.Data;
using MenuPlanner.Shared.models;
using MenuPlanner.Shared.models.enums;
using Moq;

namespace MenuPlanner.ServerTests
{
    /// <summary>Class used to mock the Database</summary>
    public class MockDbSetUp
    {
        public List<Ingredient> Ingredients { get; } = new List<Ingredient>();
        public List<MenuIngredient> MenuIngredients { get; } = new List<MenuIngredient>();
        public List<Menu> Menus { get; } = new List<Menu>();
        public Mock<MenuPlannerContext> menuPlannerMock { get; } = new Mock<MenuPlannerContext>();

        public Mock<MenuPlannerContext> SetUpDBContext()
        {
            SetUpIngredients();
            AddIngredients();

            SetUpMenus();
            AddMenus();
            SetUpMenuIngredients();
            AddMenuIngredients();
            return menuPlannerMock;
        }

        #region SetUpLists
        private void SetUpIngredients()
        {
            var pouletGschnetzlets = new Ingredient()
            {
                Id = Guid.NewGuid(),
                Name = "Poulet Geschnetzletes",
            };

            var poulet = new Ingredient()
            {
                Id = Guid.NewGuid(),
                Name = "Poulet",
                ChildIngredients = new List<Ingredient>()
                {
                    pouletGschnetzlets
                }
            };

            var huhn = new Ingredient()
            {
                Id = Guid.NewGuid(),
                Name = "Huhn",
                ChildIngredients = new List<Ingredient>()
                {
                    poulet
                }
            };

            var rind = new Ingredient()
            {
                Id = Guid.NewGuid(),
                Name = "Rind"
            };

            var oel = new Ingredient()
            {
                Id = Guid.NewGuid(),
                Name = "Öl"
            };

            Ingredients.Add(huhn);
            Ingredients.Add(poulet);
            Ingredients.Add(rind);
            Ingredients.Add(pouletGschnetzlets);
            Ingredients.Add(oel);
        }

        private void SetUpMenuIngredients()
        {
            if (Menus.IsNullOrEmpty()) SetUpMenus();
            if (Ingredients.IsNullOrEmpty()) SetUpIngredients();

            MenuIngredients.Add(new MenuIngredient()
            {
                Id = Guid.NewGuid(),
                Quantity = new Quantity()
                {
                    QuantityValue = 12d,
                    Unit = UnitEnum.kg
                },
                Ingredient = Ingredients.Find(i => i.Name.Equals("Poulet Geschnetzletes")),
                Menu = Menus.Find(m => m.Name.Equals("Chicken Nuggets"))
            });

            MenuIngredients.Add(new MenuIngredient()
            {
                Id = Guid.NewGuid(),
                Quantity = new Quantity()
                {
                    QuantityValue = 1d,
                    Unit = UnitEnum.kg
                },
                Ingredient = Ingredients.Find(i => i.Name.Equals("Poulet")),
                Menu = Menus.Find(m => m.Name.Equals("Pouletbrüstli"))
            });

            MenuIngredients.Add(new MenuIngredient()
            {
                Id = Guid.NewGuid(),
                Quantity = new Quantity()
                {
                    QuantityValue = 1d,
                    Unit = UnitEnum.kg
                },
                Ingredient = Ingredients.Find(i => i.Name.Equals("Huhn")),
                Menu = Menus.Find(m => m.Name.Equals("Ofen Huhn"))
            });

            MenuIngredients.Add(new MenuIngredient()
            {
                Id = Guid.NewGuid(),
                Quantity = new Quantity()
                {
                    QuantityValue = 1d,
                    Unit = UnitEnum.kg
                },
                Ingredient = Ingredients.Find(i => i.Name.Equals("Rind")),
                Menu = Menus.Find(m => m.Name.Equals("Steak"))
            });
        }

        private void SetUpMenus()
        {
            Menus.Add(new Menu()
            {
                Id = Guid.NewGuid(),
                Name = "Chicken Nuggets"
            });

            Menus.Add(new Menu()
            {
                Id = Guid.NewGuid(),
                Name = "Pouletbrüstli"
            });

            Menus.Add(new Menu()
            {
                Id = Guid.NewGuid(),
                Name = "Ofen Huhn"
            });

            Menus.Add(new Menu()
            {
                Id = Guid.NewGuid(),
                Name = "Steak"
            });

            Menus.Add(new Menu()
            {
                Id = Guid.NewGuid(),
                Name = "Old Name"
            });
        }

        #endregion

        #region AddListToMockDBContext
        private void AddIngredients()
        {
            //https://stackoverflow.com/questions/51384331/mocking-dbcontext-in-ef-6-not-working-as-expected
            //https://docs.microsoft.com/en-us/ef/ef6/fundamentals/testing/mocking

            var mockDbSet = DbContextMock.GetQueryableMockDbSet(Ingredients);
            menuPlannerMock.Setup(c => c.Ingredients).Returns(mockDbSet.Object);
        }

        private void AddMenus()
        {
            //https://stackoverflow.com/questions/51384331/mocking-dbcontext-in-ef-6-not-working-as-expected
            //https://docs.microsoft.com/en-us/ef/ef6/fundamentals/testing/mocking

            var mockDbSet = DbContextMock.GetQueryableMockDbSet(Menus);
            menuPlannerMock.Setup(c => c.Menus).Returns(mockDbSet.Object);
        }

        private void AddMenuIngredients()
        {
            //https://stackoverflow.com/questions/51384331/mocking-dbcontext-in-ef-6-not-working-as-expected
            //https://docs.microsoft.com/en-us/ef/ef6/fundamentals/testing/mocking

            var mockDbSet = DbContextMock.GetQueryableMockDbSet(MenuIngredients);
            menuPlannerMock.Setup(c => c.MenuIngredients).Returns(mockDbSet.Object);
        }

        #endregion

    }
}
