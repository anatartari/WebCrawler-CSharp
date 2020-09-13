using System;
using System.Collections.Generic;
using System.Text;
using WebCrawlingConsole.Models;

namespace WebCrawlingConsole
{
    class Book
    {
        public long BookId { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<Author> Autor { get; set; }
    }
}
