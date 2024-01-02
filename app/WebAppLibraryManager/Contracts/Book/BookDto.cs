namespace WebAppLibraryManager.Contracts
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public ICollection<LoanDto> Loans { get; set; }
        public Guid OwnerId { get; set; }
        public UserDto Owner { get; set; }

        public override string ToString()
        {
            return $"Title: {Title} - Author: {Author}";
        }
    }
}
