namespace Cookapp_API.DataAccess.DTO.AllInOneDTO.AccountDTO
{
    public class LoginResponse
    {
        public string Id { get; set; } = null!;

        public string username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Roleid { get; set; } = null!;

        
        public Token token { get; set; } = new Token();
    }
}
