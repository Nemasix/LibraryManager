using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Domain.Entities
{
    /// <summary>
    /// Represents a book entity.
    /// </summary>
    public class Book : BaseEntity
    {
        /// <summary>
        /// Gets or sets the title of the book.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the author of the book.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the ISBN (International Standard Book Number) of the book.
        /// </summary>
        public string ISBN { get; set; }

        /// <summary>
        /// Gets or sets the number of loans of the book.
        /// </summary>
        public ICollection<Loan> Loans { get; set; }
        public Guid OwnerId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Title))
            {
                yield return new ValidationResult("Title is required", new[] { nameof(Title) });
            }
            if (string.IsNullOrEmpty(Author))
            {
                yield return new ValidationResult("Author is required", new[] { nameof(Author) });
            }
            if (string.IsNullOrEmpty(ISBN))
            {
                yield return new ValidationResult("ISBN is required", new[] { nameof(ISBN) });
            }
            if(Title.Length > 200)
            {
                yield return new ValidationResult("Title must be less than 200 characters long", new[] { nameof(Title) });
            }
            if(Author.Length > 200)
            {
                yield return new ValidationResult("Author must be less than 200 characters long", new[] { nameof(Author) });
            }
            if(!Regex.Match(ISBN, @"^(?:ISBN(?:-1[03])?:?\ )?(?=[0-9X]{10}$|(?=(?:[0-9]+[-\ ]){3})[-\ 0-9X]{13}$|97[89][0-9]{10}$|(?=(?:[0-9]+[-\ ]){4})[-\ 0-9]{17}$)(?:97[89][-\ ]?)?[0-9]{1,5}[-\ ]?[0-9]+[-\ ]?[0-9]+[-\ ]?[0-9X]$").Success)
            {
                yield return new ValidationResult("ISBN is not valid", new[] { nameof(ISBN) });
            }
        }
    }
}