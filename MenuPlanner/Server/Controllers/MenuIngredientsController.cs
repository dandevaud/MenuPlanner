using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MenuPlanner.Server.SqlImplementation;
using MenuPlanner.Shared.models;

namespace MenuPlanner.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuIngredientsController : ControllerBase
    {
        private readonly MenuPlannerContext _context;

        public MenuIngredientsController(MenuPlannerContext context)
        {
            _context = context;
        }

        // GET: api/MenuIngredients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuIngredient>>> GetMenuIngredients()
        {
            return await _context.MenuIngredients.ToListAsync();
        }

        // GET: api/MenuIngredients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuIngredient>> GetMenuIngredient(Guid id)
        {
            var menuIngredient = await _context.MenuIngredients.FindAsync(id);

            if (menuIngredient == null)
            {
                return NotFound();
            }

            return menuIngredient;
        }

        // PUT: api/MenuIngredients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMenuIngredient(Guid id, MenuIngredient menuIngredient)
        {
            if (id != menuIngredient.Id)
            {
                return BadRequest();
            }

            _context.Entry(menuIngredient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuIngredientExists(id))
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

        // POST: api/MenuIngredients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MenuIngredient>> PostMenuIngredient(MenuIngredient menuIngredient)
        {
            _context.MenuIngredients.Add(menuIngredient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMenuIngredient", new { id = menuIngredient.Id }, menuIngredient);
        }

        // DELETE: api/MenuIngredients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenuIngredient(Guid id)
        {
            var menuIngredient = await _context.MenuIngredients.FindAsync(id);
            if (menuIngredient == null)
            {
                return NotFound();
            }

            _context.MenuIngredients.Remove(menuIngredient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MenuIngredientExists(Guid id)
        {
            return _context.MenuIngredients.Any(e => e.Id == id);
        }
    }
}
