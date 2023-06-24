using Cookapp_API.Enum;

namespace Cookapp_API.DTOs.Account
{
    public class AccountQuery
    {
        public AccountRole? Role { get; set; } = AccountRole.customer;
        public bool? isActive { get; set; } = false;
    }
}
