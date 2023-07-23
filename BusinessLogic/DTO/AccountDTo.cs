namespace BusinessLogic.DTO
{
    public class AccountDTo
    {
        public int Id { get; set; }

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public int Role { get; set; }

        public string Name { get; set; } = null!;
    }

    public class AccountRoleDto : AccountDTo
    {
        public bool IsAdmin => Role == 1;

        public bool IsUser => Role == 2;

        public bool IsWriter => Role == 3;

        public bool IsReviewer => Role == 4;

        public bool IsAdminOrReviewer => IsAdmin || IsReviewer;
    }
}
