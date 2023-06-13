using Microsoft.CodeAnalysis.Scripting;
using Org.BouncyCastle.Crypto.Generators;

namespace Cookapp_API.DTOs.Account
{
    public class AccountWriteDTO
    {
        public string Phone { get; set; }

        public string Password
        {
            get { return password; }
            set { password = EncryptPassword(value); }
        }

        public string FullName { get; set; }

        private string password;

        public static string EncryptPassword(string pass)
        {
            return BCrypt.Net.BCrypt.HashPassword(pass);
        }
    }
    public class AccountUpdateDTO
    {
        public string? FullName { get; set; }
        public string? DateOfBirth { get; set; }
        public string? Description { get; set; }
    }

    public class ChangePasswordDTO
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword
        {
            get { return password; }
            set { password = EncryptPassword(value); }
        }
        [Required]
        public string ConfirmPassword
        {
            get { return password; }
            set { password = EncryptPassword(value); }
        }

        private string password;
        public static string EncryptPassword(string pass)
        {
            return BCrypt.Net.BCrypt.HashPassword(pass);
        }
    }

    public class AssignRoleAccountModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string RoleId { get; set; }
    }

}
