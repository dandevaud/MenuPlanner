// <copyright file="ValuesController.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Internal;
using MenuPlanner.Server.Logic;
using MenuPlanner.Server.SqlImplementation;
using MenuPlanner.Shared.models;
using MenuPlanner.Shared.models.enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MenuPlanner.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly MenuPlannerContext _context;
        private readonly SearchLogic searchLogic;

        public SearchController(MenuPlannerContext context)
        {
            _context = context;
            searchLogic = new SearchLogic(context);
        }

        // GET: api/Search/MenuWithIngredient/5
        [HttpGet("MenuWithIngredient/{id}")]
        public async Task<ActionResult<List<Menu>>> GetMenuForIngredient(Guid id)
        {
            var ingredient = await _context.Ingredients.FindAsync(id);
            return await GetMenuForIngredient(ingredient);
        }

        // Post: api/Search/MenuWithIngredient
        [HttpPost("MenuWithIngredient")]
        public async Task<ActionResult<List<Menu>>> GetMenuForIngredient(Ingredient ingredient)
        {
            var menuList = await searchLogic.GetAllMenusWithIngredientAndChildIngredient(ingredient);

            return menuList;
        }

        // GET: api/Search/MenuWithIngredient?filter={filter}
        [HttpGet("MenuWithIngredient")]
        public async Task<ActionResult<List<Menu>>> GetMenuByIngredientName(String filter)
        {
           return await searchLogic.GetAllMenusWithIngredientAndChildIngredientContainingName(filter);
        }

        // GET: api/Search/MenuByName?filter={filter}
        [HttpGet("MenuByName")]
        public async Task<ActionResult<List<Menu>>> GetMenuByName(String filter)
        {
            return await searchLogic.GetAllMenusWithIngredientAndChildIngredientContainingName(filter);
        }

          // GET: api/Search/MenuByName?timeOfDay={TimeOfDay}&category={category}&season={season}
        [HttpGet("MenuBy")]
        public async Task<ActionResult<List<Menu>>> GetMenuBy()
        {
            var tasks = new List<Task<List<Menu>>>();
            var exceptions = new List<ArgumentException>();
            var toReturn = new List<Menu>();
           
            try{
                var timeOfDay = GetEnumValueFromQuery<TimeOfDay>("timeOfDay");
                toReturn.AddRange( await searchLogic.GetMenuByTimeOfDay(timeOfDay));
            } catch (ArgumentException ex){
                exceptions.Add(ex);
            }

            try{
                var category = GetEnumValueFromQuery<MenuCategory>("category");
                toReturn.AddRange( await searchLogic.GetMenuByCategory(category));
            } catch (ArgumentException ex){
                exceptions.Add(ex);
            }

            try{
                var season = GetEnumValueFromQuery<Season>("season");
                toReturn.AddRange( await searchLogic.GetMenuBySeason(season));
            } catch (ArgumentException ex){
                exceptions.Add(ex);
            }


            tasks.ForEach(t => {
                var result = await t;
            })
            return await ;
        }

        private T GetEnumValueFromQuery<T> (string queryString) where T : struct{
            var enumString = HttpContext.Request.Query[queryString].ToString().ToLower();
            T enumParsed;
            Enum.TryParse<T>(enumString,true, out enumParsed);
            return enumParsed;
        }

        // GET: api/Search/Menu?filter={filter}
        [HttpGet("Menu")]
        public async Task<ActionResult<List<Menu>>> GetMenu(String filter)
        {
            var byIngredient = searchLogic.GetAllMenusWithIngredientAndChildIngredientContainingName(filter);
            var byName = searchLogic.GetAllMenusContainingName(filter);

            var toRet = new List<Menu>();
            toRet.AddRange(await byIngredient);
            toRet.AddRange(await byName);

            return toRet;
        }

    }
}
