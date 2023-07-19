namespace BookCatalogModels.Dtos;

public record BookDto
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public DateOnly PublicationDate { get; set; }
    public string ISBN { get; set; } = "";
}