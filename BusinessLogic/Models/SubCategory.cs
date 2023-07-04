﻿using System;
using System.Collections.Generic;

namespace BusinessLogic.Models;

public partial class SubCategory
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int CategoryId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<News> News { get; set; } = new List<News>();
}
