using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MenuPlanner.Server.SqlImplementation;
using MenuPlanner.Shared.models;

namespace MenuPlanner.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenusController : ControllerBase
    {
        private readonly MenuPlannerContext _context;

        public MenusController(MenuPlannerContext context)
        {
            _context = context;
        }

        // GET: api/Menus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Menu>>> GetMenus()
        {
            return await _context.Menus.ToListAsync();
        }

        // GET: api/Menus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Menu>> GetMenu(Guid id)
        {
            var menu = await _context.Menus.FindAsync(id);

            if (menu == null)
            {
                return NotFound();
            }
            //load ingredients
            await _context.Entry(menu).Collection(m => m.Ingredients).LoadAsync();
            menu.Ingredients.ForAll(m => _context.Entry(m).Reference(i => i.Ingredient).Load());
            //load images
            await _context.Entry(menu).Collection(m => m.Images).LoadAsync();
            return menu;
        }

        /// <summary>
        /// GET: api/Menus/Images/5
        /// </summary>
        /// <param name="id">Menu Id</param>
        /// <returns>Image Collection</returns>
        [HttpGet("Images/{id}")]
        public async Task<ActionResult<List<Image>>> GetImages(Guid id)
        {
            var menu = await _context.Menus.FindAsync(id);

            if (menu == null)
            {
                return NotFound();
            }
            //load images
            await _context.Entry(menu).Collection(m => m.Images).LoadAsync();
            return Ok(menu.Images);
        }

        /// <summary>
        /// GET: api/Menus/Filter?contains=string
        /// </summary>
        /// <returns>List of Menu</returns>
        [HttpGet("Filter")]
        public async Task<ActionResult<IEnumerable<Menu>>> GetFilteredIngredients()
        {
            var partOfName = HttpContext.Request.Query["contains"].ToString().ToLower();
            if (!string.IsNullOrEmpty(partOfName))
            {
                return await _context.Menus.Where(x => x.Name.ToLower().Contains(partOfName)
                       || x.Description.ToLower().Contains(partOfName)).ToListAsync();
            }
            return new List<Menu>();
        }

        // PUT: api/Menus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMenu(Guid id, Menu menu)
        {
            if (id != menu.MenuId)
            {
                return BadRequest();
            }

            _context.Update(menu).CurrentValues.SetValues(menu);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuExists(id))
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

        // POST: api/Menus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Menu>> PostMenu(Menu menu)
        {
            _context.Menus.Add(menu);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMenu", new { id = menu.MenuId }, menu);
        }

        // DELETE: api/Menus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu(Guid id)
        {
            var menu = await _context.Menus.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }

            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MenuExists(Guid id)
        {
            return _context.Menus.Any(e => e.MenuId == id);
        }
    }
}
