namespace BusinessLogic.DTO
{
    public class SubCategoryDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int CategoryId { get; set; }
    }
}
