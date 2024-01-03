using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.TestDomain
{
    public class BookTests
    {

        [Fact]
        public void Validate_WithValidBook_ReturnsEmptyValidationResults()
        {
            // Arrange
            var book = new Book
            {
                Title = "Sample Title",
                Author = "Sample Author",
                ISBN = "1234567890"
            };

            // Act
            var validationResults = book.Validate(null);

            // Assert
            Assert.Empty(validationResults);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Validate_WithNullOrEmptyTitle_ReturnsValidationResult(string title)
        {
            // Arrange
            var book = new Book
            {
                Title = title,
                Author = "Sample Author",
                ISBN = "1234567890"
            };

            // Act
            var validationResults = book.Validate(null);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "Title is required" && vr.MemberNames.Contains(nameof(Book.Title)));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Validate_WithNullOrEmptyAuthor_ReturnsValidationResult(string author)
        {
            // Arrange
            var book = new Book
            {
                Title = "Sample Title",
                Author = author,
                ISBN = "1234567890"
            };

            // Act
            var validationResults = book.Validate(null);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "Author is required" && vr.MemberNames.Contains(nameof(Book.Author)));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Validate_WithNullOrEmptyISBN_ReturnsValidationResult(string isbn)
        {
            // Arrange
            var book = new Book
            {
                Title = "Sample Title",
                Author = "Sample Author",
                ISBN = isbn
            };

            // Act
            var validationResults = book.Validate(null);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "ISBN is required" && vr.MemberNames.Contains(nameof(Book.ISBN)));
        }

        [Theory]
        [InlineData("veglJXQcFuIrjoMnkPETaydsWYfLbpxqDzBAitVZNKOHmhGwCSURJnDGZXbAswrOxcPIVkEQeStiTChpdfyRMgmzUvuNYalqFojHWBLKVlzEXkRhTswKHtroNBQMIJcLnCiDuUSPmpxgGZAOafbyjevYWqFdgRKzObILmdCSkZcFPqsiwJueXNUMxfGnThElYDHWArQjptyavoVBGbTamDCehvSBLEdyMfAtcVFJljnXqixgPIrZRsoOpKUHNuzQWk")]
        public void Validate_WithTitleExceedingMaxLength_ReturnsValidationResult(string title)
        {
            // Arrange
            var book = new Book
            {
                Title = title,
                Author = "Sample Author",
                ISBN = "1234567890"
            };

            // Act
            var validationResults = book.Validate(null);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "Title must be less than 200 characters long" && vr.MemberNames.Contains(nameof(Book.Title)));
        }

        [Theory]
        [InlineData("veglJXQcFuIrjoMnkPETaydsWYfLbpxqDzBAitVZNKOHmhGwCSURJnDGZXbAswrOxcPIVkEQeStiTChpdfyRMgmzUvuNYalqFojHWBLKVlzEXkRhTswKHtroNBQMIJcLnCiDuUSPmpxgGZAOafbyjevYWqFdgRKzObILmdCSkZcFPqsiwJueXNUMxfGnThElYDHWArQjptyavoVBGbTamDCehvSBLEdyMfAtcVFJljnXqixgPIrZRsoOpKUHNuzQWk")]
        public void Validate_WithAuthorExceedingMaxLength_ReturnsValidationResult(string author)
        {
            // Arrange
            var book = new Book
            {
                Title = "Sample Title",
                Author = author,
                ISBN = "1234567890"
            };

            // Act
            var validationResults = book.Validate(null);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "Author must be less than 200 characters long" && vr.MemberNames.Contains(nameof(Book.Author)));
        }

        [Theory]
        [InlineData("1234567890")]
        [InlineData("978-1234567890")]
        [InlineData("ISBN 1234567890")]
        [InlineData("ISBN-10 1234567890")]
        [InlineData("ISBN-13 1234567890")]
        [InlineData("ISBN-10 123-4-567-890")]
        [InlineData("ISBN-13 123-4-567-890")]
        [InlineData("ISBN-10 123 4 567 890")]
        [InlineData("ISBN-13 123 4 567 890")]
        [InlineData("ISBN-10 123 4 567 890 X")]
        [InlineData("ISBN-13 123 4 567 890 X")]
        public void Validate_WithValidISBN_ReturnsEmptyValidationResults(string isbn)
        {
            // Arrange
            var book = new Book
            {
                Title = "Sample Title",
                Author = "Sample Author",
                ISBN = isbn
            };

            // Act
            var validationResults = book.Validate(null);

            // Assert
            Assert.Empty(validationResults);
        }

        [Theory]
        [InlineData("Invalid ISBN")]
        [InlineData("123456789")]
        [InlineData("12345678901")]
        [InlineData("978-123456789")]
        [InlineData("978-12345678901")]
        [InlineData("ISBN 123456789")]
        [InlineData("ISBN 12345678901")]
        [InlineData("ISBN-10 123456789")]
        [InlineData("ISBN-10 12345678901")]
        [InlineData("ISBN-13 123456789")]
        [InlineData("ISBN-13 12345678901")]
        [InlineData("ISBN-10 123-4-567-8901")]
        [InlineData("ISBN-13 123-4-567-8901")]
        [InlineData("ISBN-10 123 4 567 8901")]
        [InlineData("ISBN-13 123 4 567 8901")]
        [InlineData("ISBN-10 123 4 567 8901 X")]
        [InlineData("ISBN-13 123 4 567 8901 X")]
        public void Validate_WithInvalidISBN_ReturnsValidationResult(string isbn)
        {
            // Arrange
            var book = new Book
            {
                Title = "Sample Title",
                Author = "Sample Author",
                ISBN = isbn
            };

            // Act
            var validationResults = book.Validate(null);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "ISBN is not valid" && vr.MemberNames.Contains(nameof(Book.ISBN)));
        }
    }
}
