using System;
using System.Collections.Generic;

namespace BusinessLogic.Models;

public partial class News
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int CategoryId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int CreatedBy { get; set; }

    public int? NumberOfLikes { get; set; }

    public string Content { get; set; } = null!;

    public string? ImgPath { get; set; }

    public DateTime? PostedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public int? Status { get; set; }

    public int SubCategoryId { get; set; }
	public bool? IsActive { get; set; }

	public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual SubCategory SubCategory { get; set; } = null!;
}
