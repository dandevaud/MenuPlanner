﻿// <copyright file="SearchController.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MenuPlanner.Server.Contracts.Logic;
using MenuPlanner.Server.Data;
using MenuPlanner.Server.Logic;
using MenuPlanner.Shared.models;
using MenuPlanner.Shared.models.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MenuPlanner.Server.Controllers
{
    /// <summary>Search API Controller --&gt; handles all Searches for Menus and Ingredients</summary>
    [Authorize(AuthenticationSchemes ="bearer")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly MenuPlannerContext _context;
        private readonly ISearchLogic _searchLogic;

        public SearchController(MenuPlannerContext context, ISearchLogic searchLogic)
        {
            _context = context;
            _searchLogic = searchLogic;
        }

        /// <summary>Gets the menu for ingredient. GET: api/Search/MenuWithIngredient/5</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Menu Collection</returns>
        [HttpGet("MenuWithIngredient/{id}")]
        public async Task<ActionResult<SearchResponseModel<Menu>>> GetMenuForIngredient(Guid id)
        {
            var ingredient = await _context.Ingredients.FindAsync(id);
            return await GetMenuForIngredient(ingredient);
        }

        /// <summary>Gets the menu for ingredient. POST: api/Search/MenuWithIngredient</summary>
        /// <param name="ingredient">The ingredient.</param>
        /// <returns>Menu Collection</returns>
        [HttpPost("MenuWithIngredient")]
        public async Task<ActionResult<SearchResponseModel<Menu>>> GetMenuForIngredient(Ingredient ingredient)
        {

            var menuList = await _searchLogic.SearchMenus(new MenuSearchRequestModel()
            {
                Ingredients = new List<Ingredient>(){
                    ingredient}
            });

            return menuList;
        }

        /// <summary>Gets the name of the menu by ingredient. GET: api/Search/MenuWithIngredient?filter={filter}</summary>
        /// <param name="filter">The filter.</param>
        /// <returns>Menu Collection</returns>
        [HttpGet("MenuWithIngredient")]
        public async Task<ActionResult<SearchResponseModel<Menu>>> GetMenuByIngredientName(String filter)
        {
            var ingredients = await _searchLogic.SearchIngredients(new IngredientSearchRequestModel()
            {
                Name = filter
            });

            var toReturn = await _searchLogic.SearchMenus(new MenuSearchRequestModel()
            {
                Ingredients = ingredients.Result
            });

            return toReturn;
        }

        /// <summary>Gets the menu by.
        /// GET: api/Search/MenuBy?timeOfDay={TimeOfDay}&category={category}&season={season}&filter={string}&...
        /// </summary>
        /// <param name="searchRequestModel">The search request model.</param>
        /// <returns>Menu Collection</returns>
        [HttpGet("MenuBy")]
        public async Task<ActionResult<SearchResponseModel<Menu>>> GetMenuBy([FromQuery] MenuSearchRequestModel searchRequestModel)
        {
            var searchResponse = await _searchLogic.SearchMenus(searchRequestModel);
            return searchResponse;
        }

        /// <summary>Gets the menu by post. POST: api/Search/MenuBy</summary>
        /// <param name="searchRequestModel">The search request model.</param>
        /// <returns>Menu Collection</returns>
        [HttpPost("MenuBy")]
        [AllowAnonymous]
        public async Task<ActionResult<SearchResponseModel<Menu>>> GetMenuByPost(MenuSearchRequestModel searchRequestModel)
        {
            var searchResponse = await _searchLogic.SearchMenus(searchRequestModel);
            return searchResponse;
        }

        /// <summary>Gets the ingredient by.
        /// GET: api/Search/MenuBy?timeOfDay={TimeOfDay}&category={category}&season={season}&filter={string}&...
        /// </summary>
        /// <param name="searchRequestModel">The search request model.</param>
        /// <returns>Menu Collection</returns>
        [HttpGet("IngredientBy")]
        [AllowAnonymous]
        public async Task<ActionResult<SearchResponseModel<Ingredient>>> GetIngredientBy([FromQuery] IngredientSearchRequestModel searchRequestModel)
        {
            var searchResponse = await _searchLogic.SearchIngredients(searchRequestModel);
            return searchResponse;
        }

        /// <summary>Gets the menu. GET: api/Search/Menu?filter={filter}</summary>
        /// <param name="filter">The filter.</param>
        /// <returns>Menu Collection</returns>
        [HttpGet("Menu")]
        public async Task<ActionResult<SearchResponseModel<Menu>>> GetMenu(String filter)
        {
            var toRet = await _searchLogic.SearchMenus(new MenuSearchRequestModel()
            {
                Filter = filter
            });

            return toRet;
        }

    }
}
