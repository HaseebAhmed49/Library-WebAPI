using System;
using System.Collections.Generic;

namespace my_books_V1._0.Data.Models
{
    public class Author
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        // Navigation Properties

        public List<Book_Author> Book_Authors { get; set; }
    }
}
