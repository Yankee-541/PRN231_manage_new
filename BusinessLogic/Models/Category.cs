using System;
using System.Collections.Generic;

namespace BusinessLogic.Models;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<News> News { get; set; } = new List<News>();

    public virtual ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
}
