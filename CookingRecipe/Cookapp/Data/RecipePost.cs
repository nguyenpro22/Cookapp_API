using System;
using System.Collections.Generic;

namespace Cookapp.Data;

public partial class RecipePost
{
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string RefTag { get; set; } = null!;

    public string Content { get; set; } = null!;

    public TimeSpan CreateTime { get; set; }

    public TimeSpan UpdateTime { get; set; }

    public string RefCategory { get; set; } = null!;

    public string? LocationId { get; set; }

    public string? RefAccount { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual SupplierStore? Location { get; set; }

    public virtual Account? RefAccountNavigation { get; set; }
}
