using System;
using System.Collections.Generic;

namespace Cookapp_API.Data;

public partial class Recipepost
{
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string RefTag { get; set; } = null!;

    public string Content { get; set; } = null!;

    public TimeSpan CreateTime { get; set; }

    public TimeSpan UpdateTime { get; set; }

    public string RefCategory { get; set; } = null!;

    public string RefAccount { get; set; } = null!;

    public string? RefImage { get; set; }

    public string? RefVideo { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Account RefAccountNavigation { get; set; } = null!;

    public virtual Image? RefImageNavigation { get; set; }

    public virtual Video? RefVideoNavigation { get; set; }
}
