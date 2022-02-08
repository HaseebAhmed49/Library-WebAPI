using System;
using my_books_V1._0.Data.ViewModels;
using my_books_V1._0.Data.Models;
using System.Linq;
using System.Text.RegularExpressions;
using my_books_V1._0.Exceptions;
using System.Collections.Generic;
using my_books_V1._0.Data.Paging;

namespace my_books_V1._0.Data.Services
{
    public class PublishersService
    {
        private AppDbContext _context;

        public PublishersService(AppDbContext context)
        {
            _context = context;
        }

        public List<Publisher> GetAllPublishers(string sortBy, string searchString, int? pageNumber)
        {
            var allPublishers = _context.Publishers.OrderBy(n => n.Name).ToList();

            if(!string.IsNullOrEmpty(sortBy))
            {
                switch(sortBy)
                {
                    case "name_desc":
                        allPublishers = allPublishers.OrderByDescending(n => n.Name).ToList();
                        break;
                    default:
                        break;
                }

            }

            if (!string.IsNullOrEmpty(searchString))
            {
                allPublishers = allPublishers.Where(n => n.Name.Contains(searchString,StringComparison.CurrentCultureIgnoreCase)).ToList();
            }

            int pageSize = 5;
            allPublishers = PaginatedList<Publisher>.Create(allPublishers.AsQueryable(),pageNumber ?? 1,pageSize);
            return allPublishers;
        }

        public Publisher AddPublisher(PublisherVM publisher)
        {
            if (StringStartsWithNumber(publisher.Name)) throw new PublisherNameException("Name Starts With Number", publisher.Name);
            var _publisher = new Publisher()
            {
                Name = publisher.Name
            };
            _context.Publishers.Add(_publisher);
            _context.SaveChanges();

            return _publisher;
        }

        public Publisher GetPublisherById(int id) => _context.Publishers.FirstOrDefault(n=>n.Id == id);

        public PublisherWithBooksAndAuthorVM GetPublisherData(int publisherid)
        {
            var _publisherData = _context.Publishers.Where(n => n.Id == publisherid)
                .Select(n => new PublisherWithBooksAndAuthorVM()
                {
                    Name = n.Name,
                    BookAuthors = n.Books.Select(n => new BookAuthorVM()
                    {
                        BookName = n.Title,
                        BookAuthors = n.Book_Authors.Select(n => n.Author.FullName).ToList()
                    }).ToList()
                }).FirstOrDefault();

            return _publisherData;
        }

        public void DeletePublisherById(int id)
        {
            var _publisher = _context.Publishers.FirstOrDefault(n => n.Id == id);
            if(_publisher!=null)
            {
                _context.Publishers.Remove(_publisher);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"This Id {id} dont exist");
            }
        }

        private bool StringStartsWithNumber(string name) => (Regex.IsMatch(name, @"^\d"));
    }
}
