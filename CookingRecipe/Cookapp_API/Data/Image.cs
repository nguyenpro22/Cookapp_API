using System;
using System.Collections.Generic;

namespace Cookapp_API.Data;

public partial class Image
{
    public string Id { get; set; } = null!;

    public string Content { get; set; } = null!;

    public virtual ICollection<RecipePost> RecipePosts { get; set; } = new List<RecipePost>();
}
