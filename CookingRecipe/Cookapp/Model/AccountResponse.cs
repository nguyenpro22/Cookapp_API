using Cookapp.DTOs.Account;

namespace Cookapp.Model
{
    public class AccountResponse
    {
        
        public string phone { get; set; }
        public string fullName { get; set; }
        public Token token { get; set; } = new Token();
    }
}
