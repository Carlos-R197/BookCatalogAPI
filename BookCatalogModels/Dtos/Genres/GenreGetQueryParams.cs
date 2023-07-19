using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookCatalogModels.Dtos;

public record GenreGetQueryParams
{
    /// <summary>
    /// Name of genre to be searched for. Must be between 3 and 50 characters.
    /// </summary>
    /// <example>fiction</example>
    public string? Name { get; set; }
}