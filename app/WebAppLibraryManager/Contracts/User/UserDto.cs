namespace WebAppLibraryManager.Contracts
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public override string ToString()
        {
            return $"UserName: {UserName} - {LastName} {FirstName}";
        }
    }
}
