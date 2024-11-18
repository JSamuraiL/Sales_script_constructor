﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesScriptConstructor.Domain.Entities;
using SalesScriptConstructor.Domain.Interfaces.IManagers;

namespace SalesScriptConstructor.API.Controllers
{
    [Route("api/managers")]
    [ApiController]
    public class ManagersController : ControllerBase
    {
        private readonly IManagersService _managersService;

        public ManagersController(IManagersService managersService)
        {
            _managersService = managersService;
        }

        // GET: api/Managers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Manager>> GetManager(Guid id)
        {
            var manager = await _managersService.GetManagerByIdAsync(id);

            if (manager == null)
            {
                return NotFound("Менеджера с таким Id не существует");
            }

            return manager;
        }
        
        // PUT: api/Managers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> ChangeManagerDetails(Guid id, Manager manager)
        {
            try
            {
                await _managersService.UpdateManagerAsync(id, manager);
            }
            catch (ArgumentException)
            {
                return BadRequest("Ваш Id не соответствует Id в Запросе");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_managersService.ManagerExists(id))
                {
                    return NotFound("Менеджера с таким Id не существует");
                }
                throw;
            }

            return NoContent();
        }

        // POST: api/Managers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Manager>> PostManager(Manager manager)
        {
            try
            {
                await _managersService.AddManagerAsync(manager);
            }
            catch (DbUpdateException)
            {
                if (_managersService.ManagerExists(manager.Id))
                {
                    return Conflict("Менеджер с таким Id уже существует");
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetManager", new { id = manager.Id }, manager);
        }

        // DELETE: api/Managers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManager(Guid id)
        {
            try 
            { 
                await _managersService.DeleteManagerAsync(id);
            }
            catch (ArgumentNullException)
            {
                return NotFound("Менеджера с таким Id не существует");
            }
            return NoContent();
        }
    }
}
