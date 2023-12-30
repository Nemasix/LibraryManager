namespace WebAppLibraryManager.Contracts
{
    public class BookForCreationDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public Guid? OwnerId { get; set; }
    }
}
