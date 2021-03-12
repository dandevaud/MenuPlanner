// <copyright file="MenusController.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using MenuPlanner.Server.Contracts.Logic;
using MenuPlanner.Server.Data;
using MenuPlanner.Server.Logic;
using MenuPlanner.Server.Logic.EntityUpdater;
using MenuPlanner.Shared.models;
using MenuPlanner.Shared.models.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MenuPlanner.Server.Controllers
{
    /// <summary>Menu API Controller --&gt; handle all CRUD actions for the Menus</summary>
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class MenusController : ControllerBase
    {
        private readonly MenuPlannerContext _context;
        private readonly IMenuEntityUpdater _entityUpdater;
        private readonly ISearchLogic _search;

        public MenusController(MenuPlannerContext context, IMenuEntityUpdater entityUpdater, ISearchLogic searchLogic)
        {
            _context = context;
            _entityUpdater = entityUpdater;
            _search = searchLogic;
        }

        // GET: api/Menus
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<SearchResponseModel<Menu>>> GetMenus([FromQuery] SearchRequestModel searchRequest)
        {
            return await _search.GetAllMenus(searchRequest);
        }

        [HttpGet("MaxTimes")]
        [AllowAnonymous]
        public async Task<ActionResult<Dictionary<String, int>>> GetMaxTimes()
        {
            var toReturn = await _search.GetMaxTimes();
            return toReturn;
        }
            

        // GET: api/Menus/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Menu>> GetMenu(Guid id)
        {
            var menu = await _search.SearchMenus(new MenuSearchRequestModel()
            {
                Id = id
            });

            if (menu.Result.IsNullOrEmpty())
            {
                return NotFound();
            }

            return menu.Result[0];
        }

        /// <summary>
        /// Get images by Menu Id. GET: api/Menus/Images/5
        /// </summary>
        /// <param name="id">Menu Id</param>
        /// <returns>Image Collection</returns>
        [HttpGet("Images/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Image>>> GetImages(Guid id)
        {
            var menu = await _search.SearchMenus(new MenuSearchRequestModel() { Id = id });

            if (menu.Result.IsNullOrEmpty())
            {
                return NotFound();
            }
            //load images

            return Ok(menu.Result[0].Images);
        }

        // PUT: api/Menus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMenu(Guid id, Menu menu)
        {
            if (id != menu.Id)
            {
                return BadRequest();
            }

            await _entityUpdater.UpdateMenuInContext(menu);

            return NoContent();
        }


        // POST: api/Menus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Menu>> PostMenu(Menu menu)
        {

            await _entityUpdater.UpdateMenuInContext(menu);

            return CreatedAtAction("GetMenu", new { id = menu.Id }, menu);
        }

        // DELETE: api/Menus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu(Guid id)
        {


            await _entityUpdater.DeleteMenuFromDatabase(new Menu() { Id = id });

            return NoContent();
        }

    }
}
