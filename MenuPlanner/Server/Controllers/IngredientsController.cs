// <copyright file="IngredientsController.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MenuPlanner.Server.SqlImplementation;
using MenuPlanner.Shared.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MenuPlanner.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly MenuPlannerContext _context;

        public IngredientsController(MenuPlannerContext context)
        {
            _context = context;
        }

        // GET: api/Ingredients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ingredient>>> GetIngredients()
        {
            return await _context.Ingredients.ToListAsync();
        }

        // GET: api/Ingredients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ingredient>> GetIngredient(Guid id)
        {
            var ingredient = await _context.Ingredients.FindAsync(id);

            if (ingredient == null)
            {
                return NotFound();
            }

            return ingredient;
        }

        // PUT: api/Ingredients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIngredient(Guid id, Ingredient ingredient)
        {
            if (id != ingredient.IngredientId)
            {
                return BadRequest();
            }

            _context.Entry(ingredient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Ingredients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ingredient>> PostIngredient(Ingredient ingredient)
        {

            await CheckIfIngredientExistsAndUpdateOrAdd(ingredient);

            await _context.SaveChangesAsync();
            return CreatedAtAction("GetIngredient", new { id = ingredient.IngredientId }, ingredient);
        }

        /// <summary>
        /// Checks if ingredient exists and update it if yes or adds a new if not.
        /// </summary>
        /// <param name="ingredient">The ingredient.</param>
        public async Task CheckIfIngredientExistsAndUpdateOrAdd(Ingredient ingredient)
        {
            if (await _context.Ingredients.AnyAsync(x => x.Name.Equals(ingredient.Name)))
            {
                var existing = await _context.Ingredients.FirstAsync(x => x.Name.Equals(ingredient.Name));
                ingredient.IngredientId = existing.IngredientId;
                _context.Ingredients.Update(existing)?.CurrentValues?.SetValues(ingredient);
            }
            else
            {
                _context.Ingredients.Add(ingredient);
            }
        }

        // DELETE: api/Ingredients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredient(Guid id)
        {
            var ingredient = await _context.Ingredients.FindAsync(id);
            if (ingredient == null)
            {
                return NotFound();
            }

            _context.Ingredients.Remove(ingredient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IngredientExists(Guid id)
        {
            return _context.Ingredients.Any(e => e.IngredientId == id);
        }
    }
}
