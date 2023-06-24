using System;
using System.Collections.Generic;

namespace Cookapp_API.Data;

public partial class Video
{
    public string Id { get; set; } = null!;

    public string Video1 { get; set; } = null!;

    public virtual ICollection<Recipepost> Recipeposts { get; set; } = new List<Recipepost>();
}
