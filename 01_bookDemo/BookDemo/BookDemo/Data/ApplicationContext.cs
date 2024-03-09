using BookDemo.Models;

namespace BookDemo.Data;
// in memory db
public static class ApplicationContext
{
    public static List<Book> Books { get; set; }
    
    static ApplicationContext()
    {
        Books = new List<Book>()
        {
            new Book() { Id = 1, Title = "Book1", Price = 20 },
            new Book() { Id = 2, Title = "Book2", Price = 200 },
            new Book() { Id = 3, Title = "Book3", Price = 10 },
            new Book() { Id = 4, Title = "Book4", Price = 100 }
        };
    }
}