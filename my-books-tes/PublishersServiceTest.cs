using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using my_books_V1._0.Data;
using my_books_V1._0.Data.Models;
using my_books_V1._0.Data.Services;
using my_books_V1._0.Data.ViewModels;
using my_books_V1._0.Exceptions;
using NUnit.Framework;

namespace my_books_tests
{
    public class PublishersServiceTest
    {
        private static DbContextOptions<AppDbContext> DbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "myBooksStoreDbTest")
            .Options;

        AppDbContext context;
        PublishersService publishersService;

        // called as Decorator
        // [Setup] will call this method with every test
        // [OneTimeSetup] This is one time setup for all methods
        [OneTimeSetUp]
        public void Setup()
        {
            context = new AppDbContext(DbContextOptions);
            context.Database.EnsureCreated();
            // Add Data in Database
            SeedDatabase();
            publishersService = new PublishersService(context);
        }

        [Test, Order(1)]
        public void GetAllPublishers_withNoSortBy_withNoSearchString_withNoPageNumber_Test()
        {
            var result = publishersService.GetAllPublishers("","",null);
            Assert.That(result.Count,Is.EqualTo(5));
        }


        [Test, Order(2)]
        public void GetAllPublishers_withNoSortBy_withNoSearchString_withPageNumber_Test()
        {
            var result = publishersService.GetAllPublishers("", "", 2);
            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test, Order(3)]
        public void GetAllPublishers_withNoSortBy_withSearchString_withNoPageNumber_Test()
        {
            var result = publishersService.GetAllPublishers("", "3", null);
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.FirstOrDefault().Name, Is.EqualTo("Publisher 3"));
        }

        [Test, Order(4)]
        public void GetAllPublishers_withSortBy_withNoSearchString_withNoPageNumber_Test()
        {
            var result = publishersService.GetAllPublishers("name_desc", "", null);
            Assert.That(result.Count, Is.EqualTo(5));
            Assert.That(result.FirstOrDefault().Name, Is.EqualTo("Publisher 6"));
        }

        [TestCase(1), Order(5)]
        public void GetPublishersById_WithResponse_Test(int id)
        {
            var result = publishersService.GetPublisherById(id);
            Assert.That(result.Id, Is.EqualTo(id));
        }

        [Test, Order(6)]
        public void GetPublishersById_WithoutResponse_Test()
        {
            var result = publishersService.GetPublisherById(99);
            Assert.That(result,Is.Null);
        }

        [Test, Order(7)]
        public void AddPublisher_WithException_Test()
        {
            var newPublisher = new PublisherVM()
            {
                Name="123 new Publisher"
            };

            Assert.That(() => publishersService.AddPublisher(newPublisher),
                Throws.Exception.TypeOf<PublisherNameException>().With.Message.EqualTo("Name Starts With Number"));
        }

        [Test, Order(8)]
        public void AddPublisher_WithoutException_Test()
        {
            var newPublisher = new PublisherVM()
            {
                Name = "new Publisher"
            };

            var result = publishersService.AddPublisher(newPublisher);
                Assert.That(result,Is.Not.Null);
            Assert.That(result.Name,Does.StartWith("new"));
            Assert.That(result.Id,Is.Not.Null);
        }


        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
        }

        private void SeedDatabase()
        {
            var publishers = new List<Publisher>()
            {
                new Publisher()
                {
                    Id=1,
                    Name="Publisher 1"
                },
                new Publisher()
                {
                    Id=2,
                    Name="Publisher 2"
                },

                new Publisher()
                {
                    Id=3,
                    Name="Publisher 3"
                },
                new Publisher()
                {
                    Id=4,
                    Name="Publisher 4"
                },
                new Publisher()
                {
                    Id=5,
                    Name="Publisher 5"
                },
                new Publisher()
                {
                    Id=6,
                    Name="Publisher 6"
                },

            };
            context.Publishers.AddRange(publishers);

            var authors = new List<Author>()
            {
                new Author()
                {
                    Id=1,
                    FullName = "Author 1"
                },
                new Author()
                {
                    Id=2,
                    FullName = "Author 2"
                },

            };
            context.Authors.AddRange(authors);

            var books = new List<Book>()
            {
                new Book()
                {
                    Id=1,
                    Title="Book 1 Title",
                    Description = "Book 1 Description",
                    isRead = false,
                    Genre="Genre",
                    CoverUrl="...",
                    DateAdded=DateTime.Now.AddDays(-10),
                    PublisherId=1
                },
                new Book()
                {
                    Id=2,
                    Title="Book 2 Title",
                    Description = "Book 2 Description",
                    isRead = false,
                    Genre="Genre",
                    CoverUrl="...",
                    DateAdded=DateTime.Now.AddDays(-10),
                    PublisherId=1
                },
            };
            context.Books.AddRange(books);

            var book_auhtors = new List<Book_Author>()
            {
                new Book_Author()
                {
                    Id=1,
                    BookId=1,
                    AuthorId=1
                },
                new Book_Author()
                {
                    Id=2,
                    BookId=1,
                    AuthorId=2
                },
                new Book_Author()
                {
                    Id=3,
                    BookId=2,
                    AuthorId=2
                },
            };
            context.Books_Authors.AddRange(book_auhtors);
            context.SaveChanges();
        }
    }
}
