namespace BusinessLogic.DTO
{
    public class CommenDTO
    {
        public string? Name { get; set; }

        public string Content { get; set; } = null!;

        public DateTime? CreateDate { get; set; }

        public int NewsId { get; set; }

        public int CreatedBy { get; set; }
    }
}
