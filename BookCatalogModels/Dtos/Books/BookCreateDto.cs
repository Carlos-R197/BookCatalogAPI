namespace BookCatalogModels.Dtos;

public record BookCreateDto
{
    /// <summary>
    /// Title desired for the book. Must be between 3 and 255 characters.
    /// </summary>
    /// <example>The Lord of the Rings</example>
    public string Title { get; set; } = "";
    /// <summary>
    /// Publication date of the book. Must be in YYYY-MM-DD format.
    /// </summary>
    /// <example>1954-07-29</example>
    public DateOnly PublicationDate { get; set; }
    /// <summary>
    /// ISBN of the book. Can't exceed 13 characters.
    /// </summary>
    /// <example>9783710893599</example>
    public string ISBN { get; set; } = "";
    /// <summary>
    /// Collection containing the ids of the authors of this book. Must contain at least one id.
    /// </summary>
    /// <example>[1, 2, 3]</example>
    public IList<int> AuthorIds { get; set; } = new List<int>();
    /// <summary>
    /// Collection containing the ids of the genre this book belongs to. Must contain at least one id.
    /// </summary>
    /// <example>[1, 5]</example>
    public IList<int> GenreIds { get; set; } = new List<int>();
}