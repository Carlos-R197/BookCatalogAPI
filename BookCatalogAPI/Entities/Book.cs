#pragma warning disable CS8618

namespace BookCatalogAPI.Entities;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateOnly PublicationDate { get; set; }
    public string ISBN { get; set; }
    public IList<BookAuthor> BookAuthors { get; set; }
    public IList<BookGenre> BookGenres { get; set; }
}