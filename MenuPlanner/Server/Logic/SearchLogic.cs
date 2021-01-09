// <copyright file="Class.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MenuPlanner.Server.SqlImplementation;
using MenuPlanner.Shared.models;
using Microsoft.EntityFrameworkCore;

namespace MenuPlanner.Server.Logic
{
    public class SearchLogic
    {
        private readonly MenuPlannerContext _context;

        public SearchLogic(MenuPlannerContext context)
        {
            _context = context;
        }


         private async Task LoadSubIngredients(Ingredient ing)
        {
            var entry = _context.Entry(ing);
            await entry.Collection(i => i.ParentIngredients).LoadAsync();
            if (ing.ParentIngredients != null)
            {
                foreach (var i in ing.ParentIngredients)
                {
                    await LoadSubIngredients(i);
                }
            }
        }
        /// <summary>Gets all menus containing the provided filter.</summary>
        /// <param filter="filter">The filter.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public async Task<List<Menu>> GetAllMenusContainingName(string filter)
        {
            var menus = await _context.Menus.Where(m => m.Name.Contains(filter)).ToListAsync();
            return menus;
        }


        /// <summary>
        /// Gets all menus with ingredient and parent ingredient containing the provided filter.
        /// </summary>
        /// <param filter="filter">The filter.</param>
        /// <returns>List of Menus</returns>
        public async Task<List<Menu>> GetAllMenusWithIngredientAndParentIngredientContainingName(string filter)
         {
             var ingredients = await _context.Ingredients.Where(i => i.Name.Contains(filter)).ToListAsync();
             List<Menu> toRet = new List<Menu>();
             ingredients.ForEach(async i => toRet.AddRange(await GetAllMenusWithIngredientAndParentIngredient(i)));
             return toRet;
         }

        /// <summary>Gets all menus containing either the ingredient provided or any of his parent ingredients.</summary>
        /// <param name="ing">The ingredient to look for</param>
        /// <returns>all menus containing this ingredient</returns>
        public async Task<List<Menu>> GetAllMenusWithIngredientAndParentIngredient(Ingredient ing)
        {
            await LoadSubIngredients(ing);
            var ingredientsToLookFor = new List<Ingredient>();
            ingredientsToLookFor.Add(ing);
            
            ingredientsToLookFor.AddRange(await GetParentIngredientsRecursive(ing));

            var menuIngredients = await GetAllMenuIngredientWithIngredient(ingredientsToLookFor);

            return await _context.Menus.Where(m => m.Ingredients.Intersect(menuIngredients).Any()).ToListAsync();
        }

        private async Task<List<MenuIngredient>> GetAllMenuIngredientWithIngredient(List<Ingredient> ingredients)
        {
            return await _context.MenuIngredients.Where(mi => ingredients.Contains(mi.Ingredient)).ToListAsync();

        }

        private async Task<List<Ingredient>> GetParentIngredientsRecursive(Ingredient ing)
        {
            var listToRet = new List<Ingredient>();
            if (ing.ParentIngredients != null)
            {
                listToRet.AddRange(ing.ParentIngredients);
                foreach (var i in ing.ParentIngredients)
                {
                    listToRet.AddRange(await GetParentIngredientsRecursive(i));
                }
            }

            return listToRet;
        }
    }
}
