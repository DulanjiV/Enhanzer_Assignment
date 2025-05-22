using backend.Models;
using backend.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;
        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        public IActionResult CreateBook(Book book)
        {
            try
            {
                _bookService.CreateBook(book);
                return Ok(new { message = "Book created successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            try
            {
                var books = _bookService.GetAllBooks();
                return Ok(books);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            try
            {
                var book = _bookService.GetBookById(id);
                if (book == null)
                {
                    return NotFound(new { error = "Book not found" });
                }
                return Ok(book);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult PutBook(int id, Book book)
        {
            try
            {
                var existingBook = _bookService.GetBookById(id);
                if (existingBook == null)
                {
                    return NotFound(new { error = "Book not found" });
                }

                _bookService.UpdateBook(id, book);
                return Ok(new { message = "Book updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                var existingBook = _bookService.GetBookById(id);
                if (existingBook == null)
                {
                    return NotFound(new { error = "Book not found" });
                }

                _bookService.DeleteBook(id);
                return Ok(new { message = "Book deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}

