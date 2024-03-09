using BookDemo.Data;
using BookDemo.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BookDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            return Ok(ApplicationContext.Books);
        }
        
        [HttpGet("{id:int}")]
        public IActionResult GetBookById([FromRoute(Name = "id")] int id)
        {
            var book = ApplicationContext.Books.SingleOrDefault(book => book.Id.Equals(id));
            
            if (book is null)
                return NotFound();
            
            return Ok(book);
        }

        [HttpPost]
        public IActionResult CreateOneBook(Book book)
        {
            ApplicationContext.Books.Add(book);
            
            return StatusCode(201, book);
        }

        [HttpPut]
        public IActionResult UpdateBookById(Book book)
        {
            
            var entity  = ApplicationContext.Books.SingleOrDefault(b => b.Id.Equals(book.Id));
            
            if (entity is null)
                return NotFound();
            
            ApplicationContext.Books[ApplicationContext.Books.IndexOf(entity)] = book;
            return Ok(book);
        }

        [HttpDelete]
        public IActionResult DeleteAllBooks()
        {
            ApplicationContext.Books.Clear();
            return Ok("All books cleared from integrated db!");
        }

        [HttpDelete("{id:int}")]
        public IActionResult deleteBookById([FromRoute(Name = "id")] int id)
        {
            var entity = ApplicationContext.Books.Find(book => book.Id.Equals(id));

            if (entity is null)
                return NotFound(new
                {
                    statusCode = 404,
                    message = $"Book with id:{id} not found!"
                });

            ApplicationContext.Books.Remove(entity);
            
            return Ok(entity.Title + " has been deleted!");
        }

        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateOneBook([FromRoute(Name = "id")] int id,[FromBody] JsonPatchDocument<Book> bookPatch)
        {
            // check entity
            var entity = ApplicationContext.Books.Find(book => book.Id.Equals(id));
            
            if (entity is null)
                return NotFound();
            
            bookPatch.ApplyTo(entity);

            return NoContent();
        }
    }
}
