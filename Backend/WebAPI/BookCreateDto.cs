namespace WebAPI;

public sealed class BookCreateDto
{
    public required string Title { get; set; }
    
    public required string Author { get; set; }
    
    public required string PublishedDate { get; set; } // Format: YYYY-MM-DD
    
    public decimal Price { get; set; }
    
    public bool IsAvailable { get; set; }
};