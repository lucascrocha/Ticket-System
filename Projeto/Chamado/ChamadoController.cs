namespace Projeto.Chamado
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Projeto.Base;
    using Projeto.Chamado.Model;

    [Produces("application/json")]
    [Route("api/Chamado")]
    public class ChamadoController : Controller
    {
        private readonly Context _context;

        public ChamadoController(Context context)
        {
            _context = context;
        }

        // GET: api/Chamado
        [HttpGet]
        public IEnumerable<Chamado> GetChamado()
        {
            return _context.Chamado;
        }

        // GET: api/Chamado/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetChamado([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var chamado = await _context.Chamado.SingleOrDefaultAsync(m => m.Id == id);

            if (chamado == null)
            {
                return NotFound();
            }

            return Ok(chamado);
        }

        // PUT: api/Chamado/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChamado([FromRoute] int id, [FromBody] Chamado chamado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != chamado.Id)
            {
                return BadRequest();
            }

            _context.Entry(chamado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChamadoExists(id))
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

        // POST: api/Chamado
        [HttpPost]
        public async Task<IActionResult> PostChamado([FromBody] Chamado chamado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Chamado.Add(chamado);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChamado", new { id = chamado.Id }, chamado);
        }

        // DELETE: api/Chamado/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChamado([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var chamado = await _context.Chamado.SingleOrDefaultAsync(m => m.Id == id);
            if (chamado == null)
            {
                return NotFound();
            }

            _context.Chamado.Remove(chamado);
            await _context.SaveChangesAsync();

            return Ok(chamado);
        }

        private bool ChamadoExists(int id)
        {
            return _context.Chamado.Any(e => e.Id == id);
        }
    }
}