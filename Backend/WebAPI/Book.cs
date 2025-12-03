namespace WebAPI;

using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class Book
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public string Author { get; set; }

    private string publishedDate = String.Empty;
    public string PublishedDate
    {
        get => publishedDate;
        set
        {
           var regex = new Regex(@"^(?:\d{4})-(?:0[1-9]|1[0-2])-(?:0[1-9]|[12]\d|3[01])$");
           if (regex.Match(value).Success)
           {
               publishedDate = value;
           }
           else
           {
               throw new ArgumentException("Invalid date");
           }
        }
    }

    private decimal _price = 0;
    public decimal Price
    {
        get => _price;
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Price cannot be negative");
            }
            _price = value;
        }
    }

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

