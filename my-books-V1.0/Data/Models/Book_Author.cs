using System;
namespace my_books_V1._0.Data.Models
{
    public class Book_Author
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public Book Book { get; set; }

        public int AuthorId { get; set; }

        public Author Author { get; set; }
    }
}
