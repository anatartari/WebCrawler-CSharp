using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebCrawlingConsole.Models;

namespace WebCrawlingConsole.Data
{
    class DataContext :DbContext
    {

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(@"server=localhost;database=crawler_db;user=root;password=pass");
            base.OnConfiguring(optionsBuilder);
        }


        public void Start()
        {
            this.Database.EnsureCreated();
        }

    }
}
