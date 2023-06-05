using System;
using System.Collections.Generic;

namespace Cookapp_API.Data;

public partial class Category
{
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;
}
