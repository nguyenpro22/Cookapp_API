using System;
using System.Collections.Generic;

namespace Cookapp_API.Data;

public partial class Blacklist
{
    public string RefUser { get; set; } = null!;

    public string Reason { get; set; } = null!;

    public bool IsBan { get; set; }

    public virtual Account RefUserNavigation { get; set; } = null!;
}
