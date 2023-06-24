namespace Cookapp_API.DTOs.Account
{
    public class Token
    {
        public string Access_token { get; set; }
        public string Id { get; set; }
        public int Expires_in { get; set; }
    }   
}
