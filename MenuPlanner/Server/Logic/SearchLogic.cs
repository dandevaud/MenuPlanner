// <copyright file="Class.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MenuPlanner.Server.SqlImplementation;
using MenuPlanner.Shared.models;
using MenuPlanner.Shared.models.enums;
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
            await entry.Collection(i => i.ChildIngredients).LoadAsync();
            if (ing.ChildIngredients != null)
            {
                foreach (var i in ing.ChildIngredients)
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
            var menus = await _context.Menus.Where(m => m.Name.ToLower().Contains(filter.ToLower())).ToListAsync();
            return menus;
        }


        /// <summary>
        /// Gets all menus with ingredient and child ingredient containing the provided filter.
        /// </summary>
        /// <param filter="filter">The filter.</param>
        /// <returns>List of Menus</returns>
        public async Task<List<Menu>> GetAllMenusWithIngredientAndChildIngredientContainingName(string filter)
         {
             var ingredients = await _context.Ingredients.Where(i => i.Name.ToLower().Contains(filter.ToLower())).ToListAsync();
             List<Menu> toRet = new List<Menu>();
             ingredients.ForEach(async i => toRet.AddRange(await GetAllMenusWithIngredientAndChildIngredient(i)));
             return toRet;
         }

        /// <summary>Gets all menus containing either the ingredient provided or any of his Child ingredients.</summary>
        /// <param name="ing">The ingredient to look for</param>
        /// <returns>all menus containing this ingredient</returns>
        public async Task<List<Menu>> GetAllMenusWithIngredientAndChildIngredient(Ingredient ing)
        {
            await LoadSubIngredients(ing);
            var ingredientsToLookFor = new List<Ingredient>();
            ingredientsToLookFor.Add(ing);
            
            ingredientsToLookFor.AddRange(await GetChildIngredientsRecursive(ing));

            var menuIngredients = await GetAllMenuIngredientWithIngredient(ingredientsToLookFor);

            return await _context.Menus.Where(m => m.Ingredients.Intersect(menuIngredients).Any()).ToListAsync();
        }

        /// <summary>Gets all menus containing the provided time of day attribute</summary>
        /// <param name="timeOfDay">The time of day to look for</param>
        /// <returns>all menus containing the attribute</returns>
        public async Task<List<Menu>> GetMenuByTimeOfDay(TimeOfDay timeOfDay){
            return await _context.Menus.Where(m => m.TimeOfDay.HasFlag(timeOfDay)).ToListAsync();
        }

      /// <summary>Gets all menus containing the provided season attribute</summary>
        /// <param name="season">The season to look for</param>
        /// <returns>all menus containing the attribute</returns>
        public async Task<List<Menu>> GetMenuBySeason(Season season){
            return await _context.Menus.Where(m => m.Season.HasFlag(season)).ToListAsync();
        }

        /// <summary>Gets all menus containing the provided menu category attribute</summary>
        /// <param name="category">The menu category to look for</param>
        /// <returns>all menus containing the attribute</returns>
         public async Task<List<Menu>> GetMenuByCategory(MenuCategory category){
            return await _context.Menus.Where(m => m.MenuCategory.HasFlag(category)).ToListAsync();
        }

        private async Task<List<MenuIngredient>> GetAllMenuIngredientWithIngredient(List<Ingredient> ingredients)
        {
            return await _context.MenuIngredients.Where(mi => ingredients.Contains(mi.Ingredient)).ToListAsync();

        }

        private async Task<List<Ingredient>> GetChildIngredientsRecursive(Ingredient ing)
        {
            var listToRet = new List<Ingredient>();
            if (ing.ChildIngredients != null)
            {
                listToRet.AddRange(ing.ChildIngredients);
                foreach (var i in ing.ChildIngredients)
                {
                    listToRet.AddRange(await GetChildIngredientsRecursive(i));
                }
            }

            return listToRet;
        }
    }
}
