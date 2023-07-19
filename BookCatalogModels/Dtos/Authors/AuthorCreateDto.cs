namespace BookCatalogModels.Dtos;

public record AuthorCreateDto
{
    /// <summary>
    /// Name of the author. Must be between 3 and 50 characters.
    /// </summary>
    /// <example>John Doe</example>
    public string Name { get; set; } = "";
    /// <summary>
    /// Date of birth. Must be a date smaller than current date and follow the format YYYY-MM-DD.
    /// </summary>
    /// <example>1999-01-28</example>
    public DateOnly DateOfBirth { get; set; }
}