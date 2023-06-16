using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using bookAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace bookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookDB _db;
        public BookController(BookDB db) 
        {
            _db = db;
        }
        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBook()
        {
            return Ok(await _db.Books.ToListAsync());
        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(int id)
        {
            var book = _db.Books.Find(id);
            if(book == null)
            {
                return NotFound();
            }
            return book;
        }

        [HttpPost]
        public async Task<ActionResult<Book>> Create(Book book) 
        {
            _db.Add(book);
            await _db.SaveChangesAsync();
            return Ok(book);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Book book)
        {
            if (id != book.BookId)
            {
                return BadRequest();
            }
            _db.Entry(book).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return Ok(book);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var book = await _db.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            _db.Books.Remove(book);
            await _db.SaveChangesAsync();

            return Ok(book);
        }
    }
}
