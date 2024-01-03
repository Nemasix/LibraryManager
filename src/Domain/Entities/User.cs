using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<Book> Books { get; set; }
        public ICollection<Loan> Loans { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(FirstName))
            {
                yield return new ValidationResult("First name is required", new[] { nameof(FirstName) });
            }
            if (string.IsNullOrEmpty(LastName))
            {
                yield return new ValidationResult("Last name is required", new[] { nameof(LastName) });
            }
            if (string.IsNullOrEmpty(UserName))
            {
                yield return new ValidationResult("User name is required", new[] { nameof(UserName) });
            }
            if (string.IsNullOrEmpty(Email))
            {
                yield return new ValidationResult("Email is required", new[] { nameof(Email) });
            }
            if (string.IsNullOrEmpty(Password))
            {
                yield return new ValidationResult("Password is required", new[] { nameof(Password) });
            }
            if(!Regex.Match(Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
            {
                yield return new ValidationResult("Email is not valid", new[] { nameof(Email) });
            }
            if(Password.Length < 8)
            {
                yield return new ValidationResult("Password must be at least 8 characters long", new[] { nameof(Password) });
            }
            if(!Regex.Match(Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$").Success)
            {
                yield return new ValidationResult("Password must contain at least one uppercase letter, one lowercase letter and one number", new[] { nameof(Password) });
            }
        }
    }
}
