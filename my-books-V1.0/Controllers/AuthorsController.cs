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


    public class AuthorsController : ControllerBase
    {
        private AuthorsService _authorsService;

        public AuthorsController(AuthorsService authorsService)
        {
            _authorsService = authorsService;
        }

        [HttpPost("add-author")]
        public IActionResult AddAuthor([FromBody] AuthorVM author)
        {
            _authorsService.AddAuthor(author);
            return Ok();
        }

        [HttpGet("get-author-with-books-by-id/{id}")]
        public IActionResult GetAuthorsWithBooks(int id)
        {
            var response = _authorsService.GetAuthorWithBooks(id);
            return Ok(response);
        }
    }
}
