// <copyright file="IngredientsController.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MenuPlanner.Server.SqlImplementation;
using MenuPlanner.Shared.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MenuPlanner.Server.Controllers
{
    [Authorize]
    [Produces("application/json")]
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

        // GET: api/Ingredients/Filter?contains=xy
        [HttpGet("Filter")]
        public async Task<ActionResult<IEnumerable<Ingredient>>> GetFilteredIngredients()
        {
            var partOfName = HttpContext.Request.Query["contains"].ToString().ToLower();
            if (!string.IsNullOrEmpty(partOfName))
            {
                return await _context.Ingredients.Where(x => x.Name.ToLower().Contains(partOfName)).ToListAsync();
            }
            return new List<Ingredient>();
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Ingredient>> PostIngredient(Ingredient ingredient)
        {

            if (await _context.Ingredients.AnyAsync(x => x.Name.Equals(ingredient.Name)))
            {
                var existing = await _context.Ingredients.FirstAsync(x => x.Name.Equals(ingredient.Name));
                ingredient.IngredientId = existing.IngredientId;
                _context.Entry(existing).CurrentValues.SetValues(ingredient);
            }
            else
            {
                _context.Ingredients.Add(ingredient);
            }

            await _context.SaveChangesAsync();
            return CreatedAtAction("GetIngredient", new { id = ingredient.IngredientId }, ingredient);
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
