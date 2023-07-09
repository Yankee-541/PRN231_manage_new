namespace BusinessLogic.DTO
{
    public class SearchModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public int CategoryId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime? PostedDate { get; set; }

        public int? Status { get; set; }

        public int SubCategoryId { get; set; }
    }
}
