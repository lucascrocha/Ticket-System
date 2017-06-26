namespace Projeto.Pessoa
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Projeto.Base;
    using Projeto.Pessoa.Model;

    [Produces("application/json")]
    [Route("api/Pessoa")]
    public class PessoaController : Controller
    {
        private readonly Context _context;

        public PessoaController(Context context)
        {
            _context = context;
        }

        // GET: api/Pessoa
        [HttpGet]
        public IEnumerable<Pessoa> GetPessoa()
        {
            return _context.Pessoa;
        }

        // GET: api/Pessoa/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPessoa([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pessoa = await _context.Pessoa.SingleOrDefaultAsync(m => m.Id == id);

            if (pessoa == null)
            {
                return NotFound();
            }

            return Ok(pessoa);
        }

        // PUT: api/Pessoa/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPessoa([FromRoute] int id, [FromBody] Pessoa pessoa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pessoa.Id)
            {
                return BadRequest();
            }

            _context.Entry(pessoa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PessoaExists(id))
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

        // POST: api/Pessoa
        [HttpPost]
        public async Task<IActionResult> PostPessoa([FromBody] Pessoa pessoa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Pessoa.Add(pessoa);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPessoa", new { id = pessoa.Id }, pessoa);
        }

        // DELETE: api/Pessoa/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePessoa([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pessoa = await _context.Pessoa.SingleOrDefaultAsync(m => m.Id == id);
            if (pessoa == null)
            {
                return NotFound();
            }

            _context.Pessoa.Remove(pessoa);
            await _context.SaveChangesAsync();

            return Ok(pessoa);
        }

        private bool PessoaExists(int id)
        {
            return _context.Pessoa.Any(e => e.Id == id);
        }
    }
}