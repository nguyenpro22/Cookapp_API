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
    public class RecipePostsController : ControllerBase
    {
        private readonly CookingRecipeDbContext _context;

        public RecipePostsController(CookingRecipeDbContext context)
        {
            _context = context;
        }

        // GET: api/RecipePosts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipepost>>> GetRecipePosts()
        {
          if (_context.Recipeposts == null)
          {
              return NotFound();
          }
            return await _context.Recipeposts.ToListAsync();
        }

        // GET: api/RecipePosts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Recipepost>> GetRecipePost(string id)
        {
          if (_context.Recipeposts == null)
          {
              return NotFound();
          }
            var recipePost = await _context.Recipeposts.FindAsync(id);

            if (recipePost == null)
            {
                return NotFound();
            }

            return recipePost;
        }

        // PUT: api/RecipePosts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipePost(string id, Recipepost recipePost)
        {
            if (id != recipePost.Id)
            {
                return BadRequest();
            }

            _context.Entry(recipePost).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipePostExists(id))
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

        // POST: api/RecipePosts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Recipepost>> PostRecipePost(Recipepost recipePost)
        {
          if (_context.Recipeposts == null)
          {
              return Problem("Entity set 'CookingRecipeDbContext.RecipePosts'  is null.");
          }
            _context.Recipeposts.Add(recipePost);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RecipePostExists(recipePost.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRecipePost", new { id = recipePost.Id }, recipePost);
        }

        // DELETE: api/RecipePosts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipePost(string id)
        {
            if (_context.Recipeposts == null)
            {
                return NotFound();
            }
            var recipePost = await _context.Recipeposts.FindAsync(id);
            if (recipePost == null)
            {
                return NotFound();
            }

            _context.Recipeposts.Remove(recipePost);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecipePostExists(string id)
        {
            return (_context.Recipeposts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
