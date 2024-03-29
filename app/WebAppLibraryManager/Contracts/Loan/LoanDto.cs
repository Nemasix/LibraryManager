﻿namespace WebAppLibraryManager.Contracts
{
    public class LoanDto
    {
        public Guid Id { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public Guid BookId { get; set; }
        public Guid LoanerId { get; set; }
        public BookDto Book { get; set; }
        public UserDto Loaner { get; set; }

        public override string ToString()
        {
            return $"Loan from {LoanDate} to {ReturnDate}";
        }
    }
}
