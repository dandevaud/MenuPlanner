﻿// <copyright file="Class.cs" company="Alessandro Marra & Daniel Devaud">
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
    public delegate bool ContainsAnyOf<T,M>(T enumIn , M model);
    
    public class SearchLogic
    {
        private readonly MenuPlannerContext _context;

        public SearchLogic(MenuPlannerContext context)
        {
            _context = context;
        }


     

         public async Task<SearchResponseModel<Menu>> SearchMenus(MenuSearchRequestModel searchRequest)
         {
             var menuList =  _context.Menus.ToList();
             await LoadMenuSubEntities(menuList);

             menuList = GetMenuByIngredient(searchRequest, menuList);
             menuList = FilterByEnums(searchRequest.TimeOfDay, menuList,((TimeOfDay t, Menu m) =>  m.TimeOfDay.HasFlag(t)));
             menuList = FilterByEnums(searchRequest.Season, menuList, ((Season t, Menu m) => m.Season.HasFlag(t)));
             menuList = FilterByEnums(searchRequest.MenuCategory, menuList, ((MenuCategory t, Menu m) => m.Season.HasFlag(t)));

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

             menuList = GeneralSearchRequestModelHandling<Menu>(searchRequest, menuList, NamePredicate, FilterPredicate);

             return new SearchResponseModel<Menu>() {Result = menuList};

         }


         public async Task<SearchResponseModel<Ingredient>> SearchIngredients(IngredientSearchRequestModel searchRequest)
         {
             var ingredientList = _context.Ingredients.ToList();
             
             if (searchRequest.Calories > 0)
             {
                 ingredientList = ingredientList.Where(i => i.Calories <= searchRequest.Calories).ToList();
             }

             if (searchRequest.Price > 0)
             {
                 ingredientList = ingredientList.Where(i => i.Price <= searchRequest.Price).ToList();
             }
            ingredientList = FilterByEnums(searchRequest.Category,ingredientList, ((IngredientCategory t, Ingredient i) => i.Category.HasFlag(t)));
             
            //GeneralSearchRequestModel search by filter takes longest, hence reduce menulist size with easy tasks before
            bool NamePredicate(Ingredient i) => i.Name.Contains(searchRequest.Name, StringComparison.InvariantCultureIgnoreCase);

             bool FilterPredicate(Ingredient i) =>
                 i.Name.Contains(searchRequest.Filter, StringComparison.InvariantCultureIgnoreCase) || 
                 i.Category.ToString().Contains(searchRequest.Filter, StringComparison.InvariantCultureIgnoreCase);

            ingredientList = GeneralSearchRequestModelHandling<Ingredient>(searchRequest, ingredientList, NamePredicate, FilterPredicate);

             return new SearchResponseModel<Ingredient>() { Result = ingredientList };

         }

        private List<M> FilterByEnums<T,M>(T enumFilter, List<M> list, ContainsAnyOf<T,M> anyPredicate) where T: struct,Enum
         {
             //Handle default value (is always true if checked by HasFlags)
             if (!enumFilter.GetType().GetDefaultValue().Equals(enumFilter))
             {
                 var enumList = handleEnums(enumFilter);
                list = list.Where(m => enumList.Any(en => anyPredicate(en, m)
                 )).ToList();
             }

             return list;

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

         private List<Menu> GetMenuByIngredient(MenuSearchRequestModel searchRequest, List<Menu> menuList)
         {
             if (!searchRequest.Ingredients.IsNullOrEmpty())
             {
                 var ingredients = searchRequest.Ingredients.ToList();
                 var ingredientsToLookFor = new List<Ingredient>(ingredients);
                 ingredients.ForEach(async i => await LoadSubIngredients(i));
                 ingredients.ForEach(async i => ingredientsToLookFor.AddRange(await GetSubIngredients(i)));
                 menuList = menuList.Where(m => m.Ingredients.Any(i => ingredientsToLookFor.Contains(i.Ingredient))).ToList();
             }

             return menuList;
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
        private List<T> GeneralSearchRequestModelHandling<T>(SearchRequestModel searchRequest, List<T> list, Predicate<T> namePredicate, Predicate<T> filterPredicate)
         {
             if (!searchRequest.Name.IsNullOrEmpty())
             {
                 list = list.FindAll( namePredicate).ToList();
             }

             if (!searchRequest.Filter.IsNullOrEmpty())
             {
                 list = list.FindAll(filterPredicate).ToList();
             }

             return list;
         }

         private async Task LoadMenuSubEntities(List<Menu> menuList)
         {
             menuList.ForEach(async m =>
             {
                 var menuEntity = _context.Entry(m);
                 var ingredients = menuEntity.Collection(sm => sm.Ingredients).LoadAsync();
                 var images = menuEntity.Collection(sm => sm.Images).LoadAsync();
                 var comments = menuEntity.Collection(sm => sm.Comments).LoadAsync();
                 foreach (var menuIngredient in m.Ingredients)
                 {
                    await _context.Entry(menuIngredient).Reference(mi => mi.Ingredient).LoadAsync();
                    await LoadSubIngredients(menuIngredient.Ingredient);
                 }
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

    }
}