using System;
using System.Collections.Generic;

namespace my_books_V1._0.Data.ViewModels
{
    public class PublisherVM
    {
        public string Name { get; set; }
    }

    public class PublisherWithBooksAndAuthorVM
    {
        public string Name { get; set; }

        public List<BookAuthorVM> BookAuthors { get; set; }
    }

    public class BookAuthorVM
    {
        public string BookName { get; set; }

        public List<string> BookAuthors { get; set; }
    }
}
