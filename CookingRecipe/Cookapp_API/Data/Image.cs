using System;
using System.Collections.Generic;

namespace Cookapp_API.Data;

public partial class Image
{
    public string Id { get; set; } = null!;

    public byte[] Image1 { get; set; } = null!;

    public virtual ICollection<Recipepost> Recipeposts { get; set; } = new List<Recipepost>();
}
