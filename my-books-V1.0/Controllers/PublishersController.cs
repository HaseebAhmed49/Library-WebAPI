using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using my_books_V1._0.ActionResults;
using my_books_V1._0.Data.Services;
using my_books_V1._0.Data.ViewModels;
using my_books_V1._0.Exceptions;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace my_books_V1._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private PublishersService _publisherssService;
        private readonly ILogger<PublishersController> _logger;

        public PublishersController(PublishersService publishersService, ILogger<PublishersController> logger)
        {
            _publisherssService = publishersService;
            _logger = logger;  
        }

        [HttpGet("get-all-publishers")]
        public IActionResult GetAllPublishers(string sortBy, string searchString,int pageNumber)
        {
            //throw new Exception("Exception thrown Intentionally from Get All Publishers Method");
            try
            {
                _logger.LogInformation("This is just a log in GetAllPublishers()");
                var _result = _publisherssService.GetAllPublishers(sortBy, searchString, pageNumber);
                return Ok(_result);
            }
            catch (Exception ex)
            {
                return BadRequest("Sorry we couldn't load the publishers");
            }
        }


        [HttpPost("add-publisher")]
        public IActionResult AddPublisher([FromBody] PublisherVM publisher)
        {
            try
            {
                var newPublisher = _publisherssService.AddPublisher(publisher);
                return Created(nameof(AddPublisher), newPublisher);
            }
            catch(PublisherNameException ex)
            {
                return BadRequest($"{ex.Message}, Publisher Name: {ex.PublisherName}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("get-publisher-by-id/{id}")]
        public IActionResult GetPublisherById(int id)
        {
            var _response = _publisherssService.GetPublisherById(id);
            return (_response != null) ? Ok(_response) : NotFound();


            #region CustomActionResult

            //if(_response != null)
            //{
            //    var _responseObj = new CustomActionResultVM()
            //    {
            //        Publisher = _response
            //    };
            //    return new CustomActionResult(_responseObj);
            //}
            //else
            //{
            //    var _responseObj = new CustomActionResultVM()
            //    {
            //        Exception = new Exception("This is coming from publishers controller")
            //    };
            //    return new CustomActionResult(_responseObj);
            //}

            #endregion
        }

        [HttpGet("get-publisher-books-with-authors/{id}")]
        public IActionResult GetPublisherData(int id)
        {
            var response = _publisherssService.GetPublisherData(id);
            return Ok(response);
        }

        [HttpDelete("delete-publishers-by-id/{id}")]
        public IActionResult DeletePublisherById(int id)
        {
            try
            {
                _publisherssService.DeletePublisherById(id);
                return Ok();
            }            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}