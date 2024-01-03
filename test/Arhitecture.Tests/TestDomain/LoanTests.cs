using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using Domain.Entities;
using Xunit;

namespace Tests.TestDomain
{
    public class LoanTests
    {
        [Fact]
        public void Validate_ValidLoan_ReturnsEmptyValidationResults()
        {
            // Arrange
            var loan = new Loan
            {
                LoanDate = DateTime.Now.AddDays(1),
                ReturnDate = DateTime.Now.AddDays(7),
                BookId = Guid.NewGuid(),
                LoanerId = Guid.NewGuid()
            };

            // Act
            var validationResults = loan.Validate(new ValidationContext(loan));

            // Assert
            Assert.Empty(validationResults);
        }

        [Fact]
        public void Validate_InvalidLoanDate_ReturnsValidationResult()
        {
            // Arrange
            var loan = new Loan
            {
                LoanDate = DateTime.Now.AddDays(-1),
                ReturnDate = DateTime.Now.AddDays(7),
                BookId = Guid.NewGuid(),
                LoanerId = Guid.NewGuid()
            };

            // Act
            var validationResults = loan.Validate(new ValidationContext(loan));

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "Loan date must be in the future");
        }

        [Fact]
        public void Validate_InvalidReturnDate_ReturnsValidationResult()
        {
            // Arrange
            var loan = new Loan
            {
                LoanDate = DateTime.Now.AddDays(1),
                ReturnDate = DateTime.Now.AddDays(-1),
                BookId = Guid.NewGuid(),
                LoanerId = Guid.NewGuid()
            };

            // Act
            var validationResults = loan.Validate(new ValidationContext(loan));

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "Return date must be in the future");
        }

        [Fact]
        public void Validate_InvalidLoanDateAndReturnDate_ReturnsValidationResults()
        {
            // Arrange
            var loan = new Loan
            {
                LoanDate = DateTime.Now.AddDays(7),
                ReturnDate = DateTime.Now.AddDays(1),
                BookId = Guid.NewGuid(),
                LoanerId = Guid.NewGuid()
            };

            // Act
            var validationResults = loan.Validate(new ValidationContext(loan));

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "Loan date must be before return date");
        }
    }
}
