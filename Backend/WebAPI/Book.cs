namespace WebAPI;

public class Book
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public string Author { get; set; }
    
    public string PublishedDate { get; set; } // Format: YYYY-MM-DD
    
    public decimal Price { get; set; }
    
    public bool IsAvailable { get; set; }

    public Book()
    {
        
    }
    public Book(int id, BookCreateDto book) : this()
    {
        this.Id = id;
        this.Title = book.Title;
        this.Author = book.Author;
        this.PublishedDate = book.PublishedDate;
        this.Price = book.Price;
        this.IsAvailable = book.IsAvailable;
    }
}

