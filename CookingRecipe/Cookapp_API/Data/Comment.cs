using System;
using System.Collections.Generic;

namespace Cookapp_API.Data;

public partial class Comment
{
    public string Id { get; set; } = null!;

    public string Content { get; set; } = null!;

    public TimeSpan CreateTime { get; set; }

    public string RefUser { get; set; } = null!;

    public string? RefPost { get; set; }

    public virtual Recipepost? RefPostNavigation { get; set; }
}
