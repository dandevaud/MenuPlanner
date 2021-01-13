// <copyright file="Class.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using MenuPlanner.Server.SqlImplementation;
using MenuPlanner.Shared.models;
using MenuPlanner.Shared.models.enums;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Shared.models.Search;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MenuPlanner.Server.Logic
{
    public class SearchLogic
    {
        private readonly MenuPlannerContext _context;

        public SearchLogic(MenuPlannerContext context)
        {
            _context = context;
        }


     

         public async Task<List<Menu>> SearchMenus(MenuSearchRequestModel searchRequest)
         {
             var menuList = await _context.Menus.ToListAsync();
             LoadMenuSubEntities(menuList);
            
             GetMenuByIngredient(searchRequest, menuList);
             FilterByEnums(searchRequest.TimeOfDay, menuList);
             FilterByEnums(searchRequest.Season, menuList);
             FilterByEnums(searchRequest.MenuCategory, menuList);

             if (searchRequest.Votes > 0)
             {
                 menuList = menuList.Where(m => m.Votes >= searchRequest.Votes).ToList();
             }
             if (searchRequest.AverageRating > 0)
             {
                 menuList = menuList.Where(m => m.AverageRating >= searchRequest.AverageRating).ToList();
             }


            //GeneralSearchRequestModel search by filter takes longest, hence reduce menulist size with easy tasks before
            bool NamePredicate(Menu m) => m.Name.Contains(searchRequest.Name, StringComparison.InvariantCultureIgnoreCase);

             bool FilterPredicate(Menu m) =>
                 m.Name.Contains(searchRequest.Filter, StringComparison.InvariantCultureIgnoreCase) || m.MenuCategory.ToString()
                     .Contains(searchRequest.Filter, StringComparison.InvariantCultureIgnoreCase) || m.Season.ToString().Contains(searchRequest.Filter, StringComparison.InvariantCultureIgnoreCase) || m.TimeOfDay.ToString()
                     .Contains(searchRequest.Filter, StringComparison.InvariantCultureIgnoreCase) || m.Ingredients.Any(i => i.Ingredient.Name.Contains(searchRequest.Filter));

             GeneralSearchRequestModelHandling<Menu>(searchRequest, menuList, NamePredicate, FilterPredicate);

             return menuList;

         }

         private void FilterByEnums<T>(T enumFilter, List<Menu> menuList) where T: struct,Enum
         {
             //Handle default value (is always true if checked by HasFlags)
             if (!enumFilter.GetType().GetDefaultValue().Equals(enumFilter))
             {
                 var enumList = handleEnums(enumFilter);
                 menuList = menuList.Where(m => enumList.Any(en => m.TimeOfDay.HasFlag(en))).ToList();
             }


         }

         private List<T> handleEnums<T>(T enumIn) where T : struct, Enum
         {
             
                 List<T> listOfSetEnumFilter = new List<T>();
                 foreach (var enumValue in Enum.GetValues<T>())
                 {
                    if (enumIn.HasFlag(enumValue))  listOfSetEnumFilter.Add(enumValue);
                 }
                 return listOfSetEnumFilter;
         }

         private void GetMenuByIngredient(MenuSearchRequestModel searchRequest, List<Menu> menuList)
         {
             if (!searchRequest.Ingredients.IsNullOrEmpty())
             {
                 var ingredients = searchRequest.Ingredients.ToList();
                 var ingredientsToLookFor = new List<Ingredient>(ingredients);
                 ingredients.ForEach(async i => await LoadSubIngredients(i));
                 ingredients.ForEach(async i => ingredientsToLookFor.AddRange(await GetSubIngredients(i)));
                 menuList = menuList.Where(m => m.Ingredients.Any(i => ingredientsToLookFor.Contains(i.Ingredient))).ToList();
             }
         }

         public async Task<List<Ingredient>> GetSubIngredients(Ingredient ing)
         {
             var toReturn = new List<Ingredient>();
             toReturn.AddRange(ing.ChildIngredients);
             ing.ChildIngredients.ToList().ForEach(async i => toReturn.AddRange(await GetSubIngredients(i)));
             return toReturn;
         }

        /// <summary>
        /// Handles the general search model request.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="searchRequest">The search request model.</param>
        /// <param name="list">The list to search on.</param>
        /// <param name="namePredicate">The name predicate used for searching by name.</param>
        /// <param name="filterPredicate">The filter predicate used for searching by filter.</param>
        private void GeneralSearchRequestModelHandling<T>(SearchRequestModel searchRequest, List<T> list, Predicate<T> namePredicate, Predicate<T> filterPredicate)
         {
             if (!searchRequest.Name.IsNullOrEmpty())
             {
                 list = list.FindAll(namePredicate).ToList();
             }

             if (!searchRequest.Filter.IsNullOrEmpty())
             {
                 list = list.FindAll(filterPredicate).ToList();
             }
         }

         private void LoadMenuSubEntities(List<Menu> menuList)
         {
             menuList.ForEach(async m =>
             {
                 var menuEntity = _context.Entry(m);
                 var ingredients = menuEntity.Collection(sm => sm.Ingredients).LoadAsync();
                 var images = menuEntity.Collection(sm => sm.Images).LoadAsync();
                 var comments = menuEntity.Collection(sm => sm.Comments).LoadAsync();
                 await ingredients;
                 await images;
                 await comments;
             });
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



        #region Old Stuff

        /// <summary>Gets all menus containing the provided filter.</summary>
        /// <param filter="filter">The filter.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public async Task<List<Menu>> GetAllMenusContainingName(string filter)
        {
            var menus = await EntityFrameworkQueryableExtensions.ToListAsync(_context.Menus.Where(m => m.Name.ToLower().Contains(filter.ToLower())));
            return menus;
        }


        /// <summary>
        /// Gets all menus with ingredient and child ingredient containing the provided filter.
        /// </summary>
        /// <param filter="filter">The filter.</param>
        /// <returns>List of Menus</returns>
        public async Task<List<Menu>> GetAllMenusWithIngredientAndChildIngredientContainingName(string filter)
         {
             var ingredients = await EntityFrameworkQueryableExtensions.ToListAsync(_context.Ingredients.Where(i => i.Name.ToLower().Contains(filter.ToLower())));
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

            return await EntityFrameworkQueryableExtensions.ToListAsync(_context.Menus.Where(m => m.Ingredients.Intersect(menuIngredients).Any()));
        }

        /// <summary>Gets all menus containing the provided time of day attribute</summary>
        /// <param name="timeOfDay">The time of day to look for</param>
        /// <returns>all menus containing the attribute</returns>
        public async Task<List<Menu>> GetMenuByTimeOfDay(TimeOfDay timeOfDay){
            return await EntityFrameworkQueryableExtensions.ToListAsync(_context.Menus.Where(m => m.TimeOfDay.HasFlag(timeOfDay)));
        }

      /// <summary>Gets all menus containing the provided season attribute</summary>
        /// <param name="season">The season to look for</param>
        /// <returns>all menus containing the attribute</returns>
        public async Task<List<Menu>> GetMenuBySeason(Season season){
            return await EntityFrameworkQueryableExtensions.ToListAsync(_context.Menus.Where(m => m.Season.HasFlag(season)));
        }

        /// <summary>Gets all menus containing the provided menu category attribute</summary>
        /// <param name="category">The menu category to look for</param>
        /// <returns>all menus containing the attribute</returns>
         public async Task<List<Menu>> GetMenuByCategory(MenuCategory category){
            return await EntityFrameworkQueryableExtensions.ToListAsync(_context.Menus.Where(m => m.MenuCategory.HasFlag(category)));
        }

        private async Task<List<MenuIngredient>> GetAllMenuIngredientWithIngredient(List<Ingredient> ingredients)
        {
            return await EntityFrameworkQueryableExtensions.ToListAsync(_context.MenuIngredients.Where(mi => ingredients.Contains(mi.Ingredient)));

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



        

        #endregion
    }
}
