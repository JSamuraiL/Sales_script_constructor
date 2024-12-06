﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesScriptConstructor.Domain.Entities;
using SalesScriptConstructor.Domain.Interfaces.IScripts;

namespace SalesScriptConstructor.API.Controllers
{
    [Route("api/scripts")]
    [ApiController]
    public class ScriptsController : ControllerBase
    {
        private readonly IScriptsService _scriptsService;
        public ScriptsController(IScriptsService scriptsService)
        {
            _scriptsService = scriptsService ?? throw new ArgumentNullException();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Script>> GetScript(int id)
        {
            try
            {
                return await _scriptsService.GetScriptByIdAsync(id);
            }
            catch (ArgumentNullException)
            {
                return NotFound("Скрипт с таким Id не существует");
            }
            catch (Exception)
            {
                return StatusCode(500, "Неизвестная ошибка");
            }
        }

        [HttpGet("manager/{ManagerId}")]
        public async Task<IEnumerable<Script>> GetLinkedScripts(Guid ManagerId) 
        {
            try { 
                return await _scriptsService.GetScriptsByManagerIdAsync(ManagerId);
            }
            catch (Exception)
            {
                return (IEnumerable<Script>)StatusCode(500, "Неизвестная ошибка");
            }
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<Script>> CreateScript(Script script) 
        {
            try 
            {
                await _scriptsService.AddScriptAsync(script);
            }
            catch (DbUpdateException) 
            {
                if (_scriptsService.ScriptExists(script.Id)) 
                {
                    return BadRequest("Скрипт с таким id уже существует");
                }
                else throw;
            }
            catch (Exception)
            {
                return StatusCode(500, "Неизвестная ошибка");
            }
            return CreatedAtAction("GetScript", new { id = script.Id }, script);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Script>> DeleteScript(int id) 
        {
            try
            {
                await _scriptsService.DeleteScriptAsync(id);
            }
            catch (ArgumentNullException) 
            {
                return NotFound("Скрипта с таким id не существует");
            }
            catch (Exception)
            {
                return StatusCode(500, "Неизвестная ошибка");
            }
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Script>> UpdateScript(Script script,int id) 
        {
            try
            {
                await _scriptsService.UpdateScriptAsync(script, id);
            }
            catch (ArgumentNullException)
            {
                return NotFound("Скрипта с таким id не существует");
            }
            catch (ArgumentOutOfRangeException) 
            {
                return BadRequest("Ваш Id не соответствует Id в запросе");
            }
            catch (Exception)
            {
                return StatusCode(500, "Неизвестная ошибка");
            }
            return NoContent();
        }
    }
}
