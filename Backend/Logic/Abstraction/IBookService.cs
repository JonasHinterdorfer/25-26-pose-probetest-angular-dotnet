namespace WebAPI;

public interface IBookService
{
    Task<IList<Book>> GetAllBooksAsync();
    Task<Book?> GetBookByIdAsync(int id);
    Task<bool> TitleExistsAsync(string title, int? excludeId = null);
    Task<Book?> CreateBookAsync(Book book);
    Task<Book?> UpdateBookAsync(int id, Book book);
    Task<bool> DeleteBookAsync(int id);
}

