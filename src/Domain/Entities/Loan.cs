﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Loan : BaseEntity
    {
        public DateTime LoanDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public Guid BookId { get; set; }
        public Guid LoanerId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(LoanDate > ReturnDate)
            {
                yield return new ValidationResult("Loan date must be before return date", new[] { nameof(LoanDate), nameof(ReturnDate) });
            }
            if(LoanDate < DateTime.Now)
            {
                yield return new ValidationResult("Loan date must be in the future", new[] { nameof(LoanDate) });
            }
            if(ReturnDate < DateTime.Now)
            {
                yield return new ValidationResult("Return date must be in the future", new[] { nameof(ReturnDate) });
            }
        }
    }
}
