using System;
using System.Collections.Generic;

namespace Cookapp_API.Data;

public partial class Account
{
    public string Id { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Roleid { get; set; } = null!;

    public DateTime? Dob { get; set; }

    public bool IsActive { get; set; }

    public string FullName { get; set; } = null!;


}
