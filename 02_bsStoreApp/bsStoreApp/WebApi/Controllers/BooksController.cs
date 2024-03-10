using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Models.Repositories;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly RepositoryContext _context;

        public BooksController(RepositoryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            try
            {
                return Ok(_context.Books.ToList());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
        [HttpGet("{id:int}")]
        public IActionResult GetBookById([FromRoute(Name = "id")] int id)
        {
            try
            {
                var book = _context.Books.SingleOrDefault(book => book.Id.Equals(id));
            
                if (book is null)
                    return NotFound();
            
                return Ok(book);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

        [HttpPost]
        public IActionResult CreateOneBook(Book book)
        {
            try
            {
                _context.Books.Add(book);
                _context.SaveChanges();
                return StatusCode(201, book);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateBookById([FromRoute(Name="id")] int id, Book book)
        {
            try
            {
                if (!book.Id.Equals(id))
                    return BadRequest("Your given id and book object id not matched!");
            
                var entity  = _context.Books.SingleOrDefault(b => b.Id.Equals(id));
            
                if (entity is null)
                    return NotFound();

                entity.Title = book.Title;
                entity.Price = book.Price;
                
                _context.SaveChanges();

                return Ok(book);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
        [HttpDelete("{id:int}")]
        public IActionResult deleteBookById([FromRoute(Name = "id")] int id)
        {
            try
            {
                var entity = _context.Books.SingleOrDefault(book => book.Id.Equals(id));

                if (entity is null)
                    return NotFound(new
                    {
                        statusCode = 404,
                        message = $"Book with id:{id} not found!"
                    });

                _context.Books.Remove(entity);
                _context.SaveChanges();

                return Ok(entity.Title + " has been deleted!");
            }
            catch (Exception e)
            { 
                throw new Exception(e.Message);
            }
        }

        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateOneBook([FromRoute(Name = "id")] int id,[FromBody] JsonPatchDocument bookPatch)
        {
            try
            {
                // check entity
                var entity = _context.Books.SingleOrDefault(book => book.Id.Equals(id));
            
                if (entity is null)
                    return NotFound();
            
                bookPatch.ApplyTo(entity);
                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception e)
            { 
                throw new Exception(e.Message);
            }
            
        }
    }
}
