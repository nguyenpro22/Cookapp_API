using System;
using System.Collections.Generic;

namespace Cookapp.Data;

public partial class TypePost
{
    public string RefPost { get; set; } = null!;

    public string RefType { get; set; } = null!;

    public virtual RecipePost RefPostNavigation { get; set; } = null!;

    public virtual Type RefTypeNavigation { get; set; } = null!;
}
