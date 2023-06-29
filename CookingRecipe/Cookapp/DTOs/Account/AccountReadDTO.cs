namespace Cookapp.DTOs.Account
{
    public class AccountReadDTO
    {
        public string Id { get; set; }

        public string RoleName { get; set; }

        public string Phone { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string FullName { get; set; }

        public DateTime CreateAt { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
    }
}
