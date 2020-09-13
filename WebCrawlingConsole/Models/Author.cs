using System;
using System.Collections.Generic;
using System.Text;

namespace WebCrawlingConsole.Models
{
    class Author
    {

        public long AuthorId { get; set; }

        public string Name { get; set; }

        public long BookId { get; set; }

        public Book Books { get; set; }
    }
}
