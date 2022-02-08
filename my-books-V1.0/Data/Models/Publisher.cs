using System;
using System.Collections.Generic;

namespace my_books_V1._0.Data.Models
{
    public class Publisher
    {
        public int Id { get; set; }

        public string Name { get; set; }

        // Navigation Properties
        public List<Book> Books { get; set; }
    }
}
