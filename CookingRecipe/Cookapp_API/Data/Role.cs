using System;
using System.Collections.Generic;

namespace Cookapp_API.Data;

public partial class Role
{
    public string RoleId { get; set; } = null!;

    public string RoleName { get; set; } = null!;

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
