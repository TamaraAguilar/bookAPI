using bookAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace bookAPI
{
    public class BookDB : DbContext
    {
        public BookDB(DbContextOptions<BookDB> options) : base (options) { }

        public DbSet<Book> Books => Set<Book>();
    }
}
