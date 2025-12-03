namespace WebAPI.Access;

using Microsoft.AspNetCore.Mvc;

public static class BookAccess
{
    public static void AddBookAccess(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/books",             async (IBookService bookService) => Results.Ok(await bookService.GetAllBooksAsync()));
        app.MapGet("/api/books/titleExists", async([FromQuery]string name,IBookService bookService)
            => Results.Ok(await bookService.TitleExistsAsync(name)));
        
        app.MapGet("/api/books/{id}", async (int id, IBookService bookService) =>
        {
            var book = await bookService.GetBookByIdAsync(id);
            return book is null ? Results.NotFound() : Results.Ok(book);
        });
        
        app.MapDelete("/api/books/{id}", async (int id, IBookService bookService) =>
        {
            await bookService.DeleteBookAsync(id);
            return Results.Ok();
        });

        app.MapPost("/api/books", async ([FromBody] BookCreateDto dto, IBookService bookService) =>
        {
            try
            {
                var book = await bookService.CreateBookAsync(dto);   
                return Results.Created($"/api/books/{book.Id}", book);
            }
            catch (ArgumentException e)
            {
                return Results.BadRequest(e.Message);
            }
        });

        app.MapPut("/api/books/{id}", async (int id, [FromBody] BookCreateDto dto, IBookService bookService) =>
        {
            try
            {
                var book = await bookService.UpdateBookAsync(id, dto);
                return book is null ? Results.NotFound() : Results.Ok(book);
            }
            catch (ArgumentException e)
            {
                return Results.BadRequest(e.Message);
            }
        });
    }
}