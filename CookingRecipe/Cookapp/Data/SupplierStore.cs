using System;
using System.Collections.Generic;

namespace Cookapp.Data;

public partial class SupplierStore
{
    public string Id { get; set; } = null!;

    public double? X { get; set; }

    public double? Y { get; set; }

    public string Andress { get; set; } = null!;

    public string? RefAccount { get; set; }

    public virtual ICollection<RecipePost> RecipePosts { get; set; } = new List<RecipePost>();

    public virtual Account? RefAccountNavigation { get; set; }
}
