using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cookapp_API.Data;
using Cookapp_API.DataAccess.BLL;
using Cookapp_API.DataAccess.DTO;

namespace Cookapp_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly CookingRecipeDbContext _context;
        private IConfiguration _configuration;
        public AccountsController(CookingRecipeDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult<List<AccountDTO>>> GetAccounts()
        {
          if (_context.Accounts == null)
          {
              return NotFound();
          }
            //return await _context.Accounts.ToListAsync();
            AccountBLL bll = new AccountBLL(_configuration["ConnectionStrings:CookappDB"], DataAccess.ESqlProvider.SQLSERVER, 120);
            List<AccountDTO> accounts = bll.GetAccounts();
            return accounts;
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(string id)
        {
          if (_context.Accounts == null)
          {
              return NotFound();
          }
            var account = await _context.Accounts.FindAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

        // PUT: api/Accounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<AccountDTO>> PutAccount(string id, AccountDTO account)
        {
            
            if (id != account.Id)
            {
                return Problem("Account Id is not match"); ;
            }
            AccountBLL bll = new AccountBLL(_configuration["ConnectionStrings:CookappDB"], DataAccess.ESqlProvider.SQLSERVER, 120);
            bll.UpdateAccount(id, account);
            return account;

        }

        // POST: api/Accounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AccountDTO>> PostAccount(AccountDTO account)
        {
          if (_context.Accounts == null)
          {
              return Problem("Entity set 'CookingRecipeDbContext.Accounts'  is null.");
          }
            
            AccountBLL bll = new AccountBLL(_configuration["ConnectionStrings:CookappDB"], DataAccess.ESqlProvider.SQLSERVER, 120);
            bll.CreateAccount(account);
            return account;
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(string id)
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccountExists(string id)
        {
            return (_context.Accounts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
