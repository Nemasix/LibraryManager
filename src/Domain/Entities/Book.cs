

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
        /// Gets or sets the publisher of the book.
        /// </summary>
        public string Publisher { get; set; }

        /// <summary>
        /// Gets or sets the publication date of the book.
        /// </summary>
        public DateTime PublicationDate { get; set; }

        /// <summary>
        /// Gets or sets the language of the book.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the genre of the book.
        /// </summary>
        public string Genre { get; set; }

        /// <summary>
        /// Gets or sets the price of the book.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the number of pages in the book.
        /// </summary>
        public int NumberOfPages { get; set; }
    }
}