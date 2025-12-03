using Logic.Abstraction;

namespace Logic;

public class BookService : IBookService
{
    private static List<Book> _books = new()
    {
        new Book
        {
            Id = 1,
            Title = "Clean Code",
            Author = "Robert C. Martin",
            PublishedDate = "2008-08-01",
            Price = 32.95m,
            IsAvailable = true
        },
        new Book
        {
            Id = 2,
            Title = "The Pragmatic Programmer",
            Author = "Andrew Hunt, David Thomas",
            PublishedDate = "2020-09-13",
            Price = 39.99m,
            IsAvailable = true
        },
        new Book
        {
            Id = 3,
            Title = "Design Patterns",
            Author = "Erich Gamma, Richard Helm, Ralph Johnson, John Vlissides",
            PublishedDate = "1994-10-31",
            Price = 54.99m,
            IsAvailable = false
        },
        new Book
        {
            Id = 4,
            Title = "Refactoring",
            Author = "Martin Fowler",
            PublishedDate = "2018-11-20",
            Price = 47.99m,
            IsAvailable = true
        },
        new Book
        {
            Id = 5,
            Title = "Domain-Driven Design",
            Author = "Eric Evans",
            PublishedDate = "2003-08-20",
            Price = 59.99m,
            IsAvailable = true
        },
        new Book
        {
            Id = 6,
            Title = "You Don't Know JS",
            Author = "Kyle Simpson",
            PublishedDate = "2015-01-14",
            Price = 24.99m,
            IsAvailable = true
        },
        new Book
        {
            Id = 7,
            Title = "C# in Depth",
            Author = "Jon Skeet",
            PublishedDate = "2019-03-30",
            Price = 44.99m,
            IsAvailable = false
        },
        new Book
        {
            Id = 8,
            Title = "Effective TypeScript",
            Author = "Dan Vanderkam",
            PublishedDate = "2019-10-31",
            Price = 42.99m,
            IsAvailable = true
        }
    };

    private static int _nextId = 9;

    public Task<IList<Book>> GetAllBooksAsync()
    {
    }

    public Task<Book?> GetBookByIdAsync(int id)
    {
    }

    public Task<bool> TitleExistsAsync(string title, int? excludeId = null)
    {
    }

    public Task<Book> CreateBookAsync(Book book)
    {
    }

    public Task<Book?> UpdateBookAsync(int id, Book book)
    {
    }

    public Task<bool> DeleteBookAsync(int id)
    {
    }
}

