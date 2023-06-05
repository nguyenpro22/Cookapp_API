using System;
using System.Collections.Generic;

namespace Cookapp_API.Data;

public partial class TagPost
{
    public string RefPost { get; set; } = null!;

    public string RefTag { get; set; } = null!;

    public virtual RecipePost RefPostNavigation { get; set; } = null!;

    public virtual Tag RefTagNavigation { get; set; } = null!;
}
