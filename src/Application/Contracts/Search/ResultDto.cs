using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public class ResultDto
    {
        public int num_found { get; set; }
        public IEnumerable<DocDto> docs { get; set; }
    }

    public class DocDto
    {
        public string key { get; set; }
        public int cover_i { get; set; }
        public bool has_fulltext { get; set; }
        public int edition_count { get; set; }
        public string title { get; set; }
        public string[] author_name { get; set; }
        public string[] isbn { get; set; }
        public string[] author_key { get; set; }
        public string[] subject { get; set; }
        public string[] isbn13 { get; set; }
        public string[] language { get; set; }
        public string[] publisher { get; set; }
        public string[] publish_date { get; set; }
    }
}
