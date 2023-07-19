namespace BookCatalogModels.Dtos;

public record AuthorDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public DateOnly DateOfBirth { get; set; }
}