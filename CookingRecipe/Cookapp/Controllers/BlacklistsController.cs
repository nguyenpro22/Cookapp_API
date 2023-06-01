using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cookapp.Data;

namespace Cookapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlacklistsController : ControllerBase
    {
        private readonly CookingRecipeDbContext _context;

        public BlacklistsController(CookingRecipeDbContext context)
        {
            _context = context;
        }

        // GET: api/Blacklists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Blacklist>>> GetBlacklists()
        {
          if (_context.Blacklists == null)
          {
              return NotFound();
          }
            return await _context.Blacklists.ToListAsync();
        }

        // GET: api/Blacklists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Blacklist>> GetBlacklist(string id)
        {
          if (_context.Blacklists == null)
          {
              return NotFound();
          }
            var blacklist = await _context.Blacklists.FindAsync(id);

            if (blacklist == null)
            {
                return NotFound();
            }

            return blacklist;
        }

        // PUT: api/Blacklists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlacklist(string id, Blacklist blacklist)
        {
            if (id != blacklist.RefUser)
            {
                return BadRequest();
            }

            _context.Entry(blacklist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlacklistExists(id))
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

        // POST: api/Blacklists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Blacklist>> PostBlacklist(Blacklist blacklist)
        {
          if (_context.Blacklists == null)
          {
              return Problem("Entity set 'CookingRecipeDbContext.Blacklists'  is null.");
          }
            _context.Blacklists.Add(blacklist);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BlacklistExists(blacklist.RefUser))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBlacklist", new { id = blacklist.RefUser }, blacklist);
        }

        // DELETE: api/Blacklists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlacklist(string id)
        {
            if (_context.Blacklists == null)
            {
                return NotFound();
            }
            var blacklist = await _context.Blacklists.FindAsync(id);
            if (blacklist == null)
            {
                return NotFound();
            }

            _context.Blacklists.Remove(blacklist);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BlacklistExists(string id)
        {
            return (_context.Blacklists?.Any(e => e.RefUser == id)).GetValueOrDefault();
        }
    }
}
