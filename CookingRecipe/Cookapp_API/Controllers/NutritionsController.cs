using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cookapp_API.Data;

namespace Cookapp_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NutritionsController : ControllerBase
    {
        private readonly CookingRecipeDbContext _context;

        public NutritionsController(CookingRecipeDbContext context)
        {
            _context = context;
        }

        // GET: api/Nutritions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Nutrition>>> GetNutritions()
        {
          if (_context.Nutritions == null)
          {
              return NotFound();
          }
            return await _context.Nutritions.ToListAsync();
        }

        // GET: api/Nutritions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Nutrition>> GetNutrition(string id)
        {
          if (_context.Nutritions == null)
          {
              return NotFound();
          }
            var nutrition = await _context.Nutritions.FindAsync(id);

            if (nutrition == null)
            {
                return NotFound();
            }

            return nutrition;
        }

        // PUT: api/Nutritions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNutrition(string id, Nutrition nutrition)
        {
            if (id != nutrition.Id)
            {
                return BadRequest();
            }

            _context.Entry(nutrition).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NutritionExists(id))
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

        // POST: api/Nutritions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Nutrition>> PostNutrition(Nutrition nutrition)
        {
          if (_context.Nutritions == null)
          {
              return Problem("Entity set 'CookingRecipeDbContext.Nutritions'  is null.");
          }
            _context.Nutritions.Add(nutrition);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (NutritionExists(nutrition.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetNutrition", new { id = nutrition.Id }, nutrition);
        }

        // DELETE: api/Nutritions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNutrition(string id)
        {
            if (_context.Nutritions == null)
            {
                return NotFound();
            }
            var nutrition = await _context.Nutritions.FindAsync(id);
            if (nutrition == null)
            {
                return NotFound();
            }

            _context.Nutritions.Remove(nutrition);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NutritionExists(string id)
        {
            return (_context.Nutritions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
