using Cookapp_API.DataAccess.DTO;
using Cookapp_API.DTOs.Account;

namespace Cookapp_API.Model
{
    public class AccountModel
    {
        public AccountDTO AccountDTO { get; set; } = new AccountDTO();
        public Token Token { get; set; } = new Token();
    }
}
