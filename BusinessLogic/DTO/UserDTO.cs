namespace BusinessLogic.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public int Role { get; set; }

        public string Name { get; set; } = null!;

        public DateTime Dob { get; set; }
		public bool? IsActive { get; set; }

	}
}
