using System.Text;

namespace WebAppLibraryManager.Contracts
{
    public class SearchDto
    {
        public string? Title { get; set; }
        public string? Author { get; set; }
        public int? Page { get; set; }
        public int? Limit { get; set; }
    }
}
