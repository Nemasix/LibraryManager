namespace WebAppLibraryManager.Contracts
{
    public class LoanForUpdateDto
    {
        public DateTime LoanDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public Guid BookId { get; set; }
        public Guid LoanerId { get; set; }
    }
}
