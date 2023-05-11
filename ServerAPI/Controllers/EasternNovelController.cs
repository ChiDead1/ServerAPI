using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary584;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServerAPI.Controllers
{ 
 [Route("api/[controller]")]
[ApiController]
public class EasternNovelLibaryController : ControllerBase
{
    private readonly MasterContext _context;

    public EasternNovelLibaryController(MasterContext context)
    {
        _context = context;
    }

    // GET: api/EasternNovelLibary
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EasternNovelLibary>>> GetEasternNovelLibary()
    {
        return await _context.EasternNovelLibary.ToListAsync();
    }

    // GET: api/EasternNovelLibary/5
    [HttpGet("{id}")]
  
        public async Task<ActionResult<EasternNovelLibary>> GeteasternNovelLibary(int id)
    {
        var easternNovelLibary = await _context.EasternNovelLibary.FindAsync(id);

        if (easternNovelLibary == null)
        {
            return NotFound();
        }

        return easternNovelLibary;
    }

    // PUT: api/EasternNovelLibary/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
        //[Authorize(Roles = "RegisteredUser")]
        public async Task<IActionResult> PuteasternNovelLibary(int id, EasternNovelLibary easternNovelLibary)
    {
        if (id != easternNovelLibary.Id)
        {
            return BadRequest();
        }

        _context.Entry(easternNovelLibary).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!easternNovelLibaryExists(id))
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

    // POST: api/EasternNovelLibary
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
       // [Authorize(Roles = "RegisteredUser")]
        public async Task<ActionResult<EasternNovelLibary>> PosteasternNovelLibary(EasternNovelLibary easternNovelLibary)
    {
        _context.EasternNovelLibary.Add(easternNovelLibary);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GeteasternNovelLibary", new { id = easternNovelLibary.Id }, easternNovelLibary);
    }

    // DELETE: api/EasternNovelLibary/5
    [HttpDelete("{id}")]
       // [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteeasternNovelLibary(int id)
    {
        var easternNovelLibary = await _context.EasternNovelLibary.FindAsync(id);
        if (easternNovelLibary == null)
        {
            return NotFound();
        }

        _context.EasternNovelLibary.Remove(easternNovelLibary);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool easternNovelLibaryExists(int id)
    {
        return _context.EasternNovelLibary.Any(e => e.Id == id);
    }
}
}
