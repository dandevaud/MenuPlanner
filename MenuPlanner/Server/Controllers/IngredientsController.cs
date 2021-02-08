// <copyright file="IngredientsController.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using MenuPlanner.Server.Contracts.Logic;
using MenuPlanner.Server.Data;
using MenuPlanner.Server.Logic;
using MenuPlanner.Server.Logic.EntityUpdater;
using MenuPlanner.Shared.models;
using MenuPlanner.Shared.models.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MenuPlanner.Server.Controllers
{
    /// <summary>Ingredients API Controller --&gt; Handles CRUD actions for all ingredients</summary>
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly MenuPlannerContext _context;
        private readonly IIngredientEntityUpdater _ingredientEntityUpdater;
        private readonly ISearchLogic _searchLogic;

        public IngredientsController(MenuPlannerContext context, IIngredientEntityUpdater ingredientEntityUpdater, ISearchLogic searchLogic)
        {
            _context = context;
            _ingredientEntityUpdater = ingredientEntityUpdater;
            _searchLogic = searchLogic;

        }

        // GET: api/Ingredients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ingredient>>> GetIngredients()
        {
            var toReturn = await _searchLogic.GetAllIngredients();
            return toReturn.Result.OrderBy(i => i.Name).ToList();
        }

        // GET: api/Ingredients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ingredient>> GetIngredient(Guid id)
        {
            var ingredients = await _searchLogic.SearchIngredients(new IngredientSearchRequestModel() {Id = id});
            if (ingredients.Result.IsNullOrEmpty())
            {
                return NotFound();
            }
            var ingredient = ingredients.Result[0];

            return ingredient;
        }

        // PUT: api/Ingredients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIngredient(Guid id, Ingredient ingredient)
        {
           
            if (id != ingredient.Id)
            {
                return BadRequest();
            }

            await _ingredientEntityUpdater.CheckIfIngredientExistsAndUpdateOrAdd(ingredient);

            return NoContent();
        }

        // POST: api/Ingredients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Ingredient>> PostIngredient(Ingredient ingredient)
        {
            await _ingredientEntityUpdater.CheckIfIngredientExistsAndUpdateOrAdd(ingredient);

            return CreatedAtAction("GetIngredient", new { id = ingredient.Id }, ingredient);
        }

        // DELETE: api/Ingredients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredient(Guid id)
        {
            if (await _ingredientEntityUpdater.DeleteIngredient(id))
            {
                return NoContent();
            }
            return NotFound();
           
        }
    }
}
