namespace BusinessLogic.DTO
{
    public class NewsDTO
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public int CategoryId { get; set; }

        public int CreatedBy { get; set; }

        public int? NumberOfLikes { get; set; }

        public string Content { get; set; } = null!;

        public string? ImgPath { get; set; }

        public DateTime? PostedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public int? Status { get; set; }

        public int SubCategoryId { get; set; }
    }
}
