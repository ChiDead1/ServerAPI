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
    public class NovelController : ControllerBase
    {
        private readonly MasterContext _context;

        public NovelController(MasterContext context)
        {
            _context = context;
        }

        // GET: api/Novel
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Novel>>> GetNovel()
        {
            return await _context.Novel.ToListAsync();
        }

        // GET: api/Novel/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Novel>> getNovelId(int id)
        {
            var Novel = await _context.Novel.FindAsync(id);

            if (Novel == null)
            {
                return NotFound();
            }

            return Novel;
        }

        // PUT: api/Novel/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNovel(int id, Novel Novel)
        {
            if (id != Novel.Id)
            {
                return BadRequest();
            }

            _context.Entry(Novel).State = EntityState.Modified;

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

        // POST: api/Novel
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Novel>> PostNovel(Novel Novel)
        {
            _context.Novel.Add(Novel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNovel", new { id = Novel.Id }, Novel);
        }

        // DELETE: api/Novel/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNovel(int id)
        {
            var Novel = await _context.Novel.FindAsync(id);
            if (Novel == null)
            {
                return NotFound();
            }

            _context.Novel.Remove(Novel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NovelExists(int id)
        {
            return _context.Novel.Any(e => e.Id == id);
        }
    }
}