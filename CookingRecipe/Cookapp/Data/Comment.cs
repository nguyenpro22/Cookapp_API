using System;
using System.Collections.Generic;

namespace Cookapp.Data;

public partial class Comment
{
    public string Id { get; set; } = null!;

    public string Content { get; set; } = null!;

    public TimeSpan CreateTime { get; set; }

    public string RefUser { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string? RefPost { get; set; }

    public virtual RecipePost? RefPostNavigation { get; set; }
}
