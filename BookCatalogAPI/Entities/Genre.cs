#pragma warning disable CS8618

namespace BookCatalogAPI.Entities;

public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public IList<BookGenre> BookGenres { get; set; }
}