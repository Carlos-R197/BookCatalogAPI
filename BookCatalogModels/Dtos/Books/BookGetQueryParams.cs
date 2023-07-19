namespace BookCatalogModels.Dtos;

public record BookGetQueryParams
{
    /// <summary>
    /// Title to search for. Must be between 3 and 255 characters. This string is case-insensitive.
    /// </summary>
    /// <example>harry potter</example>
    public string? Title { get; set; }
    /// <summary>
    /// The date to use as the beginning of the range to search for. Must be smaller than toPublishedDate.
    /// </summary>
    /// <example>1980-05-24</example>
    public DateOnly? FromPublishedDate { get; set; }
    /// <summary>
    /// The date to use as the end of the range to search for. Must be larger than fromPublishedDate.
    /// </summary>
    /// <example>2000-01-01</example>
    public DateOnly? ToPublishedDate { get; set; }
    /// <summary>
    /// Provide this id if you only want the books of a specific author.
    /// </summary>
    public int? AuthorId { get; set; }
    /// <summary>
    /// Provide this id if you only want the books of a specific genre.
    /// </summary>
    public int? GenreId { get; set; }
    /// <summary>
    /// The page desired. Will default to 1 if not specified.
    /// </summary>
    /// <example>1</example>
    public int Page { get; set; } = 1;
    /// <summary>
    /// The page size desired. Must be between 1 and 100 and will default to 20 if not provided.
    /// </summary>
    /// <example>20</example>
    public int PageSize { get; set; } = 20;
}