using System;
using System.Collections.Generic;

namespace BusinessLogic.Models;

public partial class Comment
{
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public int NewsId { get; set; }

    public int CreatedBy { get; set; }

	public bool? IsActive { get; set; }
	public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual News News { get; set; } = null!;
}
