namespace BookCatalogModels.Dtos;

public record AuthorGetQueryParams
{
    /// <summary>
    /// Name to search for. Must be between 3 and 50 characters.
    /// </summary>
    /// <example>John</example>
    public string? Name { get; set; }
    /// <summary>
    /// Date to start search from. Must be smaller than toBirth.
    /// </summary>
    /// <example>1950-01-01</example>
    public DateOnly? FromBirth { get; set; }
    /// <summary>
    /// End of date range for search. Must be larger than fromBirth.
    /// </summary>
    /// <example>1990-01-01</example>
    public DateOnly? ToBirth { get; set; }
    /// <summary>
    /// Amount of items to return in a request. Must be between 1 and 100.
    /// </summary>
    /// <example>20</example>
    public int PageSize { get; set; } = 20;
    /// <summary>
    /// The desired page.
    /// </summary>
    /// <example>1</example>
    public int Page { get; set; } = 1;
}