#pragma warning disable CS8618

namespace BookCatalogAPI.Entities;

public class Author
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public IList<BookAuthor> BookAuthors { get; set; }
}