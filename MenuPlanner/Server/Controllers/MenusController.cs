using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Internal;
using MenuPlanner.Server.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MenuPlanner.Server.SqlImplementation;
using MenuPlanner.Shared.models;
using Microsoft.AspNetCore.Authorization;

namespace MenuPlanner.Server.Controllers
{


    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
     public class MenusController : ControllerBase
    {
        private readonly MenuPlannerContext _context;
        private readonly EntityUpdater entityUpdater;


        public MenusController(MenuPlannerContext context)
        {
            _context = context;
            entityUpdater = new EntityUpdater(context);
           
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
            
                menu?.Ingredients?.ToList().ForEach(m =>
                    _context.Entry(m).Reference<Ingredient>(
                            i => i.Ingredient
                        )
                        .Load());
            
           

            //load images
            await _context.Entry(menu).Collection(m => m.Images).LoadAsync();
            //load Comments
            await _context.Entry(menu).Collection(m => m.Comments).LoadAsync();
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

       // PUT: api/Menus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMenu(Guid id, Menu menu)
        {
            if (id != menu.MenuId)
            {
                return BadRequest();
            }
            
            //to improve -> remove all the images from this menu
            var foundMenu = await _context.Menus.FindAsync(id);
            if (foundMenu == null)
            {
                return NotFound();
            }

            await entityUpdater.UpdateMenuInContext(menu, foundMenu);


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
