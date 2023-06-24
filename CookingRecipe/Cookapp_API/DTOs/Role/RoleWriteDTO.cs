using System.ComponentModel.DataAnnotations;

namespace Cookapp_API.DTOs.Role
{
    public class RoleWriteDTO
    {

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string RoleName { get; set; }


    }
}
