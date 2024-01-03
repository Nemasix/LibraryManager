using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.TestDomain
{
    public class UserTests
    {

        [Fact]
        public void Validate_ValidUser_ReturnsEmptyCollection()
        {
            // Arrange
            var user = new User
            {
                FirstName = "John",
                LastName = "Doe",
                UserName = "johndoe",
                Email = "johndoe@example.com",
                Password = "Password123"
            };

            // Act
            var validationResults = user.Validate(null);
            // Assert
            Assert.Empty(validationResults);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Validate_MissingFirstName_ReturnsValidationError(string firstName)
        {
            // Arrange
            var user = new User
            {
                FirstName = firstName,
                LastName = "Doe",
                UserName = "johndoe",
                Email = "johndoe@example.com",
                Password = "Password123"
            };

            // Act
            var validationResults = user.Validate(null);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "First name is required");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Validate_MissingLastName_ReturnsValidationError(string lastName)
        {
            // Arrange
            var user = new User
            {
                FirstName = "John",
                LastName = lastName,
                UserName = "johndoe",
                Email = "johndoe@example.com",
                Password = "Password123"
            };

            // Act
            var validationResults = user.Validate(null);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "Last name is required");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Validate_MissingUserName_ReturnsValidationError(string userName)
        {
            // Arrange
            var user = new User
            {
                FirstName = "John",
                LastName = "Doe",
                UserName = userName,
                Email = "johndoe@example.com",
                Password = "Password123"
            };

            // Act
            var validationResults = user.Validate(null);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "User name is required");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Validate_MissingEmail_ReturnsValidationError(string email)
        {
            // Arrange
            var user = new User
            {
                FirstName = "John",
                LastName = "Doe",
                UserName = "johndoe",
                Email = email,
                Password = "Password123"
            };

            // Act
            var validationResults = user.Validate(null);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "Email is required");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Validate_MissingPassword_ReturnsValidationError(string password)
        {
            // Arrange
            var user = new User
            {
                FirstName = "John",
                LastName = "Doe",
                UserName = "johndoe",
                Email = "johndoe@example.com",
                Password = password
            };

            // Act
            var validationResults = user.Validate(null);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "Password is required");
        }

        [Theory]
        [InlineData("johndoe")]
        [InlineData("johndoe@example")]
        [InlineData("johndoe@example.")]
        [InlineData("johndoe@example.c")]
        public void Validate_InvalidEmail_ReturnsValidationError(string email)
        {
            // Arrange
            var user = new User
            {
                FirstName = "John",
                LastName = "Doe",
                UserName = "johndoe",
                Email = email,
                Password = "Password123"
            };

            // Act
            var validationResults = user.Validate(null);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "Email is not valid");
        }

        [Theory]
        [InlineData("Password")]
        [InlineData("password123")]
        [InlineData("PASSWORD123")]
        [InlineData("Password")]
        [InlineData("Password123456789")]
        public void Validate_InvalidPassword_ReturnsValidationError(string password)
        {
            // Arrange
            var user = new User
            {
                FirstName = "John",
                LastName = "Doe",
                UserName = "johndoe",
                Email = "johndoe@example.com",
                Password = password
            };

            // Act
            var validationResults = user.Validate(null);

            // Assert
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "Password must contain at least one uppercase letter, one lowercase letter and one number");
        }

        [Theory]
        [InlineData("Password123")]
        [InlineData("Password123456")]
        public void Validate_ValidPassword_ReturnsEmptyCollection(string password)
        {
            // Arrange
            var user = new User
            {
                FirstName = "John",
                LastName = "Doe",
                UserName = "johndoe",
                Email = "johndoe@example.com",
                Password = password
            };

            // Act
            var validationResults = user.Validate(null);

            // Assert
            Assert.Empty(validationResults);
        }
    }
}
