using System;
using System.Collections.Generic;

namespace Cookapp_API.Data;

public partial class Account
{
    public string Id { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Roleid { get; set; } = null!;

    public DateTime? Dob { get; set; }

    public bool IsActive { get; set; }

    public string FullName { get; set; } = null!;

    public virtual ICollection<RecipePost> RecipePosts { get; set; } = new List<RecipePost>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<SupplierStore> SupplierStores { get; set; } = new List<SupplierStore>();
}
