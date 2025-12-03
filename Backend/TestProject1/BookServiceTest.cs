namespace TestProject1;

using WebAPI;

public class BookServiceTest
{
    [Fact]
    public async Task ShouldCreateBook()
    {
        BookService bs = new();
        
        var x = await  bs.CreateBookAsync(new BookCreateDto()
        { Title         = "Title", Author = "Author", PublishedDate = "2005-11-15", Price = 123,
            IsAvailable = false
        });
        Assert.Equal("Title", x.Title);
        Assert.NotEqual(0, x.Id);
    }
}