using System;
using System.Collections.Generic;

namespace Cookapp.Data;

public partial class Ingredient
{
    public string Id { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string? RefPost { get; set; }

    public string Name { get; set; } = null!;
}
