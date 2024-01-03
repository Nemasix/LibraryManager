using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public class SearchDto
    {
        public string Title { get; set; }
        public string Author { get; set; }

        public string Query
        {
            get
            {
                var query = new StringBuilder();
                if (!string.IsNullOrEmpty(Title))
                {
                    query.Append($"title:{Title}");
                }
                if (!string.IsNullOrEmpty(Author))
                {
                    if (query.Length > 0)
                    {
                        query.Append(" AND ");
                    }
                    query.Append($"author:{Author}");
                }
                return query.ToString();
            }
        }
    }
}
