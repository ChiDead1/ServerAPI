using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Diagnostics.Metrics;
using ClassLibrary584;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NovelLibaryController : ControllerBase
    {
        private readonly MasterContext _context;

        public NovelLibaryController(MasterContext context)
        {
            _context = context;
        }

        // GET: api/NovelLibary
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NovelLibary>>> GetNovelLibary()
        {
            return await _context.NovelL.ToListAsync();
        }

        // GET: api/NovelLibary/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NovelLibary>> getNovelLibaryId(int id)
        {
            var NovelL = await _context.NovelL.FindAsync(id);

            if (NovelL == null)
            {
                return NotFound();
            }

            return NovelL;
        }

        // PUT: api/NovelLibary/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNovelLibary(int id, NovelLibary NovelL)
        {
            if (id != NovelL.Id)
            {
                return BadRequest();
            }

            _context.Entry(NovelL).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NovelExists(id))
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

        // POST: api/NovelLibary
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NovelLibary>> PostNovelLibary(NovelLibary NovelL)
        {
            _context.NovelL.Add(NovelL);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNovelL", new { id = NovelL.Id }, NovelL);
        }

        // DELETE: api/NovelLibary/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNovelLibary(int id)
        {
            var NovelL = await _context.NovelL.FindAsync(id);
            if (NovelL == null)
            {
                return NotFound();
            }

            _context.NovelL.Remove(NovelL);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NovelExists(int id)
        {
            return _context.NovelL.Any(e => e.Id == id);
        }
    }
}