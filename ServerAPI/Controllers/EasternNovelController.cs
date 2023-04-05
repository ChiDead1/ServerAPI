using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary584;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    public async Task<ActionResult<EasternNovelLibary>> GetCity(int id)
    {
        var city = await _context.EasternNovelLibary.FindAsync(id);

        if (city == null)
        {
            return NotFound();
        }

        return city;
    }

    // PUT: api/EasternNovelLibary/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCity(int id, EasternNovelLibary city)
    {
        if (id != city.Id)
        {
            return BadRequest();
        }

        _context.Entry(city).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CityExists(id))
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
    public async Task<ActionResult<EasternNovelLibary>> PostCity(EasternNovelLibary city)
    {
        _context.EasternNovelLibary.Add(city);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetCity", new { id = city.Id }, city);
    }

    // DELETE: api/EasternNovelLibary/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCity(int id)
    {
        var city = await _context.EasternNovelLibary.FindAsync(id);
        if (city == null)
        {
            return NotFound();
        }

        _context.EasternNovelLibary.Remove(city);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CityExists(int id)
    {
        return _context.EasternNovelLibary.Any(e => e.Id == id);
    }
}
}
