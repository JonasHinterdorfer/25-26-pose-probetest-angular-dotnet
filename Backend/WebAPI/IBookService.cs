namespace WebAPI;

public interface IBookService
{
    Task<IList<Book>> GetAllBooksAsync();
    Task<Book?>       GetBookByIdAsync(int    id);
    Task<bool>        TitleExistsAsync(string title);
    Task<Book>       CreateBookAsync(BookCreateDto book);
    Task<Book?>       UpdateBookAsync(int     id, BookCreateDto book);
    Task<bool>        DeleteBookAsync(int     id);
}