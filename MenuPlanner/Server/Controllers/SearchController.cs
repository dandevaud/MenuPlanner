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
using Shared.models.Search;

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

            var menuList = await searchLogic.SearchMenus(new MenuSearchRequestModel(){
                Ingredients = new List<Ingredient>(){
                    ingredient}
            });

            return menuList.Result;
        }

        // GET: api/Search/MenuWithIngredient?filter={filter}
        [HttpGet("MenuWithIngredient")]
        public async Task<ActionResult<List<Menu>>> GetMenuByIngredientName(String filter)
        {
           var ingredients = await searchLogic.SearchIngredients(new IngredientSearchRequestModel()
           {
               Name = filter
           });

           var toReturn = await searchLogic.SearchMenus(new MenuSearchRequestModel()
           {
               Ingredients = ingredients.Result
           });

           return toReturn.Result;
        }

        // GET: api/Search/MenuBy?timeOfDay={TimeOfDay}&category={category}&season={season}&filter={string}&...
        [HttpGet("MenuBy")]
        public async Task<ActionResult<List<Menu>>> GetMenuBy([FromQuery] MenuSearchRequestModel searchRequestModel)
        {
            var searchResponse = await searchLogic.SearchMenus(searchRequestModel);
            return searchResponse.Result;
        }

        // GET: api/Search/MenuBy?timeOfDay={TimeOfDay}&category={category}&season={season}&filter={string}&...
        
        [HttpGet("IngredientBy")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Ingredient>>> GetIngredientBy([FromQuery] IngredientSearchRequestModel searchRequestModel)
        {
            var searchResponse = await searchLogic.SearchIngredients(searchRequestModel);
            return searchResponse.Result;
        }


        // GET: api/Search/Menu?filter={filter}
        [HttpGet("Menu")]
        public async Task<ActionResult<List<Menu>>> GetMenu(String filter)
        {
           var toRet = await searchLogic.SearchMenus(new MenuSearchRequestModel(){
                Filter = filter});

            return toRet.Result;
        }

    }
}
