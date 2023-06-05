using System;
using System.Collections.Generic;

namespace Cookapp_API.Data;

public partial class NutriPost
{
    public string? RefNutri { get; set; }

    public string? RefPost { get; set; }

    public virtual Nutrition? RefNutriNavigation { get; set; }

    public virtual RecipePost? RefPostNavigation { get; set; }
}
