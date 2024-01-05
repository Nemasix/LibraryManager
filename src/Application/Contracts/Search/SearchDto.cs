using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public class SearchDto
    {
        public string? Title { get; set; }
        public string? Author { get; set; }
        public int? Page { get; set; }
        public int? Limit { get; set; }
    }
}
