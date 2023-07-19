namespace BookCatalogModels.Dtos;

public record GenreCreateDto
{
    /// <summary>
    /// Name of the genre to be created. Cannot be longer than 50 characters.
    /// </summary>
    /// <example>Non-fiction</example>
    public string Name { get; set; } = "";
    /// <summary>
    /// Description of the genre's description. Must be between 10 and 250 characters.
    /// </summary>
    /// <example>
    /// Nonfiction is literature that, regardless of the subject matter,
    /// has a simple goal: to provide information.
    ///</example>
    public string Description { get; set; } = "";
}