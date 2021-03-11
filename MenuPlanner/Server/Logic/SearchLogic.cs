// <copyright file="Class.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using MenuPlanner.Server.Contracts.Logic;
using MenuPlanner.Server.Data;
using MenuPlanner.Shared.models;
using MenuPlanner.Shared.Models;
using MenuPlanner.Shared.models.enums;
using MenuPlanner.Shared.models.Search;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MenuPlanner.Server.Logic
{
    public delegate bool ContainsAnyOf<T, M>(T enumIn, M model);

    /// <summary>Class used to return the Menus from Database, based on filter object</summary>
    public class SearchLogic : ISearchLogic
    {
        private readonly MenuPlannerContext _context;

        public SearchLogic(MenuPlannerContext context)
        {
            _context = context;
        }

        public async Task<SearchResponseModel<Menu>> GetAllMenus()
        {
            var menus = (await _context.Menus.Include(m =>m.Images).ToListAsync()).OrderByDescending(a => a.AverageRating);
            return new SearchResponseModel<Menu>() {Result = menus.ToList()};
        }
        public async Task<SearchResponseModel<Ingredient>> GetAllIngredients()
        {
            var toReturn = await _context.Ingredients
                .Include(i => i.ParentIngredients)
                .Include(i => i.ChildIngredients)
                .ToListAsync();
            
            return new SearchResponseModel<Ingredient>() { Result = toReturn };
        }

        public async Task<Dictionary<string, int>> GetMaxTimes()
        {
            var prepTime = await _context.Menus.MaxAsync(m => m.PrepTime);
            var cookTime = await _context.Menus.MaxAsync(m => m.CookTime);
            var totalTime = await _context.Menus.MaxAsync(m => m.PrepTime + m.CookTime);
            var dict = new Dictionary<string, int>()
            {
                { "prepTime",prepTime } ,
                { "cookTime", cookTime } ,
                { "totalTime", totalTime }
            };
            return dict;
        }

        public async Task<SearchResponseModel<Menu>> SearchMenus(MenuSearchRequestModel searchRequest)
        {
           
            if (!searchRequest.Id.Equals(Guid.Empty))
            {

                var entities = await GetEntityById<Menu>(searchRequest);
                foreach(var i in entities){
                     await LoadMenuSubEntities(i);
                }
                return CreateSearchResponseModel(searchRequest, entities);

            }
           var menuList = _context.Menus;
           foreach (var m in menuList.ToList())
           {
               await LoadMenuSubEntities(m);
            }

           

           var results = await GetMenuByIngredient(searchRequest, menuList);
           results = FilterByEnumsFlags(searchRequest.TimeOfDay, results, ((TimeOfDay t, Menu m) => m.TimeOfDay.HasFlag(t)));
           results = FilterByEnumsFlags(searchRequest.Season, results, ((Season t, Menu m) => m.Season.HasFlag(t)));
           results = FilterByEnums(searchRequest.MenuCategory, results, ((MenuCategory t, Menu m) => m.MenuCategory.Equals(t)));
           results = FilterByEnums(searchRequest.Diet, results, ((Diet d, Menu m) => m.Diet.HasFlag(d)));

           results = HandleTime(results, searchRequest.CookTime, (Menu m) => m.CookTime.CompareTo(searchRequest.CookTime) <= 0);
           results = HandleTime(results, searchRequest.PrepTime, (Menu m) => m.PrepTime.CompareTo(searchRequest.PrepTime) <= 0);
           results = HandleTime(results, searchRequest.TotalTime, (Menu m) => (m.CookTime+m.PrepTime).CompareTo(searchRequest.TotalTime) <= 0);



            if (searchRequest.Votes > 0)
            {
                results = results.Where(m => m.Votes >= searchRequest.Votes);
            }
            if (searchRequest.AverageRating > 0)
            {
                results = results.Where(m => m.AverageRating >= searchRequest.AverageRating);
            }

            //GeneralSearchRequestModel search by filter takes longest, hence reduce menuList size with easy tasks before
            bool NamePredicate(Menu m) => m.Name.Contains(searchRequest.Name, StringComparison.InvariantCultureIgnoreCase);

            bool FilterPredicate(Menu m) =>
                m.Name.Contains(searchRequest.Filter, StringComparison.InvariantCultureIgnoreCase) || 
                m.MenuCategory.ToString().Contains(searchRequest.Filter, StringComparison.InvariantCultureIgnoreCase) || 
                m.Season.ToString().Contains(searchRequest.Filter, StringComparison.InvariantCultureIgnoreCase) || 
                m.TimeOfDay.ToString().Contains(searchRequest.Filter, StringComparison.InvariantCultureIgnoreCase) || 
                m.Ingredients.Any(i => i.Ingredient.Name.Contains(searchRequest.Filter, StringComparison.InvariantCultureIgnoreCase)) ||
                m.Tags.Any(t => t.Name.Contains(searchRequest.Filter, StringComparison.InvariantCultureIgnoreCase));

            results = GeneralSearchRequestModelHandling(searchRequest, results, NamePredicate, FilterPredicate);
            return CreateSearchResponseModel(searchRequest, results);
        }

        private IEnumerable<T> LimitSearch<T>(IEnumerable<T> list, SearchRequestModel searchRequest)
        {
            if (searchRequest.Count > 0)
            {
                list = list.Skip(searchRequest.Skip).Take(searchRequest.Count);
            }
            return list;
        }

        public async Task<SearchResponseModel<Ingredient>> SearchIngredients(IngredientSearchRequestModel searchRequest)
        {
            if (!searchRequest.Id.Equals(Guid.Empty))
            {
                var entities = await GetEntityById<Ingredient>(searchRequest);
                foreach (var i in entities)
                {
                    await LoadSubIngredients(i);
                }
               
                return CreateSearchResponseModel(searchRequest, entities);
                

            }
            IEnumerable<Ingredient> ingredientList = _context.Ingredients.ToList();

            if (searchRequest.Calories > 0)
            {
                ingredientList = ingredientList.Where(i => i.Calories <= searchRequest.Calories);
            }

            if (searchRequest.Price > 0)
            {
                ingredientList = ingredientList.Where(i => i.Price <= searchRequest.Price);
            }
            ingredientList = FilterByEnums(searchRequest.Category, ingredientList, ((IngredientCategory t, Ingredient i) => i.Category.HasFlag(t)));

            //GeneralSearchRequestModel search by filter takes longest, hence reduce menuList size with easy tasks before
            bool NamePredicate(Ingredient i) => i.Name.Contains(searchRequest.Name, StringComparison.InvariantCultureIgnoreCase);

            bool FilterPredicate(Ingredient i) =>
                i.Name.Contains(searchRequest.Filter, StringComparison.InvariantCultureIgnoreCase) ||
                i.Category.ToString().Contains(searchRequest.Filter, StringComparison.InvariantCultureIgnoreCase);

            ingredientList = GeneralSearchRequestModelHandling(searchRequest, ingredientList, NamePredicate, FilterPredicate);
            return CreateSearchResponseModel(searchRequest, ingredientList);
        }

        private SearchResponseModel<T> CreateSearchResponseModel<T>(SearchRequestModel searchRequest, IEnumerable<T> list)
        {
            int totalResults = list.Count();
            list = LimitSearch(list, searchRequest);
            return new SearchResponseModel<T>()
                {Result = list.ToList(), TotalResults = totalResults, Count = searchRequest.Count, Skip = searchRequest.Skip};
        }

        private IEnumerable<M> FilterByEnumsFlags<T, M>(T enumFilter, IEnumerable<M> list, ContainsAnyOf<T, M> anyPredicate) where T : struct, Enum
        {
            //Handle default value (is always true if checked by HasFlags)
            if (!enumFilter.GetType().GetDefaultValue().Equals(enumFilter))
            {
                var enumList = handleEnums(enumFilter);
                list = list.Where(m => enumList.Any(en => anyPredicate(en, m)
                ));
            }

            return list;
        }

        private IEnumerable<M> FilterByEnums<T, M>(T enumFilter, IEnumerable<M> list, ContainsAnyOf<T, M> anyPredicate) where T : struct, Enum
        {
            //Handle default value (is always true if checked by HasFlags)
            if (!enumFilter.GetType().GetDefaultValue().Equals(enumFilter))
            {
                list = list.Where(m => anyPredicate(enumFilter,m));
            }

            return list;
        }

        private List<T> handleEnums<T>(T enumIn) where T : struct, Enum
        {
            List<T> listOfSetEnumFilter = new List<T>();
            foreach (var enumValue in Enum.GetValues<T>())
            {
                if (!enumValue.GetType().GetDefaultValue().Equals(enumValue))
                {
                    if (enumIn.HasFlag(enumValue)) listOfSetEnumFilter.Add(enumValue);
                }
            }
            return listOfSetEnumFilter;
        }

        private async Task<IEnumerable<Menu>> GetMenuByIngredient(MenuSearchRequestModel searchRequest, IEnumerable<Menu> menuList)
        {
            if (!searchRequest.Ingredients.IsNullOrEmpty())
            {
                var ingredients = new List<Ingredient>();
                var ingToSearchList = searchRequest.Ingredients;
                foreach (var i in ingToSearchList)
                {
                    var entity = await _context.Ingredients.FindAsync(i.Id);
                    ingredients.Add(entity);
                }

               var ingredientsToLookFor = new List<Ingredient>(ingredients);
               foreach (var i in ingredients)
               {
                   await LoadSubIngredients(i);
                   ingredientsToLookFor.AddRange(await GetSubIngredients(i));
               }
               menuList = menuList.
                    Where(m => m.Ingredients.
                        Any(i =>
                            ingredientsToLookFor.
                                Any(itlf =>
                                    itlf.Id.Equals(i.Ingredient.Id)))).
                    ToList();
            }

            return menuList;
        }

        public async Task<List<Ingredient>> GetSubIngredients(Ingredient ing)
        {
            var toReturn = new List<Ingredient>();
            if (ing.ChildIngredients != null)
            {
                toReturn.AddRange(ing.ChildIngredients);
                var list = ing.ChildIngredients;
                foreach (var i in list)
                {
                    toReturn.AddRange(await GetSubIngredients(i));
                }
            }

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
        private IEnumerable<T> GeneralSearchRequestModelHandling<T>(SearchRequestModel searchRequest, IEnumerable<T> list, Func<T, bool> namePredicate, Func<T, bool> filterPredicate)
        {
            if (!searchRequest.Name.IsNullOrEmpty())
            {
                list = list.Where(namePredicate);
            }

            if (!searchRequest.Filter.IsNullOrEmpty())
            {
                list = list.Where(filterPredicate);
            }

            return list;
        }

        private async Task<IEnumerable<T>> GetEntityById<T>(SearchRequestModel searchRequest) where T : Entity
        {
            return new List<T>(){
                await _context.FindAsync<T>(searchRequest.Id)
            };
        }

        private async Task LoadMenuSubEntities(Menu menu)
        {
                var menuEntity = _context.Entry(menu);
                menuEntity.State = EntityState.Unchanged;
                await menuEntity.Collection(sm => sm.Ingredients).LoadAsync();
                await menuEntity.Collection(sm => sm.Comments).LoadAsync();
                await  menuEntity.Collection(m => m.Images).LoadAsync();
                await menuEntity.Collection(m => m.Tags).LoadAsync();
              
            foreach (var menuIngredient in menu.Ingredients)
                {
                    await _context.Entry(menuIngredient).Reference(mi => mi.Ingredient).LoadAsync();
                    await LoadSubIngredients(menuIngredient.Ingredient);
                }
     

        }

        private async Task LoadSubIngredients(Ingredient ing)
        {
            EntityEntry<Ingredient> entry;

            entry = _context.ChangeTracker.Entries<Ingredient>()
                 .FirstOrDefault(i => i.Entity.Id.Equals(ing.Id));

            if (entry == null)
            {
                entry = _context.Entry(ing);
                entry.State = EntityState.Unchanged;
            }
            else if (entry.State == EntityState.Detached)
            {
                await entry.ReloadAsync();
            }

            await entry.Collection(i => i.ChildIngredients).LoadAsync();
            if (ing.ChildIngredients != null)
            {
                foreach (var i in ing.ChildIngredients)
                {
                    await LoadSubIngredients(i);
                }
            }
        }

        private IEnumerable<Menu> HandleTime(IEnumerable<Menu> menulist, int value, Func<Menu,bool> where)
        {
            if (value > 0) {
                return menulist.Where(where);
            }
            return menulist;
        }

    }
}
