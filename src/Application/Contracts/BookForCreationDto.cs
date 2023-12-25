using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Contracts
{
    
    public class BookForCreationDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string Publisher { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Language { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
        public int NumberOfPages { get; set; }
        
    }
}