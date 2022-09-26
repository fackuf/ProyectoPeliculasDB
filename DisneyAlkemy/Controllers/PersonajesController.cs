using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DisneyAlkemy.Data.DataModels;
using DisneyAlkemy.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using DisneyAlkemy.Helpers;

namespace DisneyAlkemy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonajesController : ControllerBase
    {
        private readonly DisneyContext _context;

        public PersonajesController(DisneyContext context)
        {
            _context = context;
        }

        // GET: api/Personajes
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<ActionResult<IEnumerable<Personaje>>> Getpersonajes()
        {
            return await _context.personajes.ToListAsync();
        }

        // GET: api/Personajes/5
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<ActionResult<Personaje>> GetPersonaje(int id)
        {
            var personaje = await _context.personajes.FindAsync(id);

            if (personaje == null)
            {
                return NotFound();
            }

            return personaje;
        }

        // PUT: api/Personajes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> PutPersonaje(int id, Personaje personaje)
        {
            if (id != personaje.Id)
            {
                return BadRequest();
            }

            _context.Entry(personaje).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonajeExists(id))
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

        // POST: api/Personajes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<ActionResult<Personaje>> PostPersonaje(Personaje personaje)
        {
            _context.personajes.Add(personaje);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersonaje", new { id = personaje.Id }, personaje);
        }

        // DELETE: api/Personajes/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> DeletePersonaje(int id)
        {
            var personaje = await _context.personajes.FindAsync(id);
            if (personaje == null)
            {
                return NotFound();
            }

            _context.personajes.Remove(personaje);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonajeExists(int id)
        {
            return _context.personajes.Any(e => e.Id == id);
        }

        
        [Route("/characters")]
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
      
        public ActionResult<string> Listado()
        {
            var listado = new Personaje();
            var datos = _context.personajes.ToList();
          try
            {
                foreach(var pj in datos)
                {
                    listado.imagenPersonaje = pj.imagenPersonaje;
                    listado.nombre = pj.nombre;
                }
                return listado.imagenPersonaje+listado.nombre;

            }
            catch(Exception ex) 
            {
                throw new Exception("Error", ex);
            }
            
        }

        [HttpGet]
        [Route("/charactersdetails")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        public async Task<ActionResult<IEnumerable<Personaje>>> PersonajesDetail()
        {
            return await _context.personajes.ToListAsync();
        }
    }

}
