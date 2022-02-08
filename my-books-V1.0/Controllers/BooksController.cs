using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using my_books_V1._0.Data.Services;
using my_books_V1._0.Data.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace my_books_V1._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        public BooksService _booksService;

        public BooksController(BooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpPost("add-book-with-authors")]
        public IActionResult AddBook([FromBody]BookVM book)
        {
            _booksService.AddBookwithAuthors(book);
            return Ok();
        }

        [HttpGet("get-all-books")]
        public IActionResult GetAllBooks()
        {
            var allBooks = _booksService.GetAllBooks();
            return Ok(allBooks);
        }

        [HttpGet("get-book-by-id/{id}")]
        public IActionResult GetBook(int id)
        {
            var book = _booksService.GetBookById(id);
            return Ok(book);
        }

        [HttpPut("update-book-by-id/{id}")]
        public  IActionResult UpdateBookById(int id, [FromBody]BookVM book)
        {
            var updatedBook = _booksService.UpdateBookById(id,book);
            return Ok(updatedBook);
        }

        [HttpDelete("delete-book-by-id/{id}")]
        public IActionResult DeleteBookById(int id)
        {
            _booksService.DeleteBookById(id);
            return Ok();
        }
    }
}
